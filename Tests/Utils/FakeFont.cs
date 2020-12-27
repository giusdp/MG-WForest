using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Widgets.TextWidget;

namespace WForest.Tests.Utils
{
    class FakeFont : Font
    {
        public (int, int) MeasureTextResult = (0, 0);
        public FakeFont() : base(null)
        {
        }

        public FakeFont((int, int) measureTextResult) : base(null)
        {
            MeasureTextResult = measureTextResult;
        }

        public override (int, int) MeasureText(string text, int fontSize) => MeasureTextResult;
    }
}