using Microsoft.Xna.Framework;
using Serilog;
using WForest.Devices;
using WForest.UI.Utils;

namespace WForest.UI.Properties
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

    public class Draggable : Property
    {
        internal override int Priority { get; } = 4;

        private readonly IDevice _device;

        public Draggable(IDevice device)
        {
            _device = device;
        }

        internal override void ApplyOn(WidgetTree.WidgetTree widgetNode)
        {
            var dragCtx = new DragCtx();
            widgetNode.Data.AddOnPressed(() =>
            {
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
                    var nx = x + (devX - dragCtx.DevX);
                    var ny = y + (devY - dragCtx.DevY);

                    if (widgetNode.Parent != null)
                    {
                        if (nx + w > widgetNode.Parent.Data.Space.X + widgetNode.Parent.Data.Space.Width)
                            nx = widgetNode.Parent.Data.Space.Width - w;
                        else if (nx < widgetNode.Parent.Data.Space.X) nx = widgetNode.Parent.Data.Space.X;

                        if (ny + h > widgetNode.Parent.Data.Space.Y + widgetNode.Parent.Data.Space.Height)
                            ny = widgetNode.Parent.Data.Space.Height - h;
                        else if (ny < widgetNode.Parent.Data.Space.Y)
                            ny = widgetNode.Parent.Data.Space.Y;
                    }

                    WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(nx, ny, w, h));
                    dragCtx.Set(devLoc);
                }
            });

            widgetNode.Data.AddOnRelease(() => dragCtx.IsDragging = false);
            widgetNode.Data.AddOnExit(() => dragCtx.IsDragging = false);
        }
    }
}