using System;
using Microsoft.Xna.Framework;
using PiBa.UI.Properties;
using PiBa.UI.Properties.Actions;
using PiBa.UI.Properties.Border;
using PiBa.UI.Properties.Grid.Center;
using PiBa.UI.Properties.Grid.Column;
using PiBa.UI.Properties.Grid.Row;
using PiBa.UI.Properties.Margins;

namespace PiBa.UI.Factories
{
    public static class Properties
    {
        #region Grid

        public static IProperty Row() => new Row();
        public static IProperty Column() => new Column();
        public static IProperty Center() => new Center();

        #endregion

        #region Margins

        public static IProperty Margin(int m) => new Margin(m, m, m, m);

        public static IProperty Margin(int marginLeft, int marginRight, int marginTop, int marginBottom)
            => new Margin(marginLeft, marginRight, marginTop, marginBottom);

        public static IProperty MarginLeft(int marginLeft) => new Margin(marginLeft, 0, 0, 0);
        public static IProperty MarginRight(int marginRight) => new Margin(0, marginRight, 0, 0);
        public static IProperty MarginTop(int marginTop) => new Margin(0, 0, marginTop, 0);
        public static IProperty MarginBottom(int marginBottom) => new Margin(0, 0, 0, marginBottom);

        #endregion

        #region Border

        public static IProperty Border() => new Border();
        public static IProperty Border(Color color) => CheckArgAndCreate(color, c => new Border {Color = c});
        public static IProperty Border(int width) => new Border {LineWidth = width};

        public static IProperty Border(Color color, int width) =>
            CheckArgAndCreate(color, c => new Border {Color = c, LineWidth = width});

        #endregion

        #region Actions

        public static IProperty OnPress(Action logic) => CheckArgAndCreate(logic, action => new OnPress(action));
        public static IProperty OnRelease(Action logic) => CheckArgAndCreate(logic, action => new OnRelease(action));
        public static IProperty OnEnter(Action logic) => CheckArgAndCreate(logic, action => new OnEnter(action));
        public static IProperty OnExit(Action logic) => CheckArgAndCreate(logic, action => new OnExit(action));

        #endregion

        #region Utils

        private static IProperty CheckArgAndCreate<T>(T arg, Func<T, IProperty> f) =>
            arg == null ? throw new ArgumentNullException(nameof(arg)) : f(arg);

        #endregion
    }
}