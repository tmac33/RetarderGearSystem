// Type: WuXiaoYangGraduationDesign.MyCommands
// Assembly: Gear, Version=1.0.5619.28247, Culture=neutral, PublicKeyToken=null
// MVID: 37313088-B81D-4F89-988D-7DF444CCA003
// Assembly location: F:\Tmac\Gear.dll

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Threading;
using System.Windows.Forms;

namespace WuXiaoYangGraduationDesign
{
  public class MyCommands
  {
    private Editor ed = Application.get_DocumentManager().get_MdiActiveDocument().get_Editor();
    private double m;
    private double h;
    private int z;
    private double a;
    private string doDemo;
    private int delay;

    [CommandMethod]
    public void MyCommand()
    {
      if (!this.ShowDialog())
        return;
      double num1 = 1.0;
      double num2 = 0.25;
      double num3 = this.m * (double) this.z;
      double da = (2.0 * num1 + (double) this.z) * this.m;
      double df = ((double) this.z - 2.0 * num1 - 2.0 * num2) * this.m;
      double db = num3 * Math.Cos(this.a * Math.PI / 180.0);
      Point3d point = this.GetPoint();
      DateTime now1 = DateTime.Now;
      Circle circle1 = new Circle(point, Vector3d.get_ZAxis(), db / 2.0);
      Circle cir1 = new Circle(point, Vector3d.get_ZAxis(), da / 2.0);
      Circle circle2 = new Circle(point, Vector3d.get_ZAxis(), df / 2.0);
      Circle pitchCircle = new Circle(point, Vector3d.get_ZAxis(), num3 / 2.0);
      Point3dCollection point3dCollection = new Point3dCollection();
      Polyline3d evolent1 = this.CreatEvolent(point, da, db, cir1);
      Polyline3d evolent2 = this.MirrorEvolent(evolent1, pitchCircle, point);
      Arc arc = this.CreatArc(point, evolent1, evolent2);
      Line line1 = new Line(point, ((Curve) evolent1).get_StartPoint());
      Line line2 = new Line(point, ((Curve) evolent2).get_StartPoint());
      DBObjectCollection objectCollection1 = new DBObjectCollection();
      objectCollection1.Add((DBObject) evolent1);
      objectCollection1.Add((DBObject) evolent2);
      objectCollection1.Add((DBObject) line2);
      objectCollection1.Add((DBObject) line1);
      objectCollection1.Add((DBObject) arc);
      DBObjectCollection objectCollection2 = new DBObjectCollection();
      Entity[] entityArray = this.ArrayPolar((Entity) (Region.CreateFromCurves(objectCollection1).get_Item(0) as Region), point, this.z, 2.0 * Math.PI);
      objectCollection1.Clear();
      objectCollection1.Add((DBObject) circle2);
      Region region1 = Region.CreateFromCurves(objectCollection1).get_Item(0) as Region;
      foreach (Entity entity in entityArray)
      {
        Region region2 = entity as Region;
        if (DisposableWrapper.op_Inequality((DisposableWrapper) region2, (DisposableWrapper) null))
          region1.BooleanOperation((BooleanOperationType) 0, region2);
      }
      objectCollection1.Clear();
      Circle circle3 = new Circle(point, Vector3d.get_ZAxis(), 0.15 * (df - 10.0));
      objectCollection1.Add((DBObject) circle3);
      DBObjectCollection fromCurves = Region.CreateFromCurves(objectCollection1);
      region1.BooleanOperation((BooleanOperationType) 2, fromCurves.get_Item(0) as Region);
      Solid3d solid3d = new Solid3d();
      solid3d.Extrude(region1, this.h, 0.0);
      solid3d.BooleanOperation((BooleanOperationType) 2, this.NewBox(point, this.h, df));
      if (this.doDemo == "Y")
      {
        this.AddEntityToModelSpace((Entity) evolent1);
        this.AddEntityToModelSpace((Entity) evolent2);
        this.AddEntityToModelSpace((Entity) arc);
        this.AddEntityToModelSpace((Entity) line1);
        this.AddEntityToModelSpace((Entity) line2);
        this.AddEntityToModelSpace((Entity) (((RXObject) region1).Clone() as Region));
        Thread.Sleep(this.delay * 5);
      }
      this.ZoomToEntity((Entity) solid3d);
      this.AddEntityToModelSpace((Entity) solid3d);
      DateTime now2 = DateTime.Now;
      this.ed.WriteMessage("\n耗时{0}。", new object[1]
      {
        (object) this.Elapsed(now1, now2)
      });
    }

