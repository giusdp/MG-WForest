namespace WForest.UI.Props.Dragging
{
    /// <summary>
    /// Property to make the widget unmovable on the X axis.
    /// </summary>
    public class FixX : Prop
    {
        internal FixX(){}
        
        /// <summary>
        /// This ApplyOn does nothing. Just having this property is enough for a widget to become unmovable on the X axis.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
        }
    }
}