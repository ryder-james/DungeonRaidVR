namespace DungeonRaid.Abilities.Effects.Improveables {
    public abstract class ImproveableEffect : Effect {
		protected const string ImproveableEffectMenuPrefix = EffectMenuPrefix + "Improveable/";

		public abstract void Reset();
		public abstract void Improve();
    }
}