using UnityEditor;
using UnityEngine;

namespace Euphrates
{
    [CustomEditor(typeof(SnapShotCamera))]
	public class SnapShotInspector : UnityEditor.Editor
	{
        SnapShotCamera _target;

        void OnEnable()
        {
            _target = (SnapShotCamera)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Take Snap"))
            {
                _target.TakeShot();
            }
        }
    }
}
