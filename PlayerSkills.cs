using AOSharp.Common.GameData;
using AOSharp.Core;
using AOSharp.Core.IPC;
using AOSharp.Core.UI;
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
    public class PlayerSkills
    {
        private List<Stat> _skills = new List<Stat>
        {
            Stat.Agility,
            Stat.Stamina,
            Stat.Strength,
            Stat.Intelligence,
            Stat.Psychic,
            Stat.Sense,
            Stat.BodyDevelopment,
            Stat.EvadeClsC,
            Stat.DuckExp,
            Stat.DodgeRanged,
            Stat.Parry,
            Stat.NanoPool,
            Stat.NanoResist,
            Stat._1hBlunt,
            Stat.Piercing,
            Stat.Skill2hEdged,
            Stat.MartialArts,
            Stat.MeleeInit,
            Stat._1hEdged,
            Stat._2hBlunt,
            Stat.MeleeEnergy,
            Stat.MultiMelee,
            Stat.PhysicalInit,
            Stat.SneakAttack,
            Stat.FastAttack,
            Stat.Riposte,
            Stat.Brawl,
            Stat.Dimach,
            Stat.Pistol,
            Stat.MGSMG,
            Stat.Shotgun,
            Stat.RangedEnergy,
            Stat.HeavyWeapons,
            Stat.RangedInit,
            Stat.Bow,
            Stat.AssaultRifle,
            Stat.Rifle,
            Stat.Grenade,
            Stat.MultiRanged,
            Stat.FlingShot,
            Stat.Burst,
            Stat.BowSpecialAttack,
            Stat.AimedShot,
            Stat.FullAuto,
            Stat.SharpObject,
            Stat.MaterialMetamorphosis,
            Stat.PsychologicalModification,
            Stat.SpaceTime,
            Stat.NanoCInit,
            Stat.BiologicalMetamorphosis,
            Stat.SensoryImprovement,
            Stat.MaterialCreation,
            Stat.VehicleAir,
            Stat.VehicleWater,
            Stat.Adventuring,
            Stat.VehicleGround,
            Stat.RunSpeed,
            Stat.Perception,
            Stat.Psychology,
            Stat.FirstAid,
            Stat.Concealment,
            Stat.TrapDisarm,
            Stat.Treatment,
            Stat.MechanicalEngineering,
            Stat.QuantumFT,
            Stat.WeaponSmithing,
            Stat.Tutoring,
            Stat.ComputerLiteracy,
            Stat.ElectricalEngineering,
            Stat.Chemistry,
            Stat.NanoProgramming,
            Stat.BreakingEntry,
            Stat.Pharmaceuticals,
        };

        public void SetSkills(GameTuple<Stat, uint>[] skills)
        {
            Network.Send(new SkillMessage
            {
                Skills = skills
            });
        }

        public GameTuple<Stat, uint>[] GetSkills()
        {
            GameTuple<Stat, uint>[] skills = new GameTuple<Stat, uint>[_skills.Count];

            for (int i = 0; i < _skills.Count; i++)
            {
                skills[i] = new GameTuple<Stat, uint>
                {
                    Value1 = _skills[i],
                    Value2 = (uint)DynelManager.LocalPlayer.GetStat(_skills[i], 0)
                };
            }

            return skills;
        }
    }
}