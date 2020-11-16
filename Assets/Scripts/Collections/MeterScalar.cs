using DungeonRaid.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterScalar : MonoBehaviour {
	[SerializeField] private MeterComponent baseMeter = null;
	[SerializeField] private Vector3 minimumScale = Vector3.one * 0.001f;
	[SerializeField] private Vector3 scalarAxisMultipliers = Vector3.one;

	private Vector3 baseScale;

	private void Start() {
		baseScale = transform.localScale;
		baseMeter.OnValueChanged += UpdateScale;
	}

	private void UpdateScale() {
		Vector3 scale = Vector3.Lerp(minimumScale, baseScale, baseMeter.NormalizedValue);
		scale = Vector3.Scale(scale, scalarAxisMultipliers);

		transform.localScale = scale;
	}
}
