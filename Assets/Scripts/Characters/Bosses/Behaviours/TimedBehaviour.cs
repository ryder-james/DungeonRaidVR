using UnityEngine;

namespace DungeonRaid.Characters.Bosses.Behaviours {
	public abstract class TimedBehaviour : ContinuousBehaviour {
		[SerializeField] private float duration = 3;

		private float durationTimer = 0;

		protected override void Update() {
			if (autoUpdating) {
				durationTimer += Time.deltaTime;

				if (durationTimer >= duration) {
					EndBehaviour();
				}
			}
		}

		public override void BeginBehaviour() {
			autoUpdating = true;
			base.BeginBehaviour();
		}

		public override void EndBehaviour() {
			autoUpdating = false;
			base.EndBehaviour();
		}

		protected override void ResetBehaviour() {
			durationTimer = 0;
		}
	}
}