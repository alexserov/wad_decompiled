// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ControlType
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;

namespace System.Windows.Automation
{
  public class ControlType : AutomationIdentifier
  {
    public static readonly ControlType AppBar = new ControlType(50040, "ControlType.AppBar");
    public static readonly ControlType Button = new ControlType(50000, "ControlType.Button");
    public static readonly ControlType Calendar = new ControlType(50001, "ControlType.Calendar");
    public static readonly ControlType CheckBox = new ControlType(50002, "ControlType.CheckBox");
    public static readonly ControlType ComboBox = new ControlType(50003, "ControlType.ComboBox");
    public static readonly ControlType Custom = new ControlType(50025, "ControlType.Custom");
    public static readonly ControlType DataGrid = new ControlType(50028, "ControlType.DataGrid");
    public static readonly ControlType DataItem = new ControlType(50029, "ControlType.DataItem");
    public static readonly ControlType Document = new ControlType(50030, "ControlType.Document");
    public static readonly ControlType Edit = new ControlType(50004, "ControlType.Edit");
    public static readonly ControlType Group = new ControlType(50026, "ControlType.Group");
    public static readonly ControlType Header = new ControlType(50034, "ControlType.Header");
    public static readonly ControlType HeaderItem = new ControlType(50035, "ControlType.HeaderItem");
    public static readonly ControlType Hyperlink = new ControlType(50005, "ControlType.HyperLink");
    public static readonly ControlType Image = new ControlType(50006, "ControlType.Image");
    public static readonly ControlType List = new ControlType(50008, "ControlType.List");
    public static readonly ControlType ListItem = new ControlType(50007, "ControlType.ListItem");
    public static readonly ControlType MenuBar = new ControlType(50010, "ControlType.MenuBar");
    public static readonly ControlType Menu = new ControlType(50009, "ControlType.Menu");
    public static readonly ControlType MenuItem = new ControlType(50011, "ControlType.MenuItem");
    public static readonly ControlType Pane = new ControlType(50033, "ControlType.Pane");
    public static readonly ControlType ProgressBar = new ControlType(50012, "ControlType.ProgressBar");
    public static readonly ControlType RadioButton = new ControlType(50013, "ControlType.RadioButton");
    public static readonly ControlType ScrollBar = new ControlType(50014, "ControlType.ScrollBar");
    public static readonly ControlType SemanticZoom = new ControlType(50039, "ControlType.SemanticZoom");
    public static readonly ControlType Separator = new ControlType(50038, "ControlType.Separator");
    public static readonly ControlType Slider = new ControlType(50015, "ControlType.Slider");
    public static readonly ControlType Spinner = new ControlType(50016, "ControlType.Spinner");
    public static readonly ControlType SplitButton = new ControlType(50031, "ControlType.SplitButton");
    public static readonly ControlType StatusBar = new ControlType(50017, "ControlType.StatusBar");
    public static readonly ControlType Tab = new ControlType(50018, "ControlType.Tab");
    public static readonly ControlType TabItem = new ControlType(50019, "ControlType.TabItem");
    public static readonly ControlType Table = new ControlType(50036, "ControlType.Table");
    public static readonly ControlType Text = new ControlType(50020, "ControlType.Text");
    public static readonly ControlType Thumb = new ControlType(50027, "ControlType.Thumb");
    public static readonly ControlType TitleBar = new ControlType(50037, "ControlType.TitleBar");
    public static readonly ControlType ToolBar = new ControlType(50021, "ControlType.ToolBar");
    public static readonly ControlType ToolTip = new ControlType(50022, "ControlType.ToolTip");
    public static readonly ControlType Tree = new ControlType(50023, "ControlType.Tree");
    public static readonly ControlType TreeItem = new ControlType(50024, "ControlType.TreeItem");
    public static readonly ControlType Window = new ControlType(50032, "ControlType.Window");
    internal static Dictionary<int, ControlType> _idTable;

    internal ControlType(int id, string programmaticName)
      : base(AutomationIdType.ControlType, id, programmaticName)
    {
      if (ControlType._idTable == null)
        ControlType._idTable = new Dictionary<int, ControlType>();
      if (ControlType._idTable.ContainsKey(id))
        return;
      ControlType._idTable.Add(id, this);
    }

    internal static ControlType LookupById(int id)
    {
      ControlType controlType;
      return ControlType._idTable.TryGetValue(id, out controlType) ? controlType : (ControlType) null;
    }
  }
}
