using UnityEngine;

namespace DungeonRaid.Characters.Bosses.Pinhead {
	public class PinheadBodyFollow : MonoBehaviour {
		[SerializeField] private Transform headTransform = null;
		[SerializeField] private Transform neckTransform = null;
		[SerializeField] private LineRenderer neckRenderer = null;

		private Vector3 targetPosition;

		private const float fixedLagTimer = 0.25f;
		private float lagTimer;

		private void Start() {
			neckRenderer.SetPositions(new Vector3[] { neckTransform.position, headTransform.position });
			lagTimer = 0;
		}

		private void Update() {
			neckRenderer.SetPosition(1, headTransform.position);

			if (lagTimer >= fixedLagTimer) {
				targetPosition = headTransform.position;
				lagTimer -= fixedLagTimer;
			} else {
				lagTimer += Time.deltaTime;
			}

			Vector3 dir = targetPosition - transform.position;
			dir = Vector3.ClampMagnitude(dir, 30);

			transform.up = Vector3.Lerp(transform.up, dir, Time.deltaTime * 0.1f * (dir.magnitude * 0.03f));

			neckRenderer.SetPosition(0, neckTransform.position);
		}
	}
}