using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiBa.UI.Widgets;
using PiBa.Utilities;

namespace PiBa.UI.WidgetTreeHandlers
{
    public class WidgetTreeVisitor
    {

        private readonly HoverChecker _hoverChecker;

        public WidgetTreeVisitor()
        {
            _hoverChecker = new HoverChecker();
        }
        
        public void DrawTree(WidgetTree widgetTree, SpriteBatch spriteBatch)
        {
            TreeVisitor<Widget>.ApplyToTree(widgetTree, w => ((WidgetTree) w).DrawWidget(spriteBatch));
        }

        public void EnforceConstraints(WidgetTree widgetTree)
        {
            TreeVisitor<Widget>.ApplyToTree(widgetTree, w => ((WidgetTree) w).EnforceConstraints());
        }

        public Maybe<WidgetTree> CheckHovering(WidgetTree widgetTree, Point mouseLoc)
            => _hoverChecker.CheckHovering(widgetTree, mouseLoc);

    }
}