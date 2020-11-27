using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Serilog;
using WForest.UI.Factories;
using WForest.UI.Properties.Grid;
using WForest.UI.Widgets;
using WForest.UI.WidgetTreeHandlers;

namespace WForest.UI
{
    public class HUD
    {
        private readonly WidgetTree _root;
        private readonly WidgetTreeVisitor _widgetTreeVisitor;


        public HUD()
        {
            _widgetTreeVisitor = new WidgetTreeVisitor();

            _root = new WidgetTree(Factories.Widgets.Container(new Rectangle(0, 0, 1280, 720)));
            _root.AddProperty(Factories.Properties.Column());
             // _root.AddProperty(Factories.Properties.JustifyBetween());
            _root.AddProperty(Factories.Properties.ItemBase());

            var c = _root.AddChild(Factories.Widgets.ImageButton("SpriteBtnL"));
            c.AddProperty(Factories.Properties.MarginLeft(10));
            _root.AddChild(Factories.Widgets.ImageButton("SpriteBtnL")).AddProperty(Factories.Properties.Margin(10, 0, 20, 20));
            _root.AddChild(Factories.Widgets.ImageButton("SpriteBtnA"));
            _root.AddChild(Factories.Widgets.ImageButton("SpriteBtnL"));
            _root.AddChild(Factories.Widgets.ImageButton("SpriteBtnL"));
            _root.AddChild(Factories.Widgets.ImageButton("SpriteBtnL"));
            
            _widgetTreeVisitor.ApplyPropertiesOnTree(_root);
            
        }

        public void Update()
        {
            _widgetTreeVisitor.CheckHovering(_root, Mouse.GetState().Position);
        }

        public void Draw(SpriteBatch spriteBatch) => _widgetTreeVisitor.DrawTree(_root, spriteBatch);
    }
}