using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiBa.UI.Widgets;
using PiBa.Utilities;

namespace PiBa.UI
{
    public class WidgetTreeVisitor
    {
        public void DrawTree(WidgetTree widgetTree, SpriteBatch spriteBatch)
        {
            TreeVisitor<Widget>.ApplyToTree(widgetTree, w => ((WidgetTree) w).DrawWidget(spriteBatch));
        }

        public void EnforceConstraints(WidgetTree widgetTree)
        {
            TreeVisitor<Widget>.ApplyToTree(widgetTree, w => ((WidgetTree) w).EnforceConstraints());
        }

        public Maybe<WidgetTree> CheckHovering(WidgetTree widgetTree, Point mouseLoc)
        {
          var m = TreeVisitor<Widget>.GetLowestNodeThatHolds(widgetTree, w => ((WidgetTree) w).isHovered(mouseLoc));
          Maybe<WidgetTree> r = Maybe.None; 
          m.Match(t => r = Maybe.Some((WidgetTree) t), () => {});
          return r;
        }

    }
}