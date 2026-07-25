using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PsigenVision.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(IntOverride))]
    [CustomPropertyDrawer(typeof(FloatOverride))]
    [CustomPropertyDrawer(typeof(StringOverride))]
    [CustomPropertyDrawer(typeof(Vector3Override))]
    [CustomPropertyDrawer(typeof(Vector3IntOverride))]
    [CustomPropertyDrawer(typeof(Vector2Override))]
    [CustomPropertyDrawer(typeof(Vector2IntOverride))]
    [CustomPropertyDrawer(typeof(Vector4Override))]
    [CustomPropertyDrawer(typeof(QuaternionOverride))]
    [CustomPropertyDrawer(typeof(ColorOverride))]
    [CustomPropertyDrawer(typeof(RectOverride))]
    [CustomPropertyDrawer(typeof(RectIntOverride))]
    [CustomPropertyDrawer(typeof(LayerMaskOverride))]
    [CustomPropertyDrawer(typeof(BoundsOverride))]
    [CustomPropertyDrawer(typeof(BoundsIntOverride))]
    [CustomPropertyDrawer(typeof(GradientOverride))]
    [CustomPropertyDrawer(typeof(RenderingLayerMaskOverride))]
    [CustomPropertyDrawer(typeof(Hash128Override))]
    [CustomPropertyDrawer(typeof(CharOverride))]
#if UNITY_6000_2_OR_NEWER
    [CustomPropertyDrawer(typeof(EntityIdOverride))]
