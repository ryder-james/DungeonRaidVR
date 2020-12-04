using UnityEngine;

using Common.Physics;
using DungeonRaid.Characters.Bosses.Interactables;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Characters.Bosses.Pinhead {
	public class BowlingBall : Throwable {
		[SerializeField] private float damage = 2;
		[SerializeField] private float speed = 10;
		[SerializeField] private AudioSource rollingSound = null;
		[SerializeField] private AudioSource hitSound = null;

		private bool hasLanded = true;

		public override void Grab() {

		}

		public void OnTriggerEnter(Collider other) {
			if (other.CompareTag("Hero")) {
				other.GetComponent<Hero>().UpdateMeter("Health", -damage);
			}
		}

		public void StopRollingSound() {
			rollingSound.Stop();
			hasLanded = true;
		}

		public override void Throw(Vector3 releaseVelocity, Vector3 releaseAngularVelocity) {
			body.constraints |= RigidbodyConstraints.FreezePositionX;
			body.velocity = new Vector3(0, -speed, speed);
			body.angularVelocity = releaseAngularVelocity;
			hasLanded = false;
		}

		private void OnCollisionEnter(Collision collision) {
			if (!hasLanded && collision.gameObject.CompareTag("Ground")) {
				hitSound.Play();
				rollingSound.Play();
				hasLanded = true;
			}
		}
	}
}
