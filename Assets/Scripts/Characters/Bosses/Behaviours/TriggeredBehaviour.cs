using UnityEngine;


namespace DungeonRaid.Characters.Bosses.Behaviours {
	public abstract class TriggeredBehaviour : MonoBehaviour {
		public delegate void BehaviourNotification();

		public abstract void Trigger();

		protected virtual void Start() { }

		protected virtual void Update() { }
	}
}