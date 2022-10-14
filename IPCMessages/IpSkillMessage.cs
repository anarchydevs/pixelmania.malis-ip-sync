using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOSharp.Common.GameData;
using AOSharp.Core.IPC;
using MalisIpcSync;
using SmokeLounge.AOtomation.Messaging.GameData;
using SmokeLounge.AOtomation.Messaging.Serialization;
using SmokeLounge.AOtomation.Messaging.Serialization.MappingAttributes;

namespace MultiboxHelper.IPCMessages
{
    [AoContract((int)IPCOpcode.IpSkill)]
    public class IpSkillMessage : IPCMessage
    {
        public override short Opcode => (short)IPCOpcode.IpSkill;

        [AoMember(0, SerializeSize = ArraySizeType.Int32)]
        public GameTuple<Stat, uint>[] Skills { get; set; }

        [AoMember(1)]
        public Profession Profession { get; set; }
    }
}
