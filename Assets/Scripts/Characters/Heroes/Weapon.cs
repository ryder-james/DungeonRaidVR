using UnityEngine;

using DungeonRaid.Characters.Bosses;

namespace DungeonRaid.Characters.Heroes {
	public class Weapon : MonoBehaviour {
		[SerializeField] private float damage = 1;
		[SerializeField] private float range = 1;

		private Boss boss;

		private void Start() {
			GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
			boss = bossObj != null ? bossObj.GetComponent<Boss>() : null;
		}

		public void Attack() {
			Debug.Log($"Range: {range}");
			boss.UpdateMeter("Health", -damage);
		}
	}
}
