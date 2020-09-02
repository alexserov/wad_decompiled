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

namespace System.Windows.Automation {
    internal static class UiaConvert {
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

        static readonly Dictionary<Type, ushort> VTEnumsLookup = new Dictionary<Type, ushort> {
            {
                typeof(bool),
                11
            }, {
                typeof(sbyte),
                16
            }, {
                typeof(byte),
                17
            }, {
                typeof(short),
                2
            }, {
                typeof(ushort),
                18
            }, {
                typeof(int),
                3
            }, {
                typeof(uint),
                19
            }, {
                typeof(long),
                20
            }, {
                typeof(ulong),
                21
            }, {
                typeof(float),
                4
            }, {
                typeof(double),
                5
            }
        };

        public static bool ConvertException(COMException e, out Exception newException) {
            var flag = true;
            switch (e.HResult) {
                case -2147220992:
                    newException = new ElementNotEnabledException(innerException: e);
                    break;
                case -2147220991:
                    newException = new ElementNotAvailableException(innerException: e);
                    break;
                case -2147220990:
                    newException = new NoClickablePointException(innerException: e);
                    break;
                case -2147220989:
                    newException = new ProxyAssemblyNotLoadedException(innerException: e);
                    break;
                default:
                    newException = null;
                    flag = false;
                    break;
            }

            return flag;
        }

        public static TextPatternRangeEndpoint Convert(
            UIAutomationClient.TextPatternRangeEndpoint textPatternRangeEndpoint) {
            return (TextPatternRangeEndpoint) textPatternRangeEndpoint;
        }

        public static TextUnit Convert(UIAutomationClient.TextUnit textUnit) {
            return (TextUnit) textUnit;
        }

        public static SupportedTextSelection Convert(
            UIAutomationClient.SupportedTextSelection supportedTextSelection) {
            return (SupportedTextSelection) supportedTextSelection;
        }

        public static AutomationElementMode Convert(
            UIAutomationClient.AutomationElementMode automationElementMode) {
            return (AutomationElementMode) automationElementMode;
        }

        public static StructureChangeType Convert(
            UIAutomationClient.StructureChangeType structureChangeType) {
            return (StructureChangeType) structureChangeType;
        }

        public static ExpandCollapseState Convert(
            UIAutomationClient.ExpandCollapseState expandCollapseState) {
            return (ExpandCollapseState) expandCollapseState;
        }

        public static TreeScope Convert(UIAutomationClient.TreeScope treeScope) {
            return (TreeScope) treeScope;
        }

        public static ToggleState Convert(UIAutomationClient.ToggleState toggleState) {
            return (ToggleState) toggleState;
        }

        public static WindowInteractionState Convert(
            UIAutomationClient.WindowInteractionState windowInteractionState) {
            return (WindowInteractionState) windowInteractionState;
        }

        public static WindowVisualState Convert(UIAutomationClient.WindowVisualState windowVisualState) {
            return (WindowVisualState) windowVisualState;
        }

        public static UIAutomationClient.DockPosition Convert(DockPosition position) {
            return (UIAutomationClient.DockPosition) position;
        }

        public static RowOrColumnMajor Convert(UIAutomationClient.RowOrColumnMajor rowOrColumnMajor) {
            return (RowOrColumnMajor) rowOrColumnMajor;
        }

        public static UIAutomationClient.TreeScope Convert(TreeScope treeScope) {
            return (UIAutomationClient.TreeScope) treeScope;
        }

        public static UIAutomationClient.ScrollAmount Convert(ScrollAmount scrollAmount) {
            return (UIAutomationClient.ScrollAmount) scrollAmount;
        }

        public static UIAutomationClient.WindowVisualState Convert(
            WindowVisualState state) {
            return (UIAutomationClient.WindowVisualState) state;
        }

        public static UIAutomationClient.TextPatternRangeEndpoint Convert(
            TextPatternRangeEndpoint textPatternRangeEndpoint) {
            return (UIAutomationClient.TextPatternRangeEndpoint) textPatternRangeEndpoint;
        }

