using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WForest.UI.Widgets;
using WForest.Utilities;
using WForest.Utilities.Collections;

namespace WForest.UI.WidgetTrees
{
    public class WidgetTreeVisitor
    {
        private readonly WidgetInteractionSetter _interactionSetter;

        public WidgetTreeVisitor()
        {
            _interactionSetter = new WidgetInteractionSetter();
        }

        public static void DrawTree(WidgetTree widgetTree, SpriteBatch spriteBatch)
        {
            void DrawWidgets(List<Tree<Widget>> widgets)
            {
                if (widgets.Count == 0) return;

                // var (rounded, nonRounded) = RoundedPartition(widgets);

                Enumerable.Reverse(widgets).ToList().ForEach(w => ((WidgetTree) w).DrawWidget(spriteBatch));

                // if (!rounded.Any()) return;
                //
                // spriteBatch.End();
                // spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                //     effect: ShaderDb.Rounded);
                //
                // foreach (var (w, r) in rounded.Select(w => (w, w.Properties.OfType<Rounded>().First())))
                // {
                //     r.ApplyParameters(w.Data.Space.Width, w.Data.Space.Height, r.Radius);
                //     w.DrawWidget(spriteBatch);
                // }
                //
                // spriteBatch.End();
                // spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            }

            TreeVisitor<Widget>.ApplyToTreeLevelByLevel(widgetTree, DrawWidgets);
        }

        public static void ApplyPropertiesOnTree(WidgetTree widgetTree)
        {
            TreeVisitor<Widget>.ApplyToTreeFromLeaves(widgetTree, w => ((WidgetTree) w).ApplyProperties());
        }

        public void UpdateTree(WidgetTree widgetTree)
        {
            var hoveredWidget = WidgetInteractionSetter.GetHoveredWidget(widgetTree, Mouse.GetState().Position);
            _interactionSetter.Update(hoveredWidget);

            foreach (var tree in widgetTree)
            {
                tree.Data.Update();
            }
        }

        // private static (List<WidgetTree>, List<WidgetTree>) RoundedPartition(List<Tree<Widget>> widgets)
        // {
        //     var roundedWidgets = new List<WidgetTree>();
        //     var nonRoundedWidgets = new List<WidgetTree>();
        //     foreach (var wt in widgets.Select(widget => (WidgetTree) widget))
        //     {
        //         if (wt.Properties.OfType<Rounded>().Any()) roundedWidgets.Add(wt);
        //         else nonRoundedWidgets.Add(wt);
        //     }
        //
        //     return (roundedWidgets, nonRoundedWidgets);
        // }
    }
}