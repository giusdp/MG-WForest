using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Properties.Shaders;
using WForest.UI.Widgets;
using WForest.UI.WidgetTree.Interactions;
using WForest.Utilities;
using WForest.Utilities.Collections;

namespace WForest.UI.WidgetTree
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
                w =>
                {
                    var rs = ((WidgetTree) w).Properties.OfType<Rounded>()
                        .ToList(); // TODO update this part after implementing ad hoc props collection
                    var radius = rs.Any() ? rs.First().Radius : 0;
                    return IsMouseInsideWidgetSpace(w.Data.Space, mouseLoc, radius);
                });
            switch (m)
            {
                case Maybe<Tree<Widget>>.Some s:
                    _interactionStateHandler.UpdateInteractionState(s.Value.Data);
                    return Maybe.Some((WidgetTree) s.Value);
                default:
                    return Maybe.None;
            }
        }

        private static bool IsMouseInsideWidgetSpace(Rectangle space, Point mouseLoc, int radius)
        {
            // TODO if widget is rounded check hovering only on visible parts
            var (x, y) = mouseLoc;
            var (widgetX, widgetY, widgetWidth, widgetHeight) = space;
            var isInsideHorizontally = x >= widgetX && x <= widgetX + widgetWidth - 1;
            var isInsideVertically = y >= widgetY && y <= widgetY + widgetHeight - 1;
            return isInsideHorizontally && isInsideVertically;
        }
    }
}