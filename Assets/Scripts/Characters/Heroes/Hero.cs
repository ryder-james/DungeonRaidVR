using UnityEngine;

namespace DungeonRaid.Characters.Heroes {
	[RequireComponent(typeof(HeroController))]
	public class Hero : Character {
		[SerializeField] private float speed = 1;
		[SerializeField] private GameObject weapon = null;
		[SerializeField] private Ability[] abilities = null;

		public HeroController Controller { get; private set; }
		public float Speed { get => speed; set => speed = value; }

		protected override void Start() {
			base.Start();

			Controller = GetComponent<HeroController>();

			foreach (Ability ability in abilities) {
				ability.Owner = this;
			}
		}

		public void UseAbility(int index) {
			abilities[index].Cast();
		}

		protected override float CalculateHealth(int heroCount) {
			return 1;
		}
	}
}