        public static UIAutomationClient.TextUnit Convert(TextUnit textUnit) {
            return (UIAutomationClient.TextUnit) textUnit;
        }

        public static UIAutomationClient.SupportedTextSelection Convert(
            SupportedTextSelection supportedTextSelection) {
            return (UIAutomationClient.SupportedTextSelection) supportedTextSelection;
        }

        public static UIAutomationClient.AutomationElementMode Convert(
            AutomationElementMode automationElementMode) {
            return (UIAutomationClient.AutomationElementMode) automationElementMode;
        }

        public static UIAutomationClient.StructureChangeType Convert(
            StructureChangeType structureChangeType) {
            return (UIAutomationClient.StructureChangeType) structureChangeType;
        }

        [DllImport(dllName: "OleAut32.dll", SetLastError = true)]
        static extern IntPtr SafeArrayCreate(ushort vt, uint cDims, IntPtr rgsabound);

        [DllImport(dllName: "OleAut32.dll", SetLastError = true)]
        static extern int SafeArrayDestroy(IntPtr psa);

        [DllImport(dllName: "OleAut32.dll", SetLastError = true)]
        static extern int SafeArrayPutElement(IntPtr psa, IntPtr rgIndices, IntPtr pv);

        public static object ToObject(this Variant variant) {
            switch (variant._typeUnion._vt) {
                case 0:
                    return null;
                case 2:
                    return variant._typeUnion._unionTypes._i2;
                case 3:
                    return variant._typeUnion._unionTypes._i4;
                case 4:
                    return variant._typeUnion._unionTypes._r4;
                case 5:
                    return variant._typeUnion._unionTypes._r8;
                case 6:
                    return variant._typeUnion._unionTypes._cy;
                case 7:
                    return variant._typeUnion._unionTypes._date;
                case 8:
                    return Marshal.PtrToStringUni(ptr: variant._typeUnion._unionTypes._bstr);
                case 11:
                    return (uint) variant._typeUnion._unionTypes._bool > 0U;
                case 13:
                    return Marshal.GetObjectForIUnknown(pUnk: variant._typeUnion._unionTypes._unknown);
                case 16:
                    return variant._typeUnion._unionTypes._i1;
                case 17:
                    return variant._typeUnion._unionTypes._ui1;
                case 18:
                    return variant._typeUnion._unionTypes._ui2;
                case 19:
                    return variant._typeUnion._unionTypes._ui4;
                case 20:
                    return variant._typeUnion._unionTypes._i8;
                case 21:
                    return variant._typeUnion._unionTypes._ui8;
                case 22:
                    return variant._typeUnion._unionTypes._int;
                case 23:
                    return variant._typeUnion._unionTypes._uint;
                case 8195:
                case 8197:
                    var structure = Marshal.PtrToStructure<SafeArray>(ptr: variant._typeUnion._unionTypes._byref);
                    if (structure.cDims != 1 || (structure.fFeatures & 128) == 0)
                        throw new NotImplementedException(message: "Multidimensional or unknown type array marshaling is not yet supported");
                    var cElements = structure.rgsabound.cElements;
                    if ((variant._typeUnion._vt & 5) != 0) {
                        var destination = new double[(int) cElements];
                        Marshal.Copy(source: structure.pvData, destination: destination, startIndex: 0, length: (int) cElements);
                        return destination;
                    }

                    if ((variant._typeUnion._vt & 3) == 0)
                        throw new NotImplementedException(message: "Marshaling this SafeArray type is not yet supported. VT = " + variant._typeUnion._vt);
                    var destination1 = new int[(int) cElements];
                    Marshal.Copy(source: structure.pvData, destination: destination1, startIndex: 0, length: (int) cElements);
                    return destination1;
                default:
                    throw new NotImplementedException(message: "Unsupported variant type: VT = " + variant._typeUnion._vt);
            }
        }

