using System.Collections.Generic;
using SuchByte.MacroDeck.Plugins;
using Prototype.HellDivers2Strats.Actions;

namespace Prototype.HellDivers2Strats
{
    public class HellDiversPlugin : MacroDeckPlugin
    {
        public override void Enable()
        {
            Actions = new List<PluginAction>
            {
                new HelldiversStratagemAction(),
            };
        }
    }
}
