using System;
using Microsoft.Xna.Framework;
using WForest.src.UI.Properties;
using WForest.UI.Properties;
using WForest.UI.Properties.Actions;
using WForest.UI.Properties.Border;
using WForest.UI.Properties.Grid;
using WForest.UI.Properties.Grid.Column;
using WForest.UI.Properties.Grid.ItemProps;
using WForest.UI.Properties.Grid.JustifyProps;
using WForest.UI.Properties.Grid.Row;
using WForest.UI.Properties.Margins;

namespace WForest.UI.Factories
{
    public static class Properties
    {
        #region Grid

        public static Property Row() => new Row();
        public static Property Column() => new Column();
        public static Property Flex() => new Flex();
        public static Property Stretch() => new Stretch();
        public static Property JustifyCenter() => new JustifyCenter();
        public static Property JustifyEnd() => new JustifyEnd();
        public static Property JustifyBetween() => new JustifyBetween();
        public static Property JustifyAround() => new JustifyAround();
        public static Property ItemCenter() => new ItemCenter();
        public static Property ItemBase() => new ItemBase();

        #endregion

        #region Margins

        public static Property Margin(int m) => new Margin(m, m, m, m);

        public static Property Margin(int marginLeft, int marginRight, int marginTop, int marginBottom)
            => new Margin(marginLeft, marginRight, marginTop, marginBottom);

        public static Property MarginLeft(int marginLeft) => new Margin(marginLeft, 0, 0, 0);
        public static Property MarginRight(int marginRight) => new Margin(0, marginRight, 0, 0);
        public static Property MarginTop(int marginTop) => new Margin(0, 0, marginTop, 0);
        public static Property MarginBottom(int marginBottom) => new Margin(0, 0, 0, marginBottom);

        #endregion

        #region Border

        public static Property Border() => new Border();
        public static Property Border(Color color) => CheckArgAndCreate(color, c => new Border {Color = c});
        public static Property Border(int width) => new Border {LineWidth = width};

        public static Property Border(Color color, int width) =>
            CheckArgAndCreate(color, c => new Border {Color = c, LineWidth = width});

        #endregion

        #region Modifiers

        public static Property Rounded(int r) => new Rounded(r);

        #endregion

        #region Actions

        public static Property OnPress(Action logic) => CheckArgAndCreate(logic, action => new OnPress(action));
        public static Property OnRelease(Action logic) => CheckArgAndCreate(logic, action => new OnRelease(action));
        public static Property OnEnter(Action logic) => CheckArgAndCreate(logic, action => new OnEnter(action));
        public static Property OnExit(Action logic) => CheckArgAndCreate(logic, action => new OnExit(action));

        #endregion

        #region Utils

        private static Property CheckArgAndCreate<T>(T arg, Func<T, Property> f) =>
            arg == null ? throw new ArgumentNullException(nameof(arg)) : f(arg);

        #endregion
    }
}