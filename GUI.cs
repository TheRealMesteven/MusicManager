using PulsarModLoader.CustomGUI;
using UnityEngine;

namespace MusicManager
{
    internal class Settings : ModSettingsMenu
    {
        internal static float ChanceOfVanillaMusic = 0.1f;
        internal static bool Enabled = true;
        internal static bool CategoriesMode = false;
        internal static bool VanillaMusicEnabled = true;
        public override string Name()
        {
            return "template mod settings menu name";
        }
        public override void Draw()
        {
            Enabled = GUILayout.Toggle(Enabled, "Enabled");
            CategoriesMode = GUILayout.Toggle(CategoriesMode, "Situational Music Enabled");
            VanillaMusicEnabled = GUILayout.Toggle(VanillaMusicEnabled, "Vanilla Music Enabled");
        }
    }
}

