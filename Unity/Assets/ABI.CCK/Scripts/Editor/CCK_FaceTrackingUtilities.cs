using System.Collections.Generic;
using ABI.CCK.Components;
using UnityEditor;
using UnityEngine;

namespace ABI.CCK.Scripts.Editor
{
    public class CCK_FaceTrackingUtilities : EditorWindow
    {
        public CVRAvatar Avatar;
        public CVRFaceTracking FaceTracking;
        public int Tab = 0;
        
        private int[] _blendShapeIndexes = new int[37];

        private bool _enablePreview = false;
        private Vector3 _jawPosition = new Vector4(0f, 0f, 0f);
        private float _apeShape = 0f;
        private float _mouthUpper = 0f;
        private float _mouthLower = 0f;
        private float _mouthPout = 0f;
        private float _mouthSmileLeft = 0f;
        private float _mouthSmileRight = 0f;
        private float _mouthSadLeft = 0f;
        private float _mouthSadRight = 0f;
        private float _mouthPuffLeft = 0f;
        private float _mouthPuffRight = 0f;
        private float _mouthSuck = 0f;

        private List<string> _blendShapes = new List<string>();

        private bool _enableJawGeneration = false;
        private int _jawBlendShapeIndex = -1;
        private float _jawMovementStrength = 0.05f;
        
        private bool _enableLipGeneration = false;
        private int _lipBlendShapeIndex = -1;
        private float _lipMovementStrength = 0.05f;
        
        private bool _enableSmileGeneration = false;
        private int _smileBlendShapeIndex = -1;
        
        private bool _enableFrownGeneration = false;
        private int _frownBlendShapeIndex = -1;
        
        private bool _enablePuffGeneration = false;
        private int _puffBlendShapeIndex = -1;
        
        [MenuItem("Alpha Blend Interactive/Modules/Face Tracking Utilities")]
        private static void Init()
        {
            CCK_FaceTrackingUtilities window = (CCK_FaceTrackingUtilities)GetWindow(typeof(CCK_FaceTrackingUtilities), false, $"CCK :: Face Tracking Utilities");
            window.Show();
        }

        private void OnGUI()
        {
            var avatar = (CVRAvatar) EditorGUILayout.ObjectField("Avatar", Avatar, typeof(CVRAvatar));

            if (avatar != Avatar) FaceTracking = null;
            Avatar = avatar;

            if (Avatar == null) return;
            
            if (Avatar != null && FaceTracking == null) FaceTracking = Avatar.GetComponentInChildren<CVRFaceTracking>();
            
            if (FaceTracking == null)
            {
                EditorGUILayout.HelpBox("No Face Tracking component detected on Avatar. Would you like to add one?", MessageType.Info);

                if (GUILayout.Button("Add Face Tracking"))
                {
                    if (Avatar.bodyMesh == null)
                    {
                        EditorUtility.DisplayDialog("Error", "Your Selected Avatar has no Face Mesh selected.", "OK");
                        return;
                    }

                    FaceTracking = Avatar.gameObject.AddComponent<CVRFaceTracking>();
                    FaceTracking.FaceMesh = Avatar.bodyMesh;
                    FaceTracking.GetBlendShapeNames();
                    FaceTracking.FindVisemes();
                }
                
                return;
            }
            
            Tab = GUILayout.Toolbar (Tab, new string[] {"Preview", "Blendshape Generator"});

            switch (Tab)
            {
                case 0:
                    ShowPreviewTab();
                    break;
                case 1:
                    ShowSetupTab();
                    break;
            }
        }

