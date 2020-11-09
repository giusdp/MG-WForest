using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities;

namespace PiBa.UI.WidgetTreeHandlers
{
    public class HoverChecker
    {
        public Maybe<WidgetTree> CheckHovering(WidgetTree widgetTree, Point mouseLoc)
        {
            var m = TreeVisitor<Widget>.GetLowestNodeThatHolds(widgetTree,
                w => IsMouseInsideWidgetSpace(w.Data.Space, mouseLoc));
            Maybe<WidgetTree> r = Maybe.None;
            m.Match(t =>
            {
                t.Data.OnStartedHoveringEvent();
                r = Maybe.Some((WidgetTree) t);
            }, () => { });
            return r;
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