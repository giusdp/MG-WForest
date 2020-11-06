using System;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.Utilities
{
    public static class TreeVisitor<T>
    {
        
        public static void ApplyToTree(Tree<T> tree, Action<Tree<T>> action)
        {
            if (tree == null) throw new ArgumentNullException(nameof(tree));
            if (action == null) throw new ArgumentNullException(nameof(action));
            foreach (var node in tree) action(node);
        }
        
    }
}