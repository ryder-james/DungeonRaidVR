using UnityEngine;

namespace DungeonRaid.Characters.Bosses.Pinhead {
	public class BallResetter : MonoBehaviour {
		private void OnTriggerEnter(Collider other) {
			if (TryGetComponent(out BowlingBall ball)) {
				Rigidbody rb = other.GetComponent<Rigidbody>();
				rb.constraints = RigidbodyConstraints.None;
				rb.velocity *= 2;
				ball.StopRollingSound();
			}
		}
	}
}
