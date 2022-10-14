using AOSharp.Common.GameData;
using AOSharp.Core;
using AOSharp.Core.IPC;
using AOSharp.Core.UI;
using MultiboxHelper.IPCMessages;
using Newtonsoft.Json;
using SmokeLounge.AOtomation.Messaging.GameData;
using SmokeLounge.AOtomation.Messaging.Messages;
using SmokeLounge.AOtomation.Messaging.Messages.N3Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MalisIpcSync
{
    public class Main : AOPluginEntry
    {
        public static string PluginDir;
        private IPCChannel _ipc;
        public unsafe override void Run(string pluginDir)
        {
            Chat.WriteLine("- Mali's IP Sync -", ChatColor.Gold);

            string tutMsg = 
                "\n" +
                "/ipsync all - sync ip from current toon to all other clients\n" +
                "/ipsync profession - sync ip from current toon to all other clients with same profession as the command giver\n";
            
            Chat.WriteLine("- Mali's IP Sync -", ChatColor.Gold);
            Chat.WriteLine(tutMsg,ChatColor.DarkPink);
          
            PluginDir = pluginDir;
            _ipc = new IPCChannel(243);
            _ipc.RegisterCallback((int)IPCOpcode.IpSkill, OnIpSkillMessageReceived);

            Chat.RegisterCommand("ipsync", (string command, string[] param, ChatWindow chatWindow) =>
            {
                GameTuple<Stat, uint>[] _ipSkills = new GameTuple<Stat, uint>[73].SetIpAbleSkills();

                if (param == null || param.Count() != 1 || !Enum.TryParse(param[0].CapitalizeFirstLetter(), out SyncType syncType))
                {
                    Chat.WriteLine($"Invalid parameter '{param[0]}' (available: All, Profession)");
                    return;
                }

                _ipSkills.CopyPlayerSkills();

                _ipc.Broadcast(new IpSkillMessage
                {
                    Profession = syncType == SyncType.All ? Profession.Unknown : DynelManager.LocalPlayer.Profession,
                    Skills = _ipSkills
                });
            });
        }

        private void OnIpSkillMessageReceived(int arg1, IPCMessage msg)
        {
            IpSkillMessage ipSkillMessage = (IpSkillMessage)msg;

            if (ipSkillMessage.Profession != Profession.Unknown && ipSkillMessage.Profession != DynelManager.LocalPlayer.Profession)
                return;

            DynelManager.LocalPlayer.IpSkills(ipSkillMessage.Skills);
        }
    }

    public enum SyncType
    {
        Profession = 0,
        All = 1
    }
}