using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Prototype.HellDivers2Strats.Core
{
    public static class StratagemRegistry
    {
        private static readonly ImmutableDictionary<string, StratagemDefinition> Definitions = BuildDefinitions();

        private static ImmutableDictionary<string, StratagemDefinition> BuildDefinitions()
        {
            var builder = ImmutableDictionary.CreateBuilder<string, StratagemDefinition>(StringComparer.OrdinalIgnoreCase);

            foreach (var definition in new[]
            {
                CreateDefinition("A/ARC-3 Tesla Tower", Keys.Down, Keys.Up, Keys.Right, Keys.Up, Keys.Left, Keys.Right),
                CreateDefinition("A/FLAM-40 Flame Sentry", Keys.Down, Keys.Up, Keys.Right, Keys.Down, Keys.Up, Keys.Up),
                CreateDefinition("A/G-16 Gatling Sentry", Keys.Down, Keys.Up, Keys.Right, Keys.Left),
                CreateDefinition("A/GM-17 Gas Mortar Sentry", Keys.Down, Keys.Up, Keys.Right, Keys.Down, Keys.Left),
                CreateDefinition("A/LAS-98 Laser Sentry", Keys.Down, Keys.Up, Keys.Right, Keys.Down, Keys.Up, Keys.Right),
                CreateDefinition("A/M-12 Mortar Sentry", Keys.Down, Keys.Up, Keys.Right, Keys.Right, Keys.Down),
                CreateDefinition("A/M-23 EMS Mortar Sentry", Keys.Down, Keys.Up, Keys.Right, Keys.Down, Keys.Right),
                CreateDefinition("A/MG-43 Machine Gun Sentry", Keys.Down, Keys.Up, Keys.Right, Keys.Right, Keys.Up),
                CreateDefinition("A/MLS-4X Rocket Sentry", Keys.Down, Keys.Up, Keys.Right, Keys.Right, Keys.Left),
                CreateDefinition("AC-8 Autocannon", Keys.Down, Keys.Left, Keys.Down, Keys.Up, Keys.Up, Keys.Right),
                CreateDefinition("APW-1 Anti-Materiel Rifle", Keys.Down, Keys.Left, Keys.Right, Keys.Up, Keys.Down),
                CreateDefinition("ARC-3 Arc Thrower", Keys.Down, Keys.Right, Keys.Down, Keys.Up, Keys.Left, Keys.Left),
                CreateDefinition("AX/AR-23 Guard Dog", Keys.Down, Keys.Up, Keys.Left, Keys.Up, Keys.Right, Keys.Down),
                CreateDefinition("AX/ARC-3 K-9", Keys.Down, Keys.Up, Keys.Left, Keys.Up, Keys.Right, Keys.Left),
                CreateDefinition("AX/FLAM-75 Hot Dog", Keys.Down, Keys.Up, Keys.Left, Keys.Up, Keys.Left, Keys.Left),
                CreateDefinition("AX/LAS-5 Rover", Keys.Down, Keys.Up, Keys.Left, Keys.Up, Keys.Right, Keys.Right),
                CreateDefinition("AX/TX-13 Dog Breath", Keys.Down, Keys.Up, Keys.Left, Keys.Up, Keys.Right, Keys.Up),
                CreateDefinition("B-1 Supply Pack", Keys.Down, Keys.Left, Keys.Down, Keys.Up, Keys.Up, Keys.Down),
                CreateDefinition("B-100 Portable Hellbomb", Keys.Down, Keys.Right, Keys.Up, Keys.Up, Keys.Up),
                CreateDefinition("B/FLAM-80 Cremator", Keys.Down, Keys.Down, Keys.Right, Keys.Down, Keys.Up, Keys.Up),
                CreateDefinition("B/MD C4 Pack", Keys.Down, Keys.Right, Keys.Up, Keys.Up, Keys.Right, Keys.Up),
                CreateDefinition("Call In Super Destroyer", Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right),
                CreateDefinition("Cargo Container", Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Right, Keys.Down),
                CreateDefinition("Cargo Container", Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Right, Keys.Down),
                CreateDefinition("CQC-1 One True Flag", Keys.Down, Keys.Left, Keys.Right, Keys.Right, Keys.Up),
                CreateDefinition("CQC-20 Breaching Hammer", Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Up),
                CreateDefinition("CQC-9 Defoliation Tool", Keys.Down, Keys.Left, Keys.Right, Keys.Right, Keys.Down),
                CreateDefinition("Dark Fluid Vessel", Keys.Up, Keys.Left, Keys.Right, Keys.Down, Keys.Up, Keys.Up),
                CreateDefinition("E/AT-12 Anti-Tank Emplacement", Keys.Down, Keys.Up, Keys.Left, Keys.Right, Keys.Right, Keys.Right),
                CreateDefinition("E/GL-21 Grenadier Battlement", Keys.Down, Keys.Right, Keys.Down, Keys.Left, Keys.Right),
                CreateDefinition("E/MG-101 HMG Emplacement", Keys.Down, Keys.Up, Keys.Left, Keys.Right, Keys.Right, Keys.Left),
                CreateDefinition("Eagle 110mm Rocket Pods", Keys.Up, Keys.Right, Keys.Up, Keys.Left),
                CreateDefinition("Eagle 500kg Bomb", Keys.Up, Keys.Right, Keys.Down, Keys.Down, Keys.Down),
                CreateDefinition("Eagle Airstrike", Keys.Up, Keys.Right, Keys.Down, Keys.Right),
                CreateDefinition("Eagle Cluster Bomb", Keys.Up, Keys.Right, Keys.Down, Keys.Down, Keys.Right),
                CreateDefinition("Eagle Napalm Airstrike", Keys.Up, Keys.Right, Keys.Down, Keys.Up),
                CreateDefinition("Eagle Rearm", Keys.Up, Keys.Up, Keys.Left, Keys.Up, Keys.Right),
                CreateDefinition("Eagle Smoke Strike", Keys.Up, Keys.Right, Keys.Up, Keys.Down),
                CreateDefinition("Eagle Strafing Run", Keys.Up, Keys.Right, Keys.Right),
                CreateDefinition("EAT-17 Expendable Anti-Tank", Keys.Down, Keys.Down, Keys.Left, Keys.Up, Keys.Right),
                CreateDefinition("EAT-411 Leveller", Keys.Down, Keys.Down, Keys.Left, Keys.Up, Keys.Down),
                CreateDefinition("EAT-700 Expendable Napalm", Keys.Down, Keys.Down, Keys.Left, Keys.Up, Keys.Left),
                CreateDefinition("EXO-45 Patriot Exosuit", Keys.Left, Keys.Down, Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Down),
                CreateDefinition("EXO-49 Emancipator Exosuit", Keys.Left, Keys.Down, Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Up),
                CreateDefinition("EXO-51 Lumberer Exosuit", Keys.Left, Keys.Down, Keys.Right, Keys.Up, Keys.Right, Keys.Left, Keys.Up),
                CreateDefinition("EXO-55 Breakthrough Exosuit", Keys.Left, Keys.Down, Keys.Right, Keys.Left, Keys.Right, Keys.Down, Keys.Up),
                CreateDefinition("FAF-14 Spear", Keys.Down, Keys.Down, Keys.Up, Keys.Down, Keys.Down),
                CreateDefinition("FLAM-40 Flamethrower", Keys.Down, Keys.Left, Keys.Up, Keys.Down, Keys.Up),
                CreateDefinition("FX-12 Shield Generator Relay", Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right),
                CreateDefinition("GL-21 Grenade Launcher", Keys.Down, Keys.Left, Keys.Up, Keys.Left, Keys.Down),
                CreateDefinition("GL-28 Belt-Fed Grenade Launcher", Keys.Down, Keys.Left, Keys.Up, Keys.Left, Keys.Up, Keys.Up),
                CreateDefinition("GL-52 De-Escalator", Keys.Down, Keys.Right, Keys.Up, Keys.Left, Keys.Right),
                CreateDefinition("GR-8 Recoilless Rifle", Keys.Down, Keys.Left, Keys.Right, Keys.Right, Keys.Left),
                CreateDefinition("Hive Breaker Drill", Keys.Left, Keys.Up, Keys.Down, Keys.Right, Keys.Down, Keys.Down),
                CreateDefinition("LAS-98 Laser Cannon", Keys.Down, Keys.Left, Keys.Down, Keys.Up, Keys.Left),
                CreateDefinition("LAS-99 Quasar Cannon", Keys.Down, Keys.Down, Keys.Up, Keys.Left, Keys.Right),
                CreateDefinition("LIFT-182 Warp Pack", Keys.Down, Keys.Left, Keys.Right, Keys.Down, Keys.Left, Keys.Right),
                CreateDefinition("LIFT-850 Jump Pack", Keys.Down, Keys.Up, Keys.Up, Keys.Down, Keys.Up),
                CreateDefinition("Hover Pack", Keys.Down, Keys.Up, Keys.Up, Keys.Down, Keys.Left, Keys.Right),
                CreateDefinition("M-1000 Maxigun", Keys.Down, Keys.Left, Keys.Right, Keys.Down, Keys.Up, Keys.Up),
                CreateDefinition("M-102 Fast Recon Vehicle", Keys.Left, Keys.Down, Keys.Right, Keys.Down, Keys.Right, Keys.Down, Keys.Up),
                CreateDefinition("Supply FRV", Keys.Left, Keys.Down, Keys.Left, Keys.Left, Keys.Down, Keys.Up, Keys.Right),
                CreateDefinition("M-104 Incinerator FRV", Keys.Left, Keys.Down, Keys.Right, Keys.Left, Keys.Down, Keys.Up, Keys.Up),
                CreateDefinition("M-105 Stalwart", Keys.Down, Keys.Left, Keys.Down, Keys.Up, Keys.Up, Keys.Left),
                CreateDefinition("MD-17 Anti-Tank Mines", Keys.Down, Keys.Left, Keys.Up, Keys.Up),
                CreateDefinition("MD-6 Anti-Personnel Minefield", Keys.Down, Keys.Left, Keys.Up, Keys.Right),
                CreateDefinition("MD-8 Gas Mines", Keys.Down, Keys.Left, Keys.Left, Keys.Right),
                CreateDefinition("MD-I4 Incendiary Mines", Keys.Down, Keys.Left, Keys.Left, Keys.Down),
                CreateDefinition("MG-206 Heavy Machine Gun", Keys.Down, Keys.Left, Keys.Up, Keys.Down, Keys.Down),
                CreateDefinition("MG-43 Machine Gun", Keys.Down, Keys.Left, Keys.Down, Keys.Up, Keys.Right),
                CreateDefinition("MGX-42 Bullet Storm", Keys.Down, Keys.Left, Keys.Down, Keys.Right, Keys.Up, Keys.Left),
                CreateDefinition("MLS-4X Commando", Keys.Down, Keys.Left, Keys.Up, Keys.Down, Keys.Right),
                CreateDefinition("MS-11 Solo Silo", Keys.Down, Keys.Up, Keys.Right, Keys.Down, Keys.Down),
                CreateDefinition("Hellbomb", Keys.Down, Keys.Up, Keys.Left, Keys.Down, Keys.Up, Keys.Right, Keys.Down, Keys.Up),
                CreateDefinition("Orbital 120mm HE Barrage", Keys.Right, Keys.Right, Keys.Down, Keys.Left, Keys.Right, Keys.Down),
                CreateDefinition("Orbital 380mm HE Barrage", Keys.Right, Keys.Down, Keys.Up, Keys.Up, Keys.Left, Keys.Down, Keys.Down),
                CreateDefinition("Orbital Airburst Strike", Keys.Right, Keys.Right, Keys.Right),
                CreateDefinition("Orbital EMS Strike", Keys.Right, Keys.Right, Keys.Left, Keys.Down),
                CreateDefinition("Orbital Gas Strike", Keys.Right, Keys.Right, Keys.Down, Keys.Right),
                CreateDefinition("Orbital Gatling Barrage", Keys.Right, Keys.Down, Keys.Left, Keys.Up, Keys.Up),
                CreateDefinition("Orbital Illumination Flare", Keys.Right, Keys.Right, Keys.Left, Keys.Left),
                CreateDefinition("Orbital Laser", Keys.Right, Keys.Down, Keys.Up, Keys.Right, Keys.Down),
                CreateDefinition("Orbital Napalm Barrage", Keys.Right, Keys.Right, Keys.Down, Keys.Left, Keys.Right, Keys.Up),
                CreateDefinition("Orbital Precision Strike", Keys.Right, Keys.Right, Keys.Up),
                CreateDefinition("Orbital Railcannon Strike", Keys.Right, Keys.Up, Keys.Down, Keys.Down, Keys.Right),
                CreateDefinition("Orbital Smoke Strike", Keys.Right, Keys.Right, Keys.Down, Keys.Up),
                CreateDefinition("Orbital Walking Barrage", Keys.Right, Keys.Down, Keys.Right, Keys.Down, Keys.Right, Keys.Down),
                CreateDefinition("PLAS-45 Epoch", Keys.Down, Keys.Left, Keys.Up, Keys.Left, Keys.Right),
                CreateDefinition("Prospecting Drill", Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Down, Keys.Down),
                CreateDefinition("Cargo Container", Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Right, Keys.Down),
                CreateDefinition("Reinforce", Keys.Up, Keys.Down, Keys.Right, Keys.Left, Keys.Up),
                CreateDefinition("Link Hellpods to Destroyer", Keys.Left, Keys.Right, Keys.Up, Keys.Up, Keys.Up),
                CreateDefinition("Resupply", Keys.Down, Keys.Down, Keys.Up, Keys.Right),
                CreateDefinition("RL-77 Airburst Rocket Launcher", Keys.Down, Keys.Up, Keys.Up, Keys.Left, Keys.Right),
                CreateDefinition("RS-422 Railgun", Keys.Down, Keys.Right, Keys.Down, Keys.Up, Keys.Left, Keys.Right),
                CreateDefinition("S-11 Speargun", Keys.Down, Keys.Right, Keys.Down, Keys.Left, Keys.Up, Keys.Right),
                CreateDefinition("SEAF Artillery", Keys.Right, Keys.Up, Keys.Up, Keys.Down),
                CreateDefinition("Seismic Probe", Keys.Up, Keys.Up, Keys.Left, Keys.Right, Keys.Down, Keys.Down),
                CreateDefinition("SSSD Delivery", Keys.Down, Keys.Down, Keys.Down, Keys.Down, Keys.Down, Keys.Up, Keys.Up),
                CreateDefinition("Super Earth Flag", Keys.Down, Keys.Up, Keys.Down, Keys.Up),
                CreateDefinition("SH-20 Ballistic Shield Backpack", Keys.Down, Keys.Left, Keys.Down, Keys.Down, Keys.Up, Keys.Left),
                CreateDefinition("SH-32 Shield Generator Pack", Keys.Down, Keys.Up, Keys.Left, Keys.Right, Keys.Left, Keys.Right),
                CreateDefinition("SH-51 Directional Shield", Keys.Down, Keys.Up, Keys.Left, Keys.Right, Keys.Up, Keys.Up),
                CreateDefinition("SoS Beacon", Keys.Up, Keys.Down, Keys.Right, Keys.Up),
                CreateDefinition("Super Earth Flag", Keys.Down, Keys.Up, Keys.Down, Keys.Up),
                CreateDefinition("SSSD Delivery", Keys.Down, Keys.Down, Keys.Down, Keys.Down, Keys.Down, Keys.Up, Keys.Up),
                CreateDefinition("StA-X3 W.A.S.P. Launcher", Keys.Down, Keys.Down, Keys.Up, Keys.Down, Keys.Right),
                CreateDefinition("Super Earth Flag", Keys.Down, Keys.Up, Keys.Down, Keys.Up),
                CreateDefinition("TD-220 Bastion MK XVI", Keys.Left, Keys.Down, Keys.Right, Keys.Down, Keys.Left, Keys.Down, Keys.Up, Keys.Down, Keys.Up),
                CreateDefinition("Tectonic Drill", Keys.Up, Keys.Down, Keys.Up, Keys.Down, Keys.Up, Keys.Down),
                CreateDefinition("TX-41 Sterilizer", Keys.Down, Keys.Left, Keys.Up, Keys.Down, Keys.Left),
                CreateDefinition("Upload Data", Keys.Left, Keys.Right, Keys.Up, Keys.Up, Keys.Up)
            })

            {
                builder[definition.Id] = definition;
            }

            return builder.ToImmutable();
        }

        private static StratagemDefinition CreateDefinition(string displayName, params Keys[] sequence)
        {
            var id = Regex.Replace(displayName, "[^A-Za-z0-9]+", "-").Trim('-');
            if (string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString("N");
            }

            return new StratagemDefinition(id, displayName, sequence.ToImmutableArray());
        }

        public static StratagemDefinition Default => Definitions.TryGetValue("Reinforce", out var stratagem)
            ? stratagem
            : Definitions.Values.First();

        public static IEnumerable<StratagemDefinition> GetAll() => Definitions.Values.OrderBy(s => s.DisplayName);

        public static bool TryGet(string? id, out StratagemDefinition stratagem)
        {
            if (!string.IsNullOrWhiteSpace(id) && Definitions.TryGetValue(id, out stratagem))
            {
                return true;
            }

            stratagem = Default;
            return false;
        }
    }
}
