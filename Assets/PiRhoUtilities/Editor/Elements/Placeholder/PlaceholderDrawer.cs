﻿using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace PiRhoSoft.Utilities.Editor
{
	[CustomPropertyDrawer(typeof(PlaceholderAttribute))]
	class PlaceholderDrawer : PropertyDrawer
	{
		private const string _invalidDrawerWarning = "(PUPDID) invalid drawer for PlaceholderAttribute on field '{0}': Placeholder can only be applied to fields with TextField drawers";

		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			var element = this.CreateNextElement(property);
			var placeholderAttribute = attribute as PlaceholderAttribute;
			var textField = element.Q<TextField>();

			if (textField != null)
			{
				var placeholder = new Placeholder();
				placeholder.AddToField(textField);

				ReflectionHelper.SetupValueSourceCallback(placeholderAttribute.TextSource, property, placeholder, fieldInfo.DeclaringType, placeholderAttribute.Text, placeholderAttribute.AutoUpdate, nameof(PlaceholderAttribute), nameof(PlaceholderAttribute.TextSource), value => placeholder.text = value);

				return element;
			}
			else
			{
				Debug.LogWarningFormat(_invalidDrawerWarning, property.propertyPath);
				return new FieldContainer(property.displayName);
			}
		}
	}
}