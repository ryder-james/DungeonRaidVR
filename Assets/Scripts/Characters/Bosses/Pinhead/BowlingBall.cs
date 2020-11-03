using UnityEngine;

using Common.Physics;
using DungeonRaid.Characters.Bosses.Interactables;

namespace DungeonRaid.Characters.Bosses.Pinhead {
	public class BowlingBall : Throwable {
		public override void Grab() {

		}

		public void OnTriggerEnter(Collider other) {
			if (other.CompareTag("Hero")) {
				Debug.Log("hit!");
			}
		}

		public void OnTriggerExit(Collider other) {

		}

		public void OnTriggerStay(Collider other) {

		}

		public override void Throw(Vector3 releaseVelocity, Vector3 releaseAngularVelocity) {
			body.constraints |= RigidbodyConstraints.FreezePositionX;
			body.velocity = Vector3.forward * 6;
			body.angularVelocity = releaseAngularVelocity;
		}
	}
}
