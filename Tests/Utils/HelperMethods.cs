using WForest.UI.Widgets;
using WForest.Utilities;

namespace WForest.Tests.Utils
{
    public static class HelperMethods
    {
        public static void ApplyProps(IWidget widget) => TreeVisitor.ApplyPropsOnTree(widget);
    }
}