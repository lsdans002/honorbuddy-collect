﻿#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author: apoc $
// $Date: 2011-02-17 10:50:06 +0200 (Per, 17 Şub 2011) $
// $HeadURL: http://svn.apocdev.com/singular/trunk/Singular/Attributes.cs $
// $LastChangedBy: apoc $
// $LastChangedDate: 2011-02-17 10:50:06 +0200 (Per, 17 Şub 2011) $
// $LastChangedRevision: 72 $
// $Revision: 72 $

#endregion

using System;

using Styx.Combat.CombatRoutine;

namespace Singular
{
    [Flags]
    public enum WoWContext
    {
        Normal = 0x1,
        Instances = 0x2,
        Battlegrounds = 0x4,

        All = Normal | Instances | Battlegrounds,
    }

    public enum BehaviorType
    {
        Rest,
        PreCombatBuffs,
        PullBuffs,
        Pull,
        Heal,
        CombatBuffs,
        Combat,
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    internal sealed class ClassAttribute : Attribute
    {
        public ClassAttribute(WoWClass specificClass)
        {
            SpecificClass = specificClass;
        }

        public WoWClass SpecificClass { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    internal sealed class BehaviorAttribute : Attribute
    {
        public BehaviorAttribute(BehaviorType type)
        {
            Type = type;
        }

        public BehaviorType Type { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    internal sealed class SpecAttribute : Attribute
    {
        public SpecAttribute(TalentSpec spec)
        {
            SpecificSpec = spec;
        }

        public TalentSpec SpecificSpec { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    internal sealed class ContextAttribute : Attribute
    {
        public ContextAttribute(WoWContext context)
        {
            SpecificContext = context;
        }

        public WoWContext SpecificContext { get; private set; }
    }
}