﻿using DungeonRaid.Effects;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetArea", menuName = AbilityMenuPrefix + "Target Area")]
public class TargetAreaAbility : Ability {
	protected override void TargetCast() {
		foreach (Effect effect in Effects) {
			if (effect.ApplyToCaster) {
				effect.Apply(Owner);
			} else {
				effect.Apply(Owner);
			}
		}
	}
}
