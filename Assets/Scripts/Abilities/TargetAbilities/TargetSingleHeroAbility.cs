using UnityEngine;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetHero", menuName = AbilityMenuPrefix + "Target Hero")]
	public class TargetSingleHeroAbility : Ability {
		protected override void TargetCast() {
			throw new System.NotImplementedException();
		}
	}
}