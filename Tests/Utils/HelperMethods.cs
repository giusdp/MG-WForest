using WForest.Utilities;
using WForest.Widgets.Interfaces;

namespace Tests.Utils
{
    public static class HelperMethods
    {
        public static void ApplyProps(IWidget widget) => TreeVisitor.ApplyPropsOnTree(widget);
    }
}