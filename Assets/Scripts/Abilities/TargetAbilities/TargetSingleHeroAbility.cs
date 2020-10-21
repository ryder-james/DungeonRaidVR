using UnityEngine;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetHero", menuName = AbilityMenuPrefix + "Target Hero")]
	public class TargetSingleHeroAbility : Ability {
		protected override bool TargetCast() {
			throw new System.NotImplementedException();
		}
	}
}