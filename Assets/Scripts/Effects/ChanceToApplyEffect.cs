using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Effects {
	[CreateAssetMenu(fileName = "ChanceToApplyEffect", menuName = EffectMenuPrefix + "Chance To Apply")]
	public class ChanceToApplyEffect : Effect {
		[Range(0, 1)]
		[SerializeField] private float chanceToApply = 0.5f;
		[SerializeField] private Effect effect = null;
		public override void Apply(Character target) {
			if (Random.Range(0, 1f) < chanceToApply) {
				effect.Apply(target);
			}
		}
	}
}
