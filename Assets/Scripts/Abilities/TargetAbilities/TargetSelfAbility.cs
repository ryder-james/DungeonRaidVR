using UnityEngine;

using DungeonRaid.Abilities.Effects;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetSelf", menuName = AbilityMenuPrefix + "Target Self")]
	public class TargetSelfAbility : Ability {
		protected override bool TargetCast() {
			foreach (Effect effect in Effects) {
				effect.Apply(Owner);
			}

			return true;
		}
	}
}