using System;
using Microsoft.Xna.Framework;
using PiBa.Exceptions;
using PiBa.Rendering;
using PiBa.UI.Widgets;

namespace PiBa.UI.Factories
{
    public static class WidgetFactory
    {

        public static Button CreateButton()
        {
            return new Button(Rectangle.Empty, new Sprite(""), new Sprite(""), new Sprite("") );
        }

    }
}