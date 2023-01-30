using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class SaveRuntimePrefab : EditorWindow
{
	public class GameObjectPair
	{
		public Transform m_runtime;
		public Transform m_prefab;
	}

	private List<GameObjectPair> m_list = new List<GameObjectPair>();
	private ReorderableList m_listGUI;

	[MenuItem("ProjectF/特效工具/SaveRuntimePrefab")]
	private static void ShowWindow ()
	{
		GetWindow<SaveRuntimePrefab>().Show();
	}

	private void OnEnable ()
	{
		m_listGUI = new ReorderableList(m_list, typeof(GameObjectPair), false, false, true, true)
		{
			/*drawElementBackgroundCallback = delegate(Rect rect, int index, bool isActive, bool isFocused)
			{
			 
			},*/
			drawElementCallback = (rect, index, active, focused) =>
			{
				var goPair = m_list[index];

				rect.width /= 2;
				goPair.m_runtime = EditorGUI.ObjectField(rect, goPair.m_runtime, typeof(Transform), true) as Transform;

				rect.x += this.position.width / 2;
				goPair.m_prefab = EditorGUI.ObjectField(rect, goPair.m_prefab, typeof(Transform), false) as Transform;
			},
		};
	}

	private void OnGUI ()
	{
		m_listGUI.DoLayoutList();
		if (GUILayout.Button("Clear"))
		{
			m_list.Clear();
		}

		if (GUILayout.Button("Apply"))
		{
			for (var i = 0; i < m_list.Count; i++)
			{
				var pair = m_list[i];
				if (IsHierarchyEqual(pair.m_runtime, pair.m_prefab))
				{
					var path = AssetDatabase.GetAssetPath(pair.m_prefab.gameObject);
					PrefabUtility.SaveAsPrefabAssetAndConnect(pair.m_runtime.gameObject, path, InteractionMode.UserAction);
				}
				else
				{
					Debug.LogWarning($"{i} not Equal");
				}
			}
		}
	}

	public static bool IsHierarchyEqual (Transform goA, Transform goB)
	{
		if (goA == null || goB == null)
			return false;

		if (goA.name.Replace("(Clone)", "") != goB.name)
			return false;

		if (goA.childCount != goB.childCount)
			return false;

		for (var i = 0; i < goA.childCount; i++)
		{
			var childA = goA.GetChild(i);
			var childB = goB.Find(childA.name);
			if (childB == null)
				return false;

			if (IsHierarchyEqual(childA, childB) == false)
				return false;
		}

		return true;
	}
}
