using Microsoft.Xna.Framework;

namespace PiBa.UI.Properties.Margins
{
    public class Margin : IProperty
    {
        public int Priority { get; } = 2;
        private readonly int _margin;

        public Margin(int margin)
        {
            _margin = margin;
        }

        public void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.Margin = new Rectangle(_margin, _margin, _margin, _margin);
        }
    }
}