using ABI.CCK.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace ABI.CCK.Scripts.Editor
{
    public class CCK_WorldPreviewCapture
    {
        static RenderTexture cubemapLeft;
        static RenderTexture cubemapRight;
        static RenderTexture equirect;

        public static Texture2D CapturePreview(Transform position, int size = 4096, float height = 1f, float stereoSeparation = 0.064f)
        {
            cubemapLeft = new RenderTexture(size, size, 0);
            cubemapRight = new RenderTexture(size, size, 0);
            equirect = new RenderTexture(size, size, 0);

            cubemapLeft.dimension = TextureDimension.Cube;
            cubemapRight.dimension = TextureDimension.Cube;
            
            var cameraObject = new GameObject("WorldPreviewCamera");
            Camera cam = cameraObject.AddComponent<Camera>();
            cam.useOcclusionCulling = false;
            
            cameraObject.transform.position = position.position + new Vector3(0, height, 0);

            cam.stereoSeparation = stereoSeparation;
            cam.RenderToCubemap(cubemapLeft, 63, Camera.MonoOrStereoscopicEye.Left);
            cam.RenderToCubemap(cubemapRight, 63, Camera.MonoOrStereoscopicEye.Right);

            cubemapLeft.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Left);
            cubemapRight.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Right);
            
            RenderTexture.active = equirect;
            
            var result = new Texture2D(size, size);
            result.ReadPixels(new Rect(0, 0, result.width, result.height), 0, 0);
            result.Apply();

            Object.DestroyImmediate(cameraObject);

            return result;
        }
        
        public static void CreatePanoImages()
        {
            var foundCVRWorld = MonoBehaviour.FindObjectsOfType<CVRWorld>();
            var world = foundCVRWorld[0];
                
            Texture2D pano = CapturePreview(world.transform, 1024);
                
            byte[] bytes = pano.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/bundle_pano_1024.png", bytes);
                
            pano = CapturePreview(world.transform, 4096);
                
            bytes = pano.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/bundle_pano_4096.png", bytes);
        }
    }
}