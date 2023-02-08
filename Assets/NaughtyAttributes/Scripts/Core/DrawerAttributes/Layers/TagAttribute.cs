using System;
using System.Collections.Generic;

namespace NaughtyAttributes
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class TagAttribute : BaseLayerStringAttribute
	{
		public override string[] GetLayers()
		{
			List<string> tagList = new List<string>();
			tagList.Add("(None)");
			tagList.Add("Untagged");
			tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);
            
			return tagList.ToArray();
		}
	}
}
