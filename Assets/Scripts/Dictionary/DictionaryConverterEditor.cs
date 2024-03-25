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

            var data = (DictionaryData)target;

            GUILayout.Space(10);

            EditorGUILayout.LabelField("Dictionary Conversion Status",
                data.dictionaryConverted ? "Converted" : "Not Converted");

            GUILayout.Space(10);

            if (GUILayout.Button("Convert Dictionary to List")) data.ConvertDictionaryToList();
        }
    }
}
#endif