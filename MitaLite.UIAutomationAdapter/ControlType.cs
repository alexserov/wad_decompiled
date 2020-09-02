// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.ControlType
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;

namespace System.Windows.Automation {
    public class ControlType : AutomationIdentifier {
        public static readonly ControlType AppBar = new ControlType(id: 50040, programmaticName: "ControlType.AppBar");
        public static readonly ControlType Button = new ControlType(id: 50000, programmaticName: "ControlType.Button");
        public static readonly ControlType Calendar = new ControlType(id: 50001, programmaticName: "ControlType.Calendar");
        public static readonly ControlType CheckBox = new ControlType(id: 50002, programmaticName: "ControlType.CheckBox");
        public static readonly ControlType ComboBox = new ControlType(id: 50003, programmaticName: "ControlType.ComboBox");
        public static readonly ControlType Custom = new ControlType(id: 50025, programmaticName: "ControlType.Custom");
        public static readonly ControlType DataGrid = new ControlType(id: 50028, programmaticName: "ControlType.DataGrid");
        public static readonly ControlType DataItem = new ControlType(id: 50029, programmaticName: "ControlType.DataItem");
        public static readonly ControlType Document = new ControlType(id: 50030, programmaticName: "ControlType.Document");
        public static readonly ControlType Edit = new ControlType(id: 50004, programmaticName: "ControlType.Edit");
        public static readonly ControlType Group = new ControlType(id: 50026, programmaticName: "ControlType.Group");
        public static readonly ControlType Header = new ControlType(id: 50034, programmaticName: "ControlType.Header");
        public static readonly ControlType HeaderItem = new ControlType(id: 50035, programmaticName: "ControlType.HeaderItem");
        public static readonly ControlType Hyperlink = new ControlType(id: 50005, programmaticName: "ControlType.HyperLink");
        public static readonly ControlType Image = new ControlType(id: 50006, programmaticName: "ControlType.Image");
        public static readonly ControlType List = new ControlType(id: 50008, programmaticName: "ControlType.List");
        public static readonly ControlType ListItem = new ControlType(id: 50007, programmaticName: "ControlType.ListItem");
        public static readonly ControlType MenuBar = new ControlType(id: 50010, programmaticName: "ControlType.MenuBar");
        public static readonly ControlType Menu = new ControlType(id: 50009, programmaticName: "ControlType.Menu");
        public static readonly ControlType MenuItem = new ControlType(id: 50011, programmaticName: "ControlType.MenuItem");
        public static readonly ControlType Pane = new ControlType(id: 50033, programmaticName: "ControlType.Pane");
        public static readonly ControlType ProgressBar = new ControlType(id: 50012, programmaticName: "ControlType.ProgressBar");
        public static readonly ControlType RadioButton = new ControlType(id: 50013, programmaticName: "ControlType.RadioButton");
        public static readonly ControlType ScrollBar = new ControlType(id: 50014, programmaticName: "ControlType.ScrollBar");
        public static readonly ControlType SemanticZoom = new ControlType(id: 50039, programmaticName: "ControlType.SemanticZoom");
        public static readonly ControlType Separator = new ControlType(id: 50038, programmaticName: "ControlType.Separator");
        public static readonly ControlType Slider = new ControlType(id: 50015, programmaticName: "ControlType.Slider");
        public static readonly ControlType Spinner = new ControlType(id: 50016, programmaticName: "ControlType.Spinner");
        public static readonly ControlType SplitButton = new ControlType(id: 50031, programmaticName: "ControlType.SplitButton");
        public static readonly ControlType StatusBar = new ControlType(id: 50017, programmaticName: "ControlType.StatusBar");
        public static readonly ControlType Tab = new ControlType(id: 50018, programmaticName: "ControlType.Tab");
        public static readonly ControlType TabItem = new ControlType(id: 50019, programmaticName: "ControlType.TabItem");
        public static readonly ControlType Table = new ControlType(id: 50036, programmaticName: "ControlType.Table");
        public static readonly ControlType Text = new ControlType(id: 50020, programmaticName: "ControlType.Text");
        public static readonly ControlType Thumb = new ControlType(id: 50027, programmaticName: "ControlType.Thumb");
        public static readonly ControlType TitleBar = new ControlType(id: 50037, programmaticName: "ControlType.TitleBar");
        public static readonly ControlType ToolBar = new ControlType(id: 50021, programmaticName: "ControlType.ToolBar");
        public static readonly ControlType ToolTip = new ControlType(id: 50022, programmaticName: "ControlType.ToolTip");
        public static readonly ControlType Tree = new ControlType(id: 50023, programmaticName: "ControlType.Tree");
        public static readonly ControlType TreeItem = new ControlType(id: 50024, programmaticName: "ControlType.TreeItem");
        public static readonly ControlType Window = new ControlType(id: 50032, programmaticName: "ControlType.Window");
        internal static Dictionary<int, ControlType> _idTable;

        internal ControlType(int id, string programmaticName)
            : base(type: AutomationIdType.ControlType, id: id, programmaticName: programmaticName) {
            if (_idTable == null)
                _idTable = new Dictionary<int, ControlType>();
            if (_idTable.ContainsKey(key: id))
                return;
            _idTable.Add(key: id, value: this);
        }

        internal static ControlType LookupById(int id) {
            ControlType controlType;
            return _idTable.TryGetValue(key: id, value: out controlType) ? controlType : null;
        }
    }
}