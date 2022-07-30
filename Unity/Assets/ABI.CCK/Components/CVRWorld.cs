using System;
using UnityEngine;
using System.Collections.Generic;
#if CCK_ADDIN_HIGHLIGHT_PLUS
using HighlightPlus;
#endif
using UnityEditor;

namespace ABI.CCK.Components
{
    [RequireComponent(typeof(CVRAssetInfo))]
    [ExecuteInEditMode]
    public class CVRWorld : MonoBehaviour
    {

        public enum SpawnRule
        {
            InOrder = 1,
            Random = 2,
        }
        public enum RespawnBehaviour
        {
            Respawn = 1,
            Destroy = 2,
        }
        
        public GameObject[] spawns = new GameObject[0];
        public SpawnRule spawnRule = SpawnRule.Random;
        public GameObject referenceCamera;
        public int respawnHeightY = -100;
        public RespawnBehaviour objectRespawnBehaviour = RespawnBehaviour.Destroy;
        
        //[Space] [Header("Optional settings")] [Space]
        public CVRWarpPoint[] warpPoints = new CVRWarpPoint[0];

        public bool useAdvancedSettings = false;
        public bool allowSpawnables = true;
        public bool allowPortals = true;
        public bool allowFlying = true;
        public bool showNamePlates = true;
        public bool enableBuilder = true;

        public float baseMovementSpeed = 2f;
        public float sprintMultiplier = 2f;
        public float strafeMultiplier = 1f;
        public float crouchMultiplier = 0.5f;
        public float proneMultiplier = 0.3f;
        public float flyMultiplier = 5f;
        public float inAirMovementMultiplier = 1f;

        public float gravity = 18f;
        public float jumpHeight = 1f;
        
        public float fov = 60f;
        public bool enableZoom = true;
        
        #if CCK_ADDIN_HIGHLIGHT_PLUS
        public HighlightProfile highlightProfile;
        #endif
        
        public List<CVRObjectCatalogCategory> objectCatalogCategories = new List<CVRObjectCatalogCategory>();
        public List<CVRObjectCatalogEntry> objectCatalogEntries = new List<CVRObjectCatalogEntry>();
        
        private void OnEnable()
        {
            CVRAssetInfo info = gameObject.GetComponent<CVRAssetInfo>();
            info.type = CVRAssetInfo.AssetType.World;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;

            if (spawns.Length == 0)
            {
                DrawArrow(transform.position, new Vector3(0, transform.eulerAngles.y, 0), 1);
            }
            else
            {
                foreach (GameObject spawn in spawns)
                {
                    if (spawn != null)
                    {
                        DrawArrow(spawn.transform.position, new Vector3(0, spawn.transform.eulerAngles.y, 0), 1);
                    }
                }
            }
            
            
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up);
            Gizmos.matrix = Matrix4x4.TRS(transform.position + Vector3.up, Quaternion.identity, Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, 85f, 1f, 0.1f, 1f);
            Gizmos.matrix = Matrix4x4.TRS(transform.position + Vector3.up, Quaternion.Euler(0, 90, 0), Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, 85f, 1f, 0.1f, 1f);
            Gizmos.matrix = Matrix4x4.TRS(transform.position + Vector3.up, Quaternion.Euler(0, 180, 0), Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, 85f, 1f, 0.1f, 1f);
            Gizmos.matrix = Matrix4x4.TRS(transform.position + Vector3.up, Quaternion.Euler(0, 270, 0), Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, 85f, 1f, 0.1f, 1f);
  
            #if UNITY_EDITOR
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.fontSize = 10;
            Handles.BeginGUI();
            Vector3 pos = transform.TransformPoint(Vector3.up);
            Vector2 pos2D = HandleUtility.WorldToGUIPoint(pos);
            GUI.Label(new Rect(pos2D.x + 20, pos2D.y - 10, 100, 20), "Portal Image will be taken from here", style);
            Handles.EndGUI();
            #endif
        }
        
        private void DrawArrow(Vector3 position, Vector3 angle, float size)
        {
            var a1 = position + new Vector3(0, 0.1f * size, 0);
            var a2 = RotatePointAroundPivot(position + new Vector3(0.1f * size, 0, 0), position, angle);
            var a3 = position + new Vector3(0, -0.1f * size, 0);
            var a4 = RotatePointAroundPivot(position + new Vector3(-0.1f * size, 0, 0), position, angle);
            
            var b1 = RotatePointAroundPivot(position + new Vector3(0, 0.1f * size, 0.3f * size), position, angle);
            var b2 = RotatePointAroundPivot(position + new Vector3(0.1f * size, 0, 0.3f * size), position, angle);
            var b3 = RotatePointAroundPivot(position + new Vector3(0, -0.1f * size, 0.3f * size), position, angle);
            var b4 = RotatePointAroundPivot(position + new Vector3(-0.1f * size, 0, 0.3f * size), position, angle);
            
            var c1 = RotatePointAroundPivot(position + new Vector3(0, 0.2f * size, 0.3f * size), position, angle);
            var c2 = RotatePointAroundPivot(position + new Vector3(0.2f * size, 0, 0.3f * size), position, angle);
            var c3 = RotatePointAroundPivot(position + new Vector3(0, -0.2f * size, 0.3f * size), position, angle);
            var c4 = RotatePointAroundPivot(position + new Vector3(-0.2f * size, 0, 0.3f * size), position, angle);
            
            var d = RotatePointAroundPivot(position + new Vector3(0, 0, 0.5f * size), position, angle);
            
            Gizmos.DrawLine(position, a1);
            Gizmos.DrawLine(position, a2);
            Gizmos.DrawLine(position, a3);
            Gizmos.DrawLine(position, a4);
            
            Gizmos.DrawLine(a1, b1);
            Gizmos.DrawLine(a2, b2);
            Gizmos.DrawLine(a3, b3);
            Gizmos.DrawLine(a4, b4);
            
            Gizmos.DrawLine(b1, c1);
            Gizmos.DrawLine(b2, c2);
            Gizmos.DrawLine(b3, c3);
            Gizmos.DrawLine(b4, c4);
            
            Gizmos.DrawLine(c1, d);
            Gizmos.DrawLine(c2, d);
            Gizmos.DrawLine(c3, d);
            Gizmos.DrawLine(c4, d);
        }
        
        private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
        {
            var dir = point - pivot; // get point direction relative to pivot
            dir = Quaternion.Euler(angles) * dir; // rotate it
            point = dir + pivot; // calculate rotated point
            return point; // return it
        }
    }

    [System.Serializable]
    public class CVRObjectCatalogCategory
    {
        public string id;
        public string name;
        public Texture2D image;
    }

    [System.Serializable]
    public class CVRObjectCatalogEntry
    {
        public string name;
        public GameObject prefab;
        public Texture2D preview;
        public string categoryId = "";
        public string guid = "";
    }
}
