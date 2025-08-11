using PulsarModLoader.CustomGUI;
using System.Collections.Generic;
using UnityEngine;

namespace MusicManager
{
    internal sealed class Settings : ModSettingsMenu
    {
        internal static float ChanceOfVanillaMusic = 0.1f;
        internal static bool Enabled = true;
        internal static bool CategoriesMode = false;
        internal static bool VanillaMusicEnabled = true;
        internal static Vector2 AllSongsScroll = new Vector2(0, 0);
        internal static bool CategoryOrganizationMode = false;
        internal static float Volume = 1f;
        private int _currentCategory = 0;
        private static readonly string[] CategoryNames = new string[] 
        { 
            "Combat Music",
            "Ambient Music",
            "Boss Music",
            "Warp Music"
        };
        internal static Vector2 CategoriesScroll = new Vector2(0, 0);

        public override string Name()
        {
            return "Music Manager";
        }
        public override void OnOpen()
        {
            base.OnOpen();

        }
        public override void Draw()
        {
            GUILayout.BeginHorizontal();
            Enabled = GUILayout.Toggle(Enabled, "Enabled");
            CategoriesMode = GUILayout.Toggle(CategoriesMode, "Situational Music Enabled");
            VanillaMusicEnabled = GUILayout.Toggle(VanillaMusicEnabled, "Vanilla Music Enabled");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Volume: {Volume}");
            Volume = GUILayout.HorizontalSlider(Volume, 0f, 1f);
            GUILayout.EndHorizontal();
            if (MusicManager.Instance.FinishedLoading)
            {
                GUILayout.BeginHorizontal();
                if(GUILayout.Button("Reload Song List"))
                {
                    _ = MusicManager.Instance.ReloadSongs();
                }
                CategoryOrganizationMode = GUILayout.Toggle(CategoryOrganizationMode, "Organize Songs");
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                GUILayout.Box("All Songs");
                AllSongsScroll = GUILayout.BeginScrollView(AllSongsScroll, false, true);
                for (int i = 0; i < MusicManager.Instance.AllSongs.Count; i++)
                {
                    if (GUILayout.Button($"{MusicManager.Instance.AllSongs[i].Name}"))
                    {
                        if (!CategoryOrganizationMode)
                        {
                            //Force Play a song
                        }
                        else
                        {
                            switch (_currentCategory)
                            {
                                case 0:
                                    MusicManager.Instance.AllSongs[i].IsCombatTrack = !MusicManager.Instance.AllSongs[i].IsCombatTrack;
                                    break;
                                case 1:
                                    MusicManager.Instance.AllSongs[i].IsAmbientMusic = !MusicManager.Instance.AllSongs[i].IsAmbientMusic;
                                    break;
                                case 2:
                                    MusicManager.Instance.AllSongs[i].IsBossMusic = !MusicManager.Instance.AllSongs[i].IsBossMusic;
                                    break;
                                case 3:
                                    MusicManager.Instance.AllSongs[i].IsWarpMusic = !MusicManager.Instance.AllSongs[i].IsWarpMusic;
                                    break;
                            }
                            //[JsonProperty]
                            //internal bool IsCombatTrack;
                            //[JsonProperty]
                            //internal bool IsAmbientMusic;
                            //[JsonProperty]
                            //internal bool IsBossMusic;
                            //[JsonProperty]
                            //internal bool IsWarpMusic;
                        }
                    }
                }
                GUILayout.EndScrollView();
                GUILayout.EndVertical();

                if (CategoryOrganizationMode)
                {
                    GUILayout.BeginVertical();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("<-"))
                    {
                        CategoriesScroll = new Vector2(0, 0);
                        if (_currentCategory == 0)
                        {
                            _currentCategory = 3;
                        }
                        _currentCategory--;
                    }
                    GUILayout.Box($"{CategoryNames[_currentCategory]}");
                    if (GUILayout.Button("->"))
                    {
                        CategoriesScroll = new Vector2(0, 0);
                        if (_currentCategory == 3)
                        {
                            _currentCategory = 0;
                        }
                        _currentCategory++;
                    }
                    GUILayout.EndHorizontal();

                    CategoriesScroll = GUILayout.BeginScrollView(CategoriesScroll, false, true);
                    List<SongInfo> categorySongs = MusicManager.Instance.AllSongs.FindAll(song =>
                    {
                        switch (_currentCategory)
                        {
                            default:
                                return song.IsCombatTrack;
                            case 1:
                                return song.IsAmbientMusic;
                            case 2:
                                return song.IsBossMusic;
                            case 3:
                                return song.IsWarpMusic;
                        }
                    });
                    for (int i = 0; i < categorySongs.Count; i++)
                    {
                        switch (_currentCategory)
                        {
                            case 0:
                                categorySongs[i].IsCombatTrack = !categorySongs[i].IsCombatTrack;
                                break;
                            case 1:
                                categorySongs[i].IsAmbientMusic = !categorySongs[i].IsAmbientMusic;
                                break;
                            case 2:
                                categorySongs[i].IsBossMusic = !categorySongs[i].IsBossMusic;
                                break;
                            case 3:
                                categorySongs[i].IsWarpMusic = !categorySongs[i].IsWarpMusic;
                                break;
                        }
                    }
                    GUILayout.EndScrollView();
                    GUILayout.EndVertical();
                }
                GUILayout.EndHorizontal();
            }
            
        }
    }
}

