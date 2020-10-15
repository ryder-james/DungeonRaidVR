using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Effects {
	[CreateAssetMenu(fileName = "MeterChangeEffect", menuName = EffectMenuPrefix + "Meter Change")]
	public class MeterChangeEffect : Effect {
		[SerializeField] private string targetMeterName = "Health";
		[SerializeField] private float amount = 0;
		public override void Apply(Character target) {
			target.UpdateMeter(targetMeterName, amount);
		}
	}
}
