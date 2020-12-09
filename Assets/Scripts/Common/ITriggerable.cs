using UnityEngine;

namespace JCommon.Physics {
	public interface ITriggerable {
		void OnTriggerEnter(Collider other);
		void OnTriggerStay(Collider other);
		void OnTriggerExit(Collider other);
	}
}