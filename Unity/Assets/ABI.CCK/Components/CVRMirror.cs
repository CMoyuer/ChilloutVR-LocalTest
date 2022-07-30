using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable

public class CVRMirror : MonoBehaviour
{
    public bool m_DisablePixelLights = true;
    public int m_TextureSize = 256;
    public float m_ClipPlaneOffset = 0.07f;
    public int m_framesNeededToUpdate = 0;
 
    public LayerMask m_ReflectLayers = -1;
 
    private Dictionary<Camera, Camera> m_ReflectionCameras = new Dictionary<Camera, Camera>();
 
    private RenderTexture m_ReflectionTextureLeft = null;
    private RenderTexture m_ReflectionTextureRight = null;
    private int m_OldReflectionTextureSize = 0;
 
    private int m_frameCounter = 0;
 
    private static bool s_InsideRendering = false;
}
