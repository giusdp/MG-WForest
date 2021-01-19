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
        internal IDevice _device;
        private Widget? LastHovered { get; set; }

        internal WidgetInteractionUpdater(IDevice device)
        {
            _device = device;
        }

        internal void Update(Maybe<WidgetTree> hoveredWidget)
        {
            switch (hoveredWidget)
            {
                case Maybe<WidgetTree>.Some m:
                    HandleWidgetInteraction(m.Value.Data);
                    break;
                default:
                    ChangeWidgetIfNotPressed(null);
                    break;
            }
        }

        private void HandleWidgetInteraction(Widget widget)
        {
            if (LastHovered != widget) ChangeWidgetIfNotPressed(widget);
            HandleMouseInteraction(widget);
        }

        private void HandleMouseInteraction(Widget widget)
        {
            if (_device.IsPressed()) widget.ChangeInteraction(Interaction.Pressed);
            else if (_device.IsReleased())
            {
                if (LastHovered == widget) LastHovered.ChangeInteraction(Interaction.Released);
                else ChangeWidget(widget);
            }
        }

        private void ChangeWidgetIfNotPressed(Widget? widget)
        {
            if (widget == null)
            {
                ChangeWidget(null);
                return;
            }

            if (LastHovered != null && LastHovered.CurrentInteraction() == Interaction.Pressed)
                return;

            ChangeWidget(widget);
        }

        private void ChangeWidget(Widget? widget)
        {
            LastHovered?.ChangeInteraction(Interaction.Exited);
            LastHovered = widget;
            LastHovered?.ChangeInteraction(Interaction.Entered);
            _device.Reset();
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