﻿using UnityEngine;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetBoss", menuName = AbilityMenuPrefix + "Target Boss")]
	public class TargetBossAbility : Ability {
		protected override void TargetCast() {
			throw new System.NotImplementedException();
		}
	}
}