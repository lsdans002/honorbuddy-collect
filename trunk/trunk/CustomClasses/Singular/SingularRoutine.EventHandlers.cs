﻿#region Revision Info

// This file is part of Singular - A community driven Honorbuddy CC
// $Author: raphus $
// $Date: 2011-03-02 03:32:55 +0200 (Çar, 02 Mar 2011) $
// $HeadURL: http://svn.apocdev.com/singular/trunk/Singular/SingularRoutine.EventHandlers.cs $
// $LastChangedBy: raphus $
// $LastChangedDate: 2011-03-02 03:32:55 +0200 (Çar, 02 Mar 2011) $
// $LastChangedRevision: 130 $
// $Revision: 130 $

#endregion

using System;
using System.Collections.Generic;

using Styx;
using Styx.Combat.CombatRoutine;
using Styx.Helpers;
using Styx.Logic;
using Styx.Logic.POI;
using Styx.WoWInternals;

namespace Singular
{
    partial class SingularRoutine
    {
        public HashSet<string> _sleepAfterSuccessSpells = new HashSet<string>();

        public void AttachEventHandlers()
        {
            BotEvents.Player.OnMapChanged += Player_OnMapChanged;

            Lua.Events.AttachEvent("COMBAT_LOG_EVENT_UNFILTERED", HandleCombatLog);
            if (!Lua.Events.AddFilter("COMBAT_LOG_EVENT_UNFILTERED", "return args[2] == 'SPELL_CAST_SUCCESS' or args[2] == 'SPELL_AURA_APPLIED' or args[2] == 'SPELL_MISSED'"))
            {
                Logger.Write("ERROR: Could not add combat log event filter! - Performance may be horrible, and things may not work properly!");
            }
        }

        protected void AddSpellSucceedWait(string spellName)
        {
            if (!_sleepAfterSuccessSpells.Contains(spellName))
            {
                _sleepAfterSuccessSpells.Add(spellName);
            }
        }

        private void HandleCombatLog(object sender, LuaEventArgs args)
        {
            var e = new CombatLogEventArgs(args.EventName, args.FireTimeStamp, args.Args);
            //Logger.WriteDebug("[CombatLog] " + e.Event + " - " + e.SourceName + " - " + e.SpellName);
            switch (e.Event)
            {
                case "SPELL_AURA_APPLIED":
                case "SPELL_CAST_SUCCESS":
                    if (e.SourceGuid != Me.Guid)
                    {
                        return;
                    }
                    // Update the last spell we cast. So certain classes can 'switch' their logic around.
                    LastSpellCast = e.SpellName;
                    Logger.Write("Successfully cast " + LastSpellCast);

                    if (_sleepAfterSuccessSpells.Contains(e.SpellName))
                    {
                        StyxWoW.SleepForLagDuration();
                    }

                    // Force a wait for all summoned minions. This prevents double-casting it.
                    if (myClass == WoWClass.Warlock && e.SpellName.StartsWith("Summon "))
                    {
                        StyxWoW.SleepForLagDuration();
                    }
                    break;

                case "SPELL_MISSED":
                    //Logger.Write(e.Args.ToRealString());
                    if (e.Args[11].ToString() == "EVADE")
                    {
                        Logger.Write("Mob is evading. Blacklisting it!");
                        Blacklist.Add(e.DestGuid, TimeSpan.FromMinutes(30));
                        if (StyxWoW.Me.CurrentTargetGuid == e.DestGuid)
                            StyxWoW.Me.ClearTarget();

                        BotPoi.Clear("Blacklisting evading mob");
						StyxWoW.SleepForLagDuration();
                    }
                    break;
            }
        }

        private void Player_OnMapChanged(BotEvents.Player.MapChangedEventArgs args)
        {
			//Why would we create same behaviors all over ?
			if (lastContext == CurrentWoWContext)
				return;

            Logger.Write("Context changed. New context: " + CurrentWoWContext + ". Rebuilding behaviors.");
            CreateBehaviors();
        }
    }
}