        public static void Free(this Variant variant) {
            if (variant._typeUnion._vt == 8 && variant._typeUnion._unionTypes._bstr != IntPtr.Zero)
                Marshal.FreeBSTR(ptr: variant._typeUnion._unionTypes._bstr);
            else if ((variant._typeUnion._vt & 8192) != 0 && variant._typeUnion._unionTypes._byref != IntPtr.Zero && SafeArrayDestroy(psa: variant._typeUnion._unionTypes._byref) != 0)
                throw new Win32Exception(message: string.Format(format: "SafeArrayDestroy failed: {0}", arg0: Marshal.GetLastWin32Error()));
            variant._typeUnion._vt = 0;
            variant._typeUnion._unionTypes._byref = IntPtr.Zero;
        }

        public static Variant ToVariant(this object value) {
            var variant = new Variant();
            switch (value) {
                case null:
                    variant._typeUnion._vt = 0;
                    break;
                case bool _:
                    variant._typeUnion._vt = VTEnumsLookup[key: typeof(bool)];
                    variant._typeUnion._unionTypes._bool = (bool) value ? (short) 1 : (short) 0;
                    break;
                case sbyte _:
                    variant._typeUnion._vt = VTEnumsLookup[key: typeof(sbyte)];
                    variant._typeUnion._unionTypes._i1 = (sbyte) value;
                    break;
                case byte _:
                    variant._typeUnion._vt = VTEnumsLookup[key: typeof(byte)];
                    variant._typeUnion._unionTypes._ui1 = (byte) value;
                    break;
                case short _:
                    variant._typeUnion._vt = VTEnumsLookup[key: typeof(short)];
                    variant._typeUnion._unionTypes._i2 = (short) value;
                    break;
                case ushort _:
                    variant._typeUnion._vt = VTEnumsLookup[key: typeof(ushort)];
                    variant._typeUnion._unionTypes._ui2 = (ushort) value;
                    break;
                case int _:
                    variant._typeUnion._vt = VTEnumsLookup[key: typeof(int)];
                    variant._typeUnion._unionTypes._i4 = (int) value;
                    break;
                case uint _:
                    variant._typeUnion._vt = VTEnumsLookup[key: typeof(uint)];
                    variant._typeUnion._unionTypes._ui4 = (uint) value;
                    break;
                case long _:
                    variant._typeUnion._vt = VTEnumsLookup[key: typeof(long)];
                    variant._typeUnion._unionTypes._i8 = (long) value;
                    break;
                case ulong _:
                    variant._typeUnion._vt = VTEnumsLookup[key: typeof(ulong)];
                    variant._typeUnion._unionTypes._ui8 = (ulong) value;
                    break;
                default:
                    object obj;
                    if ((obj = value) is float) {
                        var num = (float) obj;
                        variant._typeUnion._vt = VTEnumsLookup[key: typeof(float)];
                        variant._typeUnion._unionTypes._r4 = num;
                        break;
                    }

                    switch (value) {
                        case double num:
                            variant._typeUnion._vt = VTEnumsLookup[key: typeof(double)];
                            variant._typeUnion._unionTypes._r8 = num;
                            break;
                        case string s:
                            variant._typeUnion._vt = 8;
                            variant._typeUnion._unionTypes._bstr = Marshal.StringToBSTR(s: s);
                            break;
                        case Array array:
                            var arrayType = VTEnumsLookup[key: array.GetType().GetElementType()];
                            variant._typeUnion._vt = (ushort) (8192U | arrayType);
                            variant._typeUnion._unionTypes._byref = GetUnmanagedSafeArray(array: array, arrayType: arrayType);
                            break;
                        default:
                            throw new InvalidOperationException(message: "Could not convert from Object to Variant");
                    }

                    break;
            }

            return variant;
        }