        private void ShowPreviewTab()
        {
            _enablePreview = EditorGUILayout.Toggle("Enable Preview", _enablePreview);

            FaceTracking.BlendShapeStrength = EditorGUILayout.Slider("Blend Shape Weight", FaceTracking.BlendShapeStrength, 50f, 500f);
            
            EditorGUILayout.Space();
            
            _jawPosition.x = EditorGUILayout.Slider("Jaw Position Forward", _jawPosition.x, 0f, 1f);
            _jawPosition.y = EditorGUILayout.Slider("Jaw Position Open", _jawPosition.y, 0f, 1f);
            _jawPosition.z = EditorGUILayout.Slider("Jaw Position Left Right", _jawPosition.z, -1f, 1f);
            
            EditorGUILayout.Space();
            
            _apeShape = EditorGUILayout.Slider("Mouth Ape Shape", _apeShape, 0f, 1f);
            
            EditorGUILayout.Space();
            
            _mouthUpper = EditorGUILayout.Slider("Mouth Upper", _mouthUpper, -1f, 1f);
            _mouthLower = EditorGUILayout.Slider("Mouth Lower", _mouthLower, -1f, 1f);
            
            EditorGUILayout.Space();
            
            _mouthPout = EditorGUILayout.Slider("Mouth Pout", _mouthPout, 0f, 1f);
            
            EditorGUILayout.Space();
            
            _mouthSmileLeft = EditorGUILayout.Slider("Mouth Smile Left", _mouthSmileLeft, 0f, 1f);
            _mouthSmileRight = EditorGUILayout.Slider("Mouth Smile Right", _mouthSmileRight, 0f, 1f);
            
            EditorGUILayout.Space();
            
            _mouthSadLeft = EditorGUILayout.Slider("Mouth Sad Left", _mouthSadLeft, 0f, 1f);
            _mouthSadRight = EditorGUILayout.Slider("Mouth Sad Right", _mouthSadRight, 0f, 1f);
            
            EditorGUILayout.Space();
            
            _mouthPuffLeft = EditorGUILayout.Slider("Cheek Puff Left", _mouthPuffLeft, 0f, 1f);
            _mouthPuffRight = EditorGUILayout.Slider("Cheek Puff Right", _mouthPuffRight, 0f, 1f);
            
            EditorGUILayout.Space();
            
            _mouthSuck = EditorGUILayout.Slider("Cheek Suck", _mouthSuck, 0f, 1f);

            if (FaceTracking != null && FaceTracking.FaceMesh != null)
            {
                var m = FaceTracking.FaceMesh.sharedMesh;
                if (m != null)
                {
                    for (var i = 0; i < m.blendShapeCount; i++)
                    {
                        string s = m.GetBlendShapeName(i);
                        for (var j = 0; j < FaceTracking.FaceBlendShapes.Length; j++)
                        {
                            if (s == FaceTracking.FaceBlendShapes[j])
                            {
                                _blendShapeIndexes[j] = i;
                            }
                        }
                    }
                }
            }
            
            if (FaceTracking != null && FaceTracking.FaceMesh != null && _enablePreview)
            {
                var factor = FaceTracking.enableOverdriveBlendShapes ? 0.2f : 1f;
                
                if (FaceTracking.FaceBlendShapes[2] != "-none-" && _blendShapeIndexes[2] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[2], 
                        Mathf.Clamp(_jawPosition.x, 0f, 1f) * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[3] != "-none-" && _blendShapeIndexes[3] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[3], 
                        Mathf.Clamp(_jawPosition.y, 0f, 1f) * FaceTracking.BlendShapeStrength * factor);
                
                
                if (FaceTracking.FaceBlendShapes[0] != "-none-" && _blendShapeIndexes[0] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[0], 
                        Mathf.Clamp(_jawPosition.z, 0f, 1f) * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[1] != "-none-" && _blendShapeIndexes[1] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[1], 
                        Mathf.Clamp(_jawPosition.z, -1f, 0f) * -1f * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[4] != "-none-" && _blendShapeIndexes[4] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[4], 
                        _apeShape * FaceTracking.BlendShapeStrength * factor);

                if (FaceTracking.FaceBlendShapes[5] != "-none-" && _blendShapeIndexes[5] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[5],
                        Mathf.Clamp(_mouthUpper, 0f, 1f) * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[6] != "-none-" && _blendShapeIndexes[6] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[6],
                        Mathf.Clamp(_mouthUpper, -1f, 0f) * -1f * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[7] != "-none-" && _blendShapeIndexes[7] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[7],
                        Mathf.Clamp(_mouthLower, 0f, 1f) * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[8] != "-none-" && _blendShapeIndexes[8] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[8],
                        Mathf.Clamp(_mouthLower, -1f, 0f) * -1f * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[11] != "-none-" && _blendShapeIndexes[11] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[11],
                        Mathf.Clamp(_mouthPout, 0f, 1f) * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[12] != "-none-"  && _blendShapeIndexes[12] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[12],
                        Mathf.Clamp(_mouthSmileRight, 0f, 1f) * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[13] != "-none-" && _blendShapeIndexes[13] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[13],
                        Mathf.Clamp(_mouthSmileLeft, 0f, 1f) * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[14] != "-none-" && _blendShapeIndexes[14] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[14],
                        Mathf.Clamp(_mouthSadRight, 0f, 1f) * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[15] != "-none-" && _blendShapeIndexes[15] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[15],
                        Mathf.Clamp(_mouthSadLeft, 0f, 1f) * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[16] != "-none-" && _blendShapeIndexes[16] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[16],
                        Mathf.Clamp(_mouthPuffRight, 0f, 1f) * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[17] != "-none-" && _blendShapeIndexes[17] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[17],
                        Mathf.Clamp(_mouthPuffLeft, 0f, 1f) * FaceTracking.BlendShapeStrength * factor);
                
                if (FaceTracking.FaceBlendShapes[18] != "-none-" && _blendShapeIndexes[18] <= FaceTracking.FaceMesh.sharedMesh.blendShapeCount)
                    FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[18],
                        Mathf.Clamp(_mouthSuck, 0f, 1f) * FaceTracking.BlendShapeStrength * factor);
            }
            else if (FaceTracking != null)
            {
                for (var j = 0; j < FaceTracking.FaceBlendShapes.Length; j++)
                {
                    if (FaceTracking.FaceBlendShapes[j] != "-none-")
                    {
                        FaceTracking.FaceMesh.SetBlendShapeWeight(_blendShapeIndexes[j], 0f);
                    }
                }
            }
        }
        
