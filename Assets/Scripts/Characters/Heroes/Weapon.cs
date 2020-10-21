using UnityEngine;

using DungeonRaid.Characters.Bosses;

namespace DungeonRaid.Characters.Heroes {
	public class Weapon : MonoBehaviour {
		[SerializeField] private float damage = 1;
		[SerializeField] private float range = 1;

		private Boss boss;

		private void Start() {
			boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
		}

		public void Attack() {
			Debug.Log($"Range: {range}");
			boss.UpdateMeter("Health", -damage);
		}
	}
}
