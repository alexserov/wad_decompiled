// Decompiled with JetBrains decompiler
// Type: System.Windows.Automation.UiaConvert
// Assembly: MitaLite.UIAutomationAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AA78029-452E-4BBE-B7CF-82C2B0EE29B5
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.UIAutomationAdapter.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Types;
using UIAutomationClient;

namespace System.Windows.Automation
{
  internal static class UiaConvert
  {
    internal const ushort VT_EMPTY = 0;
    internal const ushort VT_NULL = 1;
    internal const ushort VT_I2 = 2;
    internal const ushort VT_I4 = 3;
    internal const ushort VT_R4 = 4;
    internal const ushort VT_R8 = 5;
    internal const ushort VT_CY = 6;
    internal const ushort VT_DATE = 7;
    internal const ushort VT_BSTR = 8;
    internal const ushort VT_DISPATCH = 9;
    internal const ushort VT_ERROR = 10;
    internal const ushort VT_BOOL = 11;
    internal const ushort VT_VARIANT = 12;
    internal const ushort VT_UNKNOWN = 13;
    internal const ushort VT_DECIMAL = 14;
    internal const ushort VT_I1 = 16;
    internal const ushort VT_UI1 = 17;
    internal const ushort VT_UI2 = 18;
    internal const ushort VT_UI4 = 19;
    internal const ushort VT_I8 = 20;
    internal const ushort VT_UI8 = 21;
    internal const ushort VT_INT = 22;
    internal const ushort VT_UINT = 23;
    internal const ushort VT_VOID = 24;
    internal const ushort VT_HRESULT = 25;
    internal const ushort VT_PTR = 26;
    internal const ushort VT_SAFEARRAY = 27;
    internal const ushort VT_CARRAY = 28;
    internal const ushort VT_USERDEFINED = 29;
    internal const ushort VT_LPSTR = 30;
    internal const ushort VT_LPWSTR = 31;
    internal const ushort VT_RECORD = 36;
    internal const ushort VT_INT_PTR = 37;
    internal const ushort VT_UINT_PTR = 38;
    internal const ushort VT_FILETIME = 64;
    internal const ushort VT_BLOB = 65;
    internal const ushort VT_STREAM = 66;
    internal const ushort VT_STORAGE = 67;
    internal const ushort VT_STREAMED_OBJECT = 68;
    internal const ushort VT_STORED_OBJECT = 69;
    internal const ushort VT_BLOB_OBJECT = 70;
    internal const ushort VT_CF = 71;
    internal const ushort VT_CLSID = 72;
    internal const ushort VT_VERSIONED_STREAM = 73;
    internal const ushort VT_BSTR_BLOB = 4095;
    internal const ushort VT_VECTOR = 4096;
    internal const ushort VT_ARRAY = 8192;
    internal const ushort VT_BYREF = 16384;
    internal const ushort VT_RESERVED = 32768;
    internal const ushort VT_ILLEGAL = 65535;
    internal const ushort VT_ILLEGALMASKED = 4095;
    internal const ushort VT_TYPEMASK = 4095;
    private static Dictionary<Type, ushort> VTEnumsLookup = new Dictionary<Type, ushort>()
    {
      {
        typeof (bool),
        (ushort) 11
      },
      {
        typeof (sbyte),
        (ushort) 16
      },
      {
        typeof (byte),
        (ushort) 17
      },
      {
        typeof (short),
        (ushort) 2
      },
      {
        typeof (ushort),
        (ushort) 18
      },
      {
        typeof (int),
        (ushort) 3
      },
      {
        typeof (uint),
        (ushort) 19
      },
      {
        typeof (long),
        (ushort) 20
      },
      {
        typeof (ulong),
        (ushort) 21
      },
      {
        typeof (float),
        (ushort) 4
      },
      {
        typeof (double),
        (ushort) 5
      }
    };

    public static bool ConvertException(COMException e, out Exception newException)
    {
      bool flag = true;
      switch (e.HResult)
      {
        case -2147220992:
          newException = (Exception) new ElementNotEnabledException((Exception) e);
          break;
        case -2147220991:
          newException = (Exception) new ElementNotAvailableException((Exception) e);
          break;
        case -2147220990:
          newException = (Exception) new NoClickablePointException((Exception) e);
          break;
        case -2147220989:
          newException = (Exception) new ProxyAssemblyNotLoadedException((Exception) e);
          break;
        default:
          newException = (Exception) null;
          flag = false;
          break;
      }
      return flag;
    }

