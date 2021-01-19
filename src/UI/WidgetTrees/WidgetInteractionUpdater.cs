using Microsoft.Xna.Framework;
using WForest.Devices;
using WForest.UI.Widgets;
using WForest.UI.Widgets.Interactions;
using WForest.Utilities;
using WForest.Utilities.Collections;

namespace WForest.UI.WidgetTrees
{
    internal class WidgetInteractionUpdater
    {
        internal IDevice Device;
        private Widget? LastHovered { get; set; }

        internal WidgetInteractionUpdater(IDevice device)
        {
            Device = device;
        }

        internal void Update(Maybe<WidgetTree> hoveredWidget)
        {
            Device.Update();
            switch (hoveredWidget)
            {
                case Maybe<WidgetTree>.Some m:
                    HandleWidgetInteraction(m.Value.Data);
                    break;
                default:
                    ChangeWidget(null);
                    break;
            }
        }

        private void HandleWidgetInteraction(Widget widget)
        {
            if (LastHovered != widget) ChangeWidget(widget);
            HandleInteraction(widget);
        }

        private void HandleInteraction(Widget widget)
        {
            if (Device.IsPressed() && widget.CurrentInteraction() != Interaction.Pressed) 
                widget.ChangeInteraction(Interaction.Pressed);
            else if (Device.IsReleased())
            {
                if (LastHovered == widget) LastHovered.ChangeInteraction(Interaction.Released);
                else ChangeWidget(widget);
            }
        }

        private void ChangeWidget(Widget? widget)
        {
            LastHovered?.ChangeInteraction(Interaction.Exited);
            LastHovered = widget;
            LastHovered?.ChangeInteraction(Interaction.Entered);
        }

        #region Static Methods

        internal static Maybe<WidgetTree> GetHoveredWidget(WidgetTree widgetTree, Point mouseLoc)
        {
            var m = TreeVisitor<Widget>.GetLowestNodeThatHolds(widgetTree,
                w => IsMouseInsideWidgetSpace(w.Data.Space, mouseLoc));
            return m switch
            {
                Maybe<Tree<Widget>>.Some s => Maybe.Some((WidgetTree) s.Value),
                _ => Maybe.None
            };
        }

        private static bool IsMouseInsideWidgetSpace(Rectangle space, Point mouseLoc)
        {
            var (x, y) = mouseLoc;
            var (widgetX, widgetY, widgetWidth, widgetHeight) = space;
            var isInsideHorizontally = x >= widgetX && x <= widgetX + widgetWidth - 1;
            var isInsideVertically = y >= widgetY && y <= widgetY + widgetHeight - 1;
            return isInsideHorizontally && isInsideVertically;
        }

        #endregion
    }
}