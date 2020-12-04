using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "ChanceToApply", menuName = SuperEffectMenuPrefix + "Chance to Apply Effect")]
	public class ChanceToApplyEffect : Effect {
		[Range(0, 1)]
		[SerializeField] private float chanceToApply = 0.5f;
		[SerializeField] private Effect effect = null;

		public override void Apply(Character caster, Character target, Vector3 point) {
			if (Random.Range(0, 1f) < chanceToApply) {
				effect.Apply(caster, target, point);
			}
		}
	}
}
