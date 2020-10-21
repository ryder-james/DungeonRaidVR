using UnityEngine;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetPoint", menuName = AbilityMenuPrefix + "Target Point")]
	public class TargetPointAbility : Ability {
		protected override bool TargetCast() {
			throw new System.NotImplementedException();
		}
	}
}