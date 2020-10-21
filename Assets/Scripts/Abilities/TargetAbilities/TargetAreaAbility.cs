using UnityEngine;

using DungeonRaid.Abilities.Effects;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetArea", menuName = AbilityMenuPrefix + "Target Area")]
	public class TargetAreaAbility : Ability {
		protected override bool TargetCast() {
			foreach (Effect effect in Effects) {
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
