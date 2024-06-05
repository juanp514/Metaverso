using TMPro.EditorUtilities;
using UnityEditor;
using Xennial.XR.UI;

namespace Xennial.XR.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(XRInputField), true)]
    public class XRInputFieldEditor : TMP_InputFieldEditor
    {
        private SerializedProperty _autofocus;

        protected override void OnEnable()
        {
            base.OnEnable();
            _autofocus = serializedObject.FindProperty("_autofocus");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_autofocus);
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
}