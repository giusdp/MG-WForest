using System;
using Microsoft.Xna.Framework;
using WForest.Devices;
using WForest.UI.Props;
using WForest.UI.Props.Actions;
using WForest.UI.Props.Dragging;
using WForest.UI.Props.Grid;
using WForest.UI.Props.Grid.ItemProps;
using WForest.UI.Props.Grid.JustifyProps;
using WForest.UI.Props.Grid.StretchingProps;
using WForest.UI.Props.Interfaces;
using WForest.UI.Props.Text;

namespace WForest.Factories
{
    /// <summary>
    /// Static class to create Properties.
    /// </summary>
    public static class PropFactory
    {
        #region Grid

        /// <summary>
        /// Creates a Row property. Layout property that puts widgets in a horizontal sequence.
        /// </summary>
        /// <returns></returns>
        public static IProp Row() => new Row();

        /// <summary>
        /// Creates a Columns. Layout property that puts widgets in a vertical sequence. 
        /// </summary>
        /// <returns></returns>
        public static IProp Column() => new Column();

        /// <summary>
        /// Creates a Flex property. Flex makes the space of the widget as small as possible. It increases based on the children space.
        /// </summary>
        /// <returns></returns>
        public static IProp Flex() => new Flex();

        /// <summary>
        /// Creates a HorizontalStretch property. HorizontalStretch makes the widget width as big as its parent's width.
        /// </summary>
        /// <returns></returns>
        public static IProp HorizontalStretch() => new HorizontalStretch();

        /// <summary>
        /// Creates a VerticalStretch property. VerticalStretch makes the widget height as big as its parent's height.
        /// </summary>
        /// <returns></returns>
        public static IProp VerticalStretch() => new VerticalStretch();

        /// <summary>
        /// Creates a JustifyCenter property. It puts widgets to the center of a Row (horizontally) or Column (vertically).
        /// </summary>
        /// <returns></returns>
        public static IProp JustifyCenter() => new JustifyCenter();

        /// <summary>
        /// Creates a JustifyEnd property. It puts widgets to the right in a Row, or bottom in a Column.
        /// </summary>
        /// <returns></returns>
        public static IProp JustifyEnd() => new JustifyEnd();

        /// <summary>
        /// Creates a JustifyBetween property. It separates widgets in a Row or Column, placing the maximum space possible in between them.
        /// </summary>
        /// <returns></returns>
        public static IProp JustifyBetween() => new JustifyBetween();

        /// <summary>
        /// Creates a JustifyAround property. It places the maximum space possible in between them and from the window borders.
        /// </summary>
        /// <returns></returns>
        public static IProp JustifyAround() => new JustifyAround();

        /// <summary>
        /// Creates an ItemCenter property. It moves widgets to the center vertically in a Row or horizontally in a Column.
        /// </summary>
        /// <returns></returns>
        public static IProp ItemCenter() => new ItemCenter();

        /// <summary>
        /// Creates an ItemBase property. It move widgets to the bottom in a Row or to the right in a Column.
        /// </summary>
        /// <returns></returns>
        public static IProp ItemBase() => new ItemBase();

        #endregion

        #region Margins

        /// <summary>
        /// Creates a Margin property with the same margin for all sides.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static IProp Margin(int m) => new Margin(m, m, m, m);

        /// <summary>
        /// Creates a Margin property giving specific values for each side.
        /// </summary>
        /// <param name="marginLeft"></param>
        /// <param name="marginRight"></param>
        /// <param name="marginTop"></param>
        /// <param name="marginBottom"></param>
        /// <returns></returns>
        public static IProp Margin(int marginLeft, int marginRight, int marginTop, int marginBottom)
            => new Margin(marginLeft, marginRight, marginTop, marginBottom);

        /// <summary>
        /// Creates a Margin property setting the Left side margin with the value given, leaving the other sides at 0.
        /// </summary>
        /// <param name="marginLeft"></param>
        /// <returns></returns>
        public static IProp MarginLeft(int marginLeft) => new Margin(marginLeft, 0, 0, 0);

        /// <summary>
        /// Creates a Margin property setting the Right side margin with the value given, leaving the other sides at 0.
        /// </summary>
        /// <param name="marginRight"></param>
        /// <returns></returns> 
        public static IProp MarginRight(int marginRight) => new Margin(0, marginRight, 0, 0);

