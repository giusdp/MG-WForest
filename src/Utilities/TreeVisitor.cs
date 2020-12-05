using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Widgets;
using WForest.Utilities.Collections;

namespace WForest.Utilities
{
    public static class TreeVisitor<T>
    {
        public static void ApplyToTreeLevelByLevel(Tree<T> tree, Action<Tree<T>> action)
        {
            if (tree == null) throw new ArgumentNullException(nameof(tree));
            if (action == null) throw new ArgumentNullException(nameof(action));

            void ApplyLevelByLevel(List<Tree<T>> lvl)
            {
                while (lvl.Any())
                {
                    var oneLvlDown = lvl.SelectMany(c => c.Children).ToList();
                    foreach (var c in oneLvlDown)
                    {
                        action(c);
                    }

                    lvl = oneLvlDown;
                }
            }

            action(tree);
            ApplyLevelByLevel(new List<Tree<T>> {tree});
          
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