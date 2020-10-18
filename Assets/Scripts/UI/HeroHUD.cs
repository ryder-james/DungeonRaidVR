using UnityEngine;

using DungeonRaid.Collections;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.UI {
	public class HeroHUD : MonoBehaviour {
		[SerializeField] private Transform meters = null;
		[SerializeField] private GameObject meterUIPrefab = null;
		[SerializeField] private bool onRightSide = false;

		public Hero Hero { get; private set; }

		public void SetHero(Hero hero) {
			Hero = hero;

			foreach (MeterComponent meter in hero.Meters) {
				MeterUI ui = Instantiate(meterUIPrefab, meters.transform).GetComponent<MeterUI>();
				ui.Meter = meter;
				ui.RightToLeft = onRightSide;
			}
		}
	}
}