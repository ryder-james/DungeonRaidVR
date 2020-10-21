using UnityEngine;

namespace DungeonRaid.Collections {
	public class MeterComponent : MonoBehaviour {
		public delegate void ValueChangedDelegate();

		[SerializeField] private string meterName = "Name";
		[SerializeField] private Color color = Color.red;

		[SerializeField] private float maxValue = 100;

		[Tooltip("Recharge rate, in units per second")]
		[SerializeField] protected float rechargeRate = 0;

		public bool IsRecharging { get; set; } = true;
		public bool IsLocked { get; set; } = false;

		public string MeterName { get => meterName; set => meterName = value; }

		public Color Color { get => color; set => color = value; }
		public ValueChangedDelegate OnValueChanged { get; set; }

		public float Value {
			get => value;
			set { 
				if (!IsLocked) {
					float old = this.value;
					this.value = Mathf.Clamp(value, 0, MaxValue);
					OnValueChanged?.Invoke();
				}
			}
		}

		public float MaxValue { 
			get => maxValue;
			set {
				maxValue = Mathf.Max(value, 0);
				OnValueChanged?.Invoke();
			}
		}

		private float value;

		private void Awake() {
			Value = MaxValue;
		}

		private void Update() {
			if (IsRecharging) {
				Value += rechargeRate * Time.deltaTime;
			}
		}
	}
}