    private void AddEntityToModelSpace(Entity ent)
    {
      Database workingDatabase = HostApplicationServices.get_WorkingDatabase();
      using (Transaction transaction = workingDatabase.get_TransactionManager().StartTransaction())
      {
        BlockTable blockTable = (BlockTable) transaction.GetObject(workingDatabase.get_BlockTableId(), (OpenMode) 0);
        ((BlockTableRecord) transaction.GetObject(((SymbolTable) blockTable).get_Item((string) BlockTableRecord.ModelSpace), (OpenMode) 1)).AppendEntity(ent);
        transaction.AddNewlyCreatedDBObject((DBObject) ent, true);
        transaction.Commit();
      }
      this.ed.UpdateScreen();
      Thread.Sleep(this.delay);
    }

    private Entity[] ArrayPolar(Entity ent, Point3d centerPoint, int num, double angle)
    {
      Entity[] entityArray = new Entity[num];
      entityArray[0] = ent;
      for (int index = 1; index < num; ++index)
      {
        Matrix3d matrix3d = Matrix3d.Rotation(angle * (double) index / (double) num, Vector3d.get_ZAxis(), centerPoint);
        entityArray[index] = ent.GetTransformedCopy(matrix3d);
      }
      return entityArray;
    }

    private Arc CreatArc(Point3d cenPt, Polyline3d evolent1, Polyline3d evolent2)
    {
      Point3d endPoint1 = ((Curve) evolent1).get_EndPoint();
      Point3d endPoint2 = ((Curve) evolent2).get_EndPoint();
      Line line1 = new Line(cenPt, endPoint1);
      Line line2 = new Line(cenPt, endPoint2);
      // ISSUE: explicit reference operation
      double num = ((Point3d) @cenPt).DistanceTo(endPoint1);
      double angle1 = line1.get_Angle();
      double angle2 = line2.get_Angle();
      return new Arc(cenPt, num, angle1, angle2);
    }

    private Polyline3d CreatEvolent(Point3d centerPoint, double da, double db, Circle cir1)
    {
      Point3dCollection point3dCollection1 = new Point3dCollection();
      Point3d point3d1 = (Point3d) null;
      double num = 0.0;
      for (int index = 0; index <= 90; ++index)
      {
        Point3d point3d2 = Point3d.op_Addition(new Point3d(db / 2.0 * Math.Cos(num * Math.PI / 180.0) + db / 2.0 * Math.Sin(num * Math.PI / 180.0) * num * Math.PI / 180.0, db / 2.0 * Math.Sin(num * Math.PI / 180.0) - db / 2.0 * Math.Cos(num * Math.PI / 180.0) * num * Math.PI / 180.0, 0.0), Point3d.op_Subtraction(centerPoint, Point3d.get_Origin()));
        // ISSUE: explicit reference operation
        if (((Point3d) @point3d2).DistanceTo(centerPoint) < da / 2.0)
        {
          ++num;
          point3dCollection1.Add(point3d2);
          point3d1 = point3d2;
        }
        else
        {
          Line line = new Line(point3d1, point3d2);
          Point3dCollection point3dCollection2 = new Point3dCollection();
          IntPtr zero = IntPtr.Zero;
          ((Entity) cir1).IntersectWith((Entity) line, (Intersect) 0, point3dCollection2, zero, zero);
          point3dCollection1.Add(point3dCollection2.get_Item(0));
          break;
        }
      }
      return new Polyline3d((Poly3dType) 0, point3dCollection1, false);
    }

    private string Elapsed(DateTime start, DateTime stop)
    {
      TimeSpan timeSpan = stop - start;
      return string.Format("{0}天{1}小时{2}分钟{3}秒{4}毫秒", (object) timeSpan.Days, (object) timeSpan.Hours, (object) timeSpan.Minutes, (object) timeSpan.Seconds, (object) timeSpan.Milliseconds);
    }

