using UnityEngine;

using DungeonRaid.Abilities.Effects;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetHero", menuName = AbilityMenuPrefix + "Target Hero")]
	public class TargetSingleHeroAbility : Ability {
		protected override bool TargetCast(Effect[] effects) {
			if (!(Owner is Hero)) {
				return false;
			}

			Hero target = (Owner as Hero).TargetCharacter as Hero;
			if (target == null) {
				return false;
			}

			foreach (Effect effect in effects) {
				if (effect.ApplyToCaster) {
					effect.Apply(target, Owner, TargetPoint);
				} else {
					effect.Apply(target, target, TargetPoint);
				}
			}

			return true;
		}
	}
}