        private void ShowSetupTab()
        {
            if (FaceTracking != null && FaceTracking.FaceMesh != null)
            {
                var m = FaceTracking.FaceMesh.sharedMesh;
                if (m != null)
                {
                    _blendShapes.Clear();
                    _blendShapes.Add("-none-");
                    for (var i = 0; i < m.blendShapeCount; i++)
                    {
                        _blendShapes.Add(m.GetBlendShapeName(i));
                    }
                }
            }
            
            EditorGUILayout.HelpBox("The Generator uses your Avatars Voice Position to generate new Blendhapes. Please make sure it is in the middle of the mouth between the lips.", MessageType.Warning);
            
            _enableJawGeneration = EditorGUILayout.Toggle("Generate Jaw Blendshapes", _enableJawGeneration);

            if (_enableJawGeneration)
            {
                EditorGUILayout.HelpBox("You should use a Blendshape here that opens the mouth ond moves the jaw down. For example the AA Viseme.", MessageType.Info);
                _jawBlendShapeIndex = EditorGUILayout.Popup("Jaw Open Blendshape", _jawBlendShapeIndex + 1, _blendShapes.ToArray()) - 1;
                _jawMovementStrength = EditorGUILayout.FloatField("Jaw Movement Strength", _jawMovementStrength);
                EditorGUILayout.Space();
            }
            
            _enableLipGeneration = EditorGUILayout.Toggle("Generate Lip Blendshapes", _enableLipGeneration);
            
            if (_enableLipGeneration)
            {
                EditorGUILayout.HelpBox("You should use a Blendshape here that moves only the Lips and a little bit of the surrounding face", MessageType.Info);
                _lipBlendShapeIndex = EditorGUILayout.Popup("Lips Blendshape", _lipBlendShapeIndex + 1, _blendShapes.ToArray()) - 1;
                _lipMovementStrength = EditorGUILayout.FloatField("Lip Movement Strength", _lipMovementStrength);
                EditorGUILayout.Space();
            }
            
            _enableSmileGeneration = EditorGUILayout.Toggle("Separate Smile Blendshape", _enableSmileGeneration);

            if (_enableSmileGeneration)
            {
                EditorGUILayout.HelpBox("You should place a Blendshape that contains a smile expression. The Generator will separate the sides", MessageType.Info);
                _smileBlendShapeIndex = EditorGUILayout.Popup("Smile Blendshape", _smileBlendShapeIndex + 1, _blendShapes.ToArray()) - 1;
                EditorGUILayout.Space();
            }
            
            _enableFrownGeneration = EditorGUILayout.Toggle("Separate Frown Blendshape", _enableFrownGeneration);

            if (_enableFrownGeneration)
            {
                EditorGUILayout.HelpBox("You should place a Blendshape that contains a frown expression. The Generator will separate the sides", MessageType.Info);
                _frownBlendShapeIndex = EditorGUILayout.Popup("Frown Blendshape", _frownBlendShapeIndex + 1, _blendShapes.ToArray()) - 1;
                EditorGUILayout.Space();
            }

            _enablePuffGeneration = EditorGUILayout.Toggle("Separate Puff Blendshape", _enablePuffGeneration);

            if (_enablePuffGeneration)
            {
                EditorGUILayout.HelpBox("You should place a Blendshape here that Puffs both cheeks. The Generator will separate the sides", MessageType.Info);
                _puffBlendShapeIndex = EditorGUILayout.Popup("Puff Cheek Blendshape", _puffBlendShapeIndex + 1, _blendShapes.ToArray()) - 1;
                EditorGUILayout.Space();
            }


            if (GUILayout.Button("Generate Blendshapes"))
            {
                GenerateBlendShapes();
            }

        }

