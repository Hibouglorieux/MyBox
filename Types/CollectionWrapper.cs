using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using MyBox.Internal;

namespace MyBox
{
	/// <summary>
	/// CollectionWrapper used to apply custom drawers to Array fields
	/// </summary>
	[Serializable]
	public class CollectionWrapper<T> : CollectionWrapperBase, IEnumerable<T>
	{
		public T[] Value = new T[0];

		public static implicit operator T[](CollectionWrapper<T> wrapper)
		{
			return wrapper.Value;
		}

		public static implicit operator CollectionWrapper<T>(T[] array)
		{
			CollectionWrapper<T> newWrapper = new CollectionWrapper<T>();
			newWrapper.Value = array;
			return newWrapper;
		}

		public ref T this[int i]
		{
			get => ref Value[i];
		}


	public int Length => Value.Length;

		public IEnumerator<T> GetEnumerator()
		{
			return (IEnumerator<T>)(Value.GetEnumerator());
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	}

}

namespace MyBox.Internal
{
	[Serializable]
	public class CollectionWrapperBase {}
}

#if UNITY_EDITOR
namespace MyBox.Internal
{
	using UnityEditor;
	using UnityEngine;
	
	[CustomPropertyDrawer(typeof(CollectionWrapperBase), true)]
	public class CollectionWrapperDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var collection = property.FindPropertyRelative("Value");
			return EditorGUI.GetPropertyHeight(collection, true);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var collection = property.FindPropertyRelative("Value");
			EditorGUI.PropertyField(position, collection, label, true);
		}
	}
}
#endif