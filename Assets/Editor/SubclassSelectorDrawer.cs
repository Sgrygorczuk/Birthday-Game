#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

[CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
public class SubclassSelectorDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.indentLevel = 0;
        Rect lineRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        if (property.propertyType != SerializedPropertyType.ManagedReference)
        {
            EditorGUI.PropertyField(lineRect, property, GUIContent.none, true);
            return;
        }

        EditorGUI.BeginProperty(position, label, property);

        // Determine if we should use categories
        bool useCategory = !typeof(ModifierType).IsAssignableFrom(fieldInfo.FieldType);

        // Get all concrete derived types
        var types = TypeCache.GetTypesDerivedFrom(fieldInfo.FieldType)
            .Where(t => !t.IsAbstract && !t.IsGenericType)
            .OrderBy(type => type.Name)
            .ToList();

        // Build entries
        var typeEntries = types.Select(t =>
        {
            var instance = Activator.CreateInstance(t) as PickUpType;
            string category = instance?.Category ?? "Default";
            string name = ObjectNames.NicifyVariableName(t.Name);
            string labelName = useCategory ? $"{category}/{name}" : name;
            return new { Type = t, Label = labelName };
        }).OrderBy(type => type.Label).ToList().ToList();

        int currentIndex = -1;
        if (property.managedReferenceValue != null)
            currentIndex = typeEntries.FindIndex(e => e.Type == property.managedReferenceValue.GetType());

        // Draw dropdown inline with label
        string[] popupOptions = typeEntries.Select(e => e.Label).ToArray();
        int selectedIndex = EditorGUI.Popup(lineRect, $"     {label.text}", Math.Max(currentIndex, 0), popupOptions);

        if (selectedIndex != currentIndex)
        {
            property.managedReferenceValue = Activator.CreateInstance(typeEntries[selectedIndex].Type);
            property.isExpanded = true;

            // Auto-create default Modifier for StatType
            if (property.managedReferenceValue is StatType stat && stat.ModifierType == null)
            {
                var modTypes = TypeCache.GetTypesDerivedFrom(typeof(ModifierType))
                    .Where(t => !t.IsAbstract && !t.IsGenericType)
                    .ToList();
                if (modTypes.Any())
                    stat.ModifierType = Activator.CreateInstance(modTypes.First()) as ModifierType;

                // Auto-expand nested modifier
                var nestedProperty = property.FindPropertyRelative("modifier");
                if (nestedProperty != null)
                    nestedProperty.isExpanded = true;
            }
        }

        // Draw foldout for the property
        if (property.managedReferenceValue != null)
        {
            property.isExpanded = EditorGUI.Foldout(lineRect, property.isExpanded, GUIContent.none, true);

            if (property.isExpanded)
            {
                // Draw nested property recursively
                EditorGUI.PropertyField(lineRect, property, GUIContent.none, true);
            }
        }

        EditorGUI.EndProperty();
    }
}
#endif