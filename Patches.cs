using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using VLB;
using UnityEngine;

namespace MusicManager
{
    [HarmonyPatch(typeof(PLUIMainMenu), "Start")]
    class CreateMusicManager
    {
        static void Postfix()
        {
            Debug.Log("Ran MusicManager adder");
            PLNetworkManager.Instance.gameObject.AddComponent<MusicManager>();
        }
    }
    [HarmonyPatch(typeof(PLMusic), "PlayMusic")]
    class MusicPatch
    {
        static bool Prefix(string inMusicString, bool isCombatTrack, bool isPlanetTrack, bool isSpecialTrack, bool isLoopingTrack)
        {
            Debug.Log($"Ran PlayMusic: {MusicManager.Instance.StartVanillaMusic}, {MusicManager.Instance.PlayingVanillaMusic}");
            if (!Settings.Enabled)
            {
                MusicManager.Instance.PlayingVanillaMusic = true;
                return true;
            }
            isLoopingTrack = false;
            if (MusicManager.Instance.StartVanillaMusic)
            {
                MusicManager.Instance.PlayingVanillaMusic = true;
            }
            else
            {
                if (!MusicManager.Instance.PlayingVanillaMusic)
                {
                    PLMusic.Instance.m_CurrentPlayingMusicEventString = inMusicString;
                }
                PLMusic.Instance.m_CombatMusicPlaying = isCombatTrack;
                PLMusic.Instance.m_PlanetMusicPlaying = isPlanetTrack;
                PLMusic.Instance.m_LoopingMusicPlaying = false;
                PLMusic.Instance.m_SpecialMusicPlaying = isSpecialTrack;
            }
            return MusicManager.Instance.StartVanillaMusic;
        }
    }
    [HarmonyPatch(typeof(PLMusic), "OnMusicCallback")]
    class MusicEndPatch
    {
        static void Prefix(object in_cookie, AkCallbackType in_type, object in_info)
        {
            MusicManager.Instance.PlayingVanillaMusic = false;
            MusicManager.Instance.VanillaMusicHasEnded = true;
            Debug.Log($"Ran Callback: {MusicManager.Instance.EndVanillaMusic}, {MusicManager.Instance.PlayingVanillaMusic}");
        }
    }
    [HarmonyPatch(typeof(PLMusic), "StopCurrentMusic")]
    class StopMusicPatch
    {
        static bool Prefix()
        {
            Debug.Log($"Ran Stop: {MusicManager.Instance.EndVanillaMusic}, {MusicManager.Instance.PlayingVanillaMusic}");
            if (!Settings.Enabled)
            {
                return true;
            }
            if (MusicManager.Instance.EndVanillaMusic)
            {
                MusicManager.Instance.PlayingVanillaMusic = false;
            }
            return MusicManager.Instance.EndVanillaMusic;
        }
    }
    [HarmonyPatch(typeof(PLGlobal), "OnApplicationQuit")]
    class QuitSavePatch
    {
        static void Prefix()
        {
            MusicManager.Instance.OutputAllJson();
        }
    }

}
