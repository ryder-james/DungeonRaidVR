using UnityEngine;

public abstract class MeterComponent : MonoBehaviour {
	[SerializeField] protected float maxValue = 100;

	[Tooltip("Recharge rate, in units per second")]
	[SerializeField] protected float rechargeRate = 0;

	public bool IsRecharging { get; set; } = true;

	private float value;

	public float Value {
		get => value;
		set { this.value = Mathf.Clamp(value, 0, maxValue); }
	}

	public abstract Color MeterColor { get; }

	private void Awake() {
		Value = maxValue;
	}
}
