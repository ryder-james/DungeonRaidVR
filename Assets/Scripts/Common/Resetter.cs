using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Resetter : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		Debug.Log("reset!");
		Resettable resettable = other.GetComponent<Resettable>();
		if (resettable != null) {
			resettable.FullReset();
		}
	}
}
