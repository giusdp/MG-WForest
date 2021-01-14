using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using WForest.Factories;
using WForest.UI.WidgetTrees;

namespace WForest.UI
{
    /// <summary>
    /// Class to handle WidgetTrees. It holds a WidgetTree on which it can update, resize and draw.
    /// </summary>
    public class WTreeManager
    {
        private readonly WidgetTree _root;
        private readonly WidgetTreeVisitor _widgetTreeVisitor;

        internal WTreeManager(int x, int y, int width, int height, WidgetTree wTree)
        {
            Log.Information($"Created new WTree with {x},{y} coordinates and {width},{height} size.");
            _widgetTreeVisitor = new WidgetTreeVisitor();

            _root = new WidgetTree(WidgetFactory.Container(new Rectangle(x, y, width, height)));
            wTree.Parent = _root;
            _root.Children.Add(wTree);
            Resize(width, height);
        }

        /// <summary>
        /// Resize the space in which the WidgetTree is located.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Resize(int width, int height)
        {
            _root.Data.Space = new Rectangle(_root.Data.Space.X, _root.Data.Space.Y, width, height);
            WidgetTreeVisitor.ApplyPropertiesOnTree(_root);
        }

        /// <summary>
        /// Update the WidgetTree handled by the manager.
        /// It calls the Update methods of the widgets, handles the interactions with the device etc.
        /// </summary>
        public void Update() => _widgetTreeVisitor.UpdateTree(_root);

        /// <summary>
        /// Draws the WidgetTree. It visits the entire tree calling the Draw methods of the widgets.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) => WidgetTreeVisitor.DrawTree(_root, spriteBatch);
    }
}