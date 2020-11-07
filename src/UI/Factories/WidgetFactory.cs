using System;
using Microsoft.Xna.Framework;
using PiBa.Exceptions;
using PiBa.Rendering;
using PiBa.UI.Widgets;

namespace PiBa.UI.Factories
{
    public static class WidgetFactory
    {
        public static SpriteButton CreateButton()
        {
            return new SpriteButton(new Sprite("") );
        }

        public static Container CreateContainer(Rectangle space)
        {
            return new Container(space);
        }

    }
}