using UnityEngine;

namespace DungeonRaid.Collections {
	public class AmmoPool : MonoBehaviour {
		[SerializeField] private string ammoType = "";
		[SerializeField] private float maxAmmo = 10;

		private float ammoCount;

		public float AmmoCount {
			get => ammoCount;
			set { ammoCount = Mathf.Clamp(value, 0, maxAmmo); }
		}

		public string AmmoType { get => ammoType; set => ammoType = value; }

		private void Awake() {
			AmmoCount = maxAmmo;
		}
	}
}
