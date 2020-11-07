using System;
using Microsoft.Xna.Framework;
using PiBa.Exceptions;
using PiBa.Rendering;
using PiBa.UI.Widgets;

namespace PiBa.UI.Factories
{
    public static class WidgetFactory
    {
        public static SpriteButton CreateSpriteButton(Sprite sprite)
        {
            return new SpriteButton(sprite);
        }

        public static Container CreateContainer(Rectangle space)
        {
            return new Container(space);
        }

    }
}