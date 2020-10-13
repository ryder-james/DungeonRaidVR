using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Effects.StatusEffects;

namespace DungeonRaid.Effects {
	[CreateAssetMenu(fileName = "ApplyStatusEffect", menuName = EffectMenuPrefix + "Apply Status Effect")]
	public class ApplyStatusEffect : Effect {
		[SerializeField] private StatusEffect statusEffect = null;
		public override void Apply(Character target) {
			statusEffect.Apply(target);
		}
	}
}
