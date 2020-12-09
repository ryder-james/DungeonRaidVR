using UnityEngine;

namespace DungeonRaid.Abilities.Effects {
    public abstract class ChannelableEffect : Effect {
		public delegate void EffectNotification();

		[SerializeField] private bool applyAtStart = false;

		public EffectNotification OnEffectBeginChannel { get; set; }
		public EffectNotification OnEffectUpdate { get; set; }
		public EffectNotification OnEffectEndChannel { get; set; }

		public bool ApplyAtStart { get => applyAtStart; set => applyAtStart = value; }

		public void BeginChannel() {
			OnEffectBeginChannel?.Invoke();
			Begin();
		}

		public void ChannelUpdate() {
			OnEffectUpdate?.Invoke();
			Channel();
		}

		public void EndChannel() {
			OnEffectEndChannel?.Invoke();
			End();
		}

		protected abstract void Begin();
		protected abstract void Channel();
		protected abstract void End();
    }
}