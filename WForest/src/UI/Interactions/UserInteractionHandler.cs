using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WForest.Devices;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace WForest.UI.Interactions
{
    public class UserInteractionHandler
    {
        public IDevice Device { get; set; }
        public InteractionUpdater Updater { get; set; }

        private WidgetIntHolder _holder;
        private bool _wasPressed;

        public UserInteractionHandler(IDevice device, InteractionUpdater updater)
        {
            Device = device;
            Updater = updater;
            _holder = new WidgetIntHolder
            {
                InteractionForPrevious = Interaction.Untouched,
                InteractionForCurrent = Interaction.Untouched
            };
        }

        public IEnumerable<ICommandProp> UpdateAndGenerateTransitions(IWidget widget)
        {
            Device.Update(); // Get new input 
            var pointedLocation = Device.GetPointedLocation();
            // check if mouse is still on the last hovered widget, in this case we can skip traversing the tree
            Maybe<IWidget> hovered = IsStillHoveringPreviousWidget(widget, pointedLocation)
                ? Maybe.Some(_holder.CurrentHovered!)
                : GetHoveredWidget(widget, pointedLocation);

            UpdateHolderWithInteractionChanges(hovered);

            return ProcessInteractionChanges();
        }

        private bool IsStillHoveringPreviousWidget(IWidget newWidget, Vector2 pointedLoc) =>
            _holder.CurrentHovered != null
            && IsMouseInsideWidgetSpace(_holder.CurrentHovered.Space, pointedLoc)
            && !IsMouseInsideWidgetSpace(newWidget.Space, pointedLoc);

        private void UpdateHolderWithInteractionChanges(Maybe<IWidget> widget)
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

        private IEnumerable<ICommandProp> ProcessInteractionChanges()
        {
            List<ICommandProp> l = new List<ICommandProp>();
            if (_holder.PreviousHovered != null)
                l.AddRange(Updater.NextState(_holder.PreviousHovered, _holder.InteractionForPrevious));

            if (_holder.CurrentHovered != null)
                l.AddRange(Updater.NextState(_holder.CurrentHovered, _holder.InteractionForCurrent));
            return l;
        }

        private void HandleWidgetInteraction(IWidget widget)
        {
            if (_holder.CurrentHovered != widget) HandleWidgetChange(widget);
            CheckIfPressOnNewWidget();
        }

        private void CheckIfPressOnNewWidget()
        {
            if (Device.IsPressed() || Device.IsHeld())
            {
                if (_wasPressed) return;
                _holder.InteractionForCurrent = Interaction.Pressed;
                _wasPressed = true;
            }
            else if (Device.IsReleased())
            {
                if (_wasPressed) _holder.InteractionForCurrent = Interaction.Released;
                _wasPressed = false;
            }
        }


        private void HandleWidgetChange(IWidget? widget)
        {
            if (_holder.CurrentHovered == widget) return;
            if (!ShouldChange()) return;

            _wasPressed = false;

            _holder.PreviousHovered = _holder.CurrentHovered;
            _holder.InteractionForPrevious = Interaction.Exited;
            _holder.CurrentHovered = widget;
            _holder.InteractionForCurrent = Interaction.Entered;
        }

        private bool ShouldChange()
        {
            return _holder.CurrentHovered == null
                   || _holder.CurrentHovered.CurrentInteraction != Interaction.Pressed
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

        public static Maybe<IWidget> GetHoveredWidget(IWidget widget, Vector2 mouseLoc)
        {
            var m = TreeVisitor.GetLowestNodeThatHolds(widget,
                w => w.Children,
                w => IsMouseInsideWidgetSpace(w.Space, mouseLoc));
            return m switch
            {
                Maybe<IWidget>.Some s => Maybe.Some(s.Value),
                _ => Maybe.None
            };
        }

        private static bool IsMouseInsideWidgetSpace(RectangleF space, Vector2 mouseLoc)
        {
            var x = mouseLoc.X;
            var y = mouseLoc.Y;
            var (widgetX, widgetY, widgetWidth, widgetHeight) = space;
            var isInsideHorizontally = x >= widgetX && x <= widgetX + widgetWidth - 1;
            var isInsideVertically = y >= widgetY && y <= widgetY + widgetHeight - 1;
            return isInsideHorizontally && isInsideVertically;
        }

        #endregion
    }
}