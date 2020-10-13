using System.Collections;
using UnityEngine;

using DungeonRaid.Effects.StatusEffects;
using DungeonRaid.Characters;
using DungeonRaid.Effects;

[CreateAssetMenu(fileName = "RepeatedEffect", menuName = StatusEffectMenuPrefix + "Repeated Effect")]
public class RepeatedEffectStatusEffect : StatusEffect {
	[SerializeField] private Effect effect = null;

	private bool isRunning;

	protected override void StartEffect(Character target) {
		isRunning = true;
		target.gameObject.AddComponent<RepeatedEffectComponent>().Begin(this, target, effect);
	}

	protected override void StopEffect(Character target) {
		isRunning = false;
	}

	private class RepeatedEffectComponent : MonoBehaviour {
		private RepeatedEffectStatusEffect Caller { get; set; }
		private Character Target { get; set; }

		public void Begin(RepeatedEffectStatusEffect caller, Character target, Effect effect) {
			StartCoroutine(nameof(ApplyAsync), effect);
		}

		private IEnumerator ApplyAsync(Effect effect) {
			if (Caller.isRunning) {
				effect.Apply(Target);
				yield return new WaitForSeconds(1);
			} else {
				Destroy(this);
			}
		}
	}
}
