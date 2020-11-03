using Microsoft.Xna.Framework;
using PiBa.UI.Props;
using PiBa.UI.Widgets;
using PiBa.UI.Widgets.Interfaces;

namespace PiBa.UI.Factories
{
    public static class WidgetFactory
    {
        public static IWidget CreateContainerWidget(Point size)
        {
            IWidget w = new Container();
            w.AddProp(new Size(size));
            return w;
        }
    }
}