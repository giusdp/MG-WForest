using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WForest.Devices;
using WForest.UI.Widgets;
using WForest.Utilities;
using WForest.Utilities.Collections;

namespace WForest.UI.WidgetTrees
{
    internal class WidgetTreeVisitor
    {
        private readonly WidgetInteractionUpdater _interactionUpdater;

        internal WidgetTreeVisitor()
        {
            _interactionUpdater = new WidgetInteractionUpdater(MouseDevice.Instance);
        }

        internal static void ApplyPropsOnTree(IWidget widgetTree)
        {
            if (widgetTree == null) throw new ArgumentNullException(nameof(widgetTree));
            
            foreach (var widget in widgetTree.Reverse())
            {
                widget.ApplyProps();
            }
        }

        internal static void DrawTree(WidgetTree widgetTree, SpriteBatch spriteBatch)
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

        internal static void ApplyPropertiesOnTree(WidgetTree widgetTree)
        {
            TreeVisitor<Widget>.ApplyToTreeFromLeaves(widgetTree, w => ((WidgetTree) w).ApplyProperties());
        }

        internal void UpdateTree(WidgetTree widgetTree)
        {
            var hoveredWidget = WidgetInteractionUpdater.GetHoveredWidget(widgetTree, Mouse.GetState().Position);
            _interactionUpdater.Update(hoveredWidget);

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