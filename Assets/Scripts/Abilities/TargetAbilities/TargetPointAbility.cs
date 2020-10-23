using UnityEngine;

using DungeonRaid.Abilities.Effects;
using DungeonRaid.Characters;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetPoint", menuName = AbilityMenuPrefix + "Target Point")]
	public class TargetPointAbility : Ability {
		[SerializeField] private bool hitSelf = false;
		[SerializeField] private LayerMask targetLayers = 1;

		protected override bool TargetCast(Effect[] effects) {
			Collider[] overlaps = Physics.OverlapSphere(Owner.TargetPoint, 0.1f, targetLayers);

			foreach (Effect effect in effects) {
				if (effect.ApplyToCaster) {
					effect.Apply(Owner, Owner, Owner.TargetPoint);
				} else {
					foreach (Collider col in overlaps) {
						Character target = col.GetComponent<Character>();
						if (target != null) {
							if (hitSelf || target != Owner) {
								effect.Apply(Owner, target, Owner.TargetPoint);
							}
						}
					}
				}
			}

			return true;
		}
	}
}