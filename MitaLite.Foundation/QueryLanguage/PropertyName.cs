﻿// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.QueryLanguage.PropertyName
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using MS.Internal.Mita.Foundation.QueryLanguage.SSVParseLib;
using System.Text;

namespace MS.Internal.Mita.Foundation.QueryLanguage
{
  internal class PropertyName : SSYaccStackElement
  {
    private string _propertyName;

    public PropertyName(string lexeme) => this._propertyName = lexeme;

    public PropertyName(string patternName, string propertyName) => this._propertyName = patternName + "." + propertyName;

    public UIProperty GetUIProperty() => UIProperty.Get(this._propertyName);

    public bool Validate(StringBuilder errors)
    {
      if (UIProperty.Exists(this._propertyName))
        return true;
      errors.AppendLine(StringResource.Get("PropertyNotFound_1", (object) this._propertyName));
      return false;
    }
  }
}
