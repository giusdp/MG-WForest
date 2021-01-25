using System.Linq;
using Microsoft.Xna.Framework;
using WForest.Devices;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace WForest.UI.Interactions
{
    internal class InteractionUpdater
    {
        public IDevice Device { get; set; }
        public InteractionRunner Runner { get; set; }

        private WidgetIntHolder _interactionsHolder;
        private bool _wasPressed;

        internal InteractionUpdater(IDevice device, InteractionRunner runner)
        {
            Device = device;
            Runner = runner;
            _interactionsHolder = new WidgetIntHolder();
        }

        internal void Update(IWidget widget)
        {
            Device.Update(); // update device for next polling

            // check if mouse is still on the last hovered widget, in this case we can skip traversing the entire tree
            //todo

            var hovered = GetHoveredWidget(widget, Device.GetPointedLocation());
            RetrieveInteractionChanges(hovered);
            ProcessInteractionChanges();
        }

        private void ProcessInteractionChanges()
        {
            if (_interactionsHolder.PreviousHovered != null)
                Runner.ChangeState(_interactionsHolder.PreviousHovered,
                    _interactionsHolder.InteractionForPrevious);
            if (_interactionsHolder.CurrentHovered != null)
                Runner.ChangeState(_interactionsHolder.CurrentHovered,
                    _interactionsHolder.InteractionForCurrent);
        }

        private void RetrieveInteractionChanges(Maybe<IWidget> widget)
        {
            switch (widget)
            {
                case Maybe<IWidget>.Some m:
                    var w = m.Value;
                    HandleWidgetInteraction(w);
                    break;
                default:
                    HandleWidgetChange(null);
                    break;
            }
        }

        private void HandleWidgetInteraction(IWidget widget)
        {
            if (_interactionsHolder.CurrentHovered != widget) HandleWidgetChange(widget);
            CheckIfPressOnNewWidget();
        }

        private void CheckIfPressOnNewWidget()
        {
            if (Device.IsPressed() || Device.IsHeld())
            {
                if (_wasPressed) return;
                _interactionsHolder.InteractionForCurrent = Interaction.Pressed;
                _wasPressed = true;
            }
            else if (Device.IsReleased())
            {
                if (_wasPressed) _interactionsHolder.InteractionForCurrent = Interaction.Released;
                _wasPressed = false;
            }
        }


        private void HandleWidgetChange(IWidget? widget)
        {
            if (_interactionsHolder.CurrentHovered == widget) return;
            if (!ShouldChange()) return;

            _wasPressed = false;

            _interactionsHolder.PreviousHovered = _interactionsHolder.CurrentHovered;
            _interactionsHolder.InteractionForPrevious = Interaction.Exited;
            _interactionsHolder.CurrentHovered = widget;
            _interactionsHolder.InteractionForCurrent = Interaction.Entered;
        }

        private bool ShouldChange()
        {
            return _interactionsHolder.CurrentHovered == null
                   || _interactionsHolder.CurrentHovered.CurrentInteraction != Interaction.Pressed
                   || !(Device.IsPressed() || Device.IsHeld());
        }

        private struct WidgetIntHolder
        {
            public IWidget? PreviousHovered { get; set; }
            public Interaction InteractionForPrevious { get; set; }
            public IWidget? CurrentHovered { get; set; }
            public Interaction InteractionForCurrent { get; set; }
        }

        #region Static Methods

        internal static Maybe<IWidget> GetHoveredWidget(IWidget widget, Point mouseLoc)
        {
            var m = TreeVisitor.GetLowestNodeThatHolds(widget,
                w => w.Children.Reverse(),
                w => IsMouseInsideWidgetSpace(w.Space, mouseLoc));
            return m switch
            {
                Maybe<IWidget>.Some s => Maybe.Some(s.Value),
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