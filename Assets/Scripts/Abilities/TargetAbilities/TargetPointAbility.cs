using UnityEngine;

using DungeonRaid.Abilities.Effects;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetPoint", menuName = AbilityMenuPrefix + "Target Point")]
	public class TargetPointAbility : Ability {
		protected override bool TargetCast(Effect[] effects) {
			foreach (Effect effect in effects) {
				if (effect.ApplyToCaster) {
					effect.Apply(Owner, Owner, TargetPoint);
				} else {
					effect.Apply(Owner, null, TargetPoint);
				}
			}

			return true;
		}
	}
}