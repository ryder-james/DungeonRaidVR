using System.Collections;
using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Effects.StatusEffects {
	public abstract class StatusEffect : Effect {
		protected const string StatusEffectMenuPrefix = EffectMenuPrefix + "Status Effects/";

		[SerializeField] private float duration = 1;

		public override sealed void Apply(Character target) {
			StatusEffectComponent effect = target.gameObject.AddComponent<StatusEffectComponent>();
			effect.Begin(this, target, duration);
		}

		protected abstract void StartEffect(Character target);
		protected abstract void StopEffect(Character target);

		// This whole thing is gross. Find an alternative if possible.
		private class StatusEffectComponent : MonoBehaviour {
			private StatusEffect Caller { get; set; }
			private Character Target { get; set; }

			public void Begin(StatusEffect caller, Character target, float duration) {
				StartCoroutine(nameof(ApplyAsync), duration);
			}

			private IEnumerator ApplyAsync(float duration) {
				Caller.StartEffect(Target);
				yield return new WaitForSeconds(duration);
				Caller.StopEffect(Target);
				Destroy(this);
			}
		}
	}
}
