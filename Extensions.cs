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
    public static class Extensions
    {
        public static void CopyPlayerSkills(this GameTuple<Stat, uint>[] skills)
        {
            foreach (var skill in skills)
                skill.Value2 = (uint)DynelManager.LocalPlayer.GetStat(skill.Value1, 0);
        }

        public static string CapitalizeFirstLetter(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            if (s.Length == 1)
                return s.ToUpper();

            return s.Remove(1).ToUpper() + s.Substring(1);
        }

        public static void IpSkills(this LocalPlayer localPlayer, GameTuple<Stat,uint>[] skills)
        {
            Network.Send(new SkillMessage
            {
                Skills = skills
            });
        }

        public static GameTuple<Stat, uint>[] SetIpAbleSkills(this GameTuple<Stat, uint>[] skills)
        {
            return new GameTuple<Stat, uint>[]
                {
                    new GameTuple<Stat, uint>{ Value1 = Stat.Agility, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Stamina, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Strength, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Intelligence, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Psychic, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Sense, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.BodyDevelopment, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.EvadeClsC, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.DuckExp, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.DodgeRanged, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Parry, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.NanoPool, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.NanoResist, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat._1hBlunt, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Piercing, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Skill2hEdged, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.MartialArts, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.MeleeInit, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat._1hEdged, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat._2hBlunt, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.MeleeEnergy, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.MultiMelee, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.PhysicalInit, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.SneakAttack, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.FastAttack, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Riposte, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Brawl, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Dimach, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Pistol, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.MGSMG, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Shotgun, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.RangedEnergy, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.HeavyWeapons, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.RangedInit, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Bow, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.AssaultRifle, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Rifle, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Grenade, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.MultiRanged, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.FlingShot, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Burst, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.BowSpecialAttack, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.AimedShot, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.FullAuto, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.SharpObject, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.MaterialMetamorphosis, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.PsychologicalModification, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.SpaceTime, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.NanoCInit, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.BiologicalMetamorphosis, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.SensoryImprovement, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.MaterialCreation, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.VehicleAir, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.VehicleWater, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Adventuring, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.VehicleGround, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.RunSpeed, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Perception, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Psychology, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.FirstAid, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Concealment, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.TrapDisarm, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Treatment, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.MechanicalEngineering, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.QuantumFT, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.WeaponSmithing, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Tutoring, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.ComputerLiteracy, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.ElectricalEngineering, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Chemistry, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.NanoProgramming, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.BreakingEntry, Value2 = 0 },
                    new GameTuple<Stat, uint>{ Value1 = Stat.Pharmaceuticals, Value2 = 0 },
                };
        }
    }
}