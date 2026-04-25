using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using SuchByte.MacroDeck.Logging;

namespace Prototype.HellDivers2Strats.Core
{
    public static class StratagemExecutor
    {
        private const int DelayBetweenPressesMs = 40; // Reduced delay between keys
        private const int KeyPressDurationMs = 50;    // Time to hold each key

        private static readonly object SyncRoot = new();

        private static readonly Dictionary<Keys, KeySpec> KeyMap = new()
        {
            [Keys.Up] = new KeySpec(0x26, 0x48, true),
            [Keys.Down] = new KeySpec(0x28, 0x50, true),
            [Keys.Left] = new KeySpec(0x25, 0x4B, true),
            [Keys.Right] = new KeySpec(0x27, 0x4D, true),
        };

        private static readonly KeySpec LeftControlKey = new KeySpec(0xA2, 0x1D, false);

        public static void Execute(StratagemDefinition stratagem)
        {
            if (stratagem is null)
                throw new ArgumentNullException(nameof(stratagem));

            lock (SyncRoot)
            {
                // Press and HOLD Ctrl immediately (no initial delay)
                KeyEvent(LeftControlKey, false); // Key Down (hold Ctrl)

                // Press all keys in sequence while Ctrl is held
                foreach (var key in stratagem.Sequence)
                {
                    if (!KeyMap.TryGetValue(key, out var spec)) continue;

                    KeyEvent(spec, false); // Key Down
                    Thread.Sleep(KeyPressDurationMs); // Hold key for duration
                    KeyEvent(spec, true);  // Key Up
                    Thread.Sleep(DelayBetweenPressesMs); // Short delay between keys
                }

                // Release Ctrl LAST (after all keys are done)
                KeyEvent(LeftControlKey, true); // Key Up (release Ctrl)
            }
        }

        private static void KeyEvent(KeySpec spec, bool keyUp)
        {
            var flags = spec.Extended ? KEYEVENTF_EXTENDEDKEY : 0;
            if (keyUp)
                flags |= KEYEVENTF_KEYUP;

            keybd_event(spec.VirtualKey, spec.ScanCode, flags, UIntPtr.Zero);
        }

        private const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        private const uint KEYEVENTF_KEYUP = 0x0002;

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

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
    }
}
