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
                    var ddx = devX - dragCtx.DevX;
                    var ddy = devY - dragCtx.DevY;
                    var nx = x + (devX - dragCtx.DevX);
                    var ny = y + (devY - dragCtx.DevY);
                    WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(nx, ny, w, h));
                    dragCtx.Set(devLoc);
                }
            });
            
            widgetNode.Data.AddOnRelease(() => dragCtx.IsDragging = false);
            widgetNode.Data.AddOnExit(() => dragCtx.IsDragging = false);
        }
    }
}