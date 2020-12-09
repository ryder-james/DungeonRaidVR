using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects.StatusEffects {
	[CreateAssetMenu(fileName = "RepeatedEffect", menuName = StatusEffectMenuPrefix + "Repeated Effect")]
	public class RepeatedEffectStatusEffect : StatusEffect {
		[SerializeField] private Effect effect = null;

		protected override void StartEffect(Character target) {
			InvokeRepeating(target, t => effect.Apply(t, t, Caster is Hero ? (Caster as Hero).TargetPoint : Vector3.zero));
		}

		protected override void StopEffect(Character target) { }

	}
}