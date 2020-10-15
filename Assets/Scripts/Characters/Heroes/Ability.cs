using UnityEngine;

using Sirenix.OdinInspector;

using DungeonRaid.Effects;
using DungeonRaid.Characters;
using DungeonRaid.Characters.Abilities;

[CreateAssetMenu(fileName = "Ability", menuName = "Dungeon Raid/Ability")]
public abstract class Ability : ScriptableObject {
	[SerializeField] private Cost[] costs = null;
	[SerializeField] private Effect[] effects = null;

	[SerializeField] private DurationType durationType = DurationType.Instant;

	[ShowIf("DurationType", DurationType.Timed)]
	[SerializeField] private float duration = 0;

	[ShowIf("DurationType", DurationType.Channeled)]
	[SerializeField] private float maxChannelTime = 0;

	public Character Owner { get; set; }
	public DurationType DurationType { get => durationType; set => durationType = value; }

	public bool CanCast() {
		bool canCast = true;

		foreach (Cost cost in costs) {
			if (!Owner.CanAfford(cost)) {
				canCast = false;
				break;
			}
		}

		return canCast;
	}

	public bool Cast() {
		bool canCast = CanCast();

		if (canCast) {
			TargetCast();
		}

		return canCast;
	}

	protected abstract void TargetCast();
}
