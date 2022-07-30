using System;
using System.Collections.Generic;
using ABI.CCK.Components;
#if CCK_ADDIN_HIGHLIGHT_PLUS
using HighlightPlus;
#endif
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRWorld)), CanEditMultipleObjects]
    public class CCK_CVRWorldEditor : UnityEditor.Editor
    {
        private CVRWorld _world;

        private ReorderableList CategoryList = null;
        private CVRObjectCatalogCategory objectCatalogCategory = null;
        private List<string> popupListName = new List<string>();
        private List<string> popupListID = new List<string>();
        
        private ReorderableList objectCatalogList = null;
        private CVRObjectCatalogEntry objectCatalogEntry = null;

        public override void OnInspectorGUI()
        {
            if (_world == null) _world = (CVRWorld) target;
            
            EditorGUILayout.LabelField("World settings");
            
            var list = serializedObject.FindProperty("spawns");
            EditorGUILayout.PropertyField(list, new GUIContent("Spawns"), true);
            serializedObject.ApplyModifiedProperties();

            _world.spawnRule = (CVRWorld.SpawnRule) EditorGUILayout.EnumPopup("Spawn Rule", _world.spawnRule);

            _world.referenceCamera = (GameObject) EditorGUILayout.ObjectField("Reference Camera", _world.referenceCamera, typeof(GameObject), true);

            _world.respawnHeightY = EditorGUILayout.IntField("Respawn Height Y", _world.respawnHeightY);
            
            _world.objectRespawnBehaviour = (CVRWorld.RespawnBehaviour) EditorGUILayout.EnumPopup("Object Respawn Behaviour", _world.objectRespawnBehaviour);
            
            EditorGUILayout.LabelField("Optional settings");
            
            list = serializedObject.FindProperty("warpPoints");
            EditorGUILayout.PropertyField(list, new GUIContent("Warp Points"), true);
            serializedObject.ApplyModifiedProperties();

            
            GUILayout.BeginVertical("HelpBox");
            GUILayout.BeginHorizontal ();
            _world.useAdvancedSettings = EditorGUILayout.Toggle (_world.useAdvancedSettings, GUILayout.Width(16));
            EditorGUILayout.LabelField ("Use Advanced Settings", GUILayout.Width(250));
            GUILayout.EndHorizontal ();

            if (_world.useAdvancedSettings)
            {
                GUILayout.BeginHorizontal ();
                GUILayout.BeginVertical ("GroupBox");
                
                EditorGUILayout.LabelField("World Rules");
                
                _world.allowSpawnables = EditorGUILayout.Toggle("Allow Spawnables", _world.allowSpawnables);
                
                _world.allowPortals = EditorGUILayout.Toggle("Allow Portals", _world.allowPortals);
                
                _world.allowFlying = EditorGUILayout.Toggle("Allow Flying", _world.allowFlying);
                
                _world.showNamePlates = EditorGUILayout.Toggle("Show Nameplates", _world.showNamePlates);
                
                _world.enableBuilder = EditorGUILayout.Toggle("Enable Builder", _world.enableBuilder);
                
                EditorGUILayout.Space();
                
                EditorGUILayout.LabelField("Movement Parameters");
                
                EditorGUILayout.HelpBox("Changing these Values can lead to an undesirable experience.", MessageType.Warning);
                
                EditorGUILayout.Space();

                _world.baseMovementSpeed = EditorGUILayout.FloatField("Base Movement Speed", _world.baseMovementSpeed);
                
                _world.sprintMultiplier = EditorGUILayout.FloatField("Sprint Multiplier", _world.sprintMultiplier);
                
                _world.strafeMultiplier = EditorGUILayout.FloatField("Strafe Multiplier", _world.strafeMultiplier);
                
                _world.crouchMultiplier = EditorGUILayout.FloatField("Crouch Multiplier", _world.crouchMultiplier);
                
                _world.proneMultiplier = EditorGUILayout.FloatField("Prone Multiplier", _world.proneMultiplier);
                
                _world.flyMultiplier = EditorGUILayout.FloatField("Fly Multiplier", _world.flyMultiplier);
                
                _world.inAirMovementMultiplier = EditorGUILayout.FloatField("In Air Movement Multiplier", _world.inAirMovementMultiplier);
                
                _world.gravity = EditorGUILayout.FloatField("Gravity", _world.gravity);
                
                _world.jumpHeight = EditorGUILayout.FloatField("Jump Height", _world.jumpHeight);
                
                _world.fov = EditorGUILayout.Slider("Fov", _world.fov, 60f, 120f);

                _world.enableZoom = EditorGUILayout.Toggle("Enable Zoom", _world.enableZoom);

                #if CCK_ADDIN_HIGHLIGHT_PLUS
                _world.highlightProfile = (HighlightProfile) EditorGUILayout.ObjectField("Highlighting Profile", _world.highlightProfile,
                    typeof(HighlightProfile), true);
                #endif
                
                GUILayout.EndVertical ();
                GUILayout.EndHorizontal ();
            }
            
            GUILayout.EndVertical ();
            
            GUILayout.BeginVertical("HelpBox");
            GUILayout.BeginHorizontal ();
            EditorGUILayout.LabelField ("Object Catalog");
            GUILayout.EndHorizontal ();
            GUILayout.BeginHorizontal ();
            GUILayout.BeginVertical ("GroupBox");

            InitializeCategoryList();
            CategoryList.DoLayoutList();
            popupListName.Clear();
            popupListID.Clear();
            foreach (var category in _world.objectCatalogCategories)
            {
                if (string.IsNullOrEmpty(category.id)) category.id = "w~" + System.Guid.NewGuid().ToString();
                popupListName.Add(string.IsNullOrEmpty(category.name) ? "-" : category.name);
                popupListID.Add(category.id);
            }
            
            EditorGUILayout.Space();
            
            InitializeObjectCatalogList();
            objectCatalogList.DoLayoutList();
            
            GUILayout.EndVertical ();
            GUILayout.EndHorizontal ();
            GUILayout.EndVertical ();
        }

        private void InitializeCategoryList()
        {
            if (CategoryList != null) return;
            
            CategoryList = new ReorderableList(_world.objectCatalogCategories, typeof(CVRObjectCatalogCategory),
                false, true, true, true);
            CategoryList.drawHeaderCallback = OnDrawHeaderCategory;
            CategoryList.drawElementCallback = OnDrawElementCategory;
            CategoryList.elementHeightCallback = OnHeightElementCategory;
            CategoryList.onChangedCallback = OnChangedCategory; 
        }

        private void OnDrawHeaderCategory(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "Categories");
        }

        private void OnDrawElementCategory(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index > _world.objectCatalogCategories.Count) return;
            objectCatalogCategory = _world.objectCatalogCategories[index];
            
            Rect _rect = new Rect(rect.x + 50, rect.y, 50, EditorGUIUtility.singleLineHeight);
            Rect initialRect = rect;

            EditorGUI.LabelField(_rect, "Name");
            _rect.x += 50;
            _rect.width = rect.width - 50 - 50;
            
            objectCatalogCategory.name = EditorGUI.TextField(_rect, objectCatalogCategory.name);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x + 50, rect.y, 50, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Image");
            _rect.x += 50;
            _rect.width = rect.width - 50 - 50;
            
            objectCatalogCategory.image = (Texture2D) EditorGUI.ObjectField(_rect, objectCatalogCategory.image, typeof(Texture2D));

            if (objectCatalogCategory.image != null)
            {
                GUI.DrawTexture(new Rect(initialRect.x, initialRect.y, 42, 42), objectCatalogCategory.image);
            }
        }

        private float OnHeightElementCategory(int index)
        {
            return EditorGUIUtility.singleLineHeight * 2.5f;
        }

        private void OnChangedCategory(ReorderableList list)
        {
            EditorUtility.SetDirty(target);
        }

        private void InitializeObjectCatalogList()
        {
            if (objectCatalogList != null) return;
            
            objectCatalogList = new ReorderableList(_world.objectCatalogEntries, typeof(CVRObjectCatalogEntry),
                true, true, true, true);
            objectCatalogList.drawHeaderCallback = OnDrawHeaderEntry;
            objectCatalogList.drawElementCallback = OnDrawElementEntry;
            objectCatalogList.elementHeightCallback = OnHeightElementEntry;
            objectCatalogList.onChangedCallback = OnChangedEntry; 
        }

        private void OnDrawHeaderEntry(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "Objects");
        }

        private void OnDrawElementEntry(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index > _world.objectCatalogEntries.Count) return;
            objectCatalogEntry = _world.objectCatalogEntries[index];

            while (objectCatalogEntry.guid == "")
            {
                var guid = "w~" + System.Guid.NewGuid().ToString();
                var guidValid = true;
                
                foreach (var entry in _world.objectCatalogEntries)
                {
                    if (entry.guid == guid) guidValid = false;
                }

                if (!guidValid) continue;
                objectCatalogEntry.guid = guid;
            }
            
            Rect _rect = new Rect(rect.x + 100, rect.y, 100, EditorGUIUtility.singleLineHeight);
            Rect initialRect = rect;

            EditorGUI.LabelField(_rect, "Name");
            _rect.x += 60;
            _rect.width = rect.width - 160;
            
            objectCatalogEntry.name = EditorGUI.TextField(_rect, objectCatalogEntry.name);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x + 100, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Prefab");
            _rect.x += 60;
            _rect.width = rect.width - 160;
            
            objectCatalogEntry.prefab = (GameObject) EditorGUI.ObjectField(_rect, objectCatalogEntry.prefab, typeof(GameObject));

            if (objectCatalogEntry.prefab != null)
            {
                var spawnable = objectCatalogEntry.prefab.GetComponent<CVRSpawnable>();
                var builder = objectCatalogEntry.prefab.GetComponent<CVRBuilderSpawnable>();

                if (spawnable == null && builder == null) objectCatalogEntry.prefab = null;
            }
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x + 100, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Category");
            _rect.x += 60;
            _rect.width = rect.width - 160;

            var catIndex = popupListID.IndexOf(objectCatalogEntry.categoryId);
            catIndex = EditorGUI.Popup(_rect, catIndex, popupListName.ToArray());
            if (catIndex >= 0) objectCatalogEntry.categoryId = popupListID[catIndex];
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x + 100, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Preview");
            _rect.x += 60;
            _rect.width = rect.width - 160;
            
            objectCatalogEntry.preview = (Texture2D) EditorGUI.ObjectField(_rect, objectCatalogEntry.preview, typeof(Texture2D));
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            if (GUI.Button(_rect, "Generate Preview"))
            {
                GeneratePreviewImage(objectCatalogEntry);
            }
            
            if (objectCatalogEntry.preview != null)
            {
                GUI.DrawTexture(new Rect(initialRect.x, initialRect.y, 88, 88), objectCatalogEntry.preview);
            }
        }

        private void GeneratePreviewImage(CVRObjectCatalogEntry objectCatalogEntry)
        {
            if (objectCatalogEntry.prefab == null) return;
            
            var path = "Assets/ABI.CCK/Resources/WorldObjectCatalog";
            var objectPosition = Vector3.down * 1000f;
            var rt = new RenderTexture(256, 256, 32);
            
            if (!AssetDatabase.IsValidFolder(path))
                AssetDatabase.CreateFolder("Assets/ABI.CCK/Resources", "WorldObjectCatalog");

            var obj = Instantiate(objectCatalogEntry.prefab, objectPosition, Quaternion.identity);

            var renderers = obj.GetComponentsInChildren<Renderer>();
            Bounds b = new Bounds(Vector3.zero, Vector3.zero);

            foreach (var renderer in renderers)
            {
                if (b.size.y == 0f)
                {
                    b = renderer.bounds;
                }
                else
                {
                    b.Encapsulate(renderer.bounds);
                }
            }
            
            var transforms = obj.GetComponentsInChildren<Transform>();
            foreach (var transform in transforms)
            {
                transform.gameObject.layer = 12;
            }

            var cam = new GameObject("Preview Capture", new[] {typeof(Camera), typeof(Light)}).GetComponent<Camera>();
            var light = cam.gameObject.GetComponent<Light>();
            light.type = LightType.Directional;
            light.cullingMask = 1 << 12;
            cam.aspect = 1f;
            cam.targetTexture = rt;
            cam.clearFlags = CameraClearFlags.Color;
            cam.backgroundColor = Color.black;
            cam.cullingMask = 1 << 12;

            cam.transform.position = objectPosition + new Vector3(0f, b.extents.y, b.extents.y * 2f);
            cam.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            Texture2D screenShot1 = new Texture2D(256, 256, TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot1.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            AssetDatabase.CreateAsset(screenShot1, path + "/ObjectCatalog_temp_1.asset");
            AssetDatabase.ImportAsset(path + "/ObjectCatalog_temp_1.asset", ImportAssetOptions.ForceUpdate);
            RenderTexture.active = null;
            
            cam.transform.position = objectPosition + new Vector3(b.extents.y * 1.41f, b.extents.y, b.extents.y * 1.41f);
            cam.transform.eulerAngles = new Vector3(0f, 225f, 0f);
            Texture2D screenShot2 = new Texture2D(256, 256, TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot2.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            AssetDatabase.CreateAsset(screenShot2, path + "/ObjectCatalog_temp_2.asset");
            AssetDatabase.ImportAsset(path + "/ObjectCatalog_temp_2.asset", ImportAssetOptions.ForceUpdate);
            RenderTexture.active = null;
            
            cam.transform.position = objectPosition + new Vector3(b.extents.y * 2f, b.extents.y, 0f);
            cam.transform.eulerAngles = new Vector3(0f, 270f, 0f);
            Texture2D screenShot3 = new Texture2D(256, 256, TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot3.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            AssetDatabase.CreateAsset(screenShot3, path + "/ObjectCatalog_temp_3.asset");
            AssetDatabase.ImportAsset(path + "/ObjectCatalog_temp_3.asset", ImportAssetOptions.ForceUpdate);
            RenderTexture.active = null;
            
            cam.transform.position = objectPosition + new Vector3(b.extents.y * 1.41f, b.extents.y, b.extents.y * -1.41f);
            cam.transform.eulerAngles = new Vector3(0f, 315f, 0f);
            Texture2D screenShot4 = new Texture2D(256, 256, TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot4.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            AssetDatabase.CreateAsset(screenShot4, path + "/ObjectCatalog_temp_4.asset");
            AssetDatabase.ImportAsset(path + "/ObjectCatalog_temp_4.asset", ImportAssetOptions.ForceUpdate);
            RenderTexture.active = null;

            cam.transform.position = objectPosition + new Vector3(0f, b.extents.y, b.extents.y * -2f);
            cam.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Texture2D screenShot5 = new Texture2D(256, 256, TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot5.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            AssetDatabase.CreateAsset(screenShot5, path + "/ObjectCatalog_temp_5.asset");
            AssetDatabase.ImportAsset(path + "/ObjectCatalog_temp_5.asset", ImportAssetOptions.ForceUpdate);
            RenderTexture.active = null;
            
            cam.transform.position = objectPosition + new Vector3(b.extents.y * -1.41f, b.extents.y, b.extents.y * -1.41f);
            cam.transform.eulerAngles = new Vector3(0f, 45f, 0f);
            Texture2D screenShot6 = new Texture2D(256, 256, TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot6.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            AssetDatabase.CreateAsset(screenShot6, path + "/ObjectCatalog_temp_6.asset");
            AssetDatabase.ImportAsset(path + "/ObjectCatalog_temp_6.asset", ImportAssetOptions.ForceUpdate);
            RenderTexture.active = null;
            
            cam.transform.position = objectPosition + new Vector3(b.extents.y * -2f, b.extents.y, 0f);
            cam.transform.eulerAngles = new Vector3(0f, 90f, 0f);
            Texture2D screenShot7 = new Texture2D(256, 256, TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot7.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            AssetDatabase.CreateAsset(screenShot7, path + "/ObjectCatalog_temp_7.asset");
            AssetDatabase.ImportAsset(path + "/ObjectCatalog_temp_7.asset", ImportAssetOptions.ForceUpdate);
            RenderTexture.active = null;
            
            cam.transform.position = objectPosition + new Vector3(b.extents.y * -1.41f, b.extents.y, b.extents.y * 1.41f);
            cam.transform.eulerAngles = new Vector3(0f, 135f, 0f);
            Texture2D screenShot8 = new Texture2D(256, 256, TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot8.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            AssetDatabase.CreateAsset(screenShot8, path + "/ObjectCatalog_temp_8.asset");
            AssetDatabase.ImportAsset(path + "/ObjectCatalog_temp_8.asset", ImportAssetOptions.ForceUpdate);
            RenderTexture.active = null;

            DestroyImmediate(obj);
            DestroyImmediate(cam.gameObject);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            WorldObjectCatalogPreviewSelector window = (WorldObjectCatalogPreviewSelector) EditorWindow.GetWindow (
                typeof(WorldObjectCatalogPreviewSelector), 
                true, 
                "Select Catalog Object Preview"
            );

            window.objectCatalogEntry = objectCatalogEntry;
            window.previewOption1 = screenShot1;
            window.previewOption2 = screenShot2;
            window.previewOption3 = screenShot3;
            window.previewOption4 = screenShot4;
            window.previewOption5 = screenShot5;
            window.previewOption6 = screenShot6;
            window.previewOption7 = screenShot7;
            window.previewOption8 = screenShot8;

            var scale = WorldObjectCatalogPreviewSelector.previewWindowScale;
            
            window.minSize = new Vector2(1152 * scale, 576 * scale);
            window.maxSize = new Vector2(1152 * scale, 576 * scale);
            
            window.Show();
        }

        private float OnHeightElementEntry(int index)
        {
            return EditorGUIUtility.singleLineHeight * 6.25f;
        }

        private void OnChangedEntry(ReorderableList list)
        {
            EditorUtility.SetDirty(target);
        }
    }

    public class WorldObjectCatalogPreviewSelector : EditorWindow
    {
        public static float previewWindowScale = 0.75f;
        
        public CVRObjectCatalogEntry objectCatalogEntry = null;
        public Texture2D previewOption1 = null;
        public Texture2D previewOption2 = null;
        public Texture2D previewOption3 = null;
        public Texture2D previewOption4 = null;
        public Texture2D previewOption5 = null;
        public Texture2D previewOption6 = null;
        public Texture2D previewOption7 = null;
        public Texture2D previewOption8 = null;
        
        private string path = "Assets/ABI.CCK/Resources/WorldObjectCatalog";
        
        private void OnGUI()
        {
            if (GUI.Button(new Rect(16 * previewWindowScale, 16 * previewWindowScale, 256 * previewWindowScale, 256 * previewWindowScale), previewOption1))
            {
                SelectPreviewImage(objectCatalogEntry, previewOption1);
            }
            if (GUI.Button(new Rect(304 * previewWindowScale, 16 * previewWindowScale, 256 * previewWindowScale, 256 * previewWindowScale), previewOption2))
            {
                SelectPreviewImage(objectCatalogEntry, previewOption2);
            }
            if (GUI.Button(new Rect(592 * previewWindowScale, 16 * previewWindowScale, 256 * previewWindowScale, 256 * previewWindowScale), previewOption3))
            {
                SelectPreviewImage(objectCatalogEntry, previewOption3);
            }
            if (GUI.Button(new Rect(880 * previewWindowScale, 16 * previewWindowScale, 256 * previewWindowScale, 256 * previewWindowScale), previewOption4))
            {
                SelectPreviewImage(objectCatalogEntry, previewOption4);
            }
            if (GUI.Button(new Rect(16 * previewWindowScale, 304 * previewWindowScale, 256 * previewWindowScale, 256 * previewWindowScale), previewOption5))
            {
                SelectPreviewImage(objectCatalogEntry, previewOption5);
            }
            if (GUI.Button(new Rect(304 * previewWindowScale, 304 * previewWindowScale, 256 * previewWindowScale, 256 * previewWindowScale), previewOption6))
            {
                SelectPreviewImage(objectCatalogEntry, previewOption6);
            }
            if (GUI.Button(new Rect(592 * previewWindowScale, 304 * previewWindowScale, 256 * previewWindowScale, 256 * previewWindowScale), previewOption7))
            {
                SelectPreviewImage(objectCatalogEntry, previewOption7);
            }
            if (GUI.Button(new Rect(880 * previewWindowScale, 304 * previewWindowScale, 256 * previewWindowScale, 256 * previewWindowScale), previewOption8))
            {
                SelectPreviewImage(objectCatalogEntry, previewOption8);
            }
        }

        private void SelectPreviewImage(CVRObjectCatalogEntry objectCatalogEntry, Texture2D selected)
        {
            byte[] bytes = selected.EncodeToPNG();
            var filename = path + "/ObjectCatalog_" + objectCatalogEntry.name + "_" + objectCatalogEntry.guid + "_preview.png";
            System.IO.File.WriteAllBytes(filename, bytes);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Texture2D createdTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(filename);
            objectCatalogEntry.preview = createdTexture;
        }
    }
}