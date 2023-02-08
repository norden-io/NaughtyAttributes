using System;

namespace NaughtyAttributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public abstract class BaseLayerStringAttribute : DrawerAttribute {
		public abstract string[] GetLayers();
	}
}
