// Type: WuXiaoYangGraduationDesign.myCommands
// Assembly: Gear, Version=1.0.5619.28247, Culture=neutral, PublicKeyToken=null
// MVID: 37313088-B81D-4F89-988D-7DF444CCA003
// Assembly location: F:\Tmac\Gear.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace WuXiaoYangGraduationDesign
{
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  internal class myCommands
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) myCommands.resourceMan, (object) null))
          myCommands.resourceMan = new ResourceManager("WuXiaoYangGraduationDesign.myCommands", typeof (myCommands).Assembly);
        return myCommands.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return myCommands.resourceCulture;
      }
      set
      {
        myCommands.resourceCulture = value;
      }
    }

    internal static string MyCommandLocal
    {
      get
      {
        return myCommands.ResourceManager.GetString("MyCommandLocal", myCommands.resourceCulture);
      }
    }

    internal myCommands()
    {
    }
  }
}
