using Serilog;
using WForest.Devices;
using WForest.Rendering;
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
        public readonly IWidget Root;
        private UserInteractionHandler _userInteractionHandler;

        internal WTreeManager(IWidget widgetRoot)
        {
            _userInteractionHandler = new UserInteractionHandler(MouseDevice.Instance, new InteractionUpdater());
            Root = widgetRoot;
            TreeVisitor.ApplyPropsOnTree(Root);

            Log.Information(
                "Created new WTreeManager with root starting at ({X},{Y}) for ({W},{H}) of space",
                Root.Space.X, Root.Space.Y, Root.Space.Width, Root.Space.Height
            );
        }


        /// <summary>
        /// Resize the space in which the WidgetTree is located.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Resize(int width, int height)
        {
            Root.Space = new RectangleF(Root.Space.X, Root.Space.Y, width, height);
            TreeVisitor.ApplyPropsOnTree(Root);
        }

        /// <summary>
        /// Update the WidgetTree handled by the manager.
        /// It calls the Update methods of the widgets, handles the interactions with the device etc.
        /// </summary>
        public void Update()
        {
            var transitions = _userInteractionHandler.UpdateAndGenerateTransitions(Root);
            foreach (var t in transitions) t.Execute();

            TreeVisitor.UpdateTree(Root);
        }


        /// <summary>
        /// Draws the WidgetTree. It visits the entire tree calling the Draw methods of the widgets.
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(IRenderer renderer)
        {
            TreeVisitor.ApplyToTreeLevelByLevel(Root, widgets => widgets.ForEach(w =>
            {
                w.Draw(renderer);
                w.PostDrawActions.ForEach(postDraw => postDraw(renderer));
            }));
        }
    }
}