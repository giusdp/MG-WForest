using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Widgets;
using WForest.Utilities;

namespace WForest.UI.WidgetTreeHandlers
{
    public class WidgetTreeVisitor
    {

        private readonly InteractionHelper _interactionHelper;

        public WidgetTreeVisitor()
        {
            _interactionHelper = new InteractionHelper();
        }
        
        public static void DrawTree(WidgetTree widgetTree, SpriteBatch spriteBatch)
        {
            TreeVisitor<Widget>.ApplyToTreeLevelByLevel(widgetTree, w => ((WidgetTree) w).DrawWidget(spriteBatch));
        }

        public void ApplyPropertiesOnTree(WidgetTree widgetTree)
        {
            TreeVisitor<Widget>.ApplyToTreeFromLeaves(widgetTree, w => ((WidgetTree) w).ApplyProperties());
        }

        public Maybe<WidgetTree> CheckHovering(WidgetTree widgetTree, Point mouseLoc)
            => _interactionHelper.CheckHovering(widgetTree, mouseLoc);

    }
}