    public static TextPatternRangeEndpoint Convert(
      UIAutomationClient.TextPatternRangeEndpoint textPatternRangeEndpoint) => (TextPatternRangeEndpoint) textPatternRangeEndpoint;

    public static TextUnit Convert(UIAutomationClient.TextUnit textUnit) => (TextUnit) textUnit;

    public static SupportedTextSelection Convert(
      UIAutomationClient.SupportedTextSelection supportedTextSelection) => (SupportedTextSelection) supportedTextSelection;

    public static AutomationElementMode Convert(
      UIAutomationClient.AutomationElementMode automationElementMode) => (AutomationElementMode) automationElementMode;

    public static StructureChangeType Convert(
      UIAutomationClient.StructureChangeType structureChangeType) => (StructureChangeType) structureChangeType;

    public static ExpandCollapseState Convert(
      UIAutomationClient.ExpandCollapseState expandCollapseState) => (ExpandCollapseState) expandCollapseState;

    public static TreeScope Convert(UIAutomationClient.TreeScope treeScope) => (TreeScope) treeScope;

    public static ToggleState Convert(UIAutomationClient.ToggleState toggleState) => (ToggleState) toggleState;

    public static WindowInteractionState Convert(
      UIAutomationClient.WindowInteractionState windowInteractionState) => (WindowInteractionState) windowInteractionState;

    public static WindowVisualState Convert(UIAutomationClient.WindowVisualState windowVisualState) => (WindowVisualState) windowVisualState;

    public static UIAutomationClient.DockPosition Convert(DockPosition position) => (UIAutomationClient.DockPosition) position;

    public static RowOrColumnMajor Convert(UIAutomationClient.RowOrColumnMajor rowOrColumnMajor) => (RowOrColumnMajor) rowOrColumnMajor;

    public static UIAutomationClient.TreeScope Convert(TreeScope treeScope) => (UIAutomationClient.TreeScope) treeScope;

    public static UIAutomationClient.ScrollAmount Convert(ScrollAmount scrollAmount) => (UIAutomationClient.ScrollAmount) scrollAmount;

    public static UIAutomationClient.WindowVisualState Convert(
      WindowVisualState state) => (UIAutomationClient.WindowVisualState) state;

    public static UIAutomationClient.TextPatternRangeEndpoint Convert(
      TextPatternRangeEndpoint textPatternRangeEndpoint) => (UIAutomationClient.TextPatternRangeEndpoint) textPatternRangeEndpoint;

    public static UIAutomationClient.TextUnit Convert(TextUnit textUnit) => (UIAutomationClient.TextUnit) textUnit;

    public static UIAutomationClient.SupportedTextSelection Convert(
      SupportedTextSelection supportedTextSelection) => (UIAutomationClient.SupportedTextSelection) supportedTextSelection;

    public static UIAutomationClient.AutomationElementMode Convert(
      AutomationElementMode automationElementMode) => (UIAutomationClient.AutomationElementMode) automationElementMode;

    public static UIAutomationClient.StructureChangeType Convert(
      StructureChangeType structureChangeType) => (UIAutomationClient.StructureChangeType) structureChangeType;

    [DllImport("OleAut32.dll", SetLastError = true)]
    private static extern IntPtr SafeArrayCreate(ushort vt, uint cDims, IntPtr rgsabound);

    [DllImport("OleAut32.dll", SetLastError = true)]
    private static extern int SafeArrayDestroy(IntPtr psa);

    [DllImport("OleAut32.dll", SetLastError = true)]
    private static extern int SafeArrayPutElement(IntPtr psa, IntPtr rgIndices, IntPtr pv);

