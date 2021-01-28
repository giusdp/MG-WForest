using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using WForest.Devices;
using WForest.UI.Interactions;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace WForest.UI
{
    /// <summary>
    /// Class to handle WidgetTrees. It holds a WidgetTree on which it can update, resize and draw.
    /// </summary>
    public class WTreeManager
    {
        private readonly IWidget _root;
        private UserInteractionHandler _userInteractionHandler;

        internal WTreeManager(IWidget widgetRoot)
        {
            _userInteractionHandler = new UserInteractionHandler(MouseDevice.Instance, new InteractionUpdater());
            _root = widgetRoot;
            TreeVisitor.ApplyPropsOnTree(_root);

            Log.Information(
                "Created new WTreeManager with root starting at ({X},{Y}) for ({W},{H}) of space",
                _root.Space.X, _root.Space.Y, _root.Space.Width, _root.Space.Height
                );
        }

        /// <summary>
        /// Resize the space in which the WidgetTree is located.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Resize(int width, int height)
        {
            _root.Space = new Rectangle(_root.Space.X, _root.Space.Y, width, height);
            TreeVisitor.ApplyPropsOnTree(_root);
        }

        /// <summary>
        /// Update the WidgetTree handled by the manager.
        /// It calls the Update methods of the widgets, handles the interactions with the device etc.
        /// </summary>
        public void Update()
        {
            var transitions = _userInteractionHandler.UpdateAndGenerateTransitions(_root);
            foreach (var t in transitions) t.Execute();

            TreeVisitor.UpdateTree(_root);
        }


        /// <summary>
        /// Draws the WidgetTree. It visits the entire tree calling the Draw methods of the widgets.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) => DrawWidgetTree(spriteBatch);

        private void DrawWidgetTree(SpriteBatch spriteBatch)
        {
            TreeVisitor.ApplyToTreeLevelByLevel(_root, widgets => widgets.ForEach(w =>
            {
                w.Draw(spriteBatch);
                w.PostDrawActions.ForEach(postDraw => postDraw(spriteBatch));
            }));
        }
    }
}