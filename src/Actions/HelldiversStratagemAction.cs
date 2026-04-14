using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using Prototype.HellDivers2Strats.Configuration;
using Prototype.HellDivers2Strats.Core;

namespace Prototype.HellDivers2Strats.Actions
{
    public class HelldiversStratagemAction : PluginAction
    {
        public override string Name => "Helldivers 2 Stratagem";

        public override string Description => "Executes the selected Helldivers 2 stratagem input sequence.";

        public override bool CanConfigure => true;

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new StratagemActionConfigControl(this);
        }

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            var config = StratagemActionConfig.Resolve(Configuration);
            StratagemRegistry.TryGet(config.StratagemId, out var stratagem);
            StratagemExecutor.Execute(stratagem);
        }

        public override void OnActionButtonLoaded()
        {
            var config = StratagemActionConfig.Resolve(Configuration);
            StratagemRegistry.TryGet(config.StratagemId, out var stratagem);
            Configuration = config.Serialize();
            ConfigurationSummary = stratagem.DisplayName;
        }
    }
}
