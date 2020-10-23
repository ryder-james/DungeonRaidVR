using UnityEngine;

using DungeonRaid.Abilities.Effects;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetPoint", menuName = AbilityMenuPrefix + "Target Point")]
	public class TargetPointAbility : Ability {
		protected override bool TargetCast(Effect[] effects) {
			throw new System.NotImplementedException();
		}
	}
}