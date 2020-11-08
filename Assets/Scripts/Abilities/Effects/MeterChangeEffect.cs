using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "MeterChange", menuName = EffectMenuPrefix + "Meter Change")]
	public class MeterChangeEffect : ChannelableEffect {
		public enum MeterChangeType {
			Damage,
			Heal
		}

		private Character target;

		[SerializeField] private string targetMeterName = "Health";
		[SerializeField] private MeterChangeType type = MeterChangeType.Damage;
		[SerializeField] private float amount = 0;

		public override void Apply(Character caster, Character target, Vector3 point) {
			this.target = target;
		}

		protected override void Begin() {
			target.UpdateMeter(targetMeterName, amount * (type == MeterChangeType.Damage ? -1 : 1));
		}

		protected override void Channel() {
			target.UpdateMeter(targetMeterName, amount * (type == MeterChangeType.Damage ? -1 : 1));
		}

		protected override void End() {
			throw new System.NotImplementedException();
		}
	}
}
