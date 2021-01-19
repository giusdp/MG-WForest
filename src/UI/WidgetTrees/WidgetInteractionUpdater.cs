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
        private Widget? Hovered { get; set; }
        private bool _wasPressed;

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
                    UpdateHovered(null);
                    break;
            }
        }

        private void HandleWidgetInteraction(Widget widget)
        {
            if (Hovered != widget) UpdateHovered(widget);
            if (Hovered != null) HandleInteraction();
        }

        private void HandleInteraction()
        {
            if (Device.IsPressed() || Device.IsHeld())
            {
                if (_wasPressed) return;
                Hovered?.ChangeInteraction(Interaction.Pressed);
                _wasPressed = true;
            }
            else if (Device.IsReleased())
            {
                if (_wasPressed) Hovered?.ChangeInteraction(Interaction.Released);
                else UpdateHovered(Hovered);
                _wasPressed = false;
            }
        }

        private void UpdateHovered(Widget? widget)
        {
            if (Hovered != null && Hovered != widget &&
                Hovered.CurrentInteraction() == Interaction.Pressed && (Device.IsPressed()||Device.IsHeld())) return;

            _wasPressed = false;
            Hovered?.ChangeInteraction(Interaction.Exited);
            Hovered = widget;
            Hovered?.ChangeInteraction(Interaction.Entered);
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