using UnityEngine;

[CreateAssetMenu(fileName = "NoTarget", menuName = AbilityMenuPrefix + "No Target")]
public class NoTargetAbility : Ability {
	protected override void TargetCast() {
		throw new System.NotImplementedException();
	}
}
