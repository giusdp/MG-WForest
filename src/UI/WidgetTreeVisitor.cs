using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using PiBa.UI.Widgets;
using PiBa.Utilities;
using PiBa.Utilities.Collections;

namespace PiBa.UI
{
    public static class WidgetTreeVisitor
    {
        public static void DrawTree(WidgetTree widgetTree, SpriteBatch spriteBatch)
        {
            TreeVisitor<Widget>.ApplyToTree(widgetTree, w => ((WidgetTree) w).DrawWidget(spriteBatch));
        }

        public static void EnforceConstraints(WidgetTree widgetTree)
        {
            TreeVisitor<Widget>.ApplyToTree(widgetTree, w => ((WidgetTree) w).EnforceConstraints());
        }
    }
}