using UnityEditor;

namespace UIManagerLibrary.Scripts
{
    [CustomEditor(typeof(UIManager))]
    public class UIManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var uiManager = target as UIManager;
            bool componentIsDirty = false;

            if (uiManager.UIDataContext != null && uiManager.UIDataContext.Contexts.Length > 0)
            {
                string[] contexts = uiManager.UIDataContext.Contexts;
                uiManager._activeFlags = EditorGUILayout.MaskField("Enable Contexts On Startup", uiManager._activeFlags, contexts);


            }

            // Draw the default inspector
            DrawDefaultInspector();


            if (componentIsDirty) EditorUtility.SetDirty(target);
        }
    }
}
