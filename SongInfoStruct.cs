using Beebyte.Obfuscator;
using Crosstales.BWF.Data;
using Newtonsoft.Json;
using Oculus.Platform.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MusicManager
{
    internal sealed class SongCategoryData
    {
        internal SongCategoryData()
        {
            FileInfo[] files = Mod.Instance.MusicDirectory.GetFiles("*.json");
            FileInfo jsonFile = null;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Exists && files[i].Name.StartsWith("SongCategoryInfo"))
                {
                    jsonFile = files[i];
                }
            }

            if (jsonFile != null && jsonFile.Exists)
            {
                using (StreamReader r = new StreamReader(jsonFile.FullName))
                {
                    string json = r.ReadToEnd();
                    AddedSongs = JsonConvert.DeserializeObject<List<SongInfo>>(json);
                }
            }
        }
        internal List<VanillaSongInfo> VanillaSongInfos = new List<VanillaSongInfo>()
        {
             VanillaSongInfo("mx_corrupteddrone_lp2", true, false, true, true),
             VanillaSongInfo("mx_finalstand", true, false, true, true),
             VanillaSongInfo("mx_boarders", true, false, true, true),
             VanillaSongInfo("mx_lasttogo", true, false, true, true),
             VanillaSongInfo("mx_infected_commander", true, false, false, false),
             VanillaSongInfo("mx_lostcolony_theme_two", true, true, true, true),
             VanillaSongInfo("mx_colunion_v4", false, false, false, false),
             VanillaSongInfo("mx_wd_corp_v3", false, false, false, false),
             VanillaSongInfo("mx_AllGent_Heist", false, false, false, false),
             VanillaSongInfo("mx_cu_commander", true, true, true, false),
             VanillaSongInfo("mx_infected_ambient", false, false, false, false),
             VanillaSongInfo("mx_WDCommander_lp", true, true, true, false),
             VanillaSongInfo("mx_ambient_3_full", false, false, true, false),
             VanillaSongInfo("mx_lostcolony_theme_three", true, true, true, true),
             VanillaSongInfo("mx_lostcolony_theme_one", true, true, true, false),
             VanillaSongInfo("mx_ivm_genamb01", false, false, false, false),
             VanillaSongInfo("mx_ivm_genamb02", false, false, false, false),
             VanillaSongInfo("mx_CUAmbient_lp", false, true, true, false),
             VanillaSongInfo("mx_CUExplore_lp", false, true, true, false),
             VanillaSongInfo("mx_abyss_ambient_1", false, false, false, false),
             VanillaSongInfo("mx_abyss_ambient_2", false, false, false, false),
             VanillaSongInfo("mx_abyss_ambient_3", false, false, false, false),
             VanillaSongInfo("mx_abyss_ambient_4", false, false, false, false),
             VanillaSongInfo("mx_Tavern", false, true, false, false),
             VanillaSongInfo("mx_ThrivingStation", false, true, false, false),
             VanillaSongInfo("mx_Caverns", false, true, false, false),
             VanillaSongInfo("mx_Disaster", false, true, false, false),
             VanillaSongInfo("mx_CorneliaStation", false, true, false, false),
             VanillaSongInfo("mx_AlienRuins", false, true, false, false),
             VanillaSongInfo("mx_Lost", false, true, false, false),
             VanillaSongInfo("mx_ivm_wreck", false, true, false, false),
             VanillaSongInfo("mx_wdce_explore_lp2", false, true, false, false),
             VanillaSongInfo("mx_Desert", false, true, false, false),
             VanillaSongInfo("mx_ambient_1"),
             VanillaSongInfo("mx_ambient_2"),
             VanillaSongInfo("mx_ambient_3_loop"),
             VanillaSongInfo("mx_ivm_genamb01"),
             VanillaSongInfo("mx_ivm_genamb02"),
             VanillaSongInfo("mx_unseen_amb"),
             VanillaSongInfo("mx_finalstand"),
             VanillaSongInfo("mx_gap"),
             VanillaSongInfo("mx_boarders"),
             VanillaSongInfo("mx_lasttogo"),
             VanillaSongInfo("mx_CUAttack", true),
             VanillaSongInfo("mx_CUAttackAlt", true),
             VanillaSongInfo("mx_drone_attack_01_lp", true),
             VanillaSongInfo("mx_drone_attack_02_lp", true),
             VanillaSongInfo("mx_wd_attack_lp", true),
             VanillaSongInfo("mx_corrupteddrone_lp2", true),
             VanillaSongInfo("mx_AllGent_Attack", true),
             VanillaSongInfo("mx_AllGent_Heist", true),
             VanillaSongInfo("mx_infected_attack", true),
             VanillaSongInfo("mx_infected_commander", true),
             VanillaSongInfo("mx_AllGent_Commander", true),
             VanillaSongInfo("mx_FluffyBiscuit_Ambient", true),
             VanillaSongInfo("mx_FluffyBiscuitTheme_FullLength", true),
             VanillaSongInfo("mx_Polytechnic_Attack", true),
             VanillaSongInfo("mx_Polytechnic_MainTheme", true),
             VanillaSongInfo("mx_Polytechnic_SectorCommander", true),
             VanillaSongInfo("mx_unseen_action1", true),
             VanillaSongInfo("mx_unseen_action1", true),
             VanillaSongInfo("mx_colunion_v4"),
             VanillaSongInfo("mx_CUExplore_lp"),
             VanillaSongInfo("mx_drone_ambient_01_lp"),
             VanillaSongInfo("mx_drone_ambient_02_lp"),
             VanillaSongInfo("mx_AllGent_ExploreLP"),
             VanillaSongInfo("mx_AllGent_v4"),
             VanillaSongInfo("mx_infected_ambient"),
             VanillaSongInfo("mx_infected_commander"),
             VanillaSongInfo("mx_wdce_explore_lp2"),
             VanillaSongInfo("mx_unseen_amb"),
             VanillaSongInfo("mx_wd_corp_v3"),
             VanillaSongInfo("mx_Polytechnic_Ambient"),
             VanillaSongInfo("mx_Polytechnic_Exploration"),
             VanillaSongInfo("mx_unseen_amb"),
             VanillaSongInfo("mx_FluffyBiscuitTheme_FullLength"),
             VanillaSongInfo("mx_CUAttackAlt", true, true, true, false),
             VanillaSongInfo("mx_ivm_darkness", false, true, false, false),
             VanillaSongInfo("mx_unseen_action2", true, false, true, true),
             VanillaSongInfo("mx_unseen_action1", false, false, false, true),
             VanillaSongInfo("mx_AllGent_Commander", true, true, true, false),
             VanillaSongInfo("mx_warpguardian_theme_one", true, false, true, true),
             VanillaSongInfo("mx_warpguardian_theme_two", true, false, true, true)
        };
        List<SongInfo> AddedSongs = new List<SongInfo>();
        internal void AddSong(AudioClip clip)
        {

            if (AddedSongs.Exists(song => song.Name.Equals(clip.name)))
            {
                this.AddedSongs.Find(song => song.Name.Equals(clip.name)).song = clip;
            }
            else
            {
                this.AddedSongs.Add(new SongInfo(clip.name, false, true, false, false));
            }
        }
        internal void OutputJson()
        {
            string jsonString = JsonConvert.SerializeObject(AddedSongs);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(Mod.Instance.MusicDirectory.FullName, "SongCategoryInfo.json")))
            {
                outputFile.WriteLine(jsonString);
            }
        }
        private static VanillaSongInfo VanillaSongInfo(string name, bool combat = false, bool planet = false, bool special = false, bool looping = false)
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
    }
    [JsonObject(MemberSerialization.OptOut)]
    internal sealed class SongInfo
    {
        internal SongInfo(string name, bool combat = false, bool ambient = false, bool boss = false, bool warp = false)
        {
            Name = name;
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
        [JsonIgnore]
        internal AudioClip song = null;
    }
    internal sealed class VanillaSongInfo
    {
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
        private readonly string Prefix = "mx_";
        internal void PlaySong()
        {
            PLMusic.Instance.PlayMusic(string.Concat(Prefix,Name), false, false, false, false);
        }
    }
}
