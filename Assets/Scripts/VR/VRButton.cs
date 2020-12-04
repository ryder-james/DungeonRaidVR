using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR;

public class VRButton : MonoBehaviour {
	[SerializeField] private SteamVR_Action_Boolean clickAction = null;

	[SerializeField] private string activationTag = "VRHand";
	[SerializeField] private Button button = null;

	private bool isHighlighted = false;
	private bool interactedThisFrame = false;

	private void Update() {
		if (isHighlighted && clickAction.GetStateUp(SteamVR_Input_Sources.Any)) {
			PointerEventData pointer = new PointerEventData(EventSystem.current);
			ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.submitHandler);
		}
	}

	private void LateUpdate() {
		interactedThisFrame = false;
	}

	private void OnTriggerEnter(Collider other) {
		if (!interactedThisFrame && !isHighlighted && other.CompareTag(activationTag)) {
			interactedThisFrame = true;
			isHighlighted = true;
			PointerEventData pointer = new PointerEventData(EventSystem.current);
			ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
		}	
	}

	private void OnTriggerExit(Collider other) {
		if (!interactedThisFrame && isHighlighted && other.CompareTag(activationTag)) {
			interactedThisFrame = true;
			isHighlighted = false;
			PointerEventData pointer = new PointerEventData(EventSystem.current);
			ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerExitHandler);
		}
	}
}
