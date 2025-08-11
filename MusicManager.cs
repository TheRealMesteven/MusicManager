﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.CompilerServices;
using System.DirectoryServices;
using Mono.Security.X509.Extensions;
using System.Threading;
using HarmonyLib;
using ExitGames.Client.Photon;

namespace MusicManager
{
    
    internal sealed class MusicManager : MonoBehaviour
    {
        internal static MusicManager Instance;
        private UnityEngine.AudioSource source;
        internal float MusicPlayTime = 0;
        internal bool EndVanillaMusic = false;
        internal bool StartVanillaMusic = false;
        internal bool VanillaMusicHasEnded = false;
        internal bool PlayingVanillaMusic = false;
        internal bool FinishedLoading = false;
        private AudioClip NextSong;
        private CancellationTokenSource songLoaderCancelation;
        private Task SongLoader;
        internal List<SongCategoryData> SongData = new List<SongCategoryData>();
        internal List<SongInfo> AllSongs = new List<SongInfo>();
        internal List<VanillaSongInfo> VanillaSongInfos = new List<VanillaSongInfo>()
        {
             VanillaSongInfo.CreateVanillaSong("mx_corrupteddrone_lp2", true, false, true, true),
             VanillaSongInfo.CreateVanillaSong("mx_finalstand", true, false, true, true),
             VanillaSongInfo.CreateVanillaSong("mx_boarders", true, false, true, true),
             VanillaSongInfo.CreateVanillaSong("mx_lasttogo", true, false, true, true),
             VanillaSongInfo.CreateVanillaSong("mx_infected_commander", true, false, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_lostcolony_theme_two", true, true, true, true),
             VanillaSongInfo.CreateVanillaSong("mx_colunion_v4", false, false, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_wd_corp_v3", false, false, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_AllGent_Heist", false, false, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_cu_commander", true, true, true, false),
             VanillaSongInfo.CreateVanillaSong("mx_infected_ambient", false, false, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_WDCommander_lp", true, true, true, false),
             VanillaSongInfo.CreateVanillaSong("mx_ambient_3_full", false, false, true, false),
             VanillaSongInfo.CreateVanillaSong("mx_lostcolony_theme_three", true, true, true, true),
             VanillaSongInfo.CreateVanillaSong("mx_lostcolony_theme_one", true, true, true, false),
             VanillaSongInfo.CreateVanillaSong("mx_ivm_genamb01", false, false, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_ivm_genamb02", false, false, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_CUAmbient_lp", false, true, true, false),
             VanillaSongInfo.CreateVanillaSong("mx_CUExplore_lp", false, true, true, false),
             VanillaSongInfo.CreateVanillaSong("mx_abyss_ambient_1", false, false, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_abyss_ambient_2", false, false, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_abyss_ambient_3", false, false, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_abyss_ambient_4", false, false, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_Tavern", false, true, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_ThrivingStation", false, true, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_Caverns", false, true, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_Disaster", false, true, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_CorneliaStation", false, true, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_AlienRuins", false, true, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_Lost", false, true, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_ivm_wreck", false, true, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_wdce_explore_lp2", false, true, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_Desert", false, true, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_ambient_1"),
             VanillaSongInfo.CreateVanillaSong("mx_ambient_2"),
             VanillaSongInfo.CreateVanillaSong("mx_ambient_3_loop"),
             VanillaSongInfo.CreateVanillaSong("mx_ivm_genamb01"),
             VanillaSongInfo.CreateVanillaSong("mx_ivm_genamb02"),
             VanillaSongInfo.CreateVanillaSong("mx_unseen_amb"),
             VanillaSongInfo.CreateVanillaSong("mx_finalstand"),
             VanillaSongInfo.CreateVanillaSong("mx_gap"),
             VanillaSongInfo.CreateVanillaSong("mx_boarders"),
             VanillaSongInfo.CreateVanillaSong("mx_lasttogo"),
             VanillaSongInfo.CreateVanillaSong("mx_CUAttack", true),
             VanillaSongInfo.CreateVanillaSong("mx_CUAttackAlt", true),
             VanillaSongInfo.CreateVanillaSong("mx_drone_attack_01_lp", true),
             VanillaSongInfo.CreateVanillaSong("mx_drone_attack_02_lp", true),
             VanillaSongInfo.CreateVanillaSong("mx_wd_attack_lp", true),
             VanillaSongInfo.CreateVanillaSong("mx_corrupteddrone_lp2", true),
             VanillaSongInfo.CreateVanillaSong("mx_AllGent_Attack", true),
             VanillaSongInfo.CreateVanillaSong("mx_AllGent_Heist", true),
             VanillaSongInfo.CreateVanillaSong("mx_infected_attack", true),
             VanillaSongInfo.CreateVanillaSong("mx_infected_commander", true),
             VanillaSongInfo.CreateVanillaSong("mx_AllGent_Commander", true),
             VanillaSongInfo.CreateVanillaSong("mx_FluffyBiscuit_Ambient", true),
             VanillaSongInfo.CreateVanillaSong("mx_FluffyBiscuitTheme_FullLength", true),
             VanillaSongInfo.CreateVanillaSong("mx_Polytechnic_Attack", true),
             VanillaSongInfo.CreateVanillaSong("mx_Polytechnic_MainTheme", true),
             VanillaSongInfo.CreateVanillaSong("mx_Polytechnic_SectorCommander", true),
             VanillaSongInfo.CreateVanillaSong("mx_unseen_action1", true),
             VanillaSongInfo.CreateVanillaSong("mx_unseen_action1", true),
             VanillaSongInfo.CreateVanillaSong("mx_colunion_v4"),
             VanillaSongInfo.CreateVanillaSong("mx_CUExplore_lp"),
             VanillaSongInfo.CreateVanillaSong("mx_drone_ambient_01_lp"),
             VanillaSongInfo.CreateVanillaSong("mx_drone_ambient_02_lp"),
             VanillaSongInfo.CreateVanillaSong("mx_AllGent_ExploreLP"),
             VanillaSongInfo.CreateVanillaSong("mx_AllGent_v4"),
             VanillaSongInfo.CreateVanillaSong("mx_infected_ambient"),
             VanillaSongInfo.CreateVanillaSong("mx_infected_commander"),
             VanillaSongInfo.CreateVanillaSong("mx_wdce_explore_lp2"),
             VanillaSongInfo.CreateVanillaSong("mx_unseen_amb"),
             VanillaSongInfo.CreateVanillaSong("mx_wd_corp_v3"),
             VanillaSongInfo.CreateVanillaSong("mx_Polytechnic_Ambient"),
             VanillaSongInfo.CreateVanillaSong("mx_Polytechnic_Exploration"),
             VanillaSongInfo.CreateVanillaSong("mx_unseen_amb"),
             VanillaSongInfo.CreateVanillaSong("mx_FluffyBiscuitTheme_FullLength"),
             VanillaSongInfo.CreateVanillaSong("mx_CUAttackAlt", true, true, true, false),
             VanillaSongInfo.CreateVanillaSong("mx_ivm_darkness", false, true, false, false),
             VanillaSongInfo.CreateVanillaSong("mx_unseen_action2", true, false, true, true),
             VanillaSongInfo.CreateVanillaSong("mx_unseen_action1", false, false, false, true),
             VanillaSongInfo.CreateVanillaSong("mx_AllGent_Commander", true, true, true, false),
             VanillaSongInfo.CreateVanillaSong("mx_warpguardian_theme_one", true, false, true, true),
             VanillaSongInfo.CreateVanillaSong("mx_warpguardian_theme_two", true, false, true, true)
        };

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }
            Instance = this;
            source = gameObject.AddComponent<UnityEngine.AudioSource>();
            source.loop = false;
            source.volume = Settings.Volume.Value;
            SongData.Add(new SongCategoryData(Mod.Instance.MusicDirectory));
            foreach (DirectoryInfo directory in Mod.Instance.MusicSubDirectories)
            {
                SongData.Add(new SongCategoryData(directory));
            }
            _ = GetSongsFromFolder();
            PrepNextSong();
        }
        void Update()
        {
            if (!Settings.Enabled || source == null) return;

            if (!PlayingVanillaMusic)
            {
                if (!source.isPlaying)
                {
                    PlayNext();
                }
            }
            if (Settings.IsOpen)
            {
                source.volume = Settings.Volume.Value;
            }

            //if (!PLNetworkManager.Instance.IsTyping && PLInput.Instance.GetButtonDown("MusicMenu"))
            //{
            //    if (songs.Count > 0)
            //    {
            //        source.clip = songs[UnityEngine.Random.Range(0, songs.Count - 1)];
            //    }
            //}
        }

        void StopLoadingNextSong()
        {
            songLoaderCancelation.Cancel();
        }
        void PlayNext()
        {
            if (PlayingVanillaMusic)
            {
                StopVanillaMusic();
            }
            if (!Settings.CategoriesMode)
            {
                if (Settings.VanillaMusicEnabled && UnityEngine.Random.Range(0f, 1f) < Settings.ChanceOfVanillaMusic)
                {
                    PlayVanillaMusic();
                }
                else
                {
                    PlayModdedSong();
                }
            }
        }
        void StopVanillaMusic()
        {
            this.EndVanillaMusic = true;
            PLMusic.Instance.StopCurrentMusic();
            this.EndVanillaMusic = false;
        }
        private void PlayVanillaMusic()
        {
            this.StartVanillaMusic = true;
            this.VanillaSongInfos[UnityEngine.Random.Range(0, VanillaSongInfos.Count - 1)].PlaySong();
            this.StartVanillaMusic = false;
        }
        void PlayModdedSong()
        {
            if (NextSong != null)
            {
                source.clip = NextSong;
                source.Play();
            }
            PrepNextSong();
        }
        private void PrepNextSong()
        {
            if (SongLoader == null || (SongLoader != null && SongLoader.IsCompleted))
            {
                songLoaderCancelation = new CancellationTokenSource();
                bool[] bools = new bool[4];
                if (Settings.CategoriesMode)
                {
                    bools[0] = PLMusic.Instance.m_CombatMusicPlaying && !PLMusic.Instance.m_SpecialMusicPlaying;
                    bools[1] = !PLMusic.Instance.m_CombatMusicPlaying && !PLMusic.Instance.m_SpecialMusicPlaying;
                    bools[2] = PLMusic.Instance.m_CombatMusicPlaying && PLMusic.Instance.m_SpecialMusicPlaying;
                    if (PLEncounterManager.Instance.PlayerShip != null)
                    {
                        bools[3] = PLEncounterManager.Instance.PlayerShip.InWarp;
                    }
                    if (bools[3])
                    {
                        bools[0] = false;
                        bools[1] = false;
                        bools[2] = false;
                    }
                }
                bool Category = Settings.CategoriesMode;
                List<SongInfo> tempSongCategories = new List<SongInfo>();
                tempSongCategories.AddRange(AllSongs);
                SongLoader = Task.Run(async () =>
                {
                    await LoadNextSong(songLoaderCancelation.Token, tempSongCategories, Category, bools[0], bools[1], bools[2], bools[3]);
                });
                //Debug.Log("Started Prep");
                //Gotta Run it not in the Threadpool for debugging purposes
                //SongLoader = LoadNextSong(songLoaderCancelation.Token, tempSongCategories, Category, bools[0], bools[1], bools[2], bools[3]);
            }
        }
        private async Task GetSongsFromFolder()
        {
            await Task.Yield();
            Task[] SongFileInitializers = new Task[SongData.Count];
            for (int i = 0; i < SongData.Count; i++)
            {
                int temp = i;
                SongFileInitializers[i] = Task.Run(async () => await GetSongsFromDirectory(SongData[temp]));
            }
            await Task.WhenAll(SongFileInitializers);
            for (int i = 0; i < SongData.Count;i++)
            {
                AllSongs.AddRange(SongData[i].AddedSongs);
            }
            AllSongs.AsParallel().Do(song =>
            {
                if (song.Name.Contains('.'))
                {
                    song.Name = song.Name.Split(new char[] { '.' }, 2)[0];
                }
            });
            FinishedLoading = true;
            //for(int i = 0; i < songFiles.Length; i++)
            //{
            //    if (!Mod.Instance.Songs.Contains(songFiles[i]))
            //    {
            //        Mod.Instance.Songs.Add(songFiles[i]);
            //        StartCoroutine(ConvertFileToAudioClip(songFiles[i]));
            //    }
            //    yield return null;
            //}
            //for (int i = 0; i < songs.Count; i++)
            //{
            //    SongClassification.AddSong(songs[i]);
            //    yield return null;
            //}
            //yield return null;
        }
        private async Task GetSongsFromDirectory(SongCategoryData data)
        {
            await Task.Yield();
            FileInfo[] songFiles = data.Directory.GetFiles("*.*");
            for(int i = 0; i < songFiles.Length;i++)
            {
                if (!songFiles[i].Name.Contains(".json"))
                {
                    data.AddSong(songFiles[i]);
                }
                await Task.Yield();
            }
        }
        private async Task LoadNextSong(CancellationToken cancellationToken, List<SongInfo> WorkingSongData, bool categories, bool combat, bool ambient, bool boss, bool warp)
        {
            await Task.Yield();
            while (!FinishedLoading)
            {
                await Task.Delay(100);
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }
            //Debug.Log("FinishedLoading");
            if (cancellationToken.IsCancellationRequested)
            {
                return; 
            }
            if (WorkingSongData.Count < 1)
            {
                return;
            }
            List<SongInfo> songInfos = WorkingSongData;
            //Debug.Log($"{songInfos.Count}");
            //for (int i = 0; i < WorkingSongData.Count; i++)
            //{
            //    songInfos.AddRange(WorkingSongData[i].AddedSongs);
            //}
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            if (categories)
            {
                //Debug.Log("Sorting Categories");
                List<SongInfo> tempSongInfos = WorkingSongData.FindAll(song => (song.IsCombatTrack && combat) && (song.IsAmbientMusic && ambient) && (song.IsBossMusic && boss) && (song.IsWarpMusic && warp));
                if (tempSongInfos.Count > 0)
                {
                    songInfos = tempSongInfos;
                }
                Debug.Log($"{songInfos.Count}");
            }
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            //Debug.Log("Calling LoadAudioFile");
            await LoadAudioFile(songInfos[new System.Random().Next(songInfos.Count)], cancellationToken);
        }
        private async Task LoadAudioFile(SongInfo song, CancellationToken cancellationToken)
        {
            await Task.Yield();
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            //Debug.Log("Running LoadAudioFile");
            string[] songInfo = song.song.Name.Split(new char[] { '.' }, 2);
            string songName = songInfo[0];
            string fileType = songInfo[1];
            AudioType type = (AudioType)0;
            switch (fileType)
            {
                case "mp3":
                case "mp2":
                    type = AudioType.MPEG;
                    break;
                case "wav":
                    type = AudioType.WAV;
                    break;
                case "ogg":
                    type = AudioType.OGGVORBIS;
                    break;
                case "aiff":
                    type = AudioType.AIFF;
                    break;
                case "aac":
                    type = AudioType.ACC;
                    break;
                default:
                    //Debug.Log("Not Supported AudioFile type");
                    break;
            }
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            if (type != 0)
            {
                //string url = string.Format("file://{0}", songFile.Name);
                using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(song.song.FullName, type))
                {
                    www.SendWebRequest();
                    //Debug.Log("Awaiting Completion of load");
                    while (!www.isDone)
                    {
                        await Task.Delay(100);
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }
                    }
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                    if (www.result == UnityWebRequest.Result.ConnectionError)
                    {
                        //Debug.Log(www.error);
                    }
                    else
                    {
                        AudioClip addedSong = DownloadHandlerAudioClip.GetContent(www);
                        addedSong.name = song.Name;
                        NextSong = addedSong;
                    }

                }
            }
        }
        internal void OutputAllJson()
        {
            for (int i = 0; i < MusicManager.Instance.SongData.Count; i++)
            {
                MusicManager.Instance.SongData[i].OutputJson();
            }
        }
        private void ReloadDirectories()
        {
            SongData.Clear();
            SongData.Add(new SongCategoryData(Mod.Instance.MusicDirectory));
            foreach (DirectoryInfo directory in Mod.Instance.MusicSubDirectories)
            {
                SongData.Add(new SongCategoryData(directory));
            }
        }
        internal async Task ReloadSongs()
        {
            FinishedLoading = false;
            OutputAllJson();
            AllSongs.Clear();
            ReloadDirectories();
            await GetSongsFromFolder();
            FinishedLoading = true;
        }
        //IEnumerator ConvertFileToAudioClip(FileInfo songFile)
        //{
        //    string[] songInfo = songFile.Name.Split(new char[] { '.' }, 2);
        //    string songName = songInfo[0];
        //    string fileType = songInfo[1];
        //    AudioType type = (AudioType)0;
        //    switch (fileType) 
        //    {
        //        case "mp3":
        //        case "mp2":
        //            type = AudioType.MPEG;
        //            break;
        //        case "wav":
        //            type = AudioType.WAV;
        //            break;
        //        case "ogg":
        //            type = AudioType.OGGVORBIS;
        //            break;
        //        case "aiff":
        //            type = AudioType.AIFF;
        //            break;
        //        case "aac":
        //            type = AudioType.ACC;
        //            break;
        //        default:
        //            Debug.Log("Not Supported AudioFile type");
        //            break;
        //    }
        //    if (type != 0)
        //    {
        //        //string url = string.Format("file://{0}", songFile.Name);
        //        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(songFile.FullName, type))
        //        {
        //            yield return www.SendWebRequest();

        //            if (www.result == UnityWebRequest.Result.ConnectionError)
        //            {
        //                Debug.Log(www.error);
        //            }
        //            else
        //            {
        //                AudioClip addedSong = DownloadHandlerAudioClip.GetContent(www);
        //                addedSong.name = songFile.Name;
        //                songs.Add(addedSong);
        //            }
        //        }
        //    }
        //}
    }
}
