using UnityEngine;

namespace Common.Extensions {
	public static class BoundsExtensions {
		public static Rect ToScreenSpace(this Bounds bounds, Camera cam, Canvas canvas = null) {
			float scale = canvas == null ? 1 : canvas.scaleFactor;

			Vector3 cen = bounds.center;
			Vector3 ext = bounds.extents;
			Vector2[] extentPoints = new Vector2[8] {
				WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z-ext.z), cam, scale),
				WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z-ext.z), cam, scale),
				WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z+ext.z), cam, scale),
				WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z+ext.z), cam, scale),
				WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z-ext.z), cam, scale),
				WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z-ext.z), cam, scale),
				WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z+ext.z), cam, scale),
				WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z+ext.z), cam, scale)
			};
			Vector2 min = extentPoints[0];
			Vector2 max = extentPoints[0];
			foreach (Vector2 v in extentPoints) {
				min = Vector2.Min(min, v);
				max = Vector2.Max(max, v);
			}

			//Debug.Log($"{min}, {max}");

			return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
		}

		private static Vector2 WorldToGUIPoint(Vector3 world, Camera cam, float scaleFactor) {
			Vector2 screenPoint = cam.WorldToScreenPoint(world) / scaleFactor;
			screenPoint.y = Screen.height - screenPoint.y;
			return screenPoint;
		}
	}
}
