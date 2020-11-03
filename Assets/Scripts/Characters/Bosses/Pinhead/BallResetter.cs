using UnityEngine;

namespace DungeonRaid.Characters.Bosses.Pinhead {
	public class BallResetter : MonoBehaviour {
		private void OnTriggerEnter(Collider other) {
			if (other.GetComponent<BowlingBall>() != null) {
				other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			}
		}
	}
}
