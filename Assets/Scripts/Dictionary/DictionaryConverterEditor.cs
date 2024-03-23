using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Dictionary
{
    [CustomEditor(typeof(DictionaryData))]
    public class DictionaryConverterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DictionaryData data = (DictionaryData)target;

            GUILayout.Space(10);
            
            EditorGUILayout.LabelField("Dictionary Conversion Status", data.dictionaryConverted ? "Converted" : "Not Converted");

            GUILayout.Space(10);

            // Add a button to the Inspector GUI
            if (GUILayout.Button("Convert Dictionary to List"))
            {
                data.ConvertDictionaryToList();
            }
        }
    }
}
#endif