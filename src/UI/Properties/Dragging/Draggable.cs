using Microsoft.Xna.Framework;
using WForest.Devices;
using WForest.UI.Utils;

namespace WForest.UI.Properties.Dragging
{
    internal class DragCtx
    {
        public bool IsDragging { get; set; }
        public int DevX, DevY;

        public void Set(Point deviceLoc)
        {
            (DevX, DevY) = deviceLoc;
        }
    }

    /// <summary>
    /// Property to make a widget draggable using an IDevice (such as Mouse).
    /// </summary>
    public class Draggable : Property
    {
        /// <summary>
        /// This property should be applied after the widget tree is completely set up, so it has a priority of 4
        /// (after the last layout priorities that have priority of 3 such as ItemBase)
        /// </summary>
        public override int Priority { get; } = 4;

        private readonly IDevice _device;

        internal Draggable(IDevice device)
        {
            _device = device;
        }

        /// <summary>
        /// Adds an OnPress and OnRelease actions to the widget that handle the dragging logic.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
            var dragCtx = new DragCtx();

            var isXFixed = widgetNode.Properties.Find(p => p is FixX) != null;
            var isYFixed = widgetNode.Properties.Find(p => p is FixY) != null;

            widgetNode.Data.AddOnPressed(() =>
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
                    var (x, y, w, h) = widgetNode.Data.Space;
                    if (dragCtx.DevX == devX && dragCtx.DevY == devY) return;

                    var nx = x;
                    var ny = y;
                    nx += isXFixed ? 0 : devX - dragCtx.DevX;
                    ny += isYFixed ? 0 : devY - dragCtx.DevY;
                    (nx, ny) = CheckBounds(widgetNode, nx, ny, isXFixed, isYFixed);

                    WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(nx, ny, w, h));
                    dragCtx.Set(devLoc);
                }
            });

            widgetNode.Data.AddOnRelease(() => dragCtx.IsDragging = false);
            widgetNode.Data.AddOnExit(() => dragCtx.IsDragging = false);
        }

        private static (int, int) CheckBounds(WidgetTrees.WidgetTree wt, int x, int y, bool isXFixed, bool isYFixed)
        {
            var (_, _, w, h) = wt.Data.Space;
            if (wt.Parent == null) return (x, y);

            var pRight = wt.Parent.Data.Space.Right;
            var pBottom = wt.Parent.Data.Space.Bottom;

            if (!isXFixed)
            {
                if (x + w > pRight)
                    x = pRight - w;
                else if (x < wt.Parent.Data.Space.X) x = wt.Parent.Data.Space.X;
            }

            if (isYFixed) return (x, y);
            if (y + h > pBottom)
                y = pBottom - h;
            else if (y < wt.Parent.Data.Space.Y)
                y = wt.Parent.Data.Space.Y;

            return (x, y);
        }
    }
}