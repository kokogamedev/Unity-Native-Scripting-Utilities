using System;
using UnityEditor;
using UnityEngine;

namespace PsigenVision.Utilities.Editor
{
    public static class StructGUI
    {
#if UNITY_2022_3_OR_NEWER
        /// <summary>
        /// Try to draw a struct's field as a property field and sync the changes to the outer struct property
        /// </summary>
        /// <param name="fieldRect">Rectangle on the screen to use for the property field.</param>
        /// <param name="fieldName">The member variable name of the field within the struct in which it is contained.</param>
        /// <param name="fieldProperty">The field's SerializedProperty for which we are drawing a property field.</param>
        /// <param name="outerStructProperty">The outer struct property that contains the field property.</param>
        /// <param name="failureMessage">The error message to log if the modified value of the field property could not be applied to the outer struct property.</param>
        /// <returns></returns>
        public static bool TryPropertyField(Rect fieldRect, string fieldName, SerializedProperty fieldProperty, SerializedProperty outerStructProperty, string failureMessage = null)
        {
            bool success = false;
            
            //Check for any changes to the property field of the struct instance
            EditorGUI.BeginChangeCheck();
            //Draw the field property
            EditorGUI.PropertyField(fieldRect, fieldProperty);
            //If any changes were made to the field property, attempt to apply the modified value to the outer struct property
            if (EditorGUI.EndChangeCheck() 
                // Apply changes to the field property before attempting to apply its modified value to the outer struct property
                && fieldProperty.serializedObject.ApplyModifiedProperties() 
                //Try to apply the modified value of the field property to the outer struct property
                && !(success = TryApplyModifiedProperty(fieldProperty, fieldName, outerStructProperty)))
                //If the modified value of the field property could not be applied to the outer struct property, log an error
                Debug.LogError(failureMessage ?? "Failed to update struct field property");
            
            return success;
        }
        
        /// <summary>
        /// Try to draw a struct's field as a property field and sync the changes to the outer struct property
        /// </summary>
        /// <param name="fieldRect">Rectangle on the screen to use for the property field.</param>
        /// <param name="fieldName">The member variable name of the field within the struct in which it is contained.</param>
        /// <param name="fieldProperty">The field's SerializedProperty for which we are drawing a property field.</param>
        /// <param name="outerStructProperty">The outer struct property that contains the field property.</param>
        /// <param name="failureMessage">The error message to log if the modified value of the field property could not be applied to the outer struct property.</param>
        /// <returns></returns>
        public static bool PropertyField(this SerializedProperty fieldProperty, Rect fieldRect, string fieldName, SerializedProperty outerStructProperty, string failureMessage = null) => 
            TryPropertyField(fieldRect, fieldName, fieldProperty, outerStructProperty, failureMessage);
        
        /// <summary>
        /// Try to draw a struct's field as a property field and sync the changes to the outer struct property
        /// </summary>
        /// <param name="fieldRect">Rectangle on the screen to use for the property field.</param>
        /// <param name="fieldName">The member variable name of the field within the struct in which it is contained.</param>
        /// <param name="fieldProperty">The field's SerializedProperty for which we are drawing a property field.</param>
        /// <param name="outerStructProperty">The outer struct property that contains the field property.</param>
        /// <param name="label">Optional label to use. If not specified the label of the property itself is used. Use GUIContent.none to not display a label at all.</param>
        /// <param name="includeChildren">If true the property including children is drawn; otherwise only the control itself (such as only a foldout but nothing below it).</param>
        /// <param name="failureMessage">The error message to log if the modified value of the field property could not be applied to the outer struct property.</param>
        /// <returns></returns>
        public static bool TryPropertyField(Rect fieldRect, string fieldName, SerializedProperty fieldProperty, SerializedProperty outerStructProperty, GUIContent label, bool includeChildren = false, string failureMessage = null)
        {
            bool success = false;
            
            //Check for any changes to the property field of the struct instance
            EditorGUI.BeginChangeCheck();
            //Draw the field property
            EditorGUI.PropertyField(fieldRect, fieldProperty, label, includeChildren);
            //If any changes were made to the field property, attempt to apply the modified value to the outer struct property
            if (EditorGUI.EndChangeCheck() 
                // Apply changes to the field property before attempting to apply its modified value to the outer struct property
                && fieldProperty.serializedObject.ApplyModifiedProperties() 
                //Try to apply the modified value of the field property to the outer struct property
                && !(success = TryApplyModifiedProperty(fieldProperty, fieldName, outerStructProperty)))
                //If the modified value of the field property could not be applied to the outer struct property, log an error
                Debug.LogError(failureMessage ?? "Failed to update struct field property");
            
            return success;
        }

