using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.Utilities
{
    public static class TreeVisitor<T>
    {
        public static void ApplyToTreeFromRoot(Tree<T> tree, Action<Tree<T>> action)
        {
            if (tree == null) throw new ArgumentNullException(nameof(tree));
            if (action == null) throw new ArgumentNullException(nameof(action));
            foreach (var node in tree) action(node);
        }
        
        public static void ApplyToTreeFromLeaves(Tree<T> tree, Action<Tree<T>> action)
        {
            if (tree == null) throw new ArgumentNullException(nameof(tree));
            if (action == null) throw new ArgumentNullException(nameof(action));
            foreach (var node in tree.Reverse()) action(node);
        }

        public static Maybe<Tree<T>> GetLowestNodeThatHolds([NotNull] Tree<T> tree, Func<Tree<T>, bool> predicate)
        {
            if (tree == null) throw new ArgumentNullException(nameof(tree));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var nodesThatHold = tree.Children.Select(child => GetLowestNodeThatHolds(child, predicate))
                .OfType<Maybe<Tree<T>>.Some>().ToList();
            if (nodesThatHold.Any()) return nodesThatHold.First();

            return predicate(tree) ? Maybe.Some(tree) : Maybe.None;
        }
    }
}