        static IntPtr GetUnmanagedSafeArray(Array array, ushort arrayType) {
            var zero1 = IntPtr.Zero;
            var num1 = Marshal.AllocHGlobal(cb: Marshal.SizeOf<SafeArray.SafeArrayBound>() * array.Rank);
            var ptr = num1;
            for (var dimension = array.Rank - 1; dimension >= 0; --dimension) {
                var structure = new SafeArray.SafeArrayBound();
                structure.lLbound = array.GetLowerBound(dimension: dimension);
                structure.cElements = (uint) array.GetLength(dimension: dimension);
                Marshal.StructureToPtr(structure: structure, ptr: ptr, fDeleteOld: false);
                ptr += Marshal.SizeOf(structure: structure);
            }

            var targetSafeArray = SafeArrayCreate(vt: arrayType, cDims: (uint) array.Rank, rgsabound: num1);
            var arrayIndices = new int[array.Rank];
            var num2 = Marshal.AllocHGlobal(cb: arrayIndices.Length * 4);
            MarshalMultiDimensionalArrayToSafeArray(targetSafeArray: targetSafeArray, sourceArray: array, arrayIndices: arrayIndices, unmanagedIndices: num2, currentDimension: 0);
            Marshal.FreeHGlobal(hglobal: num2);
            Marshal.DestroyStructure<SafeArray.SafeArrayBound>(ptr: num1);
            IntPtr zero2;
            var num3 = zero2 = IntPtr.Zero;
            return targetSafeArray;
        }

        static IntPtr GetUnmanagedMarshaledValue(object managedValue) {
            var zero = IntPtr.Zero;
            var elementType = managedValue.GetType().GetElementType();
            IntPtr ptr;
            switch (managedValue) {
                case bool _:
                    ptr = Marshal.AllocHGlobal(cb: 2);
                    Marshal.StructureToPtr(structure: (bool) managedValue ? (short) 1 : (short) 0, ptr: ptr, fDeleteOld: false);
                    break;
                case byte _:
                    ptr = Marshal.AllocHGlobal(cb: 1);
                    Marshal.StructureToPtr(structure: (byte) managedValue, ptr: ptr, fDeleteOld: false);
                    break;
                case sbyte _:
                    ptr = Marshal.AllocHGlobal(cb: 1);
                    Marshal.StructureToPtr(structure: (sbyte) managedValue, ptr: ptr, fDeleteOld: false);
                    break;
                case short _:
                    ptr = Marshal.AllocHGlobal(cb: 2);
                    Marshal.StructureToPtr(structure: (short) managedValue, ptr: ptr, fDeleteOld: false);
                    break;
                case ushort _:
                    ptr = Marshal.AllocHGlobal(cb: 2);
                    Marshal.StructureToPtr(structure: (ushort) managedValue, ptr: ptr, fDeleteOld: false);
                    break;
                case int _:
                    ptr = Marshal.AllocHGlobal(cb: 4);
                    Marshal.StructureToPtr(structure: (int) managedValue, ptr: ptr, fDeleteOld: false);
                    break;
                case uint _:
                    ptr = Marshal.AllocHGlobal(cb: 4);
                    Marshal.StructureToPtr(structure: (uint) managedValue, ptr: ptr, fDeleteOld: false);
                    break;
                case long _:
                    ptr = Marshal.AllocHGlobal(cb: 8);
                    Marshal.StructureToPtr(structure: (long) managedValue, ptr: ptr, fDeleteOld: false);
                    break;
                case ulong _:
                    ptr = Marshal.AllocHGlobal(cb: 8);
                    Marshal.StructureToPtr(structure: (ulong) managedValue, ptr: ptr, fDeleteOld: false);
                    break;
                case float structure:
                    ptr = Marshal.AllocHGlobal(cb: 4);
                    Marshal.StructureToPtr(structure: structure, ptr: ptr, fDeleteOld: false);
                    break;
                case double structure:
                    ptr = Marshal.AllocHGlobal(cb: 8);
                    Marshal.StructureToPtr(structure: structure, ptr: ptr, fDeleteOld: false);
                    break;
                default:
                    throw new InvalidOperationException(message: "Unknown type to convert: " + elementType);
            }

            return ptr;
        }

