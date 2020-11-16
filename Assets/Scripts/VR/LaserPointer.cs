using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour {
	[SerializeField] private SteamVR_Input_Sources handType = SteamVR_Input_Sources.RightHand;
	[SerializeField] private SteamVR_Behaviour_Pose controllerPose = null;

	[SerializeField] private GameObject laserPrefab = null;
	[SerializeField] private GameObject targetSpherePrefab = null;
	[SerializeField] private LayerMask layerMask = 0;
	
	public bool IsActive { get; set; } = false;

	private GameObject laser;
	private GameObject targetSphere;
	private Vector3 offset;

	private void Start() {
		laser = Instantiate(laserPrefab, transform);
		targetSphere = Instantiate(targetSpherePrefab, transform);

		laser.SetActive(false);
		targetSphere.SetActive(false);

	}

	private void Update() {
		offset = transform.forward * 0.1f;
		offset -= transform.up * 0.01f;
		if (Physics.Raycast(controllerPose.transform.position + offset, transform.forward, out RaycastHit hit, 100, layerMask)) {
			laser.SetActive(true);
			UpdateLaser(hit);
			targetSphere.transform.position = hit.point;
			targetSphere.SetActive(true);
		} else {
			laser.SetActive(false);
			targetSphere.SetActive(false);
		}
	}

	private void UpdateLaser(RaycastHit hit) {
		laser.transform.position = Vector3.Lerp(controllerPose.transform.position + offset, hit.point, 0.5f);
		laser.transform.LookAt(hit.point);
		Vector3 newScale = laser.transform.localScale;
		newScale.z = hit.distance;
		laser.transform.localScale = newScale;
	}
}
