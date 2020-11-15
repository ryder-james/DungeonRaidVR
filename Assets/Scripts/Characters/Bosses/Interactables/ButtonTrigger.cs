using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour {
	[SerializeField] private string triggerTag = "";

	

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(triggerTag)) {

		}
	}
}