        private void GenerateBlendShapes()
        {
            if (FaceTracking.OriginalMesh == null)
            {
                FaceTracking.OriginalMesh = FaceTracking.FaceMesh.sharedMesh;
            }
            
            var mesh = FaceTracking.OriginalMesh;

            //Mesh Creation
            Mesh m = mesh.Copy();

            string pathToCurrentFolder = "Assets/FaceTracking.Generated";
            if (!AssetDatabase.IsValidFolder(pathToCurrentFolder)) AssetDatabase.CreateFolder("Assets", "FaceTracking.Generated");
            
            var meshPath = pathToCurrentFolder + "/" + Avatar.transform.name  + ".mesh";
            
            AssetDatabase.CreateAsset(m, meshPath);
            
            var worldPosition = Vector3.zero;
            
            var minX = 0f;
            var maxX = 0f;
            var minY = 0f;
            var maxY = 0f;
            var minZ = 0f;
            var maxZ = 0f;
            
            var leftWeight = 0f;
            var rightWeight = 0f;
            var upWeight = 0f;
            var downWeight = 0f;

            Transform faceMeshTransform = FaceTracking.FaceMesh.transform;
            Matrix4x4 localToWorld = faceMeshTransform.localToWorldMatrix;
            Matrix4x4 worldToLocal = faceMeshTransform.worldToLocalMatrix;

            //Jaw Blendshapes
            if (_enableJawGeneration && _jawBlendShapeIndex != -1)
            {
                Vector3[] deltaVerticesLeft = new Vector3[m.vertexCount];
                Vector3[] deltaVerticesRight = new Vector3[m.vertexCount];
                Vector3[] deltaVerticesOpen = new Vector3[m.vertexCount];
                Vector3[] deltaVerticesForward = new Vector3[m.vertexCount];
                Vector3[] deltaNormals = new Vector3[m.vertexCount];
                Vector3[] deltaTangents = new Vector3[m.vertexCount];
                m.GetBlendShapeFrameVertices(_jawBlendShapeIndex, 0, deltaVerticesLeft, deltaNormals, deltaTangents);
                m.GetBlendShapeFrameVertices(_jawBlendShapeIndex, 0, deltaVerticesRight, deltaNormals, deltaTangents);
                m.GetBlendShapeFrameVertices(_jawBlendShapeIndex, 0, deltaVerticesOpen, deltaNormals, deltaTangents);
                m.GetBlendShapeFrameVertices(_jawBlendShapeIndex, 0, deltaVerticesForward, deltaNormals, deltaTangents);
                
                for (int i = 0; i < m.vertexCount; i++)
                {
                    if (deltaVerticesLeft[i] == Vector3.zero) continue;

                    worldPosition = GetPointRelativeToAvatarVoicePosition(localToWorld, m.vertices[i]);
                    if (worldPosition.y > maxY) maxY = worldPosition.y;
                    if (worldPosition.y < minY) minY = worldPosition.y;
                }
                
                for (int i = 0; i < m.vertexCount; i++)
                {
                    if (deltaVerticesLeft[i] == Vector3.zero) continue;
                    
                    worldPosition = GetPointRelativeToAvatarVoicePosition(localToWorld, m.vertices[i]);
                    upWeight = Mathf.Clamp01(Mathf.InverseLerp(minY / 4f, 0, worldPosition.y));
                    downWeight = 1f - upWeight;

                    deltaVerticesForward[i]  = (deltaVerticesForward[i]  + localToWorld.MultiplyPoint3x4(Vector3.forward)  * _jawMovementStrength * 0.00001f) * downWeight;
                    deltaVerticesForward[i].Scale(faceMeshTransform.InverseTransformDirection(Vector3.forward));
                    deltaVerticesLeft[i]     = (deltaVerticesLeft[i]     + localToWorld.MultiplyPoint3x4(Vector3.left)  * _jawMovementStrength * 0.00001f) * downWeight;
                    deltaVerticesLeft[i].Scale(faceMeshTransform.InverseTransformDirection(Vector3.right));
                    deltaVerticesRight[i]    = (deltaVerticesRight[i]    + localToWorld.MultiplyPoint3x4(Vector3.right) * _jawMovementStrength * 0.00001f) * downWeight;
                    deltaVerticesRight[i].Scale(faceMeshTransform.InverseTransformDirection(Vector3.right));
                }
                
                m.AddBlendShapeFrame("Jaw_Right_generated",   100f, deltaVerticesRight, null, null);
                m.AddBlendShapeFrame("Jaw_Left_generated",    100f, deltaVerticesLeft, null, null);
                m.AddBlendShapeFrame("Jaw_Forward_generated", 100f, deltaVerticesForward, null, null);
                m.AddBlendShapeFrame("Jaw_Open_generated",    100f, deltaVerticesOpen, null, null);
            }
            
            //Lip Blendshapes
            if (_enableLipGeneration && _lipBlendShapeIndex != -1)
            {
                Vector3[] deltaVerticesUpLeft = new Vector3[m.vertexCount];
                Vector3[] deltaVerticesUpRight = new Vector3[m.vertexCount];
                Vector3[] deltaVerticesDownLeft = new Vector3[m.vertexCount];
                Vector3[] deltaVerticesDownRight = new Vector3[m.vertexCount];
                Vector3[] deltaNormals = new Vector3[m.vertexCount];
                Vector3[] deltaTangents = new Vector3[m.vertexCount];
                m.GetBlendShapeFrameVertices(_lipBlendShapeIndex, 0, deltaVerticesUpLeft, deltaNormals, deltaTangents);
                m.GetBlendShapeFrameVertices(_lipBlendShapeIndex, 0, deltaVerticesUpRight, deltaNormals, deltaTangents);
                m.GetBlendShapeFrameVertices(_lipBlendShapeIndex, 0, deltaVerticesDownLeft, deltaNormals, deltaTangents);
                m.GetBlendShapeFrameVertices(_lipBlendShapeIndex, 0, deltaVerticesDownRight, deltaNormals, deltaTangents);
                
                for (int i = 0; i < m.vertexCount; i++)
                {
                    if (deltaVerticesUpLeft[i] == Vector3.zero) continue;

                    worldPosition = GetPointRelativeToAvatarVoicePosition(localToWorld, m.vertices[i]);
                    if (worldPosition.x > maxX) maxX = worldPosition.x;
                    if (worldPosition.x < minX) minX = worldPosition.x;
                    if (worldPosition.y > maxY) maxY = worldPosition.y;
                    if (worldPosition.y < minY) minY = worldPosition.y;
                }
                
                for (int i = 0; i < m.vertexCount; i++)
                {
                    if (deltaVerticesUpLeft[i] == Vector3.zero) continue;
                    
                    worldPosition = GetPointRelativeToAvatarVoicePosition(localToWorld, m.vertices[i]);
                    upWeight = Mathf.Clamp01(Mathf.InverseLerp(0, 0.0001f, worldPosition.y));
                    downWeight = 1f - upWeight;
                    rightWeight = Mathf.Clamp01(Mathf.InverseLerp(minX / 4f, maxX / 4f, worldPosition.x));
                    leftWeight = 1f - rightWeight;

                    deltaVerticesUpLeft[i] = (localToWorld.MultiplyPoint3x4(Vector3.left) * _lipMovementStrength * 0.00001f) * upWeight;
                    deltaVerticesUpRight[i] = (localToWorld.MultiplyPoint3x4(Vector3.right) * _lipMovementStrength * 0.00001f) * upWeight;
                    deltaVerticesDownLeft[i] = (localToWorld.MultiplyPoint3x4(Vector3.left) * _lipMovementStrength * 0.00001f) * downWeight;
                    deltaVerticesDownRight[i] = (localToWorld.MultiplyPoint3x4(Vector3.right) * _lipMovementStrength * 0.00001f) * downWeight;
                }
                
                m.AddBlendShapeFrame("Mouth_Upper_Right_generated", 100f, deltaVerticesUpRight, null, null);
                m.AddBlendShapeFrame("Mouth_Upper_Left_generated",  100f, deltaVerticesUpLeft, null, null);
                m.AddBlendShapeFrame("Mouth_Lower_Right_generated", 100f, deltaVerticesDownRight, null, null);
                m.AddBlendShapeFrame("Mouth_Lower_Left_generated",  100f, deltaVerticesDownLeft, null, null);
            }
            
            //Smile Blendshapes
            if (_enableSmileGeneration && _smileBlendShapeIndex != -1)
            {
                Vector3[] deltaVerticesLeft = new Vector3[m.vertexCount];
                Vector3[] deltaVerticesRight = new Vector3[m.vertexCount];
                Vector3[] deltaNormals = new Vector3[m.vertexCount];
                Vector3[] deltaTangents = new Vector3[m.vertexCount];
                m.GetBlendShapeFrameVertices(_smileBlendShapeIndex, 0, deltaVerticesLeft, deltaNormals, deltaTangents);
                m.GetBlendShapeFrameVertices(_smileBlendShapeIndex, 0, deltaVerticesRight, deltaNormals, deltaTangents);

                for (int i = 0; i < m.vertexCount; i++)
                {
                    if (deltaVerticesLeft[i] == Vector3.zero) continue;

                    worldPosition = GetPointRelativeToAvatarVoicePosition(localToWorld, m.vertices[i]);
                    if (worldPosition.x > maxX) maxX = worldPosition.x;
                    if (worldPosition.x < minX) minX = worldPosition.x;
                }
                
                for (int i = 0; i < m.vertexCount; i++)
                {
                    worldPosition = GetPointRelativeToAvatarVoicePosition(localToWorld, m.vertices[i]);
                    rightWeight = Mathf.Clamp01(Mathf.InverseLerp(minX / 4f, maxX / 4f, worldPosition.x));
                    leftWeight = 1f - rightWeight;

                    deltaVerticesLeft[i] = deltaVerticesLeft[i] * leftWeight;
                    deltaVerticesRight[i] = deltaVerticesRight[i] * rightWeight;
                }

                m.AddBlendShapeFrame("Mouth_Smile_Left_generated", 100f, deltaVerticesLeft, null, null);
                m.AddBlendShapeFrame("Mouth_Smile_Right_generated", 100f, deltaVerticesRight, null, null);
            }
            
            //Frown Blendshapes
            if (_enableFrownGeneration && _frownBlendShapeIndex != -1)
            {
                Vector3[] deltaVerticesLeft = new Vector3[m.vertexCount];
                Vector3[] deltaVerticesRight = new Vector3[m.vertexCount];
                Vector3[] deltaNormals = new Vector3[m.vertexCount];
                Vector3[] deltaTangents = new Vector3[m.vertexCount];
                m.GetBlendShapeFrameVertices(_frownBlendShapeIndex, 0, deltaVerticesLeft, deltaNormals, deltaTangents);
                m.GetBlendShapeFrameVertices(_frownBlendShapeIndex, 0, deltaVerticesRight, deltaNormals, deltaTangents);

                for (int i = 0; i < m.vertexCount; i++)
                {
                    if (deltaVerticesLeft[i] == Vector3.zero) continue;

                    worldPosition = GetPointRelativeToAvatarVoicePosition(localToWorld, m.vertices[i]);
                    if (worldPosition.x > maxX) maxX = worldPosition.x;
                    if (worldPosition.x < minX) minX = worldPosition.x;
                }
                
                for (int i = 0; i < m.vertexCount; i++)
                {
                    worldPosition = GetPointRelativeToAvatarVoicePosition(localToWorld, m.vertices[i]);
                    rightWeight = Mathf.Clamp01(Mathf.InverseLerp(minX / 4f, maxX / 4f, worldPosition.x));
                    leftWeight = 1f - rightWeight;

                    deltaVerticesLeft[i] = deltaVerticesLeft[i] * leftWeight;
                    deltaVerticesRight[i] = deltaVerticesRight[i] * rightWeight;
                }

                m.AddBlendShapeFrame("Mouth_Sad_Left_generated", 100f, deltaVerticesLeft, null, null);
                m.AddBlendShapeFrame("Mouth_Sad_Right_generated", 100f, deltaVerticesRight, null, null);
            }

            //Puff Blendshapes
            if (_enablePuffGeneration && _puffBlendShapeIndex != -1)
            {
                Vector3[] deltaVerticesLeft = new Vector3[m.vertexCount];
                Vector3[] deltaVerticesRight = new Vector3[m.vertexCount];
                Vector3[] deltaNormals = new Vector3[m.vertexCount];
                Vector3[] deltaTangents = new Vector3[m.vertexCount];
                m.GetBlendShapeFrameVertices(_puffBlendShapeIndex, 0, deltaVerticesLeft, deltaNormals, deltaTangents);
                m.GetBlendShapeFrameVertices(_puffBlendShapeIndex, 0, deltaVerticesRight, deltaNormals, deltaTangents);

                for (int i = 0; i < m.vertexCount; i++)
                {
                    if (deltaVerticesLeft[i] == Vector3.zero) continue;

                    worldPosition = GetPointRelativeToAvatarVoicePosition(localToWorld, m.vertices[i]);
                    if (worldPosition.x > maxX) maxX = worldPosition.x;
                    if (worldPosition.x < minX) minX = worldPosition.x;
                }
                
                for (int i = 0; i < m.vertexCount; i++)
                {
                    worldPosition = GetPointRelativeToAvatarVoicePosition(localToWorld, m.vertices[i]);
                    rightWeight = Mathf.Clamp01(Mathf.InverseLerp(minX / 4f, maxX / 4f, worldPosition.x));
                    leftWeight = 1f - rightWeight;

                    deltaVerticesLeft[i] = deltaVerticesLeft[i] * leftWeight;
                    deltaVerticesRight[i] = deltaVerticesRight[i] * rightWeight;
                }

                m.AddBlendShapeFrame("Cheek_Puff_Left_generated", 100f, deltaVerticesLeft, null, null);
                m.AddBlendShapeFrame("Cheek_Puff_Right_generated", 100f, deltaVerticesRight, null, null);
            }

            m.RecalculateNormals();
            
            AssetDatabase.SaveAssets();

            FaceTracking.FaceMesh.sharedMesh = m;
            FaceTracking.GetBlendShapeNames();
            FaceTracking.FindVisemes();
        }

