using UnityEngine;

using DungeonRaid.Abilities.Effects;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetHero", menuName = AbilityMenuPrefix + "Target Hero")]
	public class TargetSingleHeroAbility : Ability {
		protected override bool TargetCast(ref Effect[] effects) {
			throw new System.NotImplementedException();
		}
	}
}