using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Widgets.TextWidget;

namespace WForest.Tests.Utils
{
    class FakeFont : Font
    {
        public FakeFont() : base(null)
        {
        }

        public override (int, int) MeasureText(string text, int fontSize)
        {
            return (0, 0);
        }
    }
}