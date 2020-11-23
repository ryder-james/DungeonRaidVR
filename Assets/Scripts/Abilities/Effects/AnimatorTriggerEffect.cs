using DungeonRaid.Characters;
using UnityEngine;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "AnimatorTrigger", menuName = EffectMenuPrefix + "Animator Trigger")]
	public class AnimatorTriggerEffect : Effect {
		[SerializeField] private string triggerName = "";

		public override bool ApplyToCaster => true;

		public override void Apply(Character caster, Character target, Vector3 point) {
			caster.Animator.SetTrigger(triggerName);
		}
	}
}
