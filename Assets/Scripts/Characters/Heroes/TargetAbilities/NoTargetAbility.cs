using DungeonRaid.Effects;
using UnityEngine;

[CreateAssetMenu(fileName = "NoTarget", menuName = AbilityMenuPrefix + "No Target")]
public class NoTargetAbility : Ability {
	protected override void TargetCast() {
		foreach (Effect effect in Effects) {
			effect.Apply(Owner);
		}
	}
}
