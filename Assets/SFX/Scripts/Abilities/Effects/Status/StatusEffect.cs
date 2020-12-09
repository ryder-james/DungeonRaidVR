using System.Collections;

using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Abilities.Effects.StatusEffects {
	public abstract class StatusEffect : Effect {
		protected const string StatusEffectMenuPrefix = EffectMenuPrefix + "Status Effects/";

		[SerializeField] protected float duration = 1;

		public bool IsRunning { get; set; }

		protected Character Caster { get; set; }
		protected Vector3 Point { get; set; }

		public override sealed void Apply(Character caster, Character target, Vector3 point) {
			Caster = caster;
			Point = point;

			StatusEffectComponent behaviour = target.GetFreeBehaviour<StatusEffectComponent>();
			behaviour.Begin(this, target, duration);
		}

		protected void InvokeRepeating(Character target, System.Action<Character> callback, float interval = 1) {
			RepeatingEffectComponent behaviour = target.GetFreeBehaviour<RepeatingEffectComponent>();
			behaviour.Begin(this, target, callback, interval);
		}

		protected abstract void StartEffect(Character target);
		protected abstract void StopEffect(Character target);

		private class RepeatingEffectComponent : MonoBehaviour {
			private StatusEffect Caller { get; set; }
			private Character Target { get; set; }
			public System.Action<Character> Callback { get; set; }
			private float Interval { get; set; }

			public void Begin(StatusEffect caller, Character target, System.Action<Character> callback, float interval) {
				Caller = caller;
				Target = target;
				Callback = callback;
				Interval = interval;
				StartCoroutine(nameof(RunAsync));
			}

			private IEnumerator RunAsync() {
				yield return new WaitUntil(() => Caller.IsRunning);

				while (Caller.IsRunning) {
					Callback(Target);
					if (Interval > 0) {
						yield return new WaitForSeconds(Interval);
					} else {
						yield return new WaitForEndOfFrame();
					}
				}

				enabled = false;
			}
		}

		// This whole thing is gross. Find an alternative if possible.
		private class StatusEffectComponent : MonoBehaviour {
			private StatusEffect Caller { get; set; }
			private Character Target { get; set; }

			public void Begin(StatusEffect caller, Character target, float duration) {
				Caller = caller;
				Target = target;
				StartCoroutine(nameof(ApplyAsync), duration);
				Caller.IsRunning = true;
			}

			private IEnumerator ApplyAsync(float duration) {
				Caller.StartEffect(Target);
				yield return new WaitForSeconds(duration);
				Caller.StopEffect(Target);
				Caller.IsRunning = false;

				enabled = false;
			}
		}
	}
}
