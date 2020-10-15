using UnityEngine;

using DungeonRaid.Effects.StatusEffects;
using DungeonRaid.Characters;
using DungeonRaid.Collections;

[CreateAssetMenu(fileName = "DisableMeterRecharge", menuName = StatusEffectMenuPrefix + "Disable Meter Recharge")]
public class DisableMeterRechargeStatusEffect : StatusEffect {
	[SerializeField] private string meterName = "Energy";

	protected override void StartEffect(Character target) {
		MeterComponent meter = target.FindMeter(meterName);
		if (meter != null) {
			meter.IsRecharging = false;
		}
	}

	protected override void StopEffect(Character target) {
		MeterComponent meter = target.FindMeter(meterName);
		if (meter != null) {
			meter.IsRecharging = true;
		}
	}
}