    private void GetPalameters()
    {
      this.m = this.GetDouble("\n输入模数: ", 8.0);
      this.a = this.GetDouble("\n输入压力角:", 20.0);
      this.z = this.GetInt("\n输入齿数", 24);
    }

    private double GetDouble(string str, double defaultValue)
    {
      PromptDoubleOptions promptDoubleOptions = new PromptDoubleOptions(str);
      ((PromptNumericalOptions) promptDoubleOptions).set_AllowNone(true);
      ((PromptNumericalOptions) promptDoubleOptions).set_UseDefaultValue(true);
      promptDoubleOptions.set_DefaultValue(defaultValue);
      PromptDoubleResult promptDoubleResult = this.ed.GetDouble(promptDoubleOptions);
      if (((PromptResult) promptDoubleResult).get_Status() == 5100)
        return promptDoubleResult.get_Value();
      return promptDoubleOptions.get_DefaultValue();
    }

    private int GetInt(string str, int defaultValue)
    {
      PromptIntegerOptions promptIntegerOptions = new PromptIntegerOptions(str);
      ((PromptNumericalOptions) promptIntegerOptions).set_AllowNone(true);
      ((PromptNumericalOptions) promptIntegerOptions).set_UseDefaultValue(true);
      promptIntegerOptions.set_DefaultValue(defaultValue);
      PromptIntegerResult integer = this.ed.GetInteger(promptIntegerOptions);
      if (((PromptResult) integer).get_Status() == 5100)
        return integer.get_Value();
      return promptIntegerOptions.get_DefaultValue();
    }

    private Point3d GetPoint()
    {
      PromptPointOptions promptPointOptions = new PromptPointOptions("\n点取齿轮中心点位置或[过程演示(Y)/(N)]", "Y N");
      ((PromptCornerOptions) promptPointOptions).set_AllowNone(false);
      ((PromptOptions) promptPointOptions).get_Keywords().Add("D", "D", "设置延迟时间(D)", false, true);
      ((PromptOptions) promptPointOptions).get_Keywords().set_Default("N");
      PromptPointResult point = this.ed.GetPoint(promptPointOptions);
      if (((PromptResult) point).get_Status() == 5100)
        return point.get_Value();
      if (((PromptResult) point).get_Status() == -5005)
      {
        if (((PromptResult) point).get_StringResult() == "D")
        {
          this.delay = this.GetInt("\n输入延迟时间", 200);
          this.doDemo = "Y";
          return this.GetPoint();
        }
        this.doDemo = ((PromptResult) point).get_StringResult();
        this.delay = 200;
        return this.GetPoint();
      }
      this.doDemo = "N";
      return (Point3d) null;
    }

    private Polyline3d MirrorEvolent(Polyline3d evolent1, Circle pitchCircle, Point3d centerPoint)
    {
      Point3dCollection point3dCollection = new Point3dCollection();
      IntPtr zero = IntPtr.Zero;
      ((Entity) pitchCircle).IntersectWith((Entity) evolent1, (Intersect) 0, point3dCollection, zero, zero);
      double a = 2.0 * Math.PI / (double) (4 * this.z);
      Point3d point3d1 = point3dCollection.get_Item(0);
      Vector3d vector3d1 = Vector3d.op_Multiply(Point3d.op_Subtraction(point3dCollection.get_Item(0), centerPoint), Math.Sin(a));
      // ISSUE: explicit reference operation
      Vector3d vector3d2 = ((Vector3d) @vector3d1).RotateBy(Math.PI / 2.0, Vector3d.get_ZAxis());
      Point3d point3d2 = Point3d.op_Addition(point3d1, vector3d2);
      Matrix3d matrix3d = Matrix3d.Mirroring(new Line3d(centerPoint, point3d2));
      return ((Entity) evolent1).GetTransformedCopy(matrix3d) as Polyline3d;
    }

