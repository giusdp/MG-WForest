using WForest.Utilities.Text;

namespace WForest.Tests.Utils
{
    internal class FakeFont : Font
    {
        public (int, int) MeasureTextResult = (0, 0);
        public FakeFont() : base(null)
        {
        }

        public FakeFont((int, int) measureTextResult) : base(null)
        {
            MeasureTextResult = measureTextResult;
        }

        internal override (int, int) MeasureText(string text, int fontSize) => MeasureTextResult;
    }
}