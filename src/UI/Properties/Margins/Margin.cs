namespace PiBa.UI.Properties.Margins
{
    public class Margin : IProperty
    {

        private int _margin;

        public Margin(int margin)
        {
            _margin = margin;
        }

        public void ApplyOn(WidgetTree widgetNode)
        {
            throw new System.NotImplementedException();
        }
    }
}