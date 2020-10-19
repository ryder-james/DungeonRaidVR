using UnityEngine;
using UnityEngine.UI;

using DungeonRaid.Collections;
using DungeonRaid.Characters.Heroes;
using DungeonRaid.Input;

namespace DungeonRaid.UI {
	public class HeroHUD : MonoBehaviour {
		[SerializeField, Range(1, 20)] private float reticleSpeed = 3.5f;
		[SerializeField] private bool onRightSide = false;
		[SerializeField] private Transform meters = null;
		[SerializeField] private GameObject meterUIPrefab = null;
		[SerializeField] private GameObject reticle = null;
		[SerializeField] private Color color = Color.white;
		[SerializeField] private Canvas canvas = null;

		public Hero Hero { get; private set; }
		public Vector2 ReticlePoint {
			get => reticle.transform.localPosition;
			set => reticle.transform.localPosition = value;
		}

		private void Start() {
			reticle.GetComponent<Image>().color = color;
		}

		public void SetHero(Hero hero) {
			Hero = hero;
			Hero.GetComponent<HeroController>().HUD = this;
			reticle.SetActive(true);

			foreach (MeterComponent meter in hero.Meters) {
				MeterUI ui = Instantiate(meterUIPrefab, meters.transform).GetComponent<MeterUI>();
				ui.Meter = meter;
				ui.RightToLeft = onRightSide;
			}
		}

		public void MoveReticle(Vector2 moveVector) {
			reticle.transform.localPosition = (Vector2) reticle.transform.localPosition + moveVector * reticleSpeed;
			Vector3 viewPos = Camera.main.WorldToViewportPoint(reticle.transform.position);
			viewPos.x = Mathf.Clamp01(viewPos.x);
			viewPos.y = Mathf.Clamp01(viewPos.y);
			reticle.transform.position = Camera.main.ViewportToWorldPoint(viewPos);
		}
	}
}