using UnityEngine;

using DungeonRaid.Abilities.Effects;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetArea", menuName = AbilityMenuPrefix + "Target Area")]
	public class TargetAreaAbility : Ability {
		protected override bool TargetCast(Effect[] effects) {
			foreach (Effect effect in effects) {
				if (effect.ApplyToCaster) {
					effect.Apply(Owner);
				} else {
					//effect.Apply(Owner);
				}
			}

			return true;
		}
	}
}
