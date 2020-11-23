using System.Collections;
using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Abilities.Effects {
	public abstract class Effect : ScriptableObject {
		protected const string EffectMenuPrefix = "Dungeon Raid/Effects/";

		[SerializeField] private bool applyToCaster = false;

		public virtual bool ApplyToCaster { get => applyToCaster; set => applyToCaster = value; }

		public abstract void Apply(Character caster, Character target, Vector3 point);

		protected void InvokeDelayed(Character caster, Character target, Vector3 point, System.Action<Character, Character, Vector3> callback, float delay) {
			DelayedEffectComponent behaviour = caster.GetFreeBehaviour<DelayedEffectComponent>();
			behaviour.Begin(caster, target, point, callback, delay);
		}

		public class DelayedEffectComponent : MonoBehaviour {
			private Character Caster { get; set; }
			private Character Target { get; set; }
			private Vector3 TargetPoint { get; set; }
			public System.Action<Character, Character, Vector3> Callback { get; set; }
			private float Delay { get; set; }

			public void Begin(Character caster, Character target, Vector3 point, System.Action<Character, Character, Vector3> callback, float delay) {
				Caster = caster;
				Target = target;
				TargetPoint = point;
				Callback = callback;
				Delay = delay;
				StartCoroutine(nameof(RunAsync));
			}

			private IEnumerator RunAsync() {
				yield return new WaitForSeconds(Delay);

				Callback(Caster, Target, TargetPoint);
				enabled = false;
			}
		}
	}
}
