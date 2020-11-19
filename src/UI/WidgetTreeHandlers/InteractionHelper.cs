using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.UI.WidgetTreeHandlers.Interactions;
using PiBa.Utilities;
using PiBa.Utilities.Collections;

namespace PiBa.UI.WidgetTreeHandlers
{
    public class InteractionHelper
    {
        private readonly InteractionStateHandler _interactionStateHandler;

        public InteractionHelper()
        {
            _interactionStateHandler = new InteractionStateHandler();
        }

        public Maybe<WidgetTree> CheckHovering(WidgetTree widgetTree, Point mouseLoc)
        {
            var m = TreeVisitor<Widget>.GetLowestNodeThatHolds(widgetTree,
                w => IsMouseInsideWidgetSpace(w.Data.Space, mouseLoc));
            switch (m)
            {
                case Maybe<Tree<Widget>>.Some s:
                    _interactionStateHandler.UpdateInteractionState(s.Value.Data); 
                    return Maybe.Some((WidgetTree) s.Value);
                default:
                    return Maybe.None;
            }
        }

        private static bool IsMouseInsideWidgetSpace(Rectangle space, Point mouseLoc)
        {
            var (x, y) = mouseLoc;
            var (widgetX, widgetY, widgetWidth, widgetHeight) = space;
            var isInsideHorizontally = x >= widgetX && x <= widgetX + widgetWidth - 1;
            var isInsideVertically = y >= widgetY && y <= widgetY + widgetHeight - 1;
            return isInsideHorizontally && isInsideVertically;
        }
    }

}