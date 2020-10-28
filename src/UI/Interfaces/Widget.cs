using System;
using PiBa.Interfaces;

namespace PiBa.UI.Interfaces
{
    public abstract class Widget : IGameObject
    {
        public abstract void Update();
        public abstract void Draw();
    }
}