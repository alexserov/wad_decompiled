// Decompiled with JetBrains decompiler
// Type: MS.Internal.Mita.Foundation.InputAlgorithms
// Assembly: MitaLite.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D55104E9-B4F1-4494-96EC-27213A277E13
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaLite.Foundation.dll

using System;
using System.Collections.Generic;

namespace MS.Internal.Mita.Foundation {
    internal class InputAlgorithms : IInputAlgorithms {
        const int afterMoveActionWaitDuration = 800;
        const int afterTapActionWaitDuration = 200;

        public IList<IInputAction> DynamicPress(PointI point, uint contactId) {
            IList<IInputAction> injectList = new List<IInputAction>();
            Press(location: point, injectList: injectList, pointerId: contactId);
            return injectList;
        }

        public IInputAction DynamicMove(PointI point, uint contactId) {
            IList<IInputAction> injectList = new List<IInputAction>();
            Update(location: point, injectList: injectList, pointerId: contactId);
            return injectList[index: 0];
        }

        public IInputAction DynamicMoves(IList<PointI> points, IList<uint> contactIds) {
            IList<IInputAction> injectList = new List<IInputAction>();
            MultiUpdate(locations: points, injectList: injectList, contactIds: contactIds);
            return injectList[index: 0];
        }

        public IList<IInputAction> DynamicRelease(PointI point, uint contactId) {
            IList<IInputAction> injectList = new List<IInputAction>();
            Release(location: point, injectList: injectList, contactId: contactId);
            return injectList;
        }

        public IList<IInputAction> DynamicPointerActions(PointerData[] pointerDataArray) {
            var inputActionList = new List<IInputAction>();
            inputActionList.Add(item: new MultiPointerInputAction {
                pointerData = pointerDataArray
            });
            return inputActionList;
        }

        public IList<IInputAction> DynamicDrag(
            PointI start,
            PointI end,
            uint maxDuration,
            uint packetDelta,
            uint contactId) {
            IList<IInputAction> injectList = new List<IInputAction>();
            var num1 = maxDuration / packetDelta;
            var num2 = end.X - start.X;
            var num3 = end.Y - start.Y;
            var num4 = 1U + (uint) Math.Sqrt(d: num2 * num2 + num3 * num3) / 50U;
            if (num4 > num1)
                num4 = num1;
            var location = start;
            var elapsedTime = 0;
            var num5 = (end.X - start.X) / (double) num4;
            var num6 = (end.Y - start.Y) / (double) num4;
            for (var index = 0; (long) index < (long) (num4 - 1U); ++index) {
                location.X = start.X + index * (int) num5;
                location.Y = start.Y + index * (int) num6;
                Update(location: location, injectList: injectList, pointerId: contactId);
                Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
            }

            Update(location: end, injectList: injectList, pointerId: contactId);
            return injectList;
        }

