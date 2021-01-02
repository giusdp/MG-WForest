using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using WForest.UI.WidgetTree;
using WForest.Utilities.Collections;

namespace WForest.UI
{
    public class WTreeManager
    {
        private readonly WidgetTree.WidgetTree _root;
        private readonly WidgetTreeVisitor _widgetTreeVisitor;

        internal WTreeManager(int x, int y, int width, int height, WidgetTree.WidgetTree wTree)
        {
            Log.Information($"Created new WTree with {x},{y} coordinates and {width},{height} size.");
            _widgetTreeVisitor = new WidgetTreeVisitor();

            _root = new WidgetTree.WidgetTree(Factories.WidgetFactory.Container(new Rectangle(x, y, width, height)));
            wTree.Parent = _root;
            _root.Children.Add(wTree);
            Resize(width, height);
        }

        public void Resize(int width, int height)
        {
            _root.Data.Space = new Rectangle(_root.Data.Space.X, _root.Data.Space.Y, width, height);
            WidgetTreeVisitor.ApplyPropertiesOnTree(_root);
        }

        public void Update() => _widgetTreeVisitor.UpdateTree(_root);

        public void Draw(SpriteBatch spriteBatch) => WidgetTreeVisitor.DrawTree(_root, spriteBatch);
    }
}