using Microsoft.Xna.Framework;
using WForest.Devices;
using WForest.UI.Widgets;
using WForest.UI.Widgets.Interactions;
using WForest.Utilities;
using WForest.Utilities.Collections;

namespace WForest.UI.WidgetTrees
{
    internal class InteractionUpdater
    {
        internal IDevice Device;
        private IWidget? Hovered { get; set; }
        private bool _wasPressed;

        internal InteractionUpdater(IDevice device)
        {
            Device = device;
        }

        internal void Update(Maybe<IWidget> hoveredWidget)
        {
            Device.Update();
            switch (hoveredWidget)
            {
                case Maybe<IWidget>.Some m:
                    HandleWidgetInteraction(m.Value);
                    break;
                default:
                    UpdateHovered(null);
                    break;
            }
        }

        private void HandleWidgetInteraction(IWidget widget)
        {
            if (Hovered != widget) UpdateHovered(widget);
            if (Hovered != null) HandleInteraction();
        }

        private void HandleInteraction()
        {
            if (Device.IsPressed() || Device.IsHeld())
            {
                if (_wasPressed) return;
                // Hovered?.ChangeInteraction(Interaction.Pressed);
                _wasPressed = true;
            }
            else if (Device.IsReleased())
            {
                // if (_wasPressed) Hovered?.ChangeInteraction(Interaction.Released);
                // else UpdateHovered(Hovered);
                _wasPressed = false;
            }
        }

        private void UpdateHovered(IWidget? widget)
        {
            // if (Hovered != null && Hovered != widget &&
            //     Hovered.CurrentInteraction() == Interaction.Pressed && (Device.IsPressed()||Device.IsHeld())) return;
            //
            // _wasPressed = false;
            // Hovered?.ChangeInteraction(Interaction.Exited);
            // Hovered = widget;
            // Hovered?.ChangeInteraction(Interaction.Entered);
        }

        #region Static Methods

        internal static Maybe<IWidget> GetHoveredWidget(IWidget widget, Point mouseLoc)
        {
            // var m = TreeVisitor.GetLowestNodeThatHolds(widget,
            //     w => IsMouseInsideWidgetSpace(w.Data.Space, mouseLoc));
            // return m switch
            // {
            //     Maybe<Tree<Widget>>.Some s => Maybe.Some((IWidget) s.Value),
            //     _ => Maybe.None
            // };
            return Maybe.None;
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