        public IList<IInputAction> PressAndHold(
            PointI point,
            uint holdDuration,
            uint tapCount,
            uint tapDelta,
            uint packetDelta) {
            if (packetDelta < 10U)
                throw new ArgumentOutOfRangeException(paramName: "packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
            if (holdDuration < packetDelta)
                throw new ArgumentOutOfRangeException(paramName: "holdDuration must be greater than or equal to packetDelta");
            if (tapCount > 1U && tapDelta < packetDelta)
                throw new ArgumentOutOfRangeException(paramName: "tapDelta must be greater than or equal to packetDelta");
            var elapsedTime = 0;
            var num = (int) Math.Ceiling(a: holdDuration / (double) packetDelta);
            IList<IInputAction> injectList = new List<IInputAction>();
            for (var index1 = 0; (long) index1 < (long) tapCount; ++index1) {
                if (index1 > 0)
                    Wait(elapsedTime: ref elapsedTime, duration: (int) tapDelta, injectList: injectList);
                Press(location: point, injectList: injectList);
                for (var index2 = 0; index2 < num; ++index2) {
                    Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
                    Update(location: point, injectList: injectList);
                }

                Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
                Release(location: point, injectList: injectList);
            }

            injectList.Add(item: Input.CreateWait(duration: 200));
            return injectList;
        }

        public IList<IInputAction> PressAndHoldAndDrag(
            PointI start,
            PointI end,
            uint dragDuration,
            uint holdDuration,
            uint packetDelta) {
            if (packetDelta < 10U)
                throw new ArgumentOutOfRangeException(paramName: "packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
            if (dragDuration < packetDelta)
                throw new ArgumentOutOfRangeException(paramName: "dragDuration must be greater than or equal to packetDelta");
            if (holdDuration < packetDelta)
                throw new ArgumentOutOfRangeException(paramName: "holdDuration must be greater than or equal to packetDelta");
            var elapsedTime = 0;
            IList<IInputAction> injectList = new List<IInputAction>();
            Press(location: start, injectList: injectList);
            var num1 = (int) Math.Ceiling(a: holdDuration / (double) packetDelta);
            for (var index = 0; index < num1; ++index) {
                Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
                Update(location: start, injectList: injectList);
            }

            var num2 = (int) Math.Ceiling(a: dragDuration / (double) packetDelta);
            var location = start;
            var num3 = (end.X - start.X) / (double) dragDuration;
            var num4 = (end.Y - start.Y) / (double) dragDuration;
            var num5 = (int) packetDelta;
            for (var index = 0; index < num2 - 1; ++index) {
                location.X = start.X + (int) Math.Round(a: num3 * num5);
                location.Y = start.Y + (int) Math.Round(a: num4 * num5);
                Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
                Update(location: location, injectList: injectList);
                num5 += (int) packetDelta;
            }

            Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
            Update(location: end, injectList: injectList);
            Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
            Release(location: end, injectList: injectList);
            injectList.Add(item: Input.CreateWait(duration: 800));
            return injectList;
        }

        public IList<IInputAction> PressAndHoldAndDragWithAcceleration(
            PointI start,
            PointI end,
            uint holdDuration,
            float acceleration,
            uint packetDelta) {
            if (packetDelta < 10U)
                throw new ArgumentOutOfRangeException(paramName: "packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
            if (holdDuration < packetDelta)
                throw new ArgumentOutOfRangeException(paramName: "holdDuration must be greater than or equal to packetDelta");
            if (acceleration <= 0.0)
                throw new ArgumentOutOfRangeException(paramName: "acceleration must be greater than 0.0");
            var elapsedTime = 0;
            IList<IInputAction> injectList = new List<IInputAction>();
            Press(location: start, injectList: injectList);
            var num1 = (int) Math.Ceiling(a: holdDuration / (double) packetDelta);
            for (var index = 0; index < num1; ++index) {
                Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
                Update(location: start, injectList: injectList);
            }

            var num2 = end.X - start.X;
            var num3 = end.Y - start.Y;
            var num4 = (int) Math.Ceiling(a: Math.Sqrt(d: 2.0 * Math.Sqrt(d: num2 * num2 + num3 * num3) / acceleration) / packetDelta);
            var location = start;
            var num5 = Math.Atan2(y: num3, x: num2);
            var num6 = acceleration * Math.Cos(d: num5);
            var num7 = acceleration * Math.Sin(a: num5);
            var num8 = (int) packetDelta;
            for (var index = 0; index < num4 - 1; ++index) {
                location.X = start.X + (int) Math.Round(a: 0.5 * num6 * num8 * num8);
                location.Y = start.Y + (int) Math.Round(a: 0.5 * num7 * num8 * num8);
                Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
                Update(location: location, injectList: injectList);
                num8 += (int) packetDelta;
            }

            Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
            Update(location: end, injectList: injectList);
            Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
            Release(location: end, injectList: injectList);
            injectList.Add(item: Input.CreateWait(duration: 800));
            return injectList;
        }

        public IList<IInputAction> TwoFingerPressAndHold(
            PointI pointOne,
            PointI pointTwo,
            uint holdDuration,
            uint tapCount,
            uint tapDelta,
            uint packetDelta) {
            if (packetDelta < 10U)
                throw new ArgumentOutOfRangeException(paramName: "packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
            if (holdDuration < packetDelta)
                throw new ArgumentOutOfRangeException(paramName: "holdDuration must be greater than or equal to packetDelta");
            if (tapCount > 1U && tapDelta < packetDelta)
                throw new ArgumentOutOfRangeException(paramName: "tapDelta must be greater than or equal to packetDelta");
            var elapsedTime = 0;
            var num = (int) Math.Ceiling(a: holdDuration / (double) packetDelta);
            IList<IInputAction> injectList = new List<IInputAction>();
            for (var index1 = 0; (long) index1 < (long) tapCount; ++index1) {
                if (index1 > 0)
                    Wait(elapsedTime: ref elapsedTime, duration: (int) tapDelta, injectList: injectList);
                MultiPress(locations: new List<PointI> {
                    pointOne,
                    pointTwo
                }, injectList: injectList);
                for (var index2 = 0; index2 < num; ++index2) {
                    Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
                    MultiUpdate(locations: new List<PointI> {
                        pointOne,
                        pointTwo
                    }, injectList: injectList);
                }

                Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
                MultiRelease(locations: new List<PointI> {
                    pointOne,
                    pointTwo
                }, injectList: injectList);
            }

            injectList.Add(item: Input.CreateWait(duration: 200));
            return injectList;
        }

        public IList<IInputAction> TwoFingerZoom(
            PointI pointOne,
            PointI pointTwo,
            float direction,
            uint duration,
            uint distance,
            bool pivotZoom,
            uint packetDelta) {
            if (packetDelta < 10U)
                throw new ArgumentOutOfRangeException(paramName: "packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
            if (duration < packetDelta)
                throw new ArgumentOutOfRangeException(paramName: "duration must be greater than or equal to packetDelta");
            if (direction > 360.0 || direction < 0.0)
                throw new ArgumentOutOfRangeException(paramName: "direction must be greater than or equal to 0.0 and less than or equal to 360.0");
            if (distance < 25U)
                throw new ArgumentOutOfRangeException(paramName: "distance must be greater than or equal to 25");
            var elapsedTime = 0;
            IList<IInputAction> injectList = new List<IInputAction>();
            MultiPress(locations: new List<PointI> {
                pointOne,
                pointTwo
            }, injectList: injectList);
            var num1 = (int) Math.Ceiling(a: duration / (double) packetDelta);
            var num2 = direction * Math.PI / 180.0;
            var pointI1 = pointOne;
            var pointI2 = pointTwo;
            var pointI3 = pointOne;
            var pointI4 = pointTwo;
            var a1 = distance * Math.Cos(d: num2);
            var a2 = distance * Math.Sin(a: num2);
            var a3 = -a1;
            var a4 = -a2;
            var num3 = a1 / duration;
            var num4 = a2 / duration;
            var num5 = -num3;
            var num6 = -num4;
            var num7 = (int) packetDelta;
            for (var index = 0; index < num1 - 1; ++index) {
                if (!pivotZoom) {
                    pointI1.X = pointOne.X + (int) Math.Round(a: num3 * num7);
                    pointI1.Y = pointOne.Y + (int) Math.Round(a: num4 * num7);
                }

                pointI2.X = pointTwo.X + (int) Math.Round(a: num5 * num7);
                pointI2.Y = pointTwo.Y + (int) Math.Round(a: num6 * num7);
                Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
                MultiUpdate(locations: new List<PointI> {
                    pointI1,
                    pointI2
                }, injectList: injectList);
                num7 += (int) packetDelta;
            }

            if (!pivotZoom)
                pointI3.Offset(offsetX: (int) Math.Round(a: a1), offsetY: (int) Math.Round(a: a2));
            pointI4.Offset(offsetX: (int) Math.Round(a: a3), offsetY: (int) Math.Round(a: a4));
            Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
            MultiUpdate(locations: new List<PointI> {
                pointI3,
                pointI4
            }, injectList: injectList);
            Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
            MultiRelease(locations: new List<PointI> {
                pointI3,
                pointI4
            }, injectList: injectList);
            injectList.Add(item: Input.CreateWait(duration: 800));
            return injectList;
        }

        public IList<IInputAction> TwoFingerPressAndHoldAndDragWithAcceleration(
            PointI pointOne,
            PointI pointTwo,
            float direction,
            uint distance,
            uint holdDuration,
            float acceleration,
            uint packetDelta) {
            if (packetDelta < 10U)
                throw new ArgumentOutOfRangeException(paramName: "packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
            if (holdDuration < packetDelta)
                throw new ArgumentOutOfRangeException(paramName: "duration must be greater than or equal to packetDelta");
            if (acceleration <= 0.0)
                throw new ArgumentOutOfRangeException(paramName: "acceleration must be greater than 0.0");
            if (direction > 360.0 || direction < 0.0)
                throw new ArgumentOutOfRangeException(paramName: "direction must be greater than or equal to 0.0 and less than or equal to 360.0");
            if (distance < 110U)
                throw new ArgumentOutOfRangeException(paramName: "distance must be greater than or equal to 110");
            var elapsedTime = 0;
            IList<IInputAction> injectList = new List<IInputAction>();
            MultiPress(locations: new List<PointI> {
                pointOne,
                pointTwo
            }, injectList: injectList);
            var num1 = (int) Math.Ceiling(a: holdDuration / (double) packetDelta);
            for (var index = 0; index < num1; ++index) {
                Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
                MultiUpdate(locations: new List<PointI> {
                    pointOne,
                    pointTwo
                }, injectList: injectList);
            }

            var num2 = (int) Math.Ceiling(a: Math.Sqrt(d: 2.0 * distance / acceleration) / packetDelta);
            var num3 = direction * Math.PI / 180.0;
            var pointI1 = pointOne;
            var pointI2 = pointTwo;
            var pointI3 = pointOne;
            var pointI4 = pointTwo;
            var a1 = distance * Math.Cos(d: num3);
            var a2 = distance * Math.Sin(a: num3);
            var num4 = acceleration * Math.Cos(d: num3);
            var num5 = acceleration * Math.Sin(a: num3);
            var num6 = (int) packetDelta;
            for (var index = 0; index < num2 - 1; ++index) {
                pointI1.X = pointOne.X + (int) Math.Round(a: 0.5 * num4 * num6 * num6);
                pointI1.Y = pointOne.Y + (int) Math.Round(a: 0.5 * num5 * num6 * num6);
                pointI2.X = pointTwo.X + (int) Math.Round(a: 0.5 * num4 * num6 * num6);
                pointI2.Y = pointTwo.Y + (int) Math.Round(a: 0.5 * num5 * num6 * num6);
                Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
                MultiUpdate(locations: new List<PointI> {
                    pointI1,
                    pointI2
                }, injectList: injectList);
                num6 += (int) packetDelta;
            }

            pointI3.Offset(offsetX: (int) Math.Round(a: a1), offsetY: (int) Math.Round(a: a2));
            pointI4.Offset(offsetX: (int) Math.Round(a: a1), offsetY: (int) Math.Round(a: a2));
            Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
            MultiUpdate(locations: new List<PointI> {
                pointI3,
                pointI4
            }, injectList: injectList);
            Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
            MultiRelease(locations: new List<PointI> {
                pointI3,
                pointI4
            }, injectList: injectList);
            injectList.Add(item: Input.CreateWait(duration: 800));
            return injectList;
        }

        public IList<IInputAction> TwoFingerRotate(
            PointI pointOne,
            PointI pointTwo,
            float rotationAngle,
            uint duration,
            bool pivotRotate,
            uint packetDelta) {
            if (packetDelta < 10U)
                throw new ArgumentOutOfRangeException(paramName: "packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
            if (duration < packetDelta)
                throw new ArgumentOutOfRangeException(paramName: "duration must be greater than or equal to packetDelta");
            if (pointOne == pointTwo)
                throw new ArgumentOutOfRangeException(paramName: "the two fingers must be at different points");
            var elapsedTime = 0;
            IList<IInputAction> injectList = new List<IInputAction>();
            MultiPress(locations: new List<PointI> {
                pointOne,
                pointTwo
            }, injectList: injectList);
            var num1 = (int) Math.Ceiling(a: duration / (double) packetDelta);
            var pointI1 = pointOne;
            var pointI2 = pointTwo;
            var num2 = rotationAngle * Math.PI / 180.0 / duration;
            double num3 = pointTwo.X - pointOne.X;
            double num4 = pointTwo.Y - pointOne.Y;
            var num5 = Math.Sqrt(d: num3 * num3 + num4 * num4);
            var pointI3 = pointOne;
            if (!pivotRotate) {
                num5 /= 2.0;
                pointI3.Offset(offsetX: (pointTwo.X - pointOne.X) / 2, offsetY: (pointTwo.Y - pointOne.Y) / 2);
            }

            var num6 = Math.Atan2(y: pointOne.Y - pointI3.Y, x: pointOne.X - pointI3.X);
            var num7 = Math.Atan2(y: pointTwo.Y - pointI3.Y, x: pointTwo.X - pointI3.X);
            var num8 = (int) packetDelta;
            for (var index = 0; index < num1 - 1; ++index) {
                if (!pivotRotate) {
                    pointI1.X = pointI3.X + (int) Math.Round(a: num5 * Math.Cos(d: num6 + num2 * num8));
                    pointI1.Y = pointI3.Y + (int) Math.Round(a: num5 * Math.Sin(a: num6 + num2 * num8));
                }

                pointI2.X = pointI3.X + (int) Math.Round(a: num5 * Math.Cos(d: num7 + num2 * num8));
                pointI2.Y = pointI3.Y + (int) Math.Round(a: num5 * Math.Sin(a: num7 + num2 * num8));
                Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
                MultiUpdate(locations: new List<PointI> {
                    pointI1,
                    pointI2
                }, injectList: injectList);
                num8 += (int) packetDelta;
            }

            if (!pivotRotate) {
                pointI1.X = pointI3.X + (int) Math.Round(a: num5 * Math.Cos(d: num6 + num2 * duration));
                pointI1.Y = pointI3.Y + (int) Math.Round(a: num5 * Math.Sin(a: num6 + num2 * duration));
            }

            pointI2.X = pointI3.X + (int) Math.Round(a: num5 * Math.Cos(d: num7 + num2 * duration));
            pointI2.Y = pointI3.Y + (int) Math.Round(a: num5 * Math.Sin(a: num7 + num2 * duration));
            Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
            MultiUpdate(locations: new List<PointI> {
                pointI1,
                pointI2
            }, injectList: injectList);
            Wait(elapsedTime: ref elapsedTime, duration: (int) packetDelta, injectList: injectList);
            MultiRelease(locations: new List<PointI> {
                pointI1,
                pointI2
            }, injectList: injectList);
            injectList.Add(item: Input.CreateWait(duration: 800));
            return injectList;
        }

        public IList<IInputAction> RawMultiTouchGesture(
            MultiTouchInjectionData[] injectionData,
            uint packetDelta) {
            if (packetDelta < 10U)
                throw new ArgumentOutOfRangeException(paramName: "packetDelta must be greater than or equal to MIN_PACKET_FREQUENCEY");
            IList<IInputAction> injectList = new List<IInputAction>();
            var elapsedTime = 0;
            var rowIndex = 0;
            var flag = true;
            while (flag) {
                flag = false;
                var injectListPointerData = new List<PointerData>();
                var waitData = new int?();
                for (uint pointerId = 0; (long) pointerId < (long) injectionData.Length; ++pointerId)
                    flag = EmitPoint(actions: injectionData[(int) pointerId].Actions, rowIndex: rowIndex, pointerId: pointerId, injectListPointerData: injectListPointerData, waitData: ref waitData) | flag;
                if (injectListPointerData.Count > 0)
                    injectList.Add(item: new MultiPointerInputAction {
                        pointerData = injectListPointerData.ToArray()
                    });
                if (waitData.HasValue)
                    injectList.Add(item: Input.CreateWait(duration: waitData.Value));
                else if (flag)
                    injectList.Add(item: Input.CreateWait(duration: (int) packetDelta));
                ++rowIndex;
            }

            Interpolate(elapsedTime: ref elapsedTime, injectList: injectList, packetDelta: packetDelta);
            injectList.Add(item: Input.CreateWait(duration: 800));
            ConvertBigWaitTime(injectList: injectList);
            return injectList;
        }

        bool EmitPoint(
            MultiAction[] actions,
            int rowIndex,
            uint pointerId,
            List<PointerData> injectListPointerData,
            ref int? waitData) {
            if (rowIndex > actions.Length - 1)
                return false;
            var action1 = actions[rowIndex];
            if (action1.Action == ActionType.Wait) {
                if (!waitData.HasValue || action1.Duration > waitData.Value)
                    waitData = (int) action1.Duration;
                PointerData[] pointerInputActions;
                if (rowIndex > 0) {
                    var action2 = actions[rowIndex - 1];
                    pointerInputActions = CreateMultiPointerInputActions(locations: new PointI[1] {
                        action2.Point
                    }, flag: POINTER_FLAGS.ContactMoves, pointerIds: new uint[1] {
                        pointerId
                    });
                    action1.Point = action2.Point;
                } else {
                    pointerInputActions = CreateMultiPointerInputActions(locations: new PointI[1] {
                        new PointI(x: 0, y: 0)
                    }, flag: POINTER_FLAGS.ContactMoves, pointerIds: new uint[1] {
                        pointerId
                    });
                }

                injectListPointerData.Add(item: pointerInputActions[0]);
            } else {
                var pointerInputActions = CreateMultiPointerInputActions(locations: new PointI[1] {
                    action1.Point
                }, flag: ActionTypeToPointerFlags(actionType: action1.Action), pointerIds: new uint[1] {
                    pointerId
                });
                injectListPointerData.Add(item: pointerInputActions[0]);
            }

            return rowIndex != actions.Length - 1;
        }

        POINTER_FLAGS ActionTypeToPointerFlags(ActionType actionType) {
            switch (actionType) {
                case ActionType.Press:
                    return POINTER_FLAGS.ContactDown;
                case ActionType.Move:
                    return POINTER_FLAGS.ContactMoves;
                case ActionType.Release:
                    return POINTER_FLAGS.UP;
                default:
                    return POINTER_FLAGS.NONE;
            }
        }

        void ConvertBigWaitTime(IList<IInputAction> injectList, int maxTime = 100) {
            for (var index1 = injectList.Count - 1; index1 >= 0; --index1)
                if (injectList[index: index1] is AbsoluteWaitInputAction inject) {
                    var duration = inject.duration;
                    if (duration > maxTime) {
                        injectList.RemoveAt(index: index1);
                        var num = duration / maxTime + 1;
                        for (var index2 = 0; index2 < num; ++index2) {
                            injectList.Insert(index: index1, item: Input.CreateWait(duration: Math.Min(val1: duration, val2: maxTime)));
                            duration -= maxTime;
                        }
                    }
                }
        }

        void Interpolate(ref int elapsedTime, IList<IInputAction> injectList, uint packetDelta) {
            for (var index1 = injectList.Count - 1; index1 >= 0; --index1)
                if (injectList[index: index1] is MultiPointerInputAction inject)
                    for (var index2 = 0; index2 < inject.pointerData.Length; ++index2)
                        if ((inject.pointerData[index2].flags & POINTER_FLAGS.ContactMoves) == POINTER_FLAGS.ContactMoves || (inject.pointerData[index2].flags & POINTER_FLAGS.ContactDown) == POINTER_FLAGS.ContactDown) {
                            int indexOfNext;
                            var multiPointerFrame = FindNextMultiPointerFrame(index: index1 + 1, injectList: injectList, indexOfNext: out indexOfNext);
                            if (multiPointerFrame != null) InterpolateFrame(injectList: injectList, packetDelta: packetDelta, index: indexOfNext, multiPointerInputAction: inject, nextPointerInputAction: multiPointerFrame);
                            break;
                        }

            ConvertBigWaitTime(injectList: injectList);
            for (var index = 0; index < injectList.Count; ++index)
                if (injectList[index: index] is AbsoluteWaitInputAction inject) {
                    injectList.RemoveAt(index: index);
                    var inputActionList = new List<IInputAction>();
                    Wait(elapsedTime: ref elapsedTime, duration: inject.duration, injectList: inputActionList);
                    injectList.Insert(index: index, item: inputActionList[index: 0]);
                }
        }

        MultiPointerInputAction FindNextMultiPointerFrame(
            int index,
            IList<IInputAction> injectList,
            out int indexOfNext) {
            for (var index1 = index; index1 < injectList.Count; ++index1)
                if (injectList[index: index1] is MultiPointerInputAction inject) {
                    indexOfNext = index1;
                    return inject;
                }

            indexOfNext = -1;
            return null;
        }

        void InterpolateFrame(
            IList<IInputAction> injectList,
            uint packetDelta,
            int index,
            MultiPointerInputAction multiPointerInputAction,
            MultiPointerInputAction nextPointerInputAction) {
            var num1 = Math.Ceiling(a: 50.0 / packetDelta);
            for (var index1 = 0; (double) index1 <= num1; ++index1) {
                var pointerDataList = new List<PointerData>();
                for (var index2 = 0; index2 < multiPointerInputAction.pointerData.Length; ++index2)
                    if ((multiPointerInputAction.pointerData[index2].flags & POINTER_FLAGS.ContactMoves) == POINTER_FLAGS.ContactMoves || (multiPointerInputAction.pointerData[index2].flags & POINTER_FLAGS.ContactDown) == POINTER_FLAGS.ContactDown) {
                        var pointerData1 = multiPointerInputAction.pointerData[index2];
                        var pointerData2 = nextPointerInputAction.pointerData[index2];
                        var num2 = pointerData2.location.X - pointerData1.location.X;
                        var num3 = pointerData2.location.Y - pointerData1.location.Y;
                        pointerData1.location.X += (int) Math.Round(a: num2 * index1 / num1);
                        pointerData1.location.Y += (int) Math.Round(a: num3 * index1 / num1);
                        pointerData1.flags = POINTER_FLAGS.ContactMoves | POINTER_FLAGS.PRIMARY | POINTER_FLAGS.CONFIDENCE;
                        pointerDataList.Add(item: pointerData1);
                    }

                injectList.Insert(index: index + index1 * 2, item: new MultiPointerInputAction {
                    pointerData = pointerDataList.ToArray()
                });
                injectList.Insert(index: index + index1 * 2 + 1, item: Input.CreateWait(duration: (int) packetDelta));
            }
        }

        void Press(PointI? location, IList<IInputAction> injectList, uint pointerId = 0) {
            var pointerInputAction = new PointerInputAction();
            pointerInputAction.pointerData.flags = POINTER_FLAGS.ContactDown | POINTER_FLAGS.PRIMARY | POINTER_FLAGS.CONFIDENCE;
            if (location.HasValue && location.HasValue)
                pointerInputAction.pointerData.location = location.Value;
            pointerInputAction.pointerData.pointerId = pointerId;
            injectList.Add(item: pointerInputAction);
        }

        void Wait(ref int elapsedTime, int duration, IList<IInputAction> injectList) {
            injectList.Add(item: new RelativeWaitInputAction {
                start = elapsedTime,
                duration = duration
            });
            elapsedTime += duration;
        }

        void Update(PointI location, IList<IInputAction> injectList, uint pointerId = 0) {
            injectList.Add(item: new PointerInputAction {
                pointerData = {
                    flags = POINTER_FLAGS.ContactMoves | POINTER_FLAGS.PRIMARY | POINTER_FLAGS.CONFIDENCE,
                    location = location,
                    pointerId = pointerId
                }
            });
        }

        PointerData[] CreateMultiPointerInputActions(
            IList<PointI> locations,
            POINTER_FLAGS flag,
            IList<uint> pointerIds = null) {
            var pointerDataArray = new PointerData[locations.Count];
            for (var index = 0; index < locations.Count; ++index) {
                pointerDataArray[index].flags = flag | POINTER_FLAGS.CONFIDENCE | POINTER_FLAGS.PRIMARY;
                pointerDataArray[index].location = locations[index: index];
                pointerDataArray[index].pointerId = pointerIds != null ? pointerIds[index: index] : (uint) index;
            }

            return pointerDataArray;
        }

        void MultiPress(IList<PointI> locations, IList<IInputAction> injectList) {
            var pointerInputAction = new MultiPointerInputAction {
                pointerData = CreateMultiPointerInputActions(locations: locations, flag: POINTER_FLAGS.ContactDown)
            };
            injectList.Add(item: pointerInputAction);
        }

        void MultiUpdate(
            IList<PointI> locations,
            IList<IInputAction> injectList,
            IList<uint> contactIds = null) {
            var pointerInputAction = new MultiPointerInputAction {
                pointerData = CreateMultiPointerInputActions(locations: locations, flag: POINTER_FLAGS.ContactMoves, pointerIds: contactIds)
            };
            injectList.Add(item: pointerInputAction);
        }

        void Release(PointI? location, IList<IInputAction> injectList, uint contactId = 0) {
            var pointerInputAction = new PointerInputAction();
            pointerInputAction.pointerData.flags = POINTER_FLAGS.PRIMARY | POINTER_FLAGS.CONFIDENCE | POINTER_FLAGS.UP;
            if (location.HasValue && location.HasValue)
                pointerInputAction.pointerData.location = location.Value;
            pointerInputAction.pointerData.pointerId = contactId;
            injectList.Add(item: pointerInputAction);
        }

        void MultiRelease(IList<PointI> locations, IList<IInputAction> injectList) {
            var pointerInputAction = new MultiPointerInputAction {
                pointerData = CreateMultiPointerInputActions(locations: locations, flag: POINTER_FLAGS.UP)
            };
            injectList.Add(item: pointerInputAction);
        }
    }
}