using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace MusicManager
{
    [JsonObject(MemberSerialization.OptIn)]
    internal sealed class SongInfo
    {
        public SongInfo(string name, bool combat = false, bool ambient = false, bool boss = false, bool warp = false)
        {
            Name = name;
            IsCombatTrack = combat;
            IsAmbientMusic = ambient;
            IsBossMusic = boss;
            IsWarpMusic = warp;
        }
        [JsonProperty]
        internal string Name;
        [JsonProperty]
        internal bool IsCombatTrack;
        [JsonProperty]
        internal bool IsAmbientMusic;
        [JsonProperty]
        internal bool IsBossMusic;
        [JsonProperty]
        internal bool IsWarpMusic;
        [JsonIgnore]
        internal FileInfo song = null;
    }
    internal sealed class VanillaSongInfo
    {
        internal static VanillaSongInfo CreateVanillaSong(string name, bool combat = false, bool planet = false, bool special = false, bool looping = false)
        {
            VanillaSongInfo song = new VanillaSongInfo(name.Substring(3));
            if (!combat && !special && !looping)
            {
                song.IsAmbientMusic = true;
            }
            song.IsBossMusic = special && combat;
            song.IsWarpMusic = false;
            song.IsCombatTrack = combat;
            return song;
        }
        internal VanillaSongInfo(string name, bool combat = false, bool ambient = false, bool boss = false, bool warp = false)
        {
            this.Name = name;
            IsCombatTrack = combat;
            IsAmbientMusic = ambient;
            IsBossMusic = boss;
            IsWarpMusic = warp;
        }
        internal string Name;
        internal bool IsCombatTrack;
        internal bool IsAmbientMusic;
        internal bool IsBossMusic;
        internal bool IsWarpMusic;
        private static readonly string Prefix = "mx_";
        internal void PlaySong()
        {
            PLMusic.Instance.PlayMusic(string.Concat(Prefix, Name), false, false, false, false);
        }
    }
}