        /// <summary>
        /// Try to draw a struct's field as a property field and sync the changes to the outer struct property
        /// </summary>
        /// <param name="fieldRect">Rectangle on the screen to use for the property field.</param>
        /// <param name="fieldName">The member variable name of the field within the struct in which it is contained.</param>
        /// <param name="fieldProperty">The field's SerializedProperty for which we are drawing a property field.</param>
        /// <param name="outerStructProperty">The outer struct property that contains the field property.</param>
        /// <param name="label">Optional label to use. If not specified the label of the property itself is used. Use GUIContent.none to not display a label at all.</param>
        /// <param name="includeChildren">If true the property including children is drawn; otherwise only the control itself (such as only a foldout but nothing below it).</param>
        /// <param name="failureMessage">The error message to log if the modified value of the field property could not be applied to the outer struct property.</param>
        /// <returns></returns>
        public static bool PropertyField(this SerializedProperty fieldProperty, Rect fieldRect, string fieldName, SerializedProperty outerStructProperty, GUIContent label, bool includeChildren = false, string failureMessage = null) => 
            TryPropertyField(fieldRect, fieldName, fieldProperty, outerStructProperty, label, includeChildren, failureMessage);

        /// <summary>
        /// Try to apply modifications to the field of a struct to both that field and the struct instance to which it belongs
        /// via the field's modified SerializedProperty. 
        /// </summary>
        /// <param name="fieldProp">The modified field's SerializedProperty.</param>
        /// <param name="fieldName">The name of the field within the class/struct</param>
        /// <param name="outerStructProperty"></param>
        /// <returns></returns>
        /// <remarks>This method is designed to overcome limitations of SerializedProperty's ApplyModifiedProperties() method,
        /// which does not always apply modifications to fields of structs back to the instance that contains it due to value type semantics.</remarks>
        public static bool TryApplyModifiedProperty(SerializedProperty fieldProp, string fieldName, SerializedProperty outerStructProperty)
        {
            //In the case that the outer struct property is an array element, we need to prepend the fieldName (or path) passed in with an array-access.
            //This is because the type that will be extracted will be that of the collection rather than the element within (which is what the user intends to set)
            if (outerStructProperty.propertyPath.EndsWith("]"))
                fieldName = PrependArrayAccessPath(fieldName, outerStructProperty, out outerStructProperty);
            
            //Attempt to set the boxed value of the field to an instance of the struct containing it via its path within the outer struct
            if (outerStructProperty.TrySetBoxedValueViaPath(outerStructProperty.GetSystemType(), fieldName, fieldProp,
                    out var modifiedObject)) return false;
            
            //If the boxed value of the field was successfully set, apply the modified value to the outer struct property
            outerStructProperty.boxedValue = modifiedObject;
            return outerStructProperty.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Prepends an array access segment to the given field access path to correctly address elements within arrays or lists in Unity's SerializedProperty system.
        /// </summary>
        /// <param name="fieldAccessPath">The original field access path to be prepended with the array access segment.</param>
        /// <param name="previousOuterProperty">The SerializedProperty representing the containing object of the field (in this case, the array's serialized property).</param>
        /// <param name="newOuterProperty">The updated SerializedProperty reference representing the object containing the array instance.</param>
        /// <returns>The modified field access path with the array access segment prepended.</returns>
        private static string PrependArrayAccessPath(string fieldAccessPath, SerializedProperty previousOuterProperty,
            out SerializedProperty newOuterProperty)
        {
            var path = previousOuterProperty.propertyPath;
            // Unity's SerializedProperty uses "Array.data" for array and list elements.
            // Let's sanitize the path by removing those segments first.
            path = path.Replace(".Array.data", ""); // Explicitly strip Unity's "Array.data" pattern.
            var dotSeparations = path.Split(".");
            //Prepend the final array-access portion of the path to the fieldName (or path)
            fieldAccessPath = $"{dotSeparations[^1]}.{fieldAccessPath}";
            newOuterProperty = previousOuterProperty.serializedObject.FindProperty(dotSeparations[^2]);
            return fieldAccessPath;
        }

        /// <summary>
        /// Try to apply modifications to the field of a struct to both that field and the struct instance to which it belongs
        /// via the field's modified SerializedProperty. 
        /// </summary>
        /// <param name="fieldProp">The modified field's SerializedProperty.</param>
        /// <param name="fieldName">The name of the field within the class/struct</param>
        /// <param name="outerStructProperty"></param>
        /// <returns></returns>
        /// <remarks>This method is designed to overcome limitations of SerializedProperty's ApplyModifiedProperties() method,
        /// which does not always apply modifications to fields of structs back to the instance that contains it due to value type semantics.</remarks>
        public static bool ApplyModifiedProperty(this SerializedProperty fieldProp, string fieldName, SerializedProperty outerStructProperty) => 
            TryApplyModifiedProperty(fieldProp, fieldName, outerStructProperty);

#endif
    }
}