using UnityEngine;

namespace DungeonRaid.Collections {
	public class MeterComponent : MonoBehaviour {
		[SerializeField] private string meterName = "Name";
		[SerializeField] private Color meterColor = Color.red;

		[SerializeField] private float maxValue = 100;

		[Tooltip("Recharge rate, in units per second")]
		[SerializeField] protected float rechargeRate = 0;

		public bool IsRecharging { get; set; } = true;
		public bool IsLocked { get; set; } = false;

		private float value;

		public float Value {
			get => value;
			set { 
				if (!IsLocked) {
					this.value = Mathf.Clamp(value, 0, MaxValue);
				}
			}
		}

		public float MaxValue { get => maxValue; set => maxValue = value; }
		public string MeterName { get => meterName; set => meterName = value; }
		public Color MeterColor { get => meterColor; set => meterColor = value; }

		private void Awake() {
			Value = MaxValue;
		}
	}
}
