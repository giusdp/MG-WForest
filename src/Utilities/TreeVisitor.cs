using System;
using System.Diagnostics.CodeAnalysis;
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

        public static Maybe<Tree<T>> GetLowestNodeThatHolds([NotNull] Tree<T> tree, Func<Tree<T>, bool> predicate)
        {
            if(tree ==null) throw new ArgumentNullException(nameof(tree));
            if(predicate == null) throw new ArgumentNullException(nameof(predicate));

           
            foreach (var child in tree.Children)
            {
                var res = GetLowestNodeThatHolds(child, predicate);
                if (res is Maybe<Tree<T>>.Some) return res;
            }
            
            return predicate(tree) ? Maybe.Some(tree) : Maybe.None;
        }
    }
}