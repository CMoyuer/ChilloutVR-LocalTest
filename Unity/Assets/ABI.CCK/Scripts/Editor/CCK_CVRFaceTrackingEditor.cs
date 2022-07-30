using System.Collections.Generic;
using System.Linq;
using ABI.CCK.Components;
using UnityEditor;
using UnityEngine;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRFaceTracking))]
    public class CCK_CVRFaceTrackingEditor : UnityEditor.Editor
    {
        private CVRFaceTracking _faceTracking;
        
        public override void OnInspectorGUI()
        {
            if (_faceTracking == null) _faceTracking = (CVRFaceTracking) target;

            _faceTracking.GetBlendShapeNames();

            _faceTracking.UseFacialTracking = EditorGUILayout.Toggle("Enable Facial Tracking", _faceTracking.UseFacialTracking);

            _faceTracking.BlendShapeStrength = EditorGUILayout.Slider("Blend Shape Weight", _faceTracking.BlendShapeStrength, 50f, 500f);
            
            _faceTracking.FaceMesh = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("Face Mesh", _faceTracking.FaceMesh, typeof(SkinnedMeshRenderer), true);
            
            for (int i = 0; i < CVRFaceTracking.FaceBlendShapeNames.Length; i++)
            {
                int current = 0;
                for (int j = 0; j < _faceTracking.BlendShapeNames.Count; ++j)
                    if (_faceTracking.FaceBlendShapes[i] == _faceTracking.BlendShapeNames[j])
                        current = j;
                
                int viseme = EditorGUILayout.Popup(CVRFaceTracking.FaceBlendShapeNames[i], current, _faceTracking.BlendShapeNames.ToArray());
                _faceTracking.FaceBlendShapes[i] = _faceTracking.BlendShapeNames[viseme];
            }

            if (GUILayout.Button("Auto select Blendshapes"))
            {
                _faceTracking.FindVisemes();
            }

            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.LabelField("Face Tracking ");
            EditorGUILayout.BeginVertical("GroupBox");

            _faceTracking.enableOverdriveBlendShapes = EditorGUILayout.Toggle("Enable Overdrive", _faceTracking.enableOverdriveBlendShapes);
            EditorGUILayout.HelpBox("By enabling overdrive the system expects the blendshapes to be at 500% when fully set. You can use the button below to generate 500% versions of the currently selected blendshapes.", MessageType.Info);

            if (GUILayout.Button("Generate overdrive Blendshapes (Experimental)"))
            {
                GenerateOverdriveBlendShapes();
            }
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space();

            EditorGUILayout.HelpBox("We have prepared a way to generate a simple facial tracker support for your avatars without any requirement to use a 3D model software. For a more in-depth / detailed support, please create the required blendshapes using a 3D modeling software. More information can be found in our documentation.", MessageType.Info);
            
            if (GUILayout.Button("Open Blendshape Generator (Experimental)"))
            {
                CCK_FaceTrackingUtilities window = (CCK_FaceTrackingUtilities)EditorWindow.GetWindow (typeof(CCK_FaceTrackingUtilities), true, "CCK :: Face Tracking Utilities");
                window.Avatar = _faceTracking.gameObject.GetComponent<CVRAvatar>();
                if (window.Avatar == null) window.Avatar = _faceTracking.gameObject.GetComponentInParent<CVRAvatar>();
                window.FaceTracking = _faceTracking;
                window.Tab = 1;
                window.Show();
            }

            if (GUILayout.Button("Reset to original Mesh"))
            {
                if (_faceTracking.OriginalMesh != null)
                {
                    _faceTracking.FaceMesh.sharedMesh = _faceTracking.OriginalMesh;
                }
            }
        }

        public void GenerateOverdriveBlendShapes()
        {
            Mesh mesh;
            CVRAvatar avatar = _faceTracking.GetComponentInParent<CVRAvatar>();
            
            if (_faceTracking.FaceMesh.sharedMesh != _faceTracking.OriginalMesh)
            {
                mesh = _faceTracking.FaceMesh.sharedMesh.Copy();
                
                string pathToCurrentFolder = "Assets/FaceTracking.Generated";
                if (!AssetDatabase.IsValidFolder(pathToCurrentFolder)) AssetDatabase.CreateFolder("Assets", "FaceTracking.Generated");
                var meshPath = pathToCurrentFolder + "/" + avatar.transform.name  + ".mesh";
                AssetDatabase.CreateAsset(mesh, meshPath);

                _faceTracking.FaceMesh.sharedMesh = mesh;
            }
            else
            {
                mesh = _faceTracking.FaceMesh.sharedMesh;
            }

            for (int i = 0; i < mesh.blendShapeCount; i++)
            {
                if (_faceTracking.FaceBlendShapes.Contains(mesh.GetBlendShapeName(i)))
                {
                    var frameCount = mesh.GetBlendShapeFrameCount(i);
                    var frameWeight = mesh.GetBlendShapeFrameWeight(i, frameCount - 1);
                    if (!mesh.GetBlendShapeName(i).Contains("_overdrive"))
                    {
                        Vector3[] deltaVertices = new Vector3[mesh.vertexCount];
                        Vector3[] deltaNormals = new Vector3[mesh.vertexCount];
                        Vector3[] deltaTangents = new Vector3[mesh.vertexCount];
                        mesh.GetBlendShapeFrameVertices(i, 0, deltaVertices, deltaNormals, deltaTangents);

                        for (int j=0; j < deltaVertices.Length; j++)
                        {
                            deltaVertices[j] = deltaVertices[j] * 5f;
                        }
                        
                        mesh.AddBlendShapeFrame(mesh.GetBlendShapeName(i)+"_overdrive", 100f, deltaVertices, deltaNormals, deltaTangents);
                        var index = _faceTracking.FaceBlendShapes.ToList().FindIndex(m => m == mesh.GetBlendShapeName(i));
                        _faceTracking.FaceBlendShapes[index] = mesh.GetBlendShapeName(i) + "_overdrive";
                    }
                }
            }

            _faceTracking.GetBlendShapeNames();
            _faceTracking.enableOverdriveBlendShapes = true;
            
            AssetDatabase.SaveAssets();
        }
    }
}