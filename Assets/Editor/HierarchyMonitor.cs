using TMPro;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    /// <summary>
    /// Provides event triggers for different hierarchy changes
    /// </summary>
    [InitializeOnLoad]
    public static class HierarchyMonitor
    {
        static HierarchyMonitor()
        {
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
        }

        private static void OnHierarchyChanged()
        {
            var hierarchyObject = (GameObject[]) Resources.FindObjectsOfTypeAll(typeof(GameObject));
            
            foreach (var obj in hierarchyObject)
            {
                UpdateDefaultTextNames(obj);
                
                // Maybe some other event triggers in the future
            }
        }

        private static void UpdateDefaultTextNames(GameObject obj)
        {
            const string defaultText = "New Text";
            const string replacedText = "Text (TMP)";
            
            var text = obj.GetComponent<TextMeshProUGUI>();
            
            if (text == null || (text.text != defaultText && text.text != replacedText))
                return;
            
            text.text = obj.name;
        }
    }
}