        private Vector3 GetPointRelativeToAvatarVoicePosition(Matrix4x4 matrix, Vector3 VertexPosition)
        {
            VertexPosition.Scale(Avatar.transform.localScale);
            return matrix.MultiplyPoint3x4(VertexPosition) - Avatar.transform.TransformPoint(Avatar.voicePosition);
        }
    }
    
    public static class MeshExtensions
    {
        public static Mesh Copy(this Mesh MeshHolder)
        {
            var newMesh = new Mesh();
            
            newMesh = new Mesh();
            newMesh.vertices = MeshHolder.vertices;
            newMesh.normals = MeshHolder.normals;
            newMesh.uv = MeshHolder.uv;
            newMesh.uv2 = MeshHolder.uv2;
            newMesh.uv3 = MeshHolder.uv3;
            newMesh.uv4 = MeshHolder.uv4;
            newMesh.uv5 = MeshHolder.uv5;
            newMesh.uv6 = MeshHolder.uv6;
            newMesh.uv7 = MeshHolder.uv7;
            newMesh.uv8 = MeshHolder.uv8;
            newMesh.colors = MeshHolder.colors;
            newMesh.tangents = MeshHolder.tangents;
            newMesh.triangles = MeshHolder.triangles;
            newMesh.bindposes = MeshHolder.bindposes;
            newMesh.boneWeights = MeshHolder.boneWeights;
            newMesh.bounds = MeshHolder.bounds;
            for (int shape = 0; shape < MeshHolder.blendShapeCount; shape++)
            {
                int frameCount = MeshHolder.GetBlendShapeFrameCount(shape);
                for (int frame = 0; frame < frameCount; frame++)
                {
                    string shapeName = MeshHolder.GetBlendShapeName(shape);
                    float frameWeight = MeshHolder.GetBlendShapeFrameWeight(shape, frame);
                    
                    Vector3[] dVertices = new Vector3[MeshHolder.vertexCount];
                    Vector3[] dNormals = new Vector3[MeshHolder.vertexCount];
                    Vector3[] dTangents = new Vector3[MeshHolder.vertexCount];
                    MeshHolder.GetBlendShapeFrameVertices(shape, frame, dVertices, dNormals, dTangents);
                    newMesh.AddBlendShapeFrame(shapeName, frameWeight, dVertices, dNormals, dTangents);
                }
            }

            newMesh.subMeshCount = MeshHolder.subMeshCount;
            
            for (int submesh = 0; submesh < MeshHolder.subMeshCount; submesh++)
            {
                newMesh.SetSubMesh(submesh, MeshHolder.GetSubMesh(submesh));
            }
            
            return newMesh;
        }
    }
}