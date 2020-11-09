namespace PiBa.UI.Constraints
{
    public interface IConstraint
    {
        void EnforceOn(WidgetTree widgetNode);
    }
}