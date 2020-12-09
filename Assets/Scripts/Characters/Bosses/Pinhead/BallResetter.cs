using UnityEngine;

namespace DungeonRaid.Characters.Bosses.Pinhead {
	public class BallResetter : MonoBehaviour {
		private void OnTriggerEnter(Collider other) {
			BowlingBall ball = other.GetComponentInParent<BowlingBall>();
			if (ball != null) {
				Rigidbody rb = other.GetComponentInParent<Rigidbody>();
				rb.constraints = RigidbodyConstraints.None;
				rb.velocity *= 2;
				ball.StopRollingSound();
			}
		}
	}
}
