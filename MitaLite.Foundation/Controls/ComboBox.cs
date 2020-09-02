// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Controls.ComboBox
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System.Windows.Automation;

namespace MS.Internal.Mita.Foundation.Controls
{
  public class ComboBox : ComboBox<ComboBox, ComboBoxItem>
  {
    private static IFactory<ComboBox> _factory;

    public ComboBox(UIObject uiObject)
      : base(uiObject, ComboBoxItem.Factory)
    {
    }

    internal ComboBox(AutomationElement element)
      : base(element, ComboBoxItem.Factory)
    {
    }

    public static IFactory<ComboBox> Factory
    {
      get
      {
        if (ComboBox._factory == null)
          ComboBox._factory = (IFactory<ComboBox>) new ComboBox.ComboBoxFactory();
        return ComboBox._factory;
      }
    }

    private class ComboBoxFactory : IFactory<ComboBox>
    {
      public ComboBox Create(UIObject element) => new ComboBox(element);
    }
  }
}
