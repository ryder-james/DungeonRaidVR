using UnityEngine;

using DungeonRaid.Input;
using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects.StatusEffects {
	[CreateAssetMenu(fileName = "TravelOverTime", menuName = StatusEffectMenuPrefix + "Travel Over Time")]
	public class TravelOverTimeStatusEffect : StatusEffect {
		[SerializeField] private float distance = 5;

		protected override void StartEffect(Character target) {
			(target as Hero).Controller.Mover.Manual = true;
			InvokeRepeating(target, t => {
				HeroController controller = (t as Hero).Controller;
				controller.Mover.MoveToward(controller.Direction, distance / BodyMover.SpeedMultiplier / duration);
			}, 0);
		}

		protected override void StopEffect(Character target) {
			(target as Hero).Controller.Mover.Manual = false;
		}
	}
}
