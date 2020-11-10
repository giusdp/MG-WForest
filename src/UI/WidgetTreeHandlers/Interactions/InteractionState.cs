using PiBa.UI.Widgets;

namespace PiBa.UI.WidgetTreeHandlers.Interactions
{
    public class InteractionState
    {
       public Widget LastHovered { get; set; }
       public bool IsButtonBeingPressed { get; set; }
       public bool WasButtonAlreadyPressed { get; set; }


       public void Reset()
       {
           LastHovered = null;
           IsButtonBeingPressed = false;
           WasButtonAlreadyPressed = false;
       }
    }
}