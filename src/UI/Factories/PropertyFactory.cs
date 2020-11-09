using Microsoft.Xna.Framework;
using PiBa.UI.Properties;

namespace PiBa.UI.Factories
{
    public static class PropertyFactory
    {
        public static IProperty CreateCenterProperty() => new Center();
       
    }
}