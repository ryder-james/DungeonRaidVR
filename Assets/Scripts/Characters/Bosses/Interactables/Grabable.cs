using UnityEngine;

namespace DungeonRaid.Characters.Bosses.Interactables {
	[RequireComponent(typeof(Rigidbody))]
	public abstract class Grabable : Interactable {
		protected Rigidbody body;

		protected virtual void Start() {
			body = GetComponent<Rigidbody>();
		}

		public abstract void Grab();
		public abstract void Release(Vector3 releaseVelocity, Vector3 releaseAngularVelocity);
	}
}
