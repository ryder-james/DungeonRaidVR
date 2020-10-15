using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Effects {
	
	[CreateAssetMenu(fileName = "StatChangeEffect", menuName = EffectMenuPrefix + "Stat Change")]
	public class SpeedChangeEffect : Effect {
		[SerializeField] private float amount = 0;
		public override void Apply(Character target) {
			// TODO
		}
	}
}
