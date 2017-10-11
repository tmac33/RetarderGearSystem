// Assembly: Gear, Version=1.0.5619.28247, Culture=neutral, PublicKeyToken=null
// MVID: 37313088-B81D-4F89-988D-7DF444CCA003
// Assembly location: F:\Tmac\Gear.dll

using Autodesk.AutoCAD.ApplicationServices;

namespace WuXiaoYangGraduationDesign
{
  internal class Tools
  {
    public static void NewMethod()
    {
      Application.get_DocumentManager().get_MdiActiveDocument().get_Editor().WriteMessage("\n此处程序有待后期开发！\n");
    }
  }
}
