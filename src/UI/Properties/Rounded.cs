using System;
using WForest.UI;
using WForest.UI.Properties;

namespace WForest.src.UI.Properties
{
    public class Rounded : Property
    {
        private readonly int _radius;

        internal Rounded(int r)
        {
            _radius = r;
        }
        internal override void ApplyOn(WidgetTree widgetNode)
        {
            throw new NotImplementedException();
        }
    }
}
