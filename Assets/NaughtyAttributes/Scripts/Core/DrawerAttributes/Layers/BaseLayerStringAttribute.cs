using System;

namespace NaughtyAttributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public abstract class BaseLayerStringAttribute : DrawerAttribute {
		public virtual string[] GetLayers() => new [] {"- NOT IMPLEMENTED -"};
	}
}
