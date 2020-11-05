using UnityEngine;

using Common.Physics;
using DungeonRaid.Characters.Bosses.Interactables;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Characters.Bosses.Pinhead {
	public class BowlingBall : Throwable {
		[SerializeField] private float damage = 2;
		[SerializeField] private float speed = 10;

		public override void Grab() {

		}

		public void OnTriggerEnter(Collider other) {
			if (other.CompareTag("Hero")) {
				other.GetComponent<Hero>().UpdateMeter("Health", -damage);
			}
		}

		public void OnTriggerExit(Collider other) {

		}

		public void OnTriggerStay(Collider other) {

		}

		public override void Throw(Vector3 releaseVelocity, Vector3 releaseAngularVelocity) {
			body.constraints |= RigidbodyConstraints.FreezePositionX;
			body.velocity = Vector3.forward * speed;
			body.angularVelocity = releaseAngularVelocity;
		}
	}
}