    public static object ToObject(this Variant variant)
    {
      switch (variant._typeUnion._vt)
      {
        case 0:
          return (object) null;
        case 2:
          return (object) variant._typeUnion._unionTypes._i2;
        case 3:
          return (object) variant._typeUnion._unionTypes._i4;
        case 4:
          return (object) variant._typeUnion._unionTypes._r4;
        case 5:
          return (object) variant._typeUnion._unionTypes._r8;
        case 6:
          return (object) variant._typeUnion._unionTypes._cy;
        case 7:
          return (object) variant._typeUnion._unionTypes._date;
        case 8:
          return (object) Marshal.PtrToStringUni(variant._typeUnion._unionTypes._bstr);
        case 11:
          return (object) ((uint) variant._typeUnion._unionTypes._bool > 0U);
        case 13:
          return Marshal.GetObjectForIUnknown(variant._typeUnion._unionTypes._unknown);
        case 16:
          return (object) variant._typeUnion._unionTypes._i1;
        case 17:
          return (object) variant._typeUnion._unionTypes._ui1;
        case 18:
          return (object) variant._typeUnion._unionTypes._ui2;
        case 19:
          return (object) variant._typeUnion._unionTypes._ui4;
        case 20:
          return (object) variant._typeUnion._unionTypes._i8;
        case 21:
          return (object) variant._typeUnion._unionTypes._ui8;
        case 22:
          return (object) variant._typeUnion._unionTypes._int;
        case 23:
          return (object) variant._typeUnion._unionTypes._uint;
        case 8195:
        case 8197:
          UiaConvert.SafeArray structure = Marshal.PtrToStructure<UiaConvert.SafeArray>(variant._typeUnion._unionTypes._byref);
          if (structure.cDims != (ushort) 1 || ((int) structure.fFeatures & 128) == 0)
            throw new NotImplementedException("Multidimensional or unknown type array marshaling is not yet supported");
          uint cElements = structure.rgsabound.cElements;
          if (((int) variant._typeUnion._vt & 5) != 0)
          {
            double[] destination = new double[(int) cElements];
            Marshal.Copy(structure.pvData, destination, 0, (int) cElements);
            return (object) destination;
          }
          if (((int) variant._typeUnion._vt & 3) == 0)
            throw new NotImplementedException("Marshaling this SafeArray type is not yet supported. VT = " + (object) variant._typeUnion._vt);
          int[] destination1 = new int[(int) cElements];
          Marshal.Copy(structure.pvData, destination1, 0, (int) cElements);
          return (object) destination1;
        default:
          throw new NotImplementedException("Unsupported variant type: VT = " + (object) variant._typeUnion._vt);
      }
    }

    public static void Free(this Variant variant)
    {
      if (variant._typeUnion._vt == (ushort) 8 && variant._typeUnion._unionTypes._bstr != IntPtr.Zero)
        Marshal.FreeBSTR(variant._typeUnion._unionTypes._bstr);
      else if (((int) variant._typeUnion._vt & 8192) != 0 && variant._typeUnion._unionTypes._byref != IntPtr.Zero && UiaConvert.SafeArrayDestroy(variant._typeUnion._unionTypes._byref) != 0)
        throw new Win32Exception(string.Format("SafeArrayDestroy failed: {0}", (object) Marshal.GetLastWin32Error()));
      variant._typeUnion._vt = (ushort) 0;
      variant._typeUnion._unionTypes._byref = IntPtr.Zero;
    }

    public static Variant ToVariant(this object value)
    {
      Variant variant = new Variant();
      switch (value)
      {
        case null:
          variant._typeUnion._vt = (ushort) 0;
          break;
        case bool _:
          variant._typeUnion._vt = UiaConvert.VTEnumsLookup[typeof (bool)];
          variant._typeUnion._unionTypes._bool = (bool) value ? (short) 1 : (short) 0;
          break;
        case sbyte _:
          variant._typeUnion._vt = UiaConvert.VTEnumsLookup[typeof (sbyte)];
          variant._typeUnion._unionTypes._i1 = (sbyte) value;
          break;
        case byte _:
          variant._typeUnion._vt = UiaConvert.VTEnumsLookup[typeof (byte)];
          variant._typeUnion._unionTypes._ui1 = (byte) value;
          break;
        case short _:
          variant._typeUnion._vt = UiaConvert.VTEnumsLookup[typeof (short)];
          variant._typeUnion._unionTypes._i2 = (short) value;
          break;
        case ushort _:
          variant._typeUnion._vt = UiaConvert.VTEnumsLookup[typeof (ushort)];
          variant._typeUnion._unionTypes._ui2 = (ushort) value;
          break;
        case int _:
          variant._typeUnion._vt = UiaConvert.VTEnumsLookup[typeof (int)];
          variant._typeUnion._unionTypes._i4 = (int) value;
          break;
        case uint _:
          variant._typeUnion._vt = UiaConvert.VTEnumsLookup[typeof (uint)];
          variant._typeUnion._unionTypes._ui4 = (uint) value;
          break;
        case long _:
          variant._typeUnion._vt = UiaConvert.VTEnumsLookup[typeof (long)];
          variant._typeUnion._unionTypes._i8 = (long) value;
          break;
        case ulong _:
          variant._typeUnion._vt = UiaConvert.VTEnumsLookup[typeof (ulong)];
          variant._typeUnion._unionTypes._ui8 = (ulong) value;
          break;
        default:
          object obj;
          if ((obj = value) is float)
          {
            float num = (float) obj;
            variant._typeUnion._vt = UiaConvert.VTEnumsLookup[typeof (float)];
            variant._typeUnion._unionTypes._r4 = num;
            break;
          }
          switch (value)
          {
            case double num:
              variant._typeUnion._vt = UiaConvert.VTEnumsLookup[typeof (double)];
              variant._typeUnion._unionTypes._r8 = num;
              break;
            case string s:
              variant._typeUnion._vt = (ushort) 8;
              variant._typeUnion._unionTypes._bstr = Marshal.StringToBSTR(s);
              break;
            case Array array:
              ushort arrayType = UiaConvert.VTEnumsLookup[array.GetType().GetElementType()];
              variant._typeUnion._vt = (ushort) (8192U | (uint) arrayType);
              variant._typeUnion._unionTypes._byref = UiaConvert.GetUnmanagedSafeArray(array, arrayType);
              break;
            default:
              throw new InvalidOperationException("Could not convert from Object to Variant");
          }
          break;
      }
      return variant;
    }