#endif
    [CustomPropertyDrawer(typeof(AnimationCurveOverride))]
    public class ValueOverrideDrawer : PropertyDrawer
    {
        private static ((string label, string path, float propertyHeight) Default, (string label, string path, float propertyHeight) Override) DEFAULT =
            (Default: (label: "Default", path: "defaultValue", propertyHeight: EditorGUIUtility.singleLineHeight), Override: (label: "Override", path: "overrideValue", propertyHeight: EditorGUIUtility.singleLineHeight));
        
        private static readonly Dictionary<Type, ((string label, string path, float propertyHeight) Default, (string label, string path, float propertyHeight) Override)> Data = new ()
        {
            {typeof(FloatOverride), DEFAULT},
            {typeof(IntOverride), DEFAULT},
            {typeof(CharOverride), DEFAULT},
            {typeof(StringOverride), DEFAULT},
            {typeof(Hash128Override), DEFAULT},
            {typeof(Vector3Override), DEFAULT},
            {typeof(Vector3IntOverride), DEFAULT},
            {typeof(Vector2Override), DEFAULT},
            {typeof(Vector2IntOverride), DEFAULT},
            {
                typeof(Vector4Override), 
                (Default: (label: "Default", path: "defaultValue", propertyHeight: EditorGUIUtility.singleLineHeight), Override: (label: "Override", path: "overrideValue", propertyHeight: 2*EditorGUIUtility.singleLineHeight))
            },
            {typeof(ColorOverride), DEFAULT},
            {typeof(QuaternionOverride), DEFAULT},
            {typeof(LayerMaskOverride), DEFAULT},
            {
                typeof(RectOverride), 
                (Default: (label: "Default", path: "defaultValue", propertyHeight: EditorGUIUtility.singleLineHeight), Override: (label: "Override", path: "overrideValue", propertyHeight: 2*EditorGUIUtility.singleLineHeight))
            },
            {
                typeof(RectIntOverride), 
                (Default: (label: "Default", path: "defaultValue", propertyHeight: EditorGUIUtility.singleLineHeight), Override: (label: "Override", path: "overrideValue", propertyHeight: 2*EditorGUIUtility.singleLineHeight))
            },
            {
                typeof(BoundsOverride), 
                (Default: (label: "Default", path: "defaultValue", propertyHeight: EditorGUIUtility.singleLineHeight), Override: (label: "Override", path: "overrideValue", propertyHeight: 2*EditorGUIUtility.singleLineHeight))
            },
            {
                typeof(BoundsIntOverride),
                (Default: (label: "Default", path: "defaultValue", propertyHeight: EditorGUIUtility.singleLineHeight), Override: (label: "Override", path: "overrideValue", propertyHeight: 2*EditorGUIUtility.singleLineHeight))
            },
            {typeof(GradientOverride), DEFAULT},
            {typeof(RenderingLayerMaskOverride), DEFAULT}
            #if UNITY_6000_2_OR_NEWER
            ,{typeof(EntityIdOverride), DEFAULT}, { typeof(AnimationCurveOverride), DEFAULT }
            #endif
        };

        private static (float posScale, float widthScale) DoOverrideRect = (posScale: 0f, widthScale: 0.25f);
        private static (float posScale, float widthScale) OverrideRect = (posScale: DoOverrideRect.widthScale + 0.02f, widthScale: 1 - DoOverrideRect.widthScale - 0.02f);
        private static (float posScale, float widthScale) DefaultRect = (posScale: OverrideRect.posScale, widthScale: (OverrideRect.widthScale * 2f/3f) - 0.02f);
        private static (float posScale, float widthScale) ButtonRect = (posScale: OverrideRect.posScale + DefaultRect.widthScale + 0.02f, widthScale: (OverrideRect.widthScale/3f) - 0.02f);
        
        private bool resettingDefault = false;
        private (Color normal, Color active, Color focused) defaultButtonTextColor = 
            (GUI.skin.button.normal.textColor, GUI.skin.button.active.textColor, GUI.skin.button.focused.textColor);
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.serializedObject.Update();

            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), label);
            (float Default, float Override) propertyHeight = (Data[property.GetSystemType()].Default.propertyHeight, Data[property.GetSystemType()].Override.propertyHeight);
            
            
            #region Override Toggle - Property Caching

            var doOverrideProperty = property.FindPropertyRelative("doOverride");
            var previousOverrideStatus = doOverrideProperty.boolValue;
            
            #endregion
            
            #region Calculate Rects
            
            var height = (previousOverrideStatus || resettingDefault) ? propertyHeight.Override : propertyHeight.Default;

            (Rect Toggle, Rect Override, Rect Default, Rect Button) _Rect = (
                Toggle: new Rect(position.x, position.y, position.width * DoOverrideRect.widthScale - 4, height),
                Override: new Rect(position.x + OverrideRect.posScale * position.width, position.y, position.width * OverrideRect.widthScale - 4, height),
                Default: new Rect(position.x + DefaultRect.posScale * position.width, position.y, position.width * DefaultRect.widthScale - 4, height),
                Button: new Rect(position.x + ButtonRect.posScale * position.width, position.y, position.width * ButtonRect.widthScale - 4, height));
            
            (Rect Label, Rect Toggle) ToggleRect = (
                Label: new Rect(_Rect.Toggle.x, _Rect.Toggle.y, _Rect.Toggle.width*0.9f, _Rect.Toggle.height),
                Toggle: new Rect(_Rect.Toggle.x + _Rect.Toggle.width*0.902f, _Rect.Toggle.y, _Rect.Toggle.width*0.098f, _Rect.Toggle.height)); 

            #endregion

            #region Override Toggle - Property Drawing
            
            EditorGUI.BeginChangeCheck();
            //PropertyField attempts to auto-allocate space for a label, which distorts our measurements. The Label Field must be explicitly placed
            EditorGUI.LabelField(ToggleRect.Label, DEFAULT.Override.label);
            EditorGUI.PropertyField(ToggleRect.Toggle, doOverrideProperty, GUIContent.none);

            if (EditorGUI.EndChangeCheck() 
                && previousOverrideStatus != doOverrideProperty.boolValue)
            {
                if (doOverrideProperty.serializedObject.ApplyModifiedProperties())
                {
                    if (!TryUpdateOverrideStatus(property, doOverrideProperty))
                    {
                        Debug.LogError($"Failed to update override status (doOverride) of {doOverrideProperty.propertyType} instance");
                        EditorGUI.EndProperty();
                        return;
                    }
                    // Reset default value "changing status" here if it was left enabled
                    if (resettingDefault) resettingDefault = false;
                    // Recalculate value rects for possible change in height
                    height = (doOverrideProperty.boolValue || resettingDefault) ? propertyHeight.Override : propertyHeight.Default;
                    _Rect.Override.height = _Rect.Default.height = height;
                }
            }
            
            // --------Enforce Previous Property Updates--------
            property.serializedObject.Update(); // Force the SO to pull the data back from the object immediately

            #endregion

            #region Value Property Field

            //Get the override value property
            
            if (!TryGetValueProperty(doOverrideProperty.boolValue, property, out var valueProp))
            {
                Debug.LogError($"Could not get ValueOverride property for {property.type}");
                EditorGUI.EndProperty();
                return;
            }

            EditorGUI.BeginChangeCheck();
            if (doOverrideProperty.boolValue)
            {
                //--------Draw Modifiable Property Field for Override--------
                EditorGUI.PropertyField(_Rect.Override, valueProp, GUIContent.none);
            }
            else
            {
                if (resettingDefault)
                {
                    EditorGUI.BeginChangeCheck();
                    //--------Draw Mutable Display Field for Default--------
                    EditorGUI.PropertyField(_Rect.Default, valueProp, GUIContent.none);
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (valueProp.serializedObject.ApplyModifiedProperties())
                        {
                            // --------Sync Object with Serialized State--------
                            // Re-assign the entire struct back to the property so Unity knows it changed
                            if (!TryUpdateValue(property, valueProp, doOverrideProperty.boolValue))
                            {
                                Debug.LogError("Failed to update value");
                                EditorGUI.EndProperty();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    //--------Draw Immutable Display Field for Default--------
                    EditorGUI.LabelField(_Rect.Default, valueProp.GetValueText());
                }

                var changeDefaultButtonStyle = new GUIStyle(GUI.skin.button)
                {
                    normal =
                    {
                        textColor = resettingDefault ? Color.green : defaultButtonTextColor.normal
                    },
                    active =
                    {
                        textColor = resettingDefault ? Color.green : defaultButtonTextColor.active
                    },
                    focused =
                    {
                        textColor = resettingDefault ? Color.green : defaultButtonTextColor.focused
                    }
                };
                
                if (GUI.Button(_Rect.Button, "Change", changeDefaultButtonStyle))
                    resettingDefault = !resettingDefault;
                
                //Exit resettingDefault mode when the user presses any of the "escape" keys (Escape, Enter, Return)
                if (resettingDefault && Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.Escape || Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter))
                    resettingDefault = false;
            }


            if (EditorGUI.EndChangeCheck())
            {
                if (valueProp.serializedObject.ApplyModifiedProperties())
                {
                    // --------Sync Object with Serialized State--------
                    // Re-assign the entire struct back to the property so Unity knows it changed
                    if (!TryUpdateValue(property, valueProp, doOverrideProperty.boolValue))
                    {
                        Debug.LogError("Failed to update value");
                        EditorGUI.EndProperty();
                        return;
                    }
                }
            }
            
            #endregion
            
            EditorGUI.EndProperty();
        }
        
        /// <summary>
        ///     Gets the height of the property when drawn in the inspector.
        /// </summary>
        /// <param name="property">The serialized property to calculate the height for.</param>
        /// <param name="label">The label of the property.</param>
        /// <returns>The height, in pixels, required to draw the property.</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var currentType = property.GetSystemType();
            return (resettingDefault || property.FindPropertyRelative("doOverride").boolValue) 
                ? Data[currentType].Override.propertyHeight 
                : Data[currentType].Default.propertyHeight;
        }


        private bool TryUpdateOverrideStatus(SerializedProperty outerProperty, SerializedProperty boolProperty)
        {
            if (outerProperty.TrySetBoxedValueViaPath(outerProperty.GetSystemType(), "doOverride", boolProperty, out var modifiedObject))
            {
                outerProperty.boxedValue = modifiedObject;
                return true;
            }
            
            return false;
        }
        
        private bool TryGetValueProperty(bool doOverride, SerializedProperty property, out SerializedProperty valueProp)
        {
            if (property == null) return NoValue(out valueProp);
            
            //Get the type of the passed in property
            var propType = property.GetSystemType();
            
            if (!Data.TryGetValue(propType, out var propertyDataSet)) return NoValue(out valueProp); //This is not the correct type being drawn at all somehow
            
            var propertyData = doOverride ? propertyDataSet.Override : propertyDataSet.Default; //Based on override state, return the property for the appropriate field
            
            valueProp = property.FindPropertyRelative(propertyData.path);
            
            return valueProp != null;

            bool NoValue(out SerializedProperty valueProp)
            {
                valueProp = null;
                return false;
            }
        }
        private bool TryUpdateValue(SerializedProperty outerProperty, SerializedProperty valueProp, bool doOverride)
        {
            //Get the type of the current value override UDT
            var currentType = outerProperty.GetSystemType();
            //Get the property data for this value override UDT (specifically the field path/name information)
            var currentData = (doOverride) ? Data[currentType].Override : Data[currentType].Default;
            //Try to set the value of the value override UDT struct's field using the value of the field's modified property
            if (outerProperty.TrySetBoxedValueViaPath(outerProperty.GetSystemType(), currentData.path, valueProp, out var modifiedObject))
            {
                outerProperty.boxedValue = modifiedObject;
                return true;
            }
            
            return false;
        }
    }
}