using System;
using System.Linq;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Halcyon
{
    [CustomPropertyDrawer(typeof(KeyCode))]
    public class KeyCodeDrawer :PropertyDrawer
    {

        private int m_value;
        private bool initialized = false;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!initialized)
            {
                m_value = property.enumValueIndex;
                initialized = true;
            }
            else
            {
                property.enumValueIndex = m_value;
            }
            
            
            var names = Enum.GetNames(typeof(KeyCode));

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(property.displayName, GUILayout.ExpandWidth(false));
            if (GUILayout.Button($"{names[property.enumValueIndex]}", EditorStyles.popup))
            {
                var so = ScriptableObject.CreateInstance<StringListSearchProvider>();
                so.Initialize("KeyCodes", names.ToList(),
                    (x) => { m_value = x;});
                
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)),so);
            }
            EditorGUILayout.EndHorizontal();
            
        }
    }
}