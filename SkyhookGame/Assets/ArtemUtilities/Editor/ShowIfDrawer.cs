using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfDrawer : PropertyDrawer
{
    private ShowIfAttribute hideShow;
    private SerializedProperty comparedField;

    private float propertyHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return propertyHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Set the global variables.
        hideShow = attribute as ShowIfAttribute;
        comparedField = property.serializedObject.FindProperty(hideShow.ComparedPropertyName);

        object comparedFieldValue = null;
        // Get the value of the compared field.
        switch (comparedField.propertyType)
        {
            case SerializedPropertyType.Boolean:
                comparedFieldValue = comparedField.boolValue;
                break;
            case SerializedPropertyType.String:
                comparedFieldValue = comparedField.stringValue;
                break;
            case SerializedPropertyType.Float:
                comparedFieldValue = comparedField.floatValue;
                break;
            case SerializedPropertyType.Integer:
                comparedFieldValue = comparedField.intValue;
                break;
            case SerializedPropertyType.Enum:
                comparedFieldValue = comparedField.enumNames[comparedField.enumValueIndex];
                break;
            case SerializedPropertyType.ObjectReference:
                comparedFieldValue = comparedField.objectReferenceValue;
                break;
        }

        if (comparedFieldValue == null)
            return;

        // Is the condition met? Should the field be drawn?
        bool conditionMet = false;

        // Compare the values to see if the condition is met.
        switch (hideShow.ComparisonType)
        {
            case ComparisonType.Equals:
                if (comparedFieldValue.Equals(hideShow.ComparedValue))
                    conditionMet = true;
                break;

            case ComparisonType.NotEqual:
                if (!comparedFieldValue.Equals(hideShow.ComparedValue))
                    conditionMet = true;
                break;
        }

        // The height of the property should be defaulted to the default height.
        propertyHeight = base.GetPropertyHeight(property, label);

        // If the condition is met, simply draw the field. Else...
        if (conditionMet)
            EditorGUI.PropertyField(position, property);
        else
            propertyHeight = 0f;
    }
}