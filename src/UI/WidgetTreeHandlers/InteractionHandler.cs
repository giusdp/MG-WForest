using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.UI.WidgetTreeHandlers.Interactions;
using PiBa.Utilities;
using PiBa.Utilities.Collections;

namespace PiBa.UI.WidgetTreeHandlers
{
    public class InteractionHandler
    {
        private readonly InteractionStateMachine _interactionStateMachine;

        public InteractionHandler()
        {
            _interactionStateMachine = new InteractionStateMachine();
        }

        public Maybe<WidgetTree> CheckHovering(WidgetTree widgetTree, Point mouseLoc)
        {
            var m = TreeVisitor<Widget>.GetLowestNodeThatHolds(widgetTree,
                w => IsMouseInsideWidgetSpace(w.Data.Space, mouseLoc));
            switch (m)
            {
                case Maybe<Tree<Widget>>.Some s:
                    _interactionStateMachine.UpdateInteractionState(s.Value.Data); 
                    return Maybe.Some((WidgetTree) s.Value);
                default:
                    return Maybe.None;
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