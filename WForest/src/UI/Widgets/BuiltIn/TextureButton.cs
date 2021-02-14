using System;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using WForest.Rendering;
using WForest.UI.Props.Actions;
using WForest.Utilities;

namespace WForest.UI.Widgets.BuiltIn
{
    /// <summary>
    /// Widget that displays Texture2Ds based on hovering and pressed states which can be used as a button.
    /// </summary>
    public class TextureButton : TouchableWidget
    {
        public TextureButton(Texture2D normal, Texture2D? hover = null, Texture2D? press = null)
            : base(normal, hover, press)
        {
        }
    }
}