using ABI.CCK.Components;
using UnityEditor;
using UnityEngine;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(CVRHapticZone))]
    public class CCK_CVRHapticZoneEditor : UnityEditor.Editor
    {
        private CVRHapticZone _hapticZone;
        
        public override void OnInspectorGUI()
        {
            if (_hapticZone == null) _hapticZone = (CVRHapticZone) target;

            _hapticZone.triggerForm = (CVRHapticZone.TriggerForm) EditorGUILayout.EnumPopup("Trigger Form", _hapticZone.triggerForm);

            _hapticZone.center = EditorGUILayout.Vector3Field("Trigger Center", _hapticZone.center);

            if (_hapticZone.triggerForm == CVRHapticZone.TriggerForm.Box)
            {
                _hapticZone.bounds = EditorGUILayout.Vector3Field("Trigger Bounds", _hapticZone.bounds);
            }
            else
            {
                _hapticZone.bounds.x = EditorGUILayout.FloatField("Trigger Radius", _hapticZone.bounds.x);
            }
            
            GUILayout.BeginVertical("HelpBox");

            GUILayout.BeginHorizontal ();
            _hapticZone.enableOnEnter = EditorGUILayout.Toggle (_hapticZone.enableOnEnter, GUILayout.Width(16));
            EditorGUILayout.LabelField ("On Enter", GUILayout.Width(150));
            GUILayout.EndHorizontal ();

            if (_hapticZone.enableOnEnter)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("GroupBox");

                _hapticZone.onEnterIntensity = EditorGUILayout.Slider("Intensity", _hapticZone.onEnterIntensity, 0f, 1f);
                
                GUILayout.EndVertical ();
                GUILayout.EndHorizontal ();
            }
            
            GUILayout.EndVertical();
            
            
            GUILayout.BeginVertical("HelpBox");

            GUILayout.BeginHorizontal ();
            _hapticZone.enableOnStay = EditorGUILayout.Toggle (_hapticZone.enableOnStay, GUILayout.Width(16));
            EditorGUILayout.LabelField ("On Stay", GUILayout.Width(150));
            GUILayout.EndHorizontal ();

            if (_hapticZone.enableOnStay)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("GroupBox");

                _hapticZone.onStayIntensity = EditorGUILayout.Slider("Intensity", _hapticZone.onStayIntensity, 0f, 1f);

                _hapticZone.onStayTiming = (CVRHapticZone.TriggerTiming) EditorGUILayout.EnumPopup("Timing", _hapticZone.onStayTiming);

                if (_hapticZone.onStayTiming == CVRHapticZone.TriggerTiming.random)
                {
                    _hapticZone.onStayChance = EditorGUILayout.Slider("Chance", _hapticZone.onStayChance, 0f, 1f);
                }
                
                GUILayout.EndVertical ();
                GUILayout.EndHorizontal ();
            }
            
            GUILayout.EndVertical();
            
            
            GUILayout.BeginVertical("HelpBox");

            GUILayout.BeginHorizontal ();
            _hapticZone.enableOnExit = EditorGUILayout.Toggle (_hapticZone.enableOnExit, GUILayout.Width(16));
            EditorGUILayout.LabelField ("On Exit", GUILayout.Width(150));
            GUILayout.EndHorizontal ();

            if (_hapticZone.enableOnExit)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("GroupBox");

                _hapticZone.onExitIntensity = EditorGUILayout.Slider("Intensity", _hapticZone.onExitIntensity, 0f, 1f);
                
                GUILayout.EndVertical ();
                GUILayout.EndHorizontal ();
            }
            
            GUILayout.EndVertical();
        }
    }
}