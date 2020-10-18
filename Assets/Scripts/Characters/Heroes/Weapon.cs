using UnityEngine;

public class Weapon : MonoBehaviour {
	[SerializeField] private float damage = 1;
	[SerializeField] private float range = 1;

	public void Attack() {
		Debug.Log("attack");
	}
}
