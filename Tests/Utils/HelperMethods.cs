using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace Tests.Utils
{
    public static class HelperMethods
    {
        public static void ApplyProps(IWidget widget) => TreeVisitor.ApplyPropsOnTree(widget);
    }
}