        /// <summary>
        /// Creates a Margin property setting the Top side margin with the value given, leaving the other sides at 0. 
        /// </summary>
        /// <param name="marginTop"></param>
        /// <returns></returns>
        public static IProp MarginTop(int marginTop) => new Margin(0, 0, marginTop, 0);

        /// <summary>
        /// Creates a Margin property setting the Bottom side margin with the value given, leaving the other sides at 0.  
        /// </summary>
        /// <param name="marginBottom"></param>
        /// <returns></returns>
        public static IProp MarginBottom(int marginBottom) => new Margin(0, 0, 0, marginBottom);

        #endregion

        #region Modifiers

        /// <summary>
        /// Creates a Border property with default Color (of Black) and default LineWidth (of  1).
        /// </summary>
        /// <returns></returns>
        public static IProp Border() => new Border();

        /// <summary>
        /// Creates a Border property with the given Color and LineWidth of 1.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static IProp Border(Color color) => CheckArgAndCreate(color, c => new Border {Color = c});

        /// <summary>
        /// Creates a Border property with default Color of Black and given LineWidth.
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public static IProp Border(int width) => new Border {LineWidth = width};

        /// <summary>
        /// Creates a Border property with the given Color and LineWidth.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static IProp Border(Color color, int width) =>
            CheckArgAndCreate(color, c => new Border {Color = c, LineWidth = width});

        /// <summary>
        /// Creates a Color property used to modify the color of a widget.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static IProp Color(Color color) => new ColorProp(color);

        /// <summary>
        /// Creates a Draggable property used to make a widget draggable with the given input device.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static IProp Draggable(IDevice device) => new Draggable(device);

        /// <summary>
        /// Creates a FixX IProperty that makes a widget unmovable on the X axis. 
        /// </summary>
        /// <returns></returns>
        public static IProp FixX() => new FixX();

        /// <summary>
        /// Creates a FixY IProperty that makes a widget unmovable on the Y axis.
        /// </summary>
        /// <returns></returns>
        public static IProp FixY() => new FixY();

        #endregion

        #region Font

        /// <summary>
        /// Creates a FontSize property. It's used together with the Text Widget to change the size its font.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IProp FontSize(int size) => new FontSize(size);

        /// <summary>
        /// Creates a FontFamily property. It's used together with the Text Widget to change its font.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IProp FontFamily(string name) => new FontFamily(name);

        #endregion

        #region Actions

        /// <summary>
        /// Creates an OnPress property. Adds logic to a widget when it is pressed.
        /// </summary>
        /// <param name="logic"></param>
        /// <returns></returns>
        public static IProp OnPress(Action logic) => CheckArgAndCreate(logic, action => new OnPress(action));

        /// <summary>
        /// Creates an OnRelease property. Adds logic to a widget when it is released from a pressed.
        /// </summary>
        /// <param name="logic"></param>
        /// <returns></returns>
        public static IProp OnRelease(Action logic) => CheckArgAndCreate(logic, action => new OnRelease(action));

        /// <summary>
        /// Creates an OnEnter property. Adds logic to a widget when it starts being hovered.
        /// </summary>
        /// <param name="logic"></param>
        /// <returns></returns>
        public static IProp OnEnter(Action logic) => CheckArgAndCreate(logic, action => new OnEnter(action));

        /// <summary>
        /// Creates an OnExit property. Adds logic to a widget when it stops being hovered.
        /// </summary>
        /// <param name="logic"></param>
        /// <returns></returns>
        public static IProp OnExit(Action logic) => CheckArgAndCreate(logic, action => new OnExit(action));

        /// <summary>
        /// Creates an OnUpdate property. Adds logic to a widget that is run at every Update of the game.
        /// </summary>
        /// <param name="logic"></param>
        /// <returns></returns>
        public static IProp OnUpdate(Action logic) => CheckArgAndCreate(logic, action => new OnUpdate(action));

        #endregion

        #region Utils

        private static IProp CheckArgAndCreate<T>(T arg, Func<T, IProp> f) =>
            arg == null ? throw new ArgumentNullException(nameof(arg)) : f(arg);

        #endregion
    }
}