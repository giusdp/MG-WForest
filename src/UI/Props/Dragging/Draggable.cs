using System;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.Devices;
using WForest.UI.Props.Actions;
using WForest.UI.Props.Interfaces;
using WForest.UI.Utils;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace WForest.UI.Props.Dragging
{
    internal class DragCtx
    {
        public bool IsDragging { get; set; }
        public float DevX, DevY;

        public void Set(Vector2 deviceLoc)
        {
            (DevX, DevY) = deviceLoc;
        }
    }

    /// <summary>
    /// Property to make a widget draggable using an IDevice (such as Mouse).
    /// </summary>
    public class Draggable : IApplicableProp
    {
        /// <summary>
        /// This property should be applied after the widget tree is completely set up, so it has a priority of 4
        /// (after the last layout priorities that have priority of 3 such as ItemBase)
        /// </summary>
        public int Priority { get; set; } = 4;

        /// <inheritdoc/>
        public event EventHandler? Applied;

        /// <inheritdoc/>
        public bool ApplicationDone { get; set; }

        private readonly IDevice _device;

        public Draggable(IDevice device)
        {
            _device = device;
        }

        /// <summary>
        /// Adds an OnPress and OnRelease actions to the widget that handle the dragging logic.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            ApplicationDone = false;
            var dragCtx = new DragCtx();

            widget.Props.SafeGetByProp<FixX>().TryGetValue(out var fixX);
            widget.Props.SafeGetByProp<FixY>().TryGetValue(out var fixY);

            OnPress onPress = new OnPress(() => DragAction(widget, fixX.Any(), fixY.Any(), dragCtx));
            OnRelease onRelease = new OnRelease(() => dragCtx.IsDragging = false);
            OnExit onExit = new OnExit(() => dragCtx.IsDragging = false);

            widget.Props.AddProp(onPress);
            widget.Props.AddProp(onRelease);
            widget.Props.AddProp(onExit);
            ApplicationDone = true;
            OnApplied();
        }

        private void DragAction(IWidget widget, bool isXFixed, bool isYFixed, DragCtx dragCtx)
        {
            if (isXFixed && isYFixed) return;

            var devLoc = _device.GetPointedLocation();
            if (!dragCtx.IsDragging)
            {
                dragCtx.IsDragging = true;
                dragCtx.Set(devLoc);
            }
            else
            {
                var (devX, devY) = devLoc;
                var (x, y, w, h) = widget.Space;
                if (Math.Abs(dragCtx.DevX - devX) < 0.01f && Math.Abs(dragCtx.DevY - devY) < 0.01f) return;

                var nx = x;
                var ny = y;
                nx += isXFixed ? 0 : devX - dragCtx.DevX;
                ny += isYFixed ? 0 : devY - dragCtx.DevY;
                (nx, ny) = CheckBounds(widget, nx, ny, isXFixed, isYFixed);

                WidgetsSpaceHelper.UpdateSpace(widget, new RectangleF(nx, ny, w, h));
                dragCtx.Set(devLoc);
            }
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);

        private static (float, float) CheckBounds(IWidget wt, float x, float y, bool isXFixed, bool isYFixed)
        {
            var (_, _, w, h) = wt.Space;
            if (wt.Parent == null) return (x, y);

            var pRight = wt.Parent.Space.Right;
            var pBottom = wt.Parent.Space.Bottom;

            if (!isXFixed)
            {
                if (x + w > pRight)
                    x = pRight - w;
                else if (x < wt.Parent.Space.X) x = wt.Parent.Space.X;
            }

            if (isYFixed) return (x, y);
            if (y + h > pBottom)
                y = pBottom - h;
            else if (y < wt.Parent.Space.Y)
                y = wt.Parent.Space.Y;

            return (x, y);
        }
    }
}