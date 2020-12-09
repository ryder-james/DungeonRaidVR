using UnityEngine;

namespace JCommon.Management {
	[RequireComponent(typeof(Collider))]
	public class Resetter : MonoBehaviour {
		private void OnTriggerEnter(Collider other) {
			Resettable resettable = other.GetComponent<Resettable>();
			if (resettable != null) {
				resettable.FullReset();
			}
		}
	}
}