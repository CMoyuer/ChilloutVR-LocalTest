using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#pragma warning disable

[InitializeOnLoad]
public class CCK_Init
{
    static CCK_Init()
    {
        
        void SetTag(SerializedProperty tags, string name, int index)
        {
            SerializedProperty sp = tags.GetArrayElementAtIndex(index);
            if (sp != null) sp.stringValue = name;
        }
        
        void SetLayer(SerializedProperty layers, string name, int index)
        {
            SerializedProperty sp = layers.GetArrayElementAtIndex(index);
            if (sp != null) sp.stringValue = name;
        }

        string cckSymbol = "CVR_CCK_EXISTS";
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
        if (!defines.Contains(cckSymbol))
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, (defines + ";" + cckSymbol));
            Debug.Log("[CCK:Init] Added CVR_CCK_EXISTS Scripting Symbol.");
        }

        if (LayerMask.LayerToName(10) != "PlayerNetwork" || LayerMask.LayerToName(15) != "CVRReserved4" || LayerMask.LayerToName(17) != "CVRPickup")
        {
            Debug.Log("[CCK:Init] TagManager asset has to be recreated. Now recreating.");

            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            
            SerializedProperty tagProperty = tagManager.FindProperty("tags");
            
            var tagList = new (int, string)[]
            {
                (0, "CCKEditorUI_Uploader"),
                (1, "CCKSerializable"),
                (2, "CCKLambda"),
                (3, "CCKNetwork_System"),
                (4, "CCKNetwork_Routine"),
                (5, "ThisIsGarbage")
            };
            
            foreach (var tag in tagList) SetTag(tagProperty, tag.Item2, tag.Item1);

            SerializedProperty layerProperty = tagManager.FindProperty("layers");
            
            var layerList = new (int, string)[]
            {
                (8, "PlayerLocal"),
                (9, "PlayerClone"),
                (10, "PlayerNetwork"),
                (11, "MirrorReflection"),
                (12, "CVRReserved1"),
                (13, "CVRReserved2"),
                (14, "CVRReserved3"),
                (15, "CVRReserved4"),
                (16, "PostProcessing"),
                (17, "CVRPickup"),
                (18, "CVRInteractable")
            };
            
            foreach (var layer in layerList) SetLayer(layerProperty, layer.Item2, layer.Item1);

            tagManager.ApplyModifiedProperties();
        }

        if (!PlayerSettings.virtualRealitySupported)
        {
            Debug.Log("[CCK:Init] XR and render settings have to be changed. Now changing.");
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
            PlayerSettings.colorSpace = UnityEngine.ColorSpace.Linear;

            PlayerSettings.apiCompatibilityLevel = ApiCompatibilityLevel.NET_4_6;
            
            PlayerSettings.virtualRealitySupported = true;
            PlayerSettings.SetVirtualRealitySDKs(BuildTargetGroup.Standalone, new string[] { "None", "Oculus", "OpenVR", "MockHMD" });
            PlayerSettings.stereoRenderingPath = StereoRenderingPath.SinglePass;
        }
        
        if (LayerMask.LayerToName(10) == "PlayerNetwork" && LayerMask.LayerToName(17) == "CVRPickup" && LayerMask.LayerToName(15) == "CVRReserved4"  && PlayerSettings.virtualRealitySupported )
        {
            Debug.Log("[CCK:Init] Verified TagManager and ProjectSettings. No need to readjust.");
        }
    }
}
