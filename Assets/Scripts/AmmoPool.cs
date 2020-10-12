using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour {
	[SerializeField] private string ammoType = "";
	[SerializeField] private float maxAmmo = 10;

	private float ammoCount;

	public float AmmoCount {
		get => ammoCount;
		set { ammoCount = Mathf.Clamp(value, 0, maxAmmo); }
	}

	private void Awake() {
		AmmoCount = maxAmmo;
	}
}
