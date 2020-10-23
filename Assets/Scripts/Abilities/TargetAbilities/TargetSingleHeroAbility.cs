using UnityEngine;

using DungeonRaid.Abilities.Effects;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetHero", menuName = AbilityMenuPrefix + "Target Hero")]
	public class TargetSingleHeroAbility : Ability {
		protected override bool TargetCast(Effect[] effects) {
			Hero target = Owner.TargetCharacter as Hero;
			if (target == null) {
				return false;
			}

			foreach (Effect effect in effects) {
				if (effect.ApplyToCaster) {
					effect.Apply(Owner, Owner, Owner.TargetPoint);
				} else {
					effect.Apply(Owner, target, Owner.TargetPoint);
				}
			}

			return true;
		}
	}
}