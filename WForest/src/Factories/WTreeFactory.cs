using System;
using WForest.Devices;
using WForest.Widgets.Interfaces;

namespace WForest.Factories
{
    /// <summary>
    /// Entry point of the WForest Library.
    /// Factory to create WTree, a WTree is an usable instance of a menu.
    /// </summary>
    public static class WTreeFactory
    {
        /// <summary>
        /// Creates a <see cref="WTree"/> that covers the area defined by the space of the root widget and handles
        /// resizing, updating and drawing the widget tree. By default it's given a MouseDevice.Instance to check for
        /// interactions with the widgets.
        /// </summary>
        /// <param name="wTree"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static WTree CreateWTree(IWidget wTree)
        {
            if (wTree == null) throw new ArgumentNullException(nameof(wTree));
            return new WTree(wTree, MouseDevice.Instance);
        }

        /// <summary>
        /// Creates a <see cref="WTree"/> that covers the area defined by the space of the root widget and handles
        /// resizing, updating and drawing the widget tree. The IDevice in input is used to check for interactions with the widgets.
        /// </summary>
        /// <param name="wTree"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static WTree CreateWTree(IWidget wTree, IDevice device)
        {
            if (wTree == null) throw new ArgumentNullException(nameof(wTree));
            return new WTree(wTree, device);
        }
    }
}