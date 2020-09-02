// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.Utilities.Validate
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;

namespace MS.Internal.Mita.Foundation.Utilities {
    internal static class Validate {
        public static void ArgumentNotNull(object parameter, string parameterName) {
            if (parameter == null)
                throw new ArgumentNullException(paramName: parameterName, message: StringResource.Get(id: "ParameterCannotBeNULL"));
        }

        public static void StringNeitherNullNorEmpty(string parameter, string parameterName) {
            if (string.IsNullOrEmpty(value: parameter))
                throw new ArgumentException(message: StringResource.Get(id: "StringParameterCannotBeNULLOrEmpty"), paramName: parameterName);
        }
    }
}