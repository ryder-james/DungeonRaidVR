using UnityEngine;

using DungeonRaid.Abilities.Effects;
using DungeonRaid.Characters.Heroes;
using DungeonRaid.Characters.Bosses;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetBoss", menuName = AbilityMenuPrefix + "Target Boss")]
	public class TargetBossAbility : Ability {
		protected override bool TargetCast() {
			Boss boss = (Owner as Hero).TargetCharacter as Boss;
			if (boss == null) {
				return false;
			}

			foreach (Effect effect in Effects) {
				if (effect.ApplyToCaster) {
					effect.Apply(Owner);
				} else {
					effect.Apply(boss);
				}
			}

			return true;
		}
	}
}