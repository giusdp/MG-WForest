namespace WForest.UI.Properties
{
    public interface IProperty
    {
        int Priority { get; }
        void ApplyOn(WidgetTree widgetNode);
    }
}