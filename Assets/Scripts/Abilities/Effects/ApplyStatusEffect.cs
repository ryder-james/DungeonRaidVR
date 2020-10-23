using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Abilities.Effects.StatusEffects;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "ApplyStatusEffect", menuName = EffectMenuPrefix + "Apply Status Effect")]
	public class ApplyStatusEffect : Effect {
		[SerializeField] private StatusEffect statusEffect = null;
		public override void Apply(Hero caster, Character target, Vector3 point) {
			statusEffect.Apply(caster, target, point);
		}
	}
}
