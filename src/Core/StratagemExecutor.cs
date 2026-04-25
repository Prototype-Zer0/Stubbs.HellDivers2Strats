//Currently this only works on Windows but I will work on making it work on MacOS
// Linux, BSD, TempleOS... Etc.
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using SuchByte.MacroDeck.Logging;
using System.Runtime.InteropServices;

namespace Prototype.HellDivers2Strats.Core
{
    public static class StratagemExecutor
    {
        // Random number generator for realistic timing
        private static readonly Random Random = new Random();

        // Timing constants for human-like behavior
        private const int MinKeyPressTime = 50;   // Minimum time to hold a key (ms)
        private const int MaxKeyPressTime = 100;  // Maximum time to hold a key (ms)
        private const int MinBetweenPresses = 30; // Minimum delay between keys (ms)
        private const int MaxBetweenPresses = 70; // Maximum delay between keys (ms)

        // Sync object for thread safety
        private static readonly object SyncRoot = new();

        // Key mappings (virtual key codes, scan codes, and extended key flags)
        private static readonly Dictionary<Keys, KeySpec> KeyMap = new()
        {
            { Keys.Up,    new KeySpec(0x26, 0x48, true) },
            { Keys.Down,  new KeySpec(0x28, 0x50, true) },
            { Keys.Left,  new KeySpec(0x25, 0x4B, true) },
            { Keys.Right, new KeySpec(0x27, 0x4D, true) }
        };

        // Control key specification (non-extended)
        private static readonly KeySpec LeftControlKey = new KeySpec(0xA2, 0x1D, false);

        public static void Execute(StratagemDefinition stratagem)
        {
            if (stratagem == null)
                throw new ArgumentNullException(nameof(stratagem));

            lock (SyncRoot)
            {
                // Initial random delay (human-like start)
                // The reason we are doing this because Microsoft with
                // their infinite wisdom decided their keyboard
                // input API is not designed for simulated presses.
                //TODO: MAKE THIS OS AGNOSTIC
                Thread.Sleep(Random.Next(0, 150));

                // Press and hold Ctrl
                KeyEvent(LeftControlKey, false);

                // Execute each key in sequence with realistic timing
                foreach (var key in stratagem.Sequence)
                {
                    if (!KeyMap.TryGetValue(key, out var spec)) continue;

                    // Random delay between keys (slightly varied timing)
                    Thread.Sleep(Random.Next(MinBetweenPresses, MaxBetweenPresses));

                    // Press and hold the key
                    KeyEvent(spec, false);

                    // Hold key for a random duration
                    Thread.Sleep(Random.Next(MinKeyPressTime, MaxKeyPressTime));

                    // Release key
                    KeyEvent(spec, true);
                }

                // Random delay before releasing Ctrl
                Thread.Sleep(Random.Next(MinBetweenPresses, MaxBetweenPresses));

                // Release Ctrl
                KeyEvent(LeftControlKey, true);
            }
        }

        private static void KeyEvent(KeySpec spec, bool keyUp)
        {
            uint flags = spec.Extended ? KEYEVENTF_EXTENDEDKEY : 0;

            // Set flag for key release
            if (keyUp)
                flags |= KEYEVENTF_KEYUP;

            // Send simulated key event
            keybd_event(spec.VirtualKey, spec.ScanCode, flags, UIntPtr.Zero);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        // Constants for key event flags
        private const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        // Key specification struct
        private readonly struct KeySpec
        {
            public KeySpec(byte virtualKey, byte scanCode, bool extended)
            {
                VirtualKey = virtualKey;
                ScanCode = scanCode;
                Extended = extended;
            }

            public byte VirtualKey { get; }
            public byte ScanCode { get; }
            public bool Extended { get; }
        }
    }
}
