using System;
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

namespace MusicManager
{
    
    internal sealed class MusicManager : MonoBehaviour
    {
        internal static MusicManager Instance;
        private List<AudioClip> songs = new List<AudioClip>();
        private UnityEngine.AudioSource source;
        internal float MusicPlayTime = 0;
        internal bool EndVanillaMusic = false;
        internal bool StartVanillaMusic = false;
        internal bool VanillaMusicHasEnded = false;
        internal bool PlayingVanillaMusic = false;
        internal SongCategoryData SongClassification = new SongCategoryData();
        void Awake()
        {
            Instance = this;
            source = gameObject.AddComponent<UnityEngine.AudioSource>();
            GetSongsFromFolder();
            for (int i = 0; i < songs.Count; i++)
            {
                SongClassification.AddSong(songs[i]);
            }
        }
        void Update()
        {
            if (Settings.Enabled)
            {
                if (!PlayingVanillaMusic)
                {
                    if (source != null)
                    {
                        if (!source.isPlaying)
                        {
                            PlayNext();
                        }
                    }
                }
                if (!PLNetworkManager.Instance.IsTyping && PLInput.Instance.GetButtonDown("MusicMenu"))
                {
                    if (songs.Count > 0)
                    {
                        source.clip = songs[UnityEngine.Random.Range(0, songs.Count - 1)];
                    }
                }
            }
            
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
                    if (songs.Count > 0)
                    {
                        source.clip = songs[UnityEngine.Random.Range(0, songs.Count - 1)];
                        source.Play();
                        PLMusic.Instance.PlayMusic("Modded", false, false, false, false);
                    }
                }
            }
        }
        void StopVanillaMusic()
        {
            MusicManager.Instance.EndVanillaMusic = true;
            PLMusic.Instance.StopCurrentMusic();
            MusicManager.Instance.EndVanillaMusic = false;
        }
        void PlayVanillaMusic()
        {
            StartVanillaMusic = true;
            SongClassification.VanillaSongInfos[UnityEngine.Random.Range(0, SongClassification.VanillaSongInfos.Count - 1)].PlaySong();
            StartVanillaMusic = false;
        }
        
        private void GetSongsFromFolder()
        {
            FileInfo[] songFiles = Mod.Instance.MusicDirectory.GetFiles("*.*");
            foreach (FileInfo song in songFiles)
            {
                if (!Mod.Instance.Songs.Contains(song))
                {
                    Mod.Instance.Songs.Add(song);
                    StartCoroutine(ConvertFileToAudioClip(song));
                }
            }
        }
        IEnumerator ConvertFileToAudioClip(FileInfo songFile)
        {
            string[] songInfo = songFile.Name.Split(new char[] { '.' }, 2);
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
                    Debug.Log("Not Supported AudioFile type");
                    break;
            }
            if (type != 0)
            {
                //string url = string.Format("file://{0}", songFile.Name);
                using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(songFile.FullName, type))
                {
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.ConnectionError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        AudioClip addedSong = DownloadHandlerAudioClip.GetContent(www);
                        addedSong.name = songFile.Name;
                        songs.Add(addedSong);
                    }
                }
            }
        }
    }
}
