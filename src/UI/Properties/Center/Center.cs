using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;
using Serilog;

namespace PiBa.UI.Properties.Center
{
    public class Center : IProperty
    {
        public int Priority { get; } = 1;
        public void ApplyOn(WidgetTree widgetNode)
        {
            var widget = widgetNode.Data;

            if (widgetNode.Children.Count == 0)
            {
                Log.Warning(
                    $"{widget} has no children to center.");
                return;
            }

            CenterHandler.CenterByRow(widgetNode);
        }
    }
}