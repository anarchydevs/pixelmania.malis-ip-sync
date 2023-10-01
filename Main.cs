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
using System.Globalization;
using System.IO;
using System.Linq;

namespace MalisIpcSync
{
    public class Main : AOPluginEntry
    {
        private IPCChannel _ipc;
        private PlayerSkills _playerSkills;

        public unsafe override void Run(string pluginDir)
        {
            Chat.WriteLine("- Mali's IP Sync -", ChatColor.Gold);

            string tutMsg = 
                "\n" +
                "/ipsync all - sync ip from current toon to all other clients\n" +
                "/ipsync profession - sync ip from current toon to all other clients with same profession as the command giver\n";
            
            Chat.WriteLine(tutMsg, ChatColor.DarkPink);

            _ipc = new IPCChannel(243);
            _ipc.RegisterCallback((int)IPCOpcode.IpSkill, OnIpSkillMessageReceived);
            _playerSkills = new PlayerSkills();

            Chat.RegisterCommand("ipsync", (string command, string[] param, ChatWindow chatWindow) =>
            {
                if (param == null)
                    return;

                if (param.Length != 1)
                {
                    Chat.WriteLine($"Invalid command (valid: /ipsync 'all / profession')");
                    return;
                }
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

                if (!Enum.TryParse(textInfo.ToTitleCase(param[0]), out SyncType syncType))
                {
                    Chat.WriteLine($"Invalid parameter '{param[0]}' (available: All, Profession)");
                    return;
                }

                _ipc.Broadcast(new IpSkillMessage
                {
                    Profession = syncType == SyncType.All ? Profession.Unknown : DynelManager.LocalPlayer.Profession,
                    Skills = _playerSkills.GetSkills()
                });
            });
        }

        private void OnIpSkillMessageReceived(int arg1, IPCMessage msg)
        {
            IpSkillMessage ipSkillMessage = (IpSkillMessage)msg;

            if (ipSkillMessage.Profession != Profession.Unknown && ipSkillMessage.Profession != DynelManager.LocalPlayer.Profession)
                return;

            _playerSkills.SetSkills(ipSkillMessage.Skills);
        }
    }

    public enum SyncType
    {
        Profession = 0,
        All = 1
    }
}