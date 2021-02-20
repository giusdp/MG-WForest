using System.Collections.Generic;
using WForest.Props.Interfaces;
using WForest.Widgets.Interfaces;

namespace WForest.Interactions
{
    public interface IInteractionUpdater
    {
        IEnumerable<ICommandProp> NextState(IWidget interactedObj, Interaction interaction);
    }
}