using UnityEngine;

using DungeonRaid.Abilities.Effects;

namespace DungeonRaid.Characters.Heroes {
	public class Weapon : MonoBehaviour {
		[SerializeField] private Effect[] attackEffects = null;

		private Hero wielder;

		private void Start() {
			wielder = GetComponent<Hero>();
		}

		public void Attack() {
			foreach (Effect attackEffect in attackEffects) {
				attackEffect.Apply(wielder, wielder.TargetCharacter, wielder.TargetPoint);
			}
		}
	}
}
