using System;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.Devices;
using WForest.Props.Interfaces;
using WForest.Props.Props.Actions;
using WForest.Utilities;
using WForest.Utilities.WidgetUtils;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Props.Dragging
{
    internal class DragCtx
    {
        public bool IsDragging { get; set; }
        public float DevX, DevY;

        public void Set(Vector2 deviceLoc)
        {
            DevX = deviceLoc.X;
            DevY = deviceLoc.Y;
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
        public bool IsApplied { get; set; }

        private readonly IDevice _device;
        private OnUpdate? _onUpdateDrag;

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
            IsApplied = false;
            var dragCtx = new DragCtx();

            var foundFixX = widget.Props.SafeGet<FixX>().TryGetValue(out var fixX);
            var foundFixY = widget.Props.SafeGet<FixY>().TryGetValue(out var fixY);

            bool isXFixed = foundFixX && fixX.Any();
            bool isYFixed = foundFixY && fixY.Any();
            OnPress onPress = new OnPress(() => StartDrag(widget, isXFixed, isYFixed, dragCtx));
            OnRelease onRelease = new OnRelease(() => StopDrag(widget, dragCtx));
            OnExit onExit = new OnExit(() => StopDrag(widget, dragCtx));

            widget.WithProp(onPress);
            widget.WithProp(onRelease);
            widget.WithProp(onExit);
            
            _onUpdateDrag = new OnUpdate(() => Dragging(widget, isXFixed, isYFixed, dragCtx));
            
            IsApplied = true;
            OnApplied();
        }

        private void Dragging(IWidget widget, bool isXFixed, bool isYFixed, DragCtx dragCtx)
        {
            var devLoc = _device.GetPointedLocation();
            var devX = devLoc.X;
            var devY = devLoc.Y;
            var (x, y, w, h) = widget.Space;
            if (Math.Abs(dragCtx.DevX - devX) < 0.01f && Math.Abs(dragCtx.DevY - devY) < 0.01f) return;

            var nx = x + (isXFixed ? 0 : devX - dragCtx.DevX);
            var ny = y + (isYFixed ? 0 : devY - dragCtx.DevY);
            (nx, ny) = CheckBounds(widget, nx, ny, isXFixed, isYFixed);

            WidgetSpaceHelper.UpdateSpace(widget, new RectangleF(nx, ny, w, h));
            dragCtx.Set(devLoc);
        }

        private void StartDrag(IPropHolder widget, bool isXFixed, bool isYFixed, DragCtx dragCtx)
        {
            if (isXFixed && isYFixed) return;

            var devLoc = _device.GetPointedLocation();
            if (dragCtx.IsDragging) return;
            dragCtx.IsDragging = true;
            dragCtx.Set(devLoc);
            widget.WithProp(_onUpdateDrag!);
        }

        private void StopDrag(IPropHolder widget, DragCtx ctx)
        {
            if (!ctx.IsDragging) return;
            ctx.IsDragging = false;
            widget.Props.Remove(_onUpdateDrag!);
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