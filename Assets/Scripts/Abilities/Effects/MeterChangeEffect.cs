using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "MeterChange", menuName = EffectMenuPrefix + "Meter Change")]
	public class MeterChangeEffect : Effect {
		public enum MeterChangeType {
			Damage,
			Heal
		}

		[SerializeField] private string targetMeterName = "Health";
		[SerializeField] private MeterChangeType type = MeterChangeType.Damage;
		[SerializeField] private float amount = 0;

		public override void Apply(Character target) {
			Debug.Log(target);
			Debug.Log(amount);
			Debug.Log(target.FindMeter(targetMeterName));
			target.UpdateMeter(targetMeterName, amount * (type == MeterChangeType.Damage ? -1 : 1));
		}
	}
}
