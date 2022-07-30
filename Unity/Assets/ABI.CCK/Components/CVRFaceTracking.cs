using System.Collections.Generic;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRFaceTracking : MonoBehaviour
    {
        public bool UseFacialTracking = true;
        public float BlendShapeStrength = 100f;
        public SkinnedMeshRenderer FaceMesh;
        public string[] FaceBlendShapes = new string[37];
        
        public Mesh OriginalMesh = null;
        [HideInInspector]
        public List<string> BlendShapeNames = null;
        [HideInInspector]
        public static string[] FaceBlendShapeNames = new[] {"Jaw_Right", "Jaw_Left", "Jaw_Forward", "Jaw_Open", 
            "Mouth_Ape_Shape", "Mouth_Upper_Right", "Mouth_Upper_Left", "Mouth_Lower_Right", "Mouth_Lower_Left", 
            "Mouth_Upper_Overturn", "Mouth_Lower_Overturn", "Mouth_Pout", "Mouth_Smile_Right", "Mouth_Smile_Left", 
            "Mouth_Sad_Right", "Mouth_Sad_Left", "Cheek_Puff_Right", "Cheek_Puff_Left", "Cheek_Suck", 
            "Mouth_Upper_UpRight", "Mouth_Upper_UpLeft", "Mouth_Lower_DownRight", "Mouth_Lower_DownLeft", 
            "Mouth_Upper_Inside", "Mouth_Lower_Inside", "Mouth_Lower_Overlay", "Tongue_LongStep1", "Tongue_LongStep2", 
            "Tongue_Down", "Tongue_Up", "Tongue_Right", "Tongue_Left", "Tongue_Roll", "Tongue_UpLeft_Morph", 
            "Tongue_UpRight_Morph", "Tongue_DownLeft_Morph", "Tongue_DownRight_Morph"};
        
        public bool enableOverdriveBlendShapes = false;
        
        public void GetBlendShapeNames()
        {
            if (FaceMesh != null)
            {
                BlendShapeNames = new List<string>();
                BlendShapeNames.Add("-none-");
                for (int i = 0; i < FaceMesh.sharedMesh.blendShapeCount; ++i)
                    BlendShapeNames.Add(FaceMesh.sharedMesh.GetBlendShapeName(i));
            }
            else
            {
                BlendShapeNames = new List<string>();
                BlendShapeNames.Add("-none-");
            }
        }
        
        public void FindVisemes()
        {
            for (int i = 0; i < FaceBlendShapeNames.Length; i++)
            {
                for (int j = 0; j < BlendShapeNames.Count; ++j)
                {
                    if (BlendShapeNames[j].ToLower().Contains(FaceBlendShapeNames[i].ToLower()) ||
                        BlendShapeNames[j].ToLower().Contains(FaceBlendShapeNames[i].ToLower().Replace("_", "")))
                    {
                        FaceBlendShapes[i] = BlendShapeNames[j];
                    }
                }
            }
        }
    }
}