using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PiBa.UI.Widgets;
using PiBa.Utilities;
using PiBa.Utilities.Collections;

namespace PiBa.UI.WidgetTreeHandlers
{
    public class HoverChecker
    {
        private Widget _lastHoveredOn;
        private bool _isClicked;

        public Maybe<WidgetTree> CheckHovering(WidgetTree widgetTree, Point mouseLoc)
        {
            var m = TreeVisitor<Widget>.GetLowestNodeThatHolds(widgetTree,
                w => IsMouseInsideWidgetSpace(w.Data.Space, mouseLoc));
            switch (m)
            {
                case Maybe<Tree<Widget>>.Some s:
                    HandleHoveringOnWidget(s.Value.Data);
                    HandlePressOnHoveredWidget();
                    return Maybe.Some((WidgetTree) s.Value);
                default:
                    _lastHoveredOn?.StoppedHovering();
                    _lastHoveredOn = null; 
                    return Maybe.None;
            }
        }

        private void HandleHoveringOnWidget(Widget widget)
        {
            if (_lastHoveredOn == widget) return;
            _isClicked = false;
            _lastHoveredOn?.StoppedHovering();
            _lastHoveredOn = widget;
            _lastHoveredOn.StartedHovering();
        }

        private void HandlePressOnHoveredWidget()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !_isClicked)
            {
                _lastHoveredOn?.PressedDown();
                _isClicked = true;
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released && _isClicked)
            {
                _isClicked = false;
                _lastHoveredOn?.ReleasedPress();
                _lastHoveredOn?.StartedHovering();
            }
        }

        private bool IsMouseInsideWidgetSpace(Rectangle space, Point mouseLoc)
        {
            var (x, y) = mouseLoc;
            var isInsideHorizontally = x >= space.X && x <= space.X + space.Width - 1;
            var isInsideVertically = y >= space.Y && y <= space.Y + space.Height - 1;
            return isInsideHorizontally && isInsideVertically;
        }
    }
}