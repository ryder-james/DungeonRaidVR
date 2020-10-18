using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "ChanceToApply", menuName = EffectMenuPrefix + "Chance to Apply Effect")]
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
