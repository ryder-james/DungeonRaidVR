using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Effects.StatusEffects {
	[CreateAssetMenu(fileName = "TravelOverTime", menuName = StatusEffectMenuPrefix + "Travel Over Time")]
	public class TravelOverTimeStatusEffect : StatusEffect {
		[SerializeField] private float distance = 5;

		protected override void StartEffect(Character target) {
			(target as Hero).Controller.Mover.Manual = true;
			InvokeRepeating(target, t => {
				(t as Hero).Controller.Mover.MoveToward(t.transform.forward, distance / BodyMover.SpeedMultiplier / duration);
			}, 0);
		}

		protected override void StopEffect(Character target) {
			(target as Hero).Controller.Mover.Manual = false;
		}
	}
}