    private static IntPtr GetUnmanagedSafeArray(Array array, ushort arrayType)
    {
      IntPtr zero1 = IntPtr.Zero;
      IntPtr num1 = Marshal.AllocHGlobal(Marshal.SizeOf<UiaConvert.SafeArray.SafeArrayBound>() * array.Rank);
      IntPtr ptr = num1;
      for (int dimension = array.Rank - 1; dimension >= 0; --dimension)
      {
        UiaConvert.SafeArray.SafeArrayBound structure = new UiaConvert.SafeArray.SafeArrayBound();
        structure.lLbound = array.GetLowerBound(dimension);
        structure.cElements = (uint) array.GetLength(dimension);
        Marshal.StructureToPtr<UiaConvert.SafeArray.SafeArrayBound>(structure, ptr, false);
        ptr += Marshal.SizeOf<UiaConvert.SafeArray.SafeArrayBound>(structure);
      }
      IntPtr targetSafeArray = UiaConvert.SafeArrayCreate(arrayType, (uint) array.Rank, num1);
      int[] arrayIndices = new int[array.Rank];
      IntPtr num2 = Marshal.AllocHGlobal(arrayIndices.Length * 4);
      UiaConvert.MarshalMultiDimensionalArrayToSafeArray(targetSafeArray, array, arrayIndices, num2, 0);
      Marshal.FreeHGlobal(num2);
      Marshal.DestroyStructure<UiaConvert.SafeArray.SafeArrayBound>(num1);
      IntPtr zero2;
      IntPtr num3 = zero2 = IntPtr.Zero;
      return targetSafeArray;
    }

    private static IntPtr GetUnmanagedMarshaledValue(object managedValue)
    {
      IntPtr zero = IntPtr.Zero;
      Type elementType = managedValue.GetType().GetElementType();
      IntPtr ptr;
      switch (managedValue)
      {
        case bool _:
          ptr = Marshal.AllocHGlobal(2);
          Marshal.StructureToPtr<short>((bool) managedValue ? (short) 1 : (short) 0, ptr, false);
          break;
        case byte _:
          ptr = Marshal.AllocHGlobal(1);
          Marshal.StructureToPtr<byte>((byte) managedValue, ptr, false);
          break;
        case sbyte _:
          ptr = Marshal.AllocHGlobal(1);
          Marshal.StructureToPtr<sbyte>((sbyte) managedValue, ptr, false);
          break;
        case short _:
          ptr = Marshal.AllocHGlobal(2);
          Marshal.StructureToPtr<short>((short) managedValue, ptr, false);
          break;
        case ushort _:
          ptr = Marshal.AllocHGlobal(2);
          Marshal.StructureToPtr<ushort>((ushort) managedValue, ptr, false);
          break;
        case int _:
          ptr = Marshal.AllocHGlobal(4);
          Marshal.StructureToPtr<int>((int) managedValue, ptr, false);
          break;
        case uint _:
          ptr = Marshal.AllocHGlobal(4);
          Marshal.StructureToPtr<uint>((uint) managedValue, ptr, false);
          break;
        case long _:
          ptr = Marshal.AllocHGlobal(8);
          Marshal.StructureToPtr<long>((long) managedValue, ptr, false);
          break;
        case ulong _:
          ptr = Marshal.AllocHGlobal(8);
          Marshal.StructureToPtr<ulong>((ulong) managedValue, ptr, false);
          break;
        case float structure:
          ptr = Marshal.AllocHGlobal(4);
          Marshal.StructureToPtr<float>(structure, ptr, false);
          break;
        case double structure:
          ptr = Marshal.AllocHGlobal(8);
          Marshal.StructureToPtr<double>(structure, ptr, false);
          break;
        default:
          throw new InvalidOperationException("Unknown type to convert: " + elementType.ToString());
      }
      return ptr;
    }

