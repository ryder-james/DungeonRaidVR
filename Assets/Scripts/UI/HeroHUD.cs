using UnityEngine;
using UnityEngine.UI;

using DungeonRaid.Collections;
using DungeonRaid.Characters.Heroes;
using DungeonRaid.Input;
using UnityEditor;
using DungeonRaid.Abilities;
using DeungonRaid.UI;

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

		public Hero Hero { get; private set; }

		private readonly AbilityHUD[] abilityHUDs = new AbilityHUD[4];

		private void Start() {
			reticle.GetComponent<Image>().color = color;
		}

		public void SetHero(Hero hero) {
			Hero = hero;
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

		public void CastAbility(int abilityIndex) {
			abilityHUDs[abilityIndex].StartCooldown();
		}

		public void MoveReticle(Vector2 moveVector) {
			Vector2 newPos = reticle.anchoredPosition + moveVector * reticleSpeed;
			SetReticlePosition(newPos);
		}

		public void SetReticlePosition(Vector2 newPosition) {
			//Vector3 viewPos = cam.ScreenToViewportPoint(newPosition);
			//viewPos.x = Mathf.Clamp01(viewPos.x);
			//viewPos.y = Mathf.Clamp01(viewPos.y);
			//reticle.anchoredPosition = cam.ViewportToScreenPoint(viewPos);
			reticle.anchoredPosition = newPosition;
			Hero.TestTarget(cam, reticle.anchoredPosition);
		}

		public void UpdateHighlight(GameObject target) {
			Rect bounds = BoundsToScreenRect(target);
			highlight.anchoredPosition = bounds.center;
		}

		//private void OnDrawGizmos() {
		//	Gizmos.DrawWireSphere(cam.ScreenToWorldPoint(reticle.anchoredPosition), 0.5f);
		//}

		public static Rect BoundsToScreenRect(GameObject go) {
			Vector3 cen = go.GetComponent<Renderer>().bounds.center;
			Vector3 ext = go.GetComponent<Renderer>().bounds.extents;
			Vector2[] extentPoints = new Vector2[8] {
				HandleUtility.WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z-ext.z)),
				HandleUtility.WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z-ext.z)),
				HandleUtility.WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z+ext.z)),
				HandleUtility.WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z+ext.z)),
				HandleUtility.WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z-ext.z)),
				HandleUtility.WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z-ext.z)),
				HandleUtility.WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z+ext.z)),
				HandleUtility.WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z+ext.z))
			};
			Vector2 min = extentPoints[0];
			Vector2 max = extentPoints[0];
			foreach (Vector2 v in extentPoints) {
				min = Vector2.Min(min, v);
				max = Vector2.Max(max, v);
			}
			return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
		}
	}
}