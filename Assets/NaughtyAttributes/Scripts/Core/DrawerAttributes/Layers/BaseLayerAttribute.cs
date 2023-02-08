using System;

namespace NaughtyAttributes
{
	public abstract class BaseLayerAttribute : BaseLayerStringAttribute
	{
		public bool mask { get; set; }
		
		public virtual int NameToLayer(string layerName)
		{
			return Array.IndexOf(GetLayers(), layerName); 
		}
		
		public virtual string LayerToName(int layer)
		{
			return GetLayers()[layer];
		}
		
		public BaseLayerAttribute(bool mask = false)
		{
			this.mask = mask;
		}
		
		/// <summary>
		/// Converts field LayerMask values to in game LayerMask values
		/// </summary>
		/// <param name="fieldMask"></param>
		/// <returns></returns>
		public int LayerMaskFromFieldMask(int fieldMask) {
			if (fieldMask == -1) return -1;
			int      mask   = 0;
			string[] layers = GetLayers();
			for (int c = 0; c < layers.Length; c++) {
				int layer = NameToLayer(layers[c]);
				if ((fieldMask & (1 << c)) != 0) {
					mask |= 1 << layer;
				}
				else {
					mask &= ~(1 << layer);
				}
			}
			return mask;
		}
		
		/// <summary>
		/// Converts in game LayerMask values to field LayerMask values
		/// </summary>
		/// <param name="layerMask"></param>
		/// <returns></returns>
		public int FieldMaskFromLayerMask(int layerMask) {
			if (layerMask == -1) return -1;
			int field  = 0;
			var layers = GetLayers();
			for (int c = 0; c < layers.Length; c++) {
				int layer = NameToLayer(layers[c]);
				if ((layerMask & (1 << layer)) != 0) {
					field |= 1 << c;
				}
			}

			return field;
		}
	}
}
