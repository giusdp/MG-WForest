using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiBa.UI.Widgets;
using PiBa.Utilities;

namespace PiBa.UI.WidgetTreeHandlers
{
    public class WidgetTreeVisitor
    {

        private readonly InteractionHandler _interactionHandler;

        public WidgetTreeVisitor()
        {
            _interactionHandler = new InteractionHandler();
        }
        
        public void DrawTree(WidgetTree widgetTree, SpriteBatch spriteBatch)
        {
            TreeVisitor<Widget>.ApplyToTree(widgetTree, w => ((WidgetTree) w).DrawWidget(spriteBatch));
        }

        public void ApplyProperties(WidgetTree widgetTree)
        {
            TreeVisitor<Widget>.ApplyToTree(widgetTree, w => ((WidgetTree) w).ApplyProperties());
        }

        public Maybe<WidgetTree> CheckHovering(WidgetTree widgetTree, Point mouseLoc)
            => _interactionHandler.CheckHovering(widgetTree, mouseLoc);

    }
}