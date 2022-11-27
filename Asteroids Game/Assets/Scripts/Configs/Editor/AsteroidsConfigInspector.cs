using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Configs.Editor
{
	[CustomEditor(typeof(AsteroidsConfig))]
	public class AsteroidsConfigInspector : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			if (GUILayout.Button("Add tier"))
			{
				var targetConfig = (AsteroidsConfig)target;
				targetConfig.AsteroidsTiers ??= new List<AsteroidsTierData>();
				var nextTier = targetConfig.AsteroidsTiers.Count > 0
					? targetConfig.AsteroidsTiers.Max(x => x.Tier) + 1
					: 0;

				targetConfig.AsteroidsTiers.Add(new AsteroidsTierData
				{
					Tier = nextTier
				});
			}
		}
	}
}
