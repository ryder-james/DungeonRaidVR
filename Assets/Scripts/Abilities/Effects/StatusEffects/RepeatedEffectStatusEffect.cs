using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Abilities.Effects.StatusEffects {
	[CreateAssetMenu(fileName = "RepeatedEffect", menuName = StatusEffectMenuPrefix + "Repeated Effect")]
	public class RepeatedEffectStatusEffect : StatusEffect {
		[SerializeField] private Effect effect = null;

		protected override void StartEffect(Character target) {
			InvokeRepeating(target, t => effect.Apply(Caster, t, Caster.TargetPoint));
		}

		protected override void StopEffect(Character target) { }

	}
}