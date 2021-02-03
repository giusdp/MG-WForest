using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WForest.UI.Props.Actions;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;

namespace WForest.Utilities
{
    public static class TreeVisitor
    {
        public static void ApplyPropsOnTree(IWidget widgetTree)
        {
            ApplyToTreeFromLeaves(widgetTree, w =>
            {
                foreach (var prop in w.Props.OfType<IApplicableProp>().OrderBy(p => p.Priority)) prop.ApplyOn(w);
            });
        }

        public static void ResetApplyProps(IWidget wTree)
        {
            foreach (var widget in wTree)
            foreach (var app in widget.Props.OfType<IApplicableProp>())
                app.ApplicationDone = false;
        }

        public static void UpdateTree(IWidget widget)
        {
            foreach (var w in widget)
            {
                var maybe = w.Props.SafeGetByProp<OnUpdate>();
                switch (maybe)
                {
                    case Maybe<List<IProp>>.Some s:
                        foreach (var command in s.Value.Cast<ICommandProp>()) command.Execute();
                        break;
                    default:
                        continue;
                }
            }
        }

        public static void ApplyToTreeFromLeaves(IWidget tree, Action<IWidget> action)
        {
            if (tree == null) throw new ArgumentNullException(nameof(tree));
            if (action == null) throw new ArgumentNullException(nameof(action));
            foreach (var node in tree.Reverse()) action(node);
        }

        public static void ApplyToTreeLevelByLevel(IWidget tree, Action<List<IWidget>> action)
        {
            if (tree == null) throw new ArgumentNullException(nameof(tree));
            if (action == null) throw new ArgumentNullException(nameof(action));

            void ApplyLevelByLevel(List<IWidget> lvl)
            {
                action(lvl);
                while (lvl.Any())
                {
                    var oneLvlDown = lvl.SelectMany(c => c.Children).ToList();
                    action(oneLvlDown);
                    lvl = oneLvlDown;
                }
            }

            ApplyLevelByLevel(new List<IWidget> {tree});
        }

        public static Maybe<IWidget> GetLowestNodeThatHolds([NotNull] IWidget tree,
            Func<IWidget, IEnumerable<IWidget>> childrenSelector, Func<IWidget, bool> predicate)
        {
            if (tree == null) throw new ArgumentNullException(nameof(tree));
            if (childrenSelector == null) throw new ArgumentNullException(nameof(childrenSelector));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var revCh = childrenSelector(tree);
            var nodesThatHold = revCh.Select(child => GetLowestNodeThatHolds(child, childrenSelector, predicate))
                .OfType<Maybe<IWidget>.Some>().ToList();

            if (nodesThatHold.Any()) return nodesThatHold.Last();
            else return predicate(tree) ? Maybe.Some(tree) : Maybe.None;
        }
    }
}