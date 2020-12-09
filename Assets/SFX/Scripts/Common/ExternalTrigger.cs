using UnityEngine;

namespace JCommon.Physics {
    [RequireComponent(typeof(Collider))]
    public class ExternalTrigger : MonoBehaviour {
        [SerializeField] TriggerableContainer triggerable = null;

		private void OnTriggerEnter(Collider other) {
			triggerable.Result.OnTriggerEnter(other);
		}

		private void OnTriggerStay(Collider other) {
			triggerable.Result.OnTriggerStay(other);
		}

		private void OnTriggerExit(Collider other) {
			triggerable.Result.OnTriggerExit(other);
		}
	}
}