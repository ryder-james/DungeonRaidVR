using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "DelayedEffect", menuName = SuperEffectMenuPrefix + "Delayed Effect")]
	public class DelayedEffect : Effect {
		[SerializeField, Min(0.01f)] private float delay = 0.25f;
		[SerializeField] private Effect effect = null;

		public override void Apply(Character caster, Character target, Vector3 point) {
			InvokeDelayed(caster, target, point, effect.Apply, delay);
		}
	}
}