        static void MarshalMultiDimensionalArrayToSafeArray(
            IntPtr targetSafeArray,
            Array sourceArray,
            int[] arrayIndices,
            IntPtr unmanagedIndices,
            int currentDimension) {
            for (var lowerBound = sourceArray.GetLowerBound(dimension: currentDimension); lowerBound <= sourceArray.GetUpperBound(dimension: currentDimension); ++lowerBound) {
                arrayIndices[currentDimension] = lowerBound;
                Marshal.Copy(source: arrayIndices, startIndex: currentDimension, destination: unmanagedIndices + (arrayIndices.Length - 1 - currentDimension) * 4, length: 1);
                if (currentDimension + 1 < sourceArray.Rank) {
                    MarshalMultiDimensionalArrayToSafeArray(targetSafeArray: targetSafeArray, sourceArray: sourceArray, arrayIndices: arrayIndices, unmanagedIndices: unmanagedIndices, currentDimension: currentDimension + 1);
                } else {
                    var unmanagedMarshaledValue = GetUnmanagedMarshaledValue(managedValue: sourceArray.GetValue(indices: arrayIndices));
                    var num = SafeArrayPutElement(psa: targetSafeArray, rgIndices: unmanagedIndices, pv: unmanagedMarshaledValue);
                    Marshal.FreeHGlobal(hglobal: unmanagedMarshaledValue);
                    if (num != 0)
                        throw new Win32Exception(message: string.Format(format: "SafeArrayPutElement failed: {0}", arg0: Marshal.GetLastWin32Error()));
                }
            }
        }

        public static object ConvertPropertyValue(
            AutomationProperty property,
            Variant propertyValueVariant) {
            var obj1 = propertyValueVariant.ToObject();
            if (property.Id == AutomationElement.ControlTypeProperty.Id) {
                var key = (int) obj1;
                return ControlType._idTable.ContainsKey(key: key) ? (object) ControlType._idTable[key: key] : throw new KeyNotFoundException(message: string.Format(format: "Unknown ControlType: {0}.", arg0: key));
            }

            if (property.Id == ExpandCollapsePatternIdentifiers.ExpandCollapseStateProperty.Id)
                return (ExpandCollapseState) obj1;
            if (property.Id == AutomationElement.OrientationProperty.Id)
                return (OrientationType) obj1;
            if (property.Id == AutomationElement.BoundingRectangleProperty.Id) {
                var numArray = (double[]) obj1;
                return new Rect(x: numArray[0], y: numArray[1], width: numArray[2], height: numArray[3]);
            }

            if (property.Id == WindowPatternIdentifiers.WindowInteractionStateProperty.Id)
                return (WindowInteractionState) obj1;
            if (property.Id == WindowPatternIdentifiers.WindowVisualStateProperty.Id)
                return (WindowVisualState) obj1;
            switch (obj1) {
                case IUIAutomationElementArray elementArray:
                    return new AutomationElementCollection(elementArray: elementArray);
                case object[] objArray:
                    var automationElementList = new List<AutomationElement>(capacity: objArray.Length);
                    foreach (var obj2 in objArray)
                        if (obj2 is IUIAutomationElement autoElement1)
                            automationElementList.Add(item: new AutomationElement(autoElement: autoElement1));
                    return automationElementList.ToArray();
                default:
                    return obj1 is IUIAutomationElement autoElement ? new AutomationElement(autoElement: autoElement) : obj1;
            }
        }

        internal struct SafeArray {
            internal ushort cDims;
            internal ushort fFeatures;
            internal uint cbElements;
            internal uint cLocks;
            internal IntPtr pvData;
            internal SafeArrayBound rgsabound;
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

            internal struct SafeArrayBound {
                internal uint cElements;
                internal int lLbound;
            }
        }
    }
}