    private static void MarshalMultiDimensionalArrayToSafeArray(
      IntPtr targetSafeArray,
      Array sourceArray,
      int[] arrayIndices,
      IntPtr unmanagedIndices,
      int currentDimension)
    {
      for (int lowerBound = sourceArray.GetLowerBound(currentDimension); lowerBound <= sourceArray.GetUpperBound(currentDimension); ++lowerBound)
      {
        arrayIndices[currentDimension] = lowerBound;
        Marshal.Copy(arrayIndices, currentDimension, unmanagedIndices + (arrayIndices.Length - 1 - currentDimension) * 4, 1);
        if (currentDimension + 1 < sourceArray.Rank)
        {
          UiaConvert.MarshalMultiDimensionalArrayToSafeArray(targetSafeArray, sourceArray, arrayIndices, unmanagedIndices, currentDimension + 1);
        }
        else
        {
          IntPtr unmanagedMarshaledValue = UiaConvert.GetUnmanagedMarshaledValue(sourceArray.GetValue(arrayIndices));
          int num = UiaConvert.SafeArrayPutElement(targetSafeArray, unmanagedIndices, unmanagedMarshaledValue);
          Marshal.FreeHGlobal(unmanagedMarshaledValue);
          if (num != 0)
            throw new Win32Exception(string.Format("SafeArrayPutElement failed: {0}", (object) Marshal.GetLastWin32Error()));
        }
      }
    }

    public static object ConvertPropertyValue(
      AutomationProperty property,
      Variant propertyValueVariant)
    {
      object obj1 = propertyValueVariant.ToObject();
      if (property.Id == AutomationElement.ControlTypeProperty.Id)
      {
        int key = (int) obj1;
        return ControlType._idTable.ContainsKey(key) ? (object) ControlType._idTable[key] : throw new KeyNotFoundException(string.Format("Unknown ControlType: {0}.", (object) key));
      }
      if (property.Id == ExpandCollapsePatternIdentifiers.ExpandCollapseStateProperty.Id)
        return (object) (ExpandCollapseState) obj1;
      if (property.Id == AutomationElement.OrientationProperty.Id)
        return (object) (OrientationType) obj1;
      if (property.Id == AutomationElement.BoundingRectangleProperty.Id)
      {
        double[] numArray = (double[]) obj1;
        return (object) new Rect(numArray[0], numArray[1], numArray[2], numArray[3]);
      }
      if (property.Id == WindowPatternIdentifiers.WindowInteractionStateProperty.Id)
        return (object) (WindowInteractionState) obj1;
      if (property.Id == WindowPatternIdentifiers.WindowVisualStateProperty.Id)
        return (object) (WindowVisualState) obj1;
      switch (obj1)
      {
        case IUIAutomationElementArray elementArray:
          return (object) new AutomationElementCollection(elementArray);
        case object[] objArray:
          List<AutomationElement> automationElementList = new List<AutomationElement>(objArray.Length);
          foreach (object obj2 in objArray)
          {
            if (obj2 is IUIAutomationElement autoElement1)
              automationElementList.Add(new AutomationElement(autoElement1));
          }
          return (object) automationElementList.ToArray();
        default:
          return obj1 is IUIAutomationElement autoElement ? (object) new AutomationElement(autoElement) : obj1;
      }
    }

    internal struct SafeArray
    {
      internal ushort cDims;
      internal ushort fFeatures;
      internal uint cbElements;
      internal uint cLocks;
      internal IntPtr pvData;
      internal UiaConvert.SafeArray.SafeArrayBound rgsabound;
      internal const int FADF_AUTO = 1;
      internal const int FADF_STATIC = 2;
      internal const int FADF_EMBEDDED = 4;
      internal const int FADF_FIXEDSIZE = 16;
      internal const int FADF_RECORD = 32;
      internal const int FADF_HAVEIID = 64;
      internal const int FADF_HAVEVARTYPE = 128;
      internal const int FADF_BSTR = 256;
      internal const int FADF_UNKNOWN = 512;
      internal const int FADF_DISPATCH = 1024;
      internal const int FADF_VARIANT = 2048;
      internal const int FADF_RESERVED = 61448;

      internal struct SafeArrayBound
      {
        internal uint cElements;
        internal int lLbound;
      }
    }
  }
}