    private Solid3d NewBox(Point3d centerPoint, double h, double df)
    {
      double num1 = 0.15 * (df - 10.0) / 5.0;
      double num2 = 0.15 * (df - 10.0) / 2.0;
      double num3 = h;
      Solid3d solid3d = new Solid3d();
      solid3d.CreateBox(num1, num2, num3);
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      ((Entity) solid3d).TransformBy(Matrix3d.Displacement(Vector3d.op_Addition(new Vector3d(0.15 * (df - 10.0), 0.0, h / 2.0), new Vector3d(((Point3d) @centerPoint).get_X(), ((Point3d) @centerPoint).get_Y(), ((Point3d) @centerPoint).get_Z()))));
      return solid3d;
    }

    private bool ShowDialog()
    {
      using (FormGear formGear = new FormGear())
      {
        int num = (int) Application.ShowModalDialog((Form) formGear);
        if (formGear.DialogResult != DialogResult.OK)
          return false;
        this.m = formGear.moShu;
        this.z = formGear.chiShu;
        this.a = formGear.yaLiJiao;
        this.h = formGear.thickness;
        return true;
      }
    }

    private void ZoomToEntity(Entity ent)
    {
      Database database = Application.get_DocumentManager().get_MdiActiveDocument().get_Database();
      Vector3d vector3d;
      // ISSUE: explicit reference operation
      ((Vector3d) @vector3d).\u002Ector(-1.0, -1.0, 1.0);
      using (Transaction transaction = database.get_TransactionManager().StartTransaction())
      {
        ViewTableRecord currentView = this.ed.GetCurrentView();
        Matrix3d world = Matrix3d.PlaneToWorld(vector3d);
        // ISSUE: explicit reference operation
        Matrix3d matrix3d1 = ((Matrix3d) @world).PreMultiplyBy(Matrix3d.Displacement(Point3d.op_Subtraction(((AbstractViewTableRecord) currentView).get_Target(), Point3d.get_Origin())));
        // ISSUE: explicit reference operation
        Matrix3d matrix3d2 = ((Matrix3d) @matrix3d1).PreMultiplyBy(Matrix3d.Rotation(-((AbstractViewTableRecord) currentView).get_ViewTwist(), ((AbstractViewTableRecord) currentView).get_ViewDirection(), ((AbstractViewTableRecord) currentView).get_Target()));
        // ISSUE: explicit reference operation
        Matrix3d matrix3d3 = ((Matrix3d) @matrix3d2).Inverse();
        Extents3d geometricExtents = ent.get_GeometricExtents();
        // ISSUE: explicit reference operation
        ((Extents3d) @geometricExtents).TransformBy(matrix3d3);
        // ISSUE: explicit reference operation
        Point3d minPoint = ((Extents3d) @geometricExtents).get_MinPoint();
        // ISSUE: explicit reference operation
        Point3d maxPoint = ((Extents3d) @geometricExtents).get_MaxPoint();
        Point2d point2d1;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Point2d) @point2d1).\u002Ector(((Point3d) @minPoint).get_X(), ((Point3d) @minPoint).get_Y());
        Point2d point2d2;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Point2d) @point2d2).\u002Ector(((Point3d) @maxPoint).get_X(), ((Point3d) @maxPoint).get_Y());
        ((AbstractViewTableRecord) currentView).set_CenterPoint(Point2d.op_Addition(point2d1, Vector2d.op_Division(Point2d.op_Subtraction(point2d2, point2d1), 2.0)));
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((AbstractViewTableRecord) currentView).set_Height(((Point2d) @point2d2).get_Y() - ((Point2d) @point2d1).get_Y());
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((AbstractViewTableRecord) currentView).set_Width(((Point2d) @point2d2).get_X() - ((Point2d) @point2d1).get_X());
        ((AbstractViewTableRecord) currentView).set_ViewDirection(vector3d);
        DBDictionary dbDictionary = (DBDictionary) transaction.GetObject(database.get_VisualStyleDictionaryId(), (OpenMode) 0);
        ((AbstractViewTableRecord) currentView).set_VisualStyleId(dbDictionary.GetAt("Shades of Gray"));
        this.ed.SetCurrentView(currentView);
        transaction.Commit();
      }
    }
  }
}
