namespace WForest.UI.Props.Dragging
{
    /// <summary>
    /// Property to make the widget unmovable on the Y axis.
    /// </summary>
    public class FixY : Prop
    {
        internal FixY()
        {
        }

        /// <summary>
        /// This ApplyOn does nothing. Just having this property is enough for a widget to become unmovable on the Y axis.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
        }
    }
}