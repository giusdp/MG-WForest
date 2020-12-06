using Microsoft.Xna.Framework.Input;
using WForest.UI.Widgets;
using WForest.Utilities;

namespace WForest.UI.WidgetTree.Interactions
{
    public class InteractionStateHandler
    {
        private readonly InteractionState _state;

        public InteractionStateHandler()
        {
            _state = new InteractionState();
        }

        public void UpdateInteractionState(Maybe<Widget> hoveredWidget)
        {
            switch (hoveredWidget)
            {
                case Maybe<Widget>.Some m:
                    HandleWidgetInteraction(m.Value);
                    break;
                default:
                    _state.LastHovered?.StoppedHovering();
                    _state.Reset();
                    break;
            }
        }

        private void HandleWidgetInteraction(Widget widget)
        {
            if (_state.LastHovered != widget) ChangeWidget(widget);
            else HandleMouseInteraction(widget);
        }

        private void HandleMouseInteraction(Widget widget)
        {
            if (MouseJustPressed())
            {
                widget.PressedDown();
                _state.IsButtonBeingPressed = true;
            }
            else if (MouseJustReleased())
            {
                _state.IsButtonBeingPressed = false;

                ReleaseIfNotPressedBefore(widget);

                widget.StartedHovering();
            }
        }

        private void ReleaseIfNotPressedBefore(Widget widget)
        {
            if (_state.WasButtonAlreadyPressed) _state.WasButtonAlreadyPressed = false;
            else widget.ReleasedPress();
        }

        private bool MouseJustPressed() =>
            Mouse.GetState().LeftButton == ButtonState.Pressed && !_state.IsButtonBeingPressed;

        private bool MouseJustReleased() =>
            Mouse.GetState().LeftButton == ButtonState.Released && _state.IsButtonBeingPressed;

        private void ChangeWidget(Widget widget)
        {
            _state.LastHovered?.StoppedHovering();
            _state.LastHovered = widget;
            _state.LastHovered.StartedHovering();
            _state.WasButtonAlreadyPressed = _state.IsButtonBeingPressed;
            _state.IsButtonBeingPressed = false;
        }
    }
}