using UnityEngine;

using DungeonRaid.Abilities.Effects;
using DungeonRaid.Abilities;

namespace DungeonRaid.Characters.Bosses.Interactables {
	[RequireComponent(typeof(Collider))]
	public class ChannelableTriggerVolume : TriggerVolume {
		[SerializeField] private Cost activationCost = new Cost();
		[SerializeField] private Cost channelCost = new Cost();
		[SerializeField] private ContinuousBehaviour[] behaviours = null;

		private Boss boss;

		protected override void TriggerEnter() {
			if (boss == null) {
				boss = triggeringObject.GetComponentInParent<Boss>();
			}

			if (boss.PayCost(activationCost)) {
				foreach (ContinuousBehaviour effect in behaviours) {
					effect.BeginBehaviour();
				}
			} else {
				ReleaseEarly();
			}
		}

		protected override void TriggerStay() {
			if (boss.PayCost(channelCost)) {
				foreach (ContinuousBehaviour effect in behaviours) {
					effect.UpdateBehaviour();
				}
			} else {
				ReleaseEarly();
			}
		}

		protected override void TriggerExit() {
			foreach (ContinuousBehaviour effect in behaviours) {
				effect.EndBehaviour();
			}
		}
	}
}
