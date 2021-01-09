using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WForest.UI.Widgets;
using WForest.UI.Widgets.Interactions;
using WForest.Utilities;
using WForest.Utilities.Collections;

namespace WForest.UI.WidgetTrees
{
    public class WidgetInteractionSetter
    {
        private Widget LastHovered { get; set; }
        private bool IsButtonPressed { get; set; }

        public void Update(Maybe<WidgetTree> hoveredWidget)
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
            if (MouseJustPressed())
            {
                widget.ChangeInteraction(Interaction.Pressed);
                IsButtonPressed = true;
            }
            else if (MouseJustReleased())
            {
                IsButtonPressed = false;
                if (LastHovered == widget) LastHovered.ChangeInteraction(Interaction.Released);
                else ChangeWidget(widget); 
            }
        }

        private bool MouseJustPressed() =>
            Mouse.GetState().LeftButton == ButtonState.Pressed && !IsButtonPressed;

        private bool MouseJustReleased() =>
            Mouse.GetState().LeftButton == ButtonState.Released && IsButtonPressed;

        private void ChangeWidgetIfNotPressed(Widget widget)
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

        private void ChangeWidget(Widget widget)
        {
            LastHovered?.ChangeInteraction(Interaction.Exited);
            LastHovered = widget;
            LastHovered?.ChangeInteraction(Interaction.Entered);
            IsButtonPressed = false;
        }

        #region Static Methods

        public static Maybe<WidgetTree> GetHoveredWidget(WidgetTree widgetTree, Point mouseLoc)
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
            // TODO if widget is rounded check hovering only on visible parts
            var (x, y) = mouseLoc;
            var (widgetX, widgetY, widgetWidth, widgetHeight) = space;
            var isInsideHorizontally = x >= widgetX && x <= widgetX + widgetWidth - 1;
            var isInsideVertically = y >= widgetY && y <= widgetY + widgetHeight - 1;
            return isInsideHorizontally && isInsideVertically;
        }

        #endregion
    }
}