using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Abilities.Effects.StatusEffects;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "ApplyStatusEffect", menuName = SuperEffectMenuPrefix + "Apply Status Effect")]
	public class ApplyStatusEffect : Effect {
		[SerializeField] private StatusEffect statusEffect = null;
		public override void Apply(Character caster, Character target, Vector3 point) {
			statusEffect.Apply(caster, target, point);
		}
	}
}
