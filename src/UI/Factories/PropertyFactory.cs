using System;
using PiBa.UI.Properties;
using PiBa.UI.Properties.Actions;
using PiBa.UI.Properties.Center;
using PiBa.UI.Properties.Margins;

namespace PiBa.UI.Factories
{
    public static class PropertyFactory
    {
        public static IProperty Center() => new Center();
        public static IProperty Margin(int m) => new Margin(m);

        public static IProperty OnPress(Action logic) => CheckArgAndCreate(logic, action => new OnPress(action));
        public static IProperty OnRelease(Action logic) => CheckArgAndCreate(logic, action => new OnRelease(action));
        public static IProperty OnEnter(Action logic) => CheckArgAndCreate(logic, action => new OnEnter(action));
        public static IProperty OnExit(Action logic) => CheckArgAndCreate(logic, action => new OnExit(action));

        private static IProperty CheckArgAndCreate<T>(T arg, Func<T, IProperty> f) =>
            arg == null ? throw new ArgumentNullException(nameof(arg)) : f(arg);
    }
}