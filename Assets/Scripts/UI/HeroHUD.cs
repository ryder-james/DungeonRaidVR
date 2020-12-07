using UnityEngine;
using UnityEngine.UI;

using JCommon.Extensions;

using DungeonRaid.Collections;
using DungeonRaid.Characters.Heroes;
using DungeonRaid.Input;
using DungeonRaid.Abilities;
using DungeonRaid.Characters;

namespace DungeonRaid.UI {
	public class HeroHUD : MonoBehaviour {
		[SerializeField, Range(1, 20)] private float reticleSpeed = 3.5f;
		[SerializeField] private bool onRightSide = false;
		[SerializeField] private Transform meters = null;
		[SerializeField] private Transform abilities = null;
		[SerializeField] private GameObject meterUIPrefab = null;
		[SerializeField] private GameObject abilityIconPrefab = null;
		[SerializeField] private RectTransform reticle = null;
		[SerializeField] private RectTransform highlight = null;
		[SerializeField] private Color color = Color.white;
		[SerializeField] private Camera cam = null;
		[SerializeField] private Canvas canvas = null;

		public Hero Hero { get; private set; }

		private readonly AbilityHUD[] abilityHUDs = new AbilityHUD[4];

		private Character prevTarget = null;

		private void Start() {
			reticle.GetComponent<Image>().color = color;
			highlight.GetComponent<Image>().color = color;
		}

		public void SetHero(Hero hero) {
			Hero = hero;
			Hero.Color = color;
			Hero.GetComponent<HeroController>().HUD = this;
			reticle.gameObject.SetActive(true);

			foreach (MeterComponent meter in hero.Meters) {
				MeterUI ui = Instantiate(meterUIPrefab, meters.transform).GetComponent<MeterUI>();
				ui.Meter = meter;
				ui.RightToLeft = onRightSide;
			}

			for (int i = 0; i < Hero.Abilities.Length; i++) {
				Ability ability = Hero.Abilities[i];
				Image img = Instantiate(abilityIconPrefab, abilities.transform).GetComponent<Image>();
				img.sprite = ability.Icon;
				abilityHUDs[i] = img.GetComponent<AbilityHUD>();
				abilityHUDs[i].Ability = ability;
			}

			LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
		}

		public void ShowCooldown(int abilityIndex) {
			abilityHUDs[abilityIndex].StartCooldown();
		}

		public void MoveReticle(Vector2 moveVector) {
			Vector2 newPos = reticle.anchoredPosition + moveVector * reticleSpeed;
			SetReticlePosition(newPos);
		}

		public void SetReticlePosition(Vector2 newPosition) {
			Vector3 viewPos = cam.ScreenToViewportPoint(newPosition) * canvas.scaleFactor;

			viewPos.x = Mathf.Clamp01(viewPos.x);
			viewPos.y = Mathf.Clamp01(viewPos.y);
			Vector2 screenPos = cam.ViewportToScreenPoint(viewPos) / canvas.scaleFactor;

			reticle.anchoredPosition = screenPos;

			if (Hero.CheckForTarget(cam, reticle.position)) {
				UpdateHighlightPosition(Hero.TargetCharacter);
			} else {
				highlight.gameObject.SetActive(false);
			}
		}

		private void UpdateHighlightPosition(Character target) {
			if (target == null) {
				highlight.gameObject.SetActive(false);
				return;
			}

			Vector2 screenPos = cam.WorldToScreenPoint(target.Center) / canvas.scaleFactor;
			highlight.anchoredPosition = screenPos + Vector2.up * 30;

			UpdateHighlightSize(target);

			highlight.gameObject.SetActive(true);

			prevTarget = target;
		}

		private void UpdateHighlightSize(Character target) {
			Renderer rend = target.GetComponentInChildren<Renderer>();
			Rect screenRect = rend.bounds.ToScreenSpace(cam, canvas);

			float width = Mathf.Max(75, screenRect.width + 30);
			float height = Mathf.Max(75, screenRect.height + 10);

			highlight.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
			highlight.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
		}
	}
}