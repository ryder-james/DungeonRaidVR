using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects.StatusEffects {
	[CreateAssetMenu(fileName = "StopMovement", menuName = MovementEffectMenuPrefix + "Stop Movement")]
	public class StopMovementEffect : ChannelableEffect {
		private Hero hero;

		public override void Apply(Character caster, Character target, Vector3 point) {
			hero = target as Hero;
			hero.CanMove = false;
		}

		protected override void Begin() {
		}

		protected override void Channel() {
		}

		protected override void End() {
			hero.CanMove = true;
		}
	}
}
