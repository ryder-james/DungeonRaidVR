using UnityEngine;

using DungeonRaid.Abilities.Effects;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetSelf", menuName = AbilityMenuPrefix + "Target Self")]
	public class TargetSelfAbility : Ability {
		protected override bool TargetCast(Effect[] effects) {
			foreach (Effect effect in effects) {
				effect.Apply(Owner);
			}

			return true;
		}
	}
}