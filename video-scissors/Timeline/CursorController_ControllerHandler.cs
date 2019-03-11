using Scissors.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Timeline
{
    partial class CursorController
    {        
        class ControllerHandler
        {
            private CursorController cursor;

            bool doingAction;
            CursorState state;

            int oldStart;
            int oldLength;
            float offset;
            bool createdTempLayer;            
            private ItemController targetItem;
            private LayerController targetLayer;
            private SliceController targetSlice;
            private LayerController originalLayer;
            private SliceController originalSlice;
            private LayerController tempLayer;

            private IController actionController;

            public IController ActionController { get { return actionController; } }

            public ControllerHandler(CursorController owner)
            {
                cursor = owner;
                doingAction = false;
            }

            private void CheckActionState(bool expectedState)
            {
                if (expectedState)
                {
                    if (!doingAction) throw new Exception("Action not started!");
                }
                else
                {
                    if (doingAction) throw new Exception("Action already started!");
                }
            }

            /// <summary> Ignores CursorController. </summary>
            public IController GetTargettedController(Point mouseLocation)
            {
                if (!cursor.rectangleProvider.ContainerRectangle.Contains(mouseLocation)) return null;

                List<IController> timelineChildren = cursor.timeline.GetChildren();
                foreach (IController timelineChild in timelineChildren)
                {
                    if (timelineChild.FullOccupiedRegion.IsVisible(mouseLocation))
                    {
                        if (timelineChild is SliceController)
                        {
                            List<IController> sliceChildren = (timelineChild as SliceController).GetChildren();
                            foreach (IController sliceChild in sliceChildren)
                            {
                                if (sliceChild.FullOccupiedRegion.IsVisible(mouseLocation))
                                {
                                    if (sliceChild is LayerController)
                                    {
                                        List<IController> layerChildren = (sliceChild as LayerController).GetChildren();
                                        foreach (IController layerChild in layerChildren)
                                        {
                                            if (layerChild.FullOccupiedRegion.IsVisible(mouseLocation))
                                            {
                                                if (layerChild is ItemController) return layerChild;
                                            }
                                        }
                                        return sliceChild;
                                    }
                                }
                            }
                            return timelineChild;
                        }
                        else if (timelineChild is RulerController)
                        {
                            return timelineChild;
                        }
                    }
                }
                return cursor.timeline;
            }

            public CursorState GetCursorState(Point mouseLocation)
            {
                IController targettedController = GetTargettedController(mouseLocation);
                return GetCursorState(mouseLocation, targettedController);
            }

            public CursorState GetCursorState(Point mouseLocation, IController targettedController)
            {
                if (targettedController is IDraggableController dc && dc.MoveHandleRectangle.Contains(mouseLocation))
                {
                    if (targettedController is ItemController) return CursorState.MoveItem;
                    if (targettedController is LayerController) return CursorState.MoveLayer;
                    else if (targettedController is SliceController) return CursorState.MoveSlice;
                    else return CursorState.Hover;
                }
                else if (targettedController is IResizableController rc1 && rc1.LeftResizeHandleRectangle.Contains(mouseLocation))
                {
                    if (targettedController is ItemController) return CursorState.ResizeItemLeft;
                    else return CursorState.Hover;
                }
                else if (targettedController is IResizableController rc2 && rc2.RightResizeHandleRectangle.Contains(mouseLocation))
                {
                    if (targettedController is ItemController) return CursorState.ResizeItemRight;
                    else return CursorState.Hover;
                }
                else return CursorState.Hover;
            }

            public void BeginControllerAction(Point mouseLocation, IController controller, CursorState cursorState)
            {
                CheckActionState(false);
                doingAction = true;

                state = cursorState;

                if (controller is ItemController item)
                {
                    targetItem = item;
                    oldStart = targetItem.StartPosition;
                    oldLength = targetItem.ItemLength;
                    offset = targetItem.StartPosition - mouseLocation.X / cursor.timeline.TimelineZoom;

                    if (cursorState == CursorState.MoveItem)
                    {
                        originalLayer = item.ParentLayer;
                        targetLayer = originalLayer;
                    }

                    actionController = targetItem;
                }
                else if (controller is LayerController layer)
                {
                    targetLayer = layer;
                    originalSlice = targetLayer.ParentSlice;
                    targetSlice = originalSlice;
                    createdTempLayer = false;

                    actionController = layer;
                }
                else if (controller is SliceController slice)
                {
                    targetSlice = slice;
                }
            }

            public void UpdateControllerAction(Point mouseLocation, IController targettedController)
            {
                CheckActionState(true);

                if (state == CursorState.MoveLayer)
                {
                    if (targettedController == targetLayer) return;

                    LayerController layer;
                    if (targettedController is LayerController) layer = targettedController as LayerController;
                    else if (targettedController is ItemController) layer = (targettedController as ItemController).ParentLayer;
                    else return;

                    if (layer.ParentSlice == targetLayer.ParentSlice)
                    {
                        layer.ParentSlice.SwapLayers(layer, targetLayer);
                    }
                    else
                    {
                        SliceController slice = layer.ParentSlice;
                        targetSlice.TransferLayer(targetLayer, slice, layer.GetId());

                        if (targetSlice == originalSlice)
                        {
                            if (targetSlice.LayerCount == 0)
                            {
                                tempLayer = targetSlice.CreateLayer();
                                createdTempLayer = true;
                            }
                        }
                        else if (slice == originalSlice)
                        {
                            if (createdTempLayer)
                            {
                                slice.DeleteLayer(tempLayer);
                                createdTempLayer = false;
                            }
                        }

                        targetSlice = slice;
                    }
                }
                else if (state == CursorState.MoveSlice)
                {
                    if (targettedController == targetSlice) return;

                    SliceController slice;
                    if (targettedController is SliceController) slice = targettedController as SliceController;
                    else
                    {
                        LayerController layer;
                        if (targettedController is LayerController) layer = targettedController as LayerController;
                        else if (targettedController is ItemController) layer = (targettedController as ItemController).ParentLayer;
                        else return;
                        slice = layer.ParentSlice;
                    }

                    cursor.timeline.SwapSlices(targetSlice, slice);
                }
                else
                {
                    int start = (int)Math.Round(mouseLocation.X / cursor.timeline.TimelineZoom + offset);

                    if (state == CursorState.MoveItem)
                    {
                        targetItem.StartPosition = start;

                        if (targettedController != targetItem)
                        {
                            LayerController layer = null;
                            if (targettedController is LayerController) layer = targettedController as LayerController;
                            else if (targettedController is ItemController) layer = (targettedController as ItemController).ParentLayer;

                            if (layer != null && layer != targetLayer)
                            {
                                targetLayer.TransferItem(targetItem, layer);
                                targetLayer = targetItem.ParentLayer;
                            }
                        }
                    }
                    else
                    {
                        int diff = start - oldStart;
                        if (state == CursorState.ResizeItemLeft)
                        {
                            int length = targetItem.ItemLength;
                            targetItem.ItemLength = oldLength - diff;
                            if (targetItem.ItemLength != length)
                            {
                                targetItem.StartPosition = start;
                            }
                        }
                        else if (state == CursorState.ResizeItemRight)
                        {
                            targetItem.ItemLength = oldLength + diff;
                        }
                    }
                }
            }
            
            public void EndControllerAction()
            {
                CheckActionState(true);
                doingAction = false;

                if (state == CursorState.MoveItem || state == CursorState.ResizeItemLeft || state == CursorState.ResizeItemRight)
                {
                    if (!targetItem.ParentLayer.IsPositionOkay(targetItem))
                    {
                        if (targetLayer != originalLayer)
                        {
                            targetLayer.TransferItem(targetItem, originalLayer);
                        }

                        targetItem.StartPosition = oldStart;
                        targetItem.ItemLength = oldLength;
                    }
                }

                state = CursorState.Hover;
                actionController = null;
            }
        }
    }
}
