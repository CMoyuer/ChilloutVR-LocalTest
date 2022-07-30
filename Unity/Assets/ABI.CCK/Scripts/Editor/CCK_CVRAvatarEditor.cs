using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ABI.CCK.Components;
using UnityEditor.Profiling;
using UnityEditorInternal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AnimatorController = UnityEditor.Animations.AnimatorController;
using AnimatorControllerParameterType = UnityEngine.AnimatorControllerParameterType;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRAvatar))]
    public class CCK_CVRAvatarEditor : UnityEditor.Editor
    {
        private static string[] _visemeNames = new[] {"sil", "PP", "FF", "TH", "DD", "kk", "CH", "SS", "nn", "RR", "aa", "E", "ih", "oh", "ou"};
        public static string[] coreParameters = {"MovementX", "MovementY", "Grounded", "Emote", "GestureLeft", 
            "GestureRight", "Toggle", "Sitting", "Crouching", "CancelEmote", "Prone", "Flying"};
        
        private CVRAvatar _avatar;
        private int _blink;
        
        private List<string> _blendShapeNames = null;
        
        private ReorderableList reorderableList;
        private CVRAdvancedSettingsEntry entity = null;
        private int syncedValues = 0;
        private int syncedBooleans = 0;
        private List<string> animatorParameters = new List<string>();
        private bool definitionContainsError = false;
        
        private ReorderableList taggingList;
        private CVRAvatarAdvancedTaggingEntry tagEntry;

        private void InitializeList()
        {
            if (_avatar.avatarSettings == null) return;

            reorderableList = new ReorderableList(_avatar.avatarSettings.settings, typeof(CVRAdvancedSettingsEntry),
                                                  true, true, true, true);
            reorderableList.drawHeaderCallback = OnDrawHeader;
            reorderableList.drawElementCallback = OnDrawElement;
            reorderableList.elementHeightCallback = OnHeightElement;
            reorderableList.onAddCallback = OnAdd;
            reorderableList.onChangedCallback = OnChanged; 
        }

        private void InitializeTaggingList()
        {
            taggingList = new ReorderableList(_avatar.advancedTaggingList, typeof(CVRAvatarAdvancedTaggingEntry),
                false, true, true, true);
            taggingList.drawHeaderCallback = OnDrawHeaderTagging;
            taggingList.drawElementCallback = OnDrawElementTagging;
            taggingList.elementHeightCallback = OnHeightElementTagging;
            taggingList.onAddCallback = OnAddTagging;
            taggingList.onChangedCallback = OnChangedTagging; 
        }

        public override void OnInspectorGUI()
        {
            if (_avatar == null) _avatar = (CVRAvatar) target;

            GetBlendShapeNames();
            
            EditorGUILayout.LabelField("General avatar settings", EditorStyles.boldLabel);
            _avatar.viewPosition = EditorGUILayout.Vector3Field("View Position", _avatar.viewPosition);
            EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_AVATAR_INFO_VIEWPOINT"), MessageType.Info);
            _avatar.voicePosition = EditorGUILayout.Vector3Field("Voice Position", _avatar.voicePosition);
            _avatar.voiceParent = (CVRAvatar.CVRAvatarVoiceParent) EditorGUILayout.EnumPopup("Voice Parent", _avatar.voiceParent);
            EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_AVATAR_INFO_VOICE_POSITION"), MessageType.Info);
            
            EditorGUILayout.LabelField("");
            EditorGUILayout.LabelField("Avatar customization", EditorStyles.boldLabel);
            _avatar.overrides = (AnimatorOverrideController)EditorGUILayout.ObjectField("Animation Overrides", _avatar.overrides, typeof(AnimatorOverrideController), true, null);
            EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_AVATAR_INFO_OVERRIDE_CONTROLLER"), MessageType.Info);
            
            EditorGUILayout.LabelField("");
            EditorGUILayout.LabelField("Blinking and Visemes", EditorStyles.boldLabel);
            _avatar.bodyMesh = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("Face Mesh", _avatar.bodyMesh, typeof(SkinnedMeshRenderer), true);

            _avatar.useEyeMovement = EditorGUILayout.Toggle("Use Eye Movement", _avatar.useEyeMovement);
            EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_AVATAR_INFO_EYE_MOVEMENT"), MessageType.Info);
            _avatar.useBlinkBlendshapes = EditorGUILayout.Toggle("Use Blink Blendshapes", _avatar.useBlinkBlendshapes);
            EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_AVATAR_INFO_BLinking"), MessageType.Info);
            for (int i = 0; i < 4; i++)
            {
                int current = 0;
                for (int j = 0; j < _blendShapeNames.Count; ++j)
                    if (_avatar.blinkBlendshape[i] == _blendShapeNames[j])
                        current = j;
                
                int blink = EditorGUILayout.Popup("Blink " + (i + 1), current, _blendShapeNames.ToArray());
                _avatar.blinkBlendshape[i] = _blendShapeNames[blink];
            }
            
            EditorGUILayout.Space();
            
            _avatar.useVisemeLipsync = EditorGUILayout.Toggle("Use Lip Sync", _avatar.useVisemeLipsync);

            _avatar.visemeMode = (CVRAvatar.CVRAvatarVisemeMode) EditorGUILayout.EnumPopup("Lip Sync Mode", _avatar.visemeMode);

            if (_avatar.visemeBlendshapes == null || _avatar.visemeBlendshapes.Length != _visemeNames.Length)
                _avatar.visemeBlendshapes = new string[_visemeNames.Length];

            EditorGUILayout.Space();
            
            if (_avatar.visemeMode == CVRAvatar.CVRAvatarVisemeMode.Visemes)
            {
                for (int i = 0; i < _visemeNames.Length; i++)
                {
                    int current = 0;
                    for (int j = 0; j < _blendShapeNames.Count; ++j)
                        if (_avatar.visemeBlendshapes[i] == _blendShapeNames[j])
                            current = j;

                    int viseme = EditorGUILayout.Popup("Viseme: " + _visemeNames[i], current,
                        _blendShapeNames.ToArray());
                    _avatar.visemeBlendshapes[i] = _blendShapeNames[viseme];
                }

                if (GUILayout.Button("Auto select Visemes"))
                {
                    FindVisemes();
                    _avatar.useVisemeLipsync = true;
                }
                
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_AVATAR_INFO_EYE_VISEMES"), MessageType.Info);
            }
            else if (_avatar.visemeMode == CVRAvatar.CVRAvatarVisemeMode.SingleBlendshape)
            {
                int current = 0;
                for (int j = 0; j < _blendShapeNames.Count; ++j)
                    if (_avatar.visemeBlendshapes[0] == _blendShapeNames[j])
                        current = j;
                int viseme = EditorGUILayout.Popup("Viseme", current,
                    _blendShapeNames.ToArray());
                _avatar.visemeBlendshapes[0] = _blendShapeNames[viseme];
            }

            //Advanced Tagging System
            GUILayout.BeginVertical("HelpBox");
            
            GUILayout.BeginHorizontal();
            _avatar.enableAdvancedTagging = EditorGUILayout.Toggle(_avatar.enableAdvancedTagging, GUILayout.Width(16));
            EditorGUILayout.LabelField("Enable Advanced Tagging", GUILayout.Width(250));
            GUILayout.EndHorizontal();

            if (_avatar.enableAdvancedTagging)
            {
                EditorGUILayout.HelpBox("Attention! If you are using the Advanced Tagging System, you still need to Tag your Avatar appropriately and Set all affected GameObjects here.", MessageType.Warning);
                
                if (taggingList == null) InitializeTaggingList();
                taggingList.DoLayoutList();
            }
            
            GUILayout.EndVertical();
            

            //Advanced Avatar Settings
            GUILayout.BeginVertical("HelpBox");
            
            GUILayout.BeginHorizontal();
            _avatar.avatarUsesAdvancedSettings = EditorGUILayout.Toggle(_avatar.avatarUsesAdvancedSettings, GUILayout.Width(16));
            EditorGUILayout.LabelField("Enable Advanced Settings", GUILayout.Width(250));
            GUILayout.EndHorizontal();
            
            if (_avatar.avatarUsesAdvancedSettings)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("GroupBox");

                if (_avatar.avatarSettings == null || !_avatar.avatarSettings.initialized)
                {
                    CreateAvatarSettings(_avatar);
                }

                _avatar.avatarSettings.baseController = (RuntimeAnimatorController) EditorGUILayout.ObjectField("Base Animator",
                    _avatar.avatarSettings.baseController, typeof(RuntimeAnimatorController));
                if (_avatar.avatarSettings.baseController is AnimatorOverrideController)
                    _avatar.avatarSettings.baseController = null;
                
                EditorGUILayout.HelpBox("This is the Base Animator that is extended for the creation of your Advanced Avatar Settings. "+
                                        "If you do not want to extend a specific Animator Controller, make sure that the Default Avatar Animator "+
                                        "From the Directory \"ABI.CCK/Animations\" is used here.", MessageType.Info);
                
                _avatar.avatarSettings.baseOverrideController = (RuntimeAnimatorController) EditorGUILayout.ObjectField("Override Controller",
                    _avatar.avatarSettings.baseOverrideController, typeof(RuntimeAnimatorController));
                if (_avatar.avatarSettings.baseOverrideController is AnimatorController)
                    _avatar.avatarSettings.baseOverrideController = null;
                
                EditorGUILayout.HelpBox("You can Put your previous Override Controller here in order to put your overrides in "+
                                        "the newly created Override Controller.", MessageType.Info);
                
                UpdateSyncCount();
                int current = syncedValues * 32 + Mathf.CeilToInt(syncedBooleans / 8f) * 8;
                Rect _rect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight);
                EditorGUI.ProgressBar(_rect, current / 3200f, current + " of 3200 Synced Bit used");
                
                if (reorderableList == null) InitializeList();
                reorderableList.DoLayoutList();

                if (!definitionContainsError)
                {
                    if (GUILayout.Button("CreateAnimator")) CreateAnimator();
                }

                if (_avatar.avatarSettings.overrides != null && _avatar.avatarSettings.overrides != _avatar.overrides)
                {
                    if (GUILayout.Button("Attach created Override to Avatar"))
                    {
                        _avatar.overrides = _avatar.avatarSettings.overrides;
                    }
                }
                
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }

        private void UpdateSyncCount()
        {
            syncedValues = 0;
            syncedBooleans = 0;

            if (_avatar.avatarSettings == null) return;
            
            animatorParameters.Clear();
            definitionContainsError = false;

            if (_avatar.avatarSettings.baseController != null)
            {
                var animator = (AnimatorController) _avatar.avatarSettings.baseController;

                foreach (var parameter in animator.parameters)
                {
                    if (parameter.type == AnimatorControllerParameterType.Float && parameter.name.Length > 0 &&
                        !coreParameters.Contains(parameter.name) && parameter.name.Substring(0, 1) != "#")
                    {
                        syncedValues += 1;
                        animatorParameters.Add(parameter.name);
                    }
                    if (parameter.type == AnimatorControllerParameterType.Int && parameter.name.Length > 0 &&
                        !coreParameters.Contains(parameter.name) && parameter.name.Substring(0, 1) != "#")
                    {
                        syncedValues += 1;
                        animatorParameters.Add(parameter.name);
                    }
                    if (parameter.type == AnimatorControllerParameterType.Bool && parameter.name.Length > 0 &&
                        !coreParameters.Contains(parameter.name) && parameter.name.Substring(0, 1) != "#")
                    {
                        syncedBooleans += 1;
                        animatorParameters.Add(parameter.name);
                    }
                }
            }

            foreach (var entry in _avatar.avatarSettings.settings)
            {
                if (entry == null) continue;
                if (entry.name == null) continue;
                if (entry.name.Length == 0) continue;
                if (entry.name.Substring(0, 1) == "#") continue;

                switch (entry.type)
                {
                    case CVRAdvancedSettingsEntry.SettingsType.GameObjectToggle:
                        if (animatorParameters.Contains(entry.machineName)) continue;
                        if (entry.setting.usedType == CVRAdvancesAvatarSettingBase.ParameterType.GenerateBool)
                        {
                            syncedBooleans += 1;
                        }
                        else
                        {
                            syncedValues += 1;
                        }
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.GameObjectDropdown:
                        if (animatorParameters.Contains(entry.machineName)) continue;
                        syncedValues += 1;
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.MaterialColor:
                        if (animatorParameters.Contains(entry.machineName + "-r") || animatorParameters.Contains(entry.machineName + "-g") || animatorParameters.Contains(entry.machineName + "-b")) continue;
                        syncedValues += 3;
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.Joystick2D:
                        if (animatorParameters.Contains(entry.machineName + "-x") || animatorParameters.Contains(entry.machineName + "-y")) continue;
                        syncedValues += 2;
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.Joystick3D:
                        if (animatorParameters.Contains(entry.machineName + "-x") || animatorParameters.Contains(entry.machineName + "-y") || animatorParameters.Contains(entry.machineName + "-z")) continue;
                        syncedValues += 3;
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.InputVector2:
                        if (animatorParameters.Contains(entry.machineName + "-x") || animatorParameters.Contains(entry.machineName + "-y")) continue;
                        syncedValues += 2;
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.InputVector3:
                        if (animatorParameters.Contains(entry.machineName + "-x") || animatorParameters.Contains(entry.machineName + "-y") || animatorParameters.Contains(entry.machineName + "-z")) continue;
                        syncedValues += 3;
                        break;
                    default:
                        if (animatorParameters.Contains(entry.machineName)) continue;
                        syncedValues += 1;
                        break;
                }
            }
        }

        private void CreateAnimator()
        {
            if (_avatar.avatarSettings.baseController == null)
            {
                EditorUtility.DisplayDialog("Animator Error", "The Base Animator was not selected. No new Animator Controller was created.", "OK");
                return;
            }
            
            if (_avatar.avatarSettings.animator != null)
            {
                if (!EditorUtility.DisplayDialog("Animator already created",
                    "There is Animator already created for this avatar.", "Override", "Cancel"))
                {
                    return;
                }
            }
            
            string pathToCurrentFolder = "Assets/AdvancedSettings.Generated";
            if (!AssetDatabase.IsValidFolder(pathToCurrentFolder)) AssetDatabase.CreateFolder("Assets", "AdvancedSettings.Generated");

            var folderPath = pathToCurrentFolder + "/" + _avatar.name + "_AAS";
            if (!AssetDatabase.IsValidFolder(folderPath)) AssetDatabase.CreateFolder(pathToCurrentFolder, _avatar.name + "_AAS");
            var animatorPath = pathToCurrentFolder + "/" + _avatar.name + "_AAS/" + _avatar.name + "_aas.controller";
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(_avatar.avatarSettings.baseController.GetInstanceID()), animatorPath);
            
            _avatar.avatarSettings.animator = AssetDatabase.LoadAssetAtPath<AnimatorController>(animatorPath);

            animatorParameters.Clear();

            if (_avatar.avatarSettings.baseController != null)
            {
                var animator = (AnimatorController) _avatar.avatarSettings.baseController;

                foreach (var parameter in animator.parameters)
                {
                    if (parameter.type == AnimatorControllerParameterType.Float && parameter.name.Length > 0 &&
                        !coreParameters.Contains(parameter.name) && parameter.name.Substring(0, 1) != "#")
                    {
                        animatorParameters.Add(parameter.name);
                    }
                    if (parameter.type == AnimatorControllerParameterType.Int && parameter.name.Length > 0 &&
                        !coreParameters.Contains(parameter.name) && parameter.name.Substring(0, 1) != "#")
                    {
                        animatorParameters.Add(parameter.name);
                    }
                    if (parameter.type == AnimatorControllerParameterType.Bool && parameter.name.Length > 0 &&
                        !coreParameters.Contains(parameter.name) && parameter.name.Substring(0, 1) != "#")
                    {
                        animatorParameters.Add(parameter.name);
                    }
                }
            }
            
            foreach (var entry in _avatar.avatarSettings.settings)
            {
                switch (entry.type)
                {
                    default:
                        if (animatorParameters.Contains(entry.machineName)) continue;
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.MaterialColor:
                        if (animatorParameters.Contains(entry.machineName + "-r") || animatorParameters.Contains(entry.machineName + "-g") || animatorParameters.Contains(entry.machineName + "-b")) continue;
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.Joystick2D:
                    case CVRAdvancedSettingsEntry.SettingsType.InputVector2:
                        if (animatorParameters.Contains(entry.machineName + "-x") || animatorParameters.Contains(entry.machineName + "-y")) continue;
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.Joystick3D:
                    case CVRAdvancedSettingsEntry.SettingsType.InputVector3:
                        if (animatorParameters.Contains(entry.machineName + "-x") || animatorParameters.Contains(entry.machineName + "-y") || animatorParameters.Contains(entry.machineName + "-z")) continue;
                        break;
                }

                entry.setting.SetupAnimator(ref _avatar.avatarSettings.animator, entry.machineName, folderPath);
            }

            if (_avatar.avatarSettings.baseOverrideController != null)
            {
                var overridePath = pathToCurrentFolder + "/" + _avatar.name + "_AAS/" + _avatar.name + "_aas_overrides.overrideController";
                AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(_avatar.avatarSettings.baseOverrideController.GetInstanceID()), overridePath);
                _avatar.avatarSettings.overrides = AssetDatabase.LoadAssetAtPath<AnimatorOverrideController>(overridePath);
                _avatar.avatarSettings.overrides.runtimeAnimatorController = _avatar.avatarSettings.animator;
            }
            else
            {
                _avatar.avatarSettings.overrides = new AnimatorOverrideController(_avatar.avatarSettings.animator);
                AssetDatabase.CreateAsset(_avatar.avatarSettings.overrides, pathToCurrentFolder + "/" + _avatar.name + "_AAS/" + _avatar.name + "_aas_overrides.overrideController");
            }
            
            AssetDatabase.SaveAssets();
        }
        
        private float OnHeightElement(int index)
        {
            if (index > _avatar.avatarSettings.settings.Count) return EditorGUIUtility.singleLineHeight * 1f;
            entity = _avatar.avatarSettings.settings[index];

            // When collapsed only return one line height
            if (!entity.setting.isCollapsed) return EditorGUIUtility.singleLineHeight * 1.25f;

            switch (entity.type) 
            {
                case CVRAdvancedSettingsEntry.SettingsType.GameObjectToggle:
                {
                    var gameObjectToggle = (CVRAdvancesAvatarSettingGameObjectToggle) entity.setting;
                    if (gameObjectToggle == null || gameObjectToggle.gameObjectTargets == null)
                        return EditorGUIUtility.singleLineHeight * 11.50f;
                    float height = 10.50f;
                    if (gameObjectToggle.useAnimationClip) 
                    {
                        height -= 1.25f;
                    } 
                    else 
                    {
                        foreach (var target in gameObjectToggle.gameObjectTargets) 
                        {
                            if (!target.isCollapsed) 
                            {
                                height += 1.25f;
                            } 
                            else 
                            {
                                height += 3.75f;
                            }
                        }
                        if (gameObjectToggle.gameObjectTargets.Count == 0) 
                        {
                            height += 1f;
                        }
                    }
                    return EditorGUIUtility.singleLineHeight * height;
                }
                case CVRAdvancedSettingsEntry.SettingsType.GameObjectDropdown:
                {
                    
                    var gameObjectDropdown = (CVRAdvancesAvatarSettingGameObjectDropdown) entity.setting;
                    if (gameObjectDropdown == null || gameObjectDropdown.options == null)
                        return EditorGUIUtility.singleLineHeight * 9.25f;
                    float height = 8.25f;
                    foreach (var option in gameObjectDropdown.options) 
                    {
                        height += 1;
                        if (option.isCollapsed) 
                        {
                            height += 4;
                            if (option.useAnimationClip) 
                            {
                                height -= 1.5f;
                            } 
                            else 
                            {
                                if (option.gameObjectTargets.Count != 0) 
                                {
                                    foreach (var target in option.gameObjectTargets) 
                                    {
                                        if (!target.isCollapsed) 
                                        {
                                            height += 1;
                                        } 
                                        else 
                                        {
                                            height += 3;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return EditorGUIUtility.singleLineHeight * height * 1.25f;
                }
                case CVRAdvancedSettingsEntry.SettingsType.MaterialColor: 
                {
                    var materialColor = (CVRAdvancedAvatarSettingMaterialColor) entity.setting;
                    if (materialColor == null || materialColor.materialColorTargets == null)
                        return EditorGUIUtility.singleLineHeight * 8f;
                    float height = 8f;
                    foreach (var option in materialColor.materialColorTargets) 
                    {
                        if (!option.isCollapsed) 
                        {
                            height += 1.25f;
                        } 
                        else 
                        {
                            height += 3.75f;
                        }
                    }
                    if (materialColor.materialColorTargets.Count == 0) 
                    {
                        height += 1f;
                    }
                    return EditorGUIUtility.singleLineHeight * height;
                }
                case CVRAdvancedSettingsEntry.SettingsType.Slider: 
                {
                    var slider = (CVRAdvancesAvatarSettingSlider) entity.setting;
                    if (slider == null || slider.materialPropertyTargets == null)
                        return EditorGUIUtility.singleLineHeight * 8f;
                    float height = slider.useAnimationClip?8.75f:12.5f;
                    if (slider.useAnimationClip) return height * EditorGUIUtility.singleLineHeight;
                    foreach (var option in slider.materialPropertyTargets) 
                    {
                        if (!option.isCollapsed)
                        {
                            height += 1.25f;
                        } 
                        else 
                        {
                            height += 5 * 1.25f;
                        }
                    }
                    if (slider.materialPropertyTargets.Count == 0) 
                    {
                        height += 1.25f;
                    }
                    return EditorGUIUtility.singleLineHeight * height;
                }
                case CVRAdvancedSettingsEntry.SettingsType.Joystick2D:
                case CVRAdvancedSettingsEntry.SettingsType.Joystick3D:
                    return EditorGUIUtility.singleLineHeight * 11.25f;
                    break;
            }

            return EditorGUIUtility.singleLineHeight * 8.75f;
        }

        private void OnDrawHeader(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "Inputs");
        }

        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (index > _avatar.avatarSettings.settings.Count) return;
            entity = _avatar.avatarSettings.settings[index];
            rect.y += 2;
            rect.x += 12;
            rect.width -= 12;
            Rect _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            entity.setting.isCollapsed = EditorGUI.Foldout(_rect, entity.setting.isCollapsed, "Name", true);
            _rect.x += 100;
            _rect.width = rect.width - 100;
            entity.name = EditorGUI.TextField(_rect, entity.name);

            // when collapsed skip rest of UI drawing
            if (!entity.setting.isCollapsed)
            {
                entity.RunCollapsedSetup();
                return;
            }

            if (entity.name != null)
            {
                entity.machineName = Regex.Replace(entity.name, "[^a-zA-Z0-9#]", "");
            }

            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            if (entity.name == "" || entity.machineName == "")
            {
                _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 2f);
                EditorGUI.HelpBox(_rect, "Name cannot be empty", MessageType.Error);
                definitionContainsError = true;
                return;
            }
            if (entity.name == null || entity.machineName == null)
            {
                definitionContainsError = true;
            }
            
            EditorGUI.LabelField(_rect, "Parameter");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            
            Rect warningRect = new Rect(rect.x + 60, rect.y, 20, EditorGUIUtility.singleLineHeight);

            switch (entity.type)
            {
                case CVRAdvancedSettingsEntry.SettingsType.GameObjectToggle:
                    EditorGUI.LabelField(_rect, entity.machineName);
                    if (animatorParameters.Contains(entity.machineName))
                    {
                        EditorGUI.HelpBox(warningRect, "", MessageType.Warning);
                        EditorGUI.LabelField(warningRect, new GUIContent("", "This Layer already exists and will not be regenerated."));
                    }
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.GameObjectDropdown:
                    EditorGUI.LabelField(_rect, entity.machineName);
                    if (animatorParameters.Contains(entity.machineName))
                    {
                        EditorGUI.HelpBox(warningRect, "", MessageType.Warning);
                        EditorGUI.LabelField(warningRect, new GUIContent("", "This Layer already exists and will not be regenerated."));
                    }
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.MaterialColor:
                    EditorGUI.LabelField(_rect, $"{entity.machineName}-r, {entity.machineName}-g, {entity.machineName}-b");
                    if (animatorParameters.Contains(entity.machineName + "-r") || animatorParameters.Contains(entity.machineName + "-g") || animatorParameters.Contains(entity.machineName + "-b"))
                    {
                        EditorGUI.HelpBox(warningRect, "", MessageType.Warning);
                        EditorGUI.LabelField(warningRect, new GUIContent("", "This Layer already exists and will not be regenerated."));
                    }
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.Slider:
                    EditorGUI.LabelField(_rect, entity.machineName);
                    if (animatorParameters.Contains(entity.machineName))
                    {
                        EditorGUI.HelpBox(warningRect, "", MessageType.Warning);
                        EditorGUI.LabelField(warningRect, new GUIContent("", "This Layer already exists and will not be regenerated."));
                    }
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.Joystick2D:
                    EditorGUI.LabelField(_rect, $"{entity.machineName}-x, {entity.machineName}-y");
                    if (animatorParameters.Contains(entity.machineName + "-x") || animatorParameters.Contains(entity.machineName + "-y"))
                    {
                        EditorGUI.HelpBox(warningRect, "", MessageType.Warning);
                        EditorGUI.LabelField(warningRect, new GUIContent("", "This Layer already exists and will not be regenerated."));
                    }
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.Joystick3D:
                    EditorGUI.LabelField(_rect, $"{entity.machineName}-x, {entity.machineName}-y, {entity.machineName}-z");
                    if (animatorParameters.Contains(entity.machineName + "-x") || animatorParameters.Contains(entity.machineName + "-y") || animatorParameters.Contains(entity.machineName + "-z"))
                    {
                        EditorGUI.HelpBox(warningRect, "", MessageType.Warning);
                        EditorGUI.LabelField(warningRect, new GUIContent("", "This Layer already exists and will not be regenerated."));
                    }
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.InputSingle:
                    EditorGUI.LabelField(_rect, entity.machineName);
                    if (animatorParameters.Contains(entity.machineName))
                    {
                        EditorGUI.HelpBox(warningRect, "", MessageType.Warning);
                        EditorGUI.LabelField(warningRect, new GUIContent("", "This Layer already exists and will not be regenerated."));
                    }
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.InputVector2:
                    EditorGUI.LabelField(_rect, $"{entity.machineName}-x, {entity.machineName}-y");
                    if (animatorParameters.Contains(entity.machineName + "-x") || animatorParameters.Contains(entity.machineName + "-y"))
                    {
                        EditorGUI.HelpBox(warningRect, "", MessageType.Warning);
                        EditorGUI.LabelField(warningRect, new GUIContent("", "This Layer already exists and will not be regenerated."));
                    }
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.InputVector3:
                    EditorGUI.LabelField(_rect, $"{entity.machineName}-x, {entity.machineName}-y, {entity.machineName}-z");
                    if (animatorParameters.Contains(entity.machineName + "-x") || animatorParameters.Contains(entity.machineName + "-y") || animatorParameters.Contains(entity.machineName + "-z"))
                    {
                        EditorGUI.HelpBox(warningRect, "", MessageType.Warning);
                        EditorGUI.LabelField(warningRect, new GUIContent("", "This Layer already exists and will not be regenerated."));
                    }
                    break;
            }

            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Type");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            var type = (CVRAdvancedSettingsEntry.SettingsType) EditorGUI.EnumPopup(_rect, entity.type);

            if (type != entity.type)
            {
                entity.type = type;
                
                switch (type)
                {
                    case CVRAdvancedSettingsEntry.SettingsType.GameObjectToggle:
                        entity.setting = new CVRAdvancesAvatarSettingGameObjectToggle();
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.GameObjectDropdown:
                        entity.setting = new CVRAdvancesAvatarSettingGameObjectDropdown();
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.MaterialColor:
                        entity.setting = new CVRAdvancedAvatarSettingMaterialColor();
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.Slider:
                        entity.setting = new CVRAdvancesAvatarSettingSlider();
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.Joystick2D:
                        entity.setting = new CVRAdvancesAvatarSettingJoystick2D();
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.Joystick3D:
                        entity.setting = new CVRAdvancesAvatarSettingJoystick3D();
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.InputSingle:
                        entity.setting = new CVRAdvancesAvatarSettingInputSingle();
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.InputVector2:
                        entity.setting = new CVRAdvancesAvatarSettingInputVector2();
                        break;
                    case CVRAdvancedSettingsEntry.SettingsType.InputVector3:
                        entity.setting = new CVRAdvancesAvatarSettingInputVector3();
                        break;
                }
            }

            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            if (entity.type == CVRAdvancedSettingsEntry.SettingsType.GameObjectToggle)
            {
                EditorGUI.LabelField(_rect, "Generate Type");
                _rect.x += 100;
                _rect.width = rect.width - 100;
                entity.setting.usedType = (CVRAdvancesAvatarSettingBase.ParameterType) EditorGUI.EnumPopup(_rect, entity.setting.usedType);

                rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            }
            
            if (entity.type == CVRAdvancedSettingsEntry.SettingsType.GameObjectDropdown)
            {
                EditorGUI.LabelField(_rect, "Generate Type");
                _rect.x += 100;
                _rect.width = rect.width - 100;
                entity.setting.usedType = (CVRAdvancesAvatarSettingBase.ParameterType) EditorGUI.IntPopup(
                    _rect, 
                    (int) entity.setting.usedType, 
                    new string[]{"Generate Float", "Generate Int"},
                    new int[]{1, 2});

                rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            }
            
            switch (entity.type)
            {
                case CVRAdvancedSettingsEntry.SettingsType.GameObjectToggle:
                    
                    animatorParameters.Add(entity.machineName);
                    
                    var gameObjectToggle = (CVRAdvancesAvatarSettingGameObjectToggle) entity.setting;
                    
                    // Default State
                    EditorGUI.LabelField(_rect, "Default");
                    _rect.x += 100;
                    _rect.width = rect.width - 100;
                    gameObjectToggle.defaultValue = EditorGUI.Toggle(_rect, gameObjectToggle.defaultValue);

                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

                    // Use Animation Clip
                    EditorGUI.LabelField(_rect, "Use Animation");
                    _rect.x += 100;
                    _rect.width = rect.width - 100;
                    gameObjectToggle.useAnimationClip = EditorGUI.Toggle(_rect, gameObjectToggle.useAnimationClip);

                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    if (gameObjectToggle.useAnimationClip)
                    {
                        _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

                        // Animation Clip Slot
                        EditorGUI.LabelField(_rect, "Clip");
                        _rect.x += 100;
                        _rect.width = rect.width - 100;
                        gameObjectToggle.animationClip = (AnimationClip)EditorGUI.ObjectField(_rect, gameObjectToggle.animationClip, typeof(AnimationClip), true);
                    }
                    else
                    {
                        var gameObjectList = gameObjectToggle.GetReorderableList(_avatar);
                        gameObjectList.DoList(new Rect(rect.x, rect.y, rect.width, 20f));
                    }
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.GameObjectDropdown:

                    animatorParameters.Add(entity.machineName);
                    
                    var gameObjectDropdown = (CVRAdvancesAvatarSettingGameObjectDropdown) entity.setting;

                    EditorGUI.LabelField(_rect, "Default");
                    _rect.x += 100;
                    _rect.width = rect.width - 100;
                    gameObjectDropdown.defaultValue = EditorGUI.Popup(_rect, gameObjectDropdown.defaultValue, gameObjectDropdown.getOptionsList());
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    
                    var options = gameObjectDropdown.GetReorderableList(_avatar);
                    options.DoList(new Rect(rect.x, rect.y, rect.width, 20f));
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.MaterialColor:

                    animatorParameters.Add(entity.machineName + "-r");
                    animatorParameters.Add(entity.machineName + "-g");
                    animatorParameters.Add(entity.machineName + "-b");
                    
                    var materialColor = (CVRAdvancedAvatarSettingMaterialColor) entity.setting;
                    
                    EditorGUI.LabelField(_rect, "Default");
                    _rect.x += 100;
                    _rect.width = rect.width - 100;
                    materialColor.defaultValue = EditorGUI.ColorField(_rect, new GUIContent(), materialColor.defaultValue, true, false, false);
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    
                    var materialColorList = materialColor.GetReorderableList(_avatar);
                    materialColorList.DoList(new Rect(rect.x, rect.y, rect.width, 20f));
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.Slider:

                    animatorParameters.Add(entity.machineName);
                    
                    var slider = (CVRAdvancesAvatarSettingSlider) entity.setting;

                    EditorGUI.LabelField(_rect, "Default");
                    _rect.x += 100;
                    _rect.width = rect.width - 100;
                    slider.defaultValue = EditorGUI.Slider(_rect, slider.defaultValue, 0f, 1f);
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

                    // Use Animation Clip
                    EditorGUI.LabelField(_rect, "Use Animation");
                    _rect.x += 100;
                    _rect.width = rect.width - 100;
                    slider.useAnimationClip = EditorGUI.Toggle(_rect, slider.useAnimationClip);

                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    if (slider.useAnimationClip)
                    {
                        _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

                        //Min Animation Clip Slot
                        EditorGUI.LabelField(_rect, "Min Clip");
                        _rect.x += 100;
                        _rect.width = rect.width - 100;
                        slider.minAnimationClip = (AnimationClip)EditorGUI.ObjectField(_rect, slider.minAnimationClip, typeof(AnimationClip), true);
                        
                        rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                        _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

                        //Max Animation Clip Slot
                        EditorGUI.LabelField(_rect, "Max Clip");
                        _rect.x += 100;
                        _rect.width = rect.width - 100;
                        slider.maxAnimationClip = (AnimationClip)EditorGUI.ObjectField(_rect, slider.maxAnimationClip, typeof(AnimationClip), true);
                    }
                    else
                    {
                        var materialPropertyList = slider.GetReorderableList(_avatar);
                        materialPropertyList.DoList(new Rect(rect.x, rect.y, rect.width, 20f));
                    }

                    foreach (var target in slider.materialPropertyTargets)
                    {
                        rect.y += EditorGUIUtility.singleLineHeight * 1.25f * (!target.isCollapsed ? 1 : 5);
                    }
                    rect.y += EditorGUIUtility.singleLineHeight * (3f + (slider.materialPropertyTargets.Count == 0 ? 1.25f : 0));
                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);

                    if (!slider.useAnimationClip)
                    {
                        EditorGUI.HelpBox(_rect,
                            "The Setup Utility will help you create a slider for Material properties " +
                            "If you want to bind other properties you can edit the animation files generated " +
                            "by the System after the animator was created.", MessageType.Info);
                    }

                    break;
                case CVRAdvancedSettingsEntry.SettingsType.Joystick2D:

                    animatorParameters.Add(entity.machineName + "-x");
                    animatorParameters.Add(entity.machineName + "-y");

                    var joystick = (CVRAdvancesAvatarSettingJoystick2D) entity.setting;

                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);
                    joystick.defaultValue = EditorGUI.Vector2Field(_rect, "Default", joystick.defaultValue);

                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);
                    joystick.rangeMin = EditorGUI.Vector2Field(_rect, "Range Min", joystick.rangeMin);
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);
                    joystick.rangeMax = EditorGUI.Vector2Field(_rect, "Range Max", joystick.rangeMax);
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);
                    
                    EditorGUI.HelpBox(_rect, "This Settings does not provide a Setup Utility. "+
                                             "But it will create the necessary Animator Layers, Parameters and Animations. "+
                                             "So you can edit them to your liking after the animator was created.", MessageType.Info);
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.Joystick3D:

                    animatorParameters.Add(entity.machineName + "-x");
                    animatorParameters.Add(entity.machineName + "-y");
                    animatorParameters.Add(entity.machineName + "-z");

                    var joystick3D = (CVRAdvancesAvatarSettingJoystick3D) entity.setting;
                    
                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);
                    joystick3D.defaultValue = EditorGUI.Vector3Field(_rect, "Default", joystick3D.defaultValue);
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);
                    joystick3D.rangeMin = EditorGUI.Vector2Field(_rect, "Range Min", joystick3D.rangeMin);
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);
                    joystick3D.rangeMax = EditorGUI.Vector2Field(_rect, "Range Max", joystick3D.rangeMax);
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);
                    
                    EditorGUI.HelpBox(_rect, "This Settings does not provide a Setup Utility. "+
                                             "But it will create the necessary Animator Layers, Parameters and Animations. "+
                                             "So you can edit them to your liking after the animator was created.", MessageType.Info);
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.InputSingle:

                    animatorParameters.Add(entity.machineName);
                    
                    var inputSingle = (CVRAdvancesAvatarSettingInputSingle) entity.setting;

                    EditorGUI.LabelField(_rect, "Default");
                    _rect.x += 100;
                    _rect.width = rect.width - 100;
                    inputSingle.defaultValue = EditorGUI.FloatField(_rect, inputSingle.defaultValue);
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);
                    
                    EditorGUI.HelpBox(_rect, "This Settings does not provide a Setup Utility. "+
                                             "But it will create the necessary Animator Layers, Parameters and Animations. "+
                                             "So you can edit them to your liking after the animator was created.", MessageType.Info);
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.InputVector2:

                    animatorParameters.Add(entity.machineName + "-x");
                    animatorParameters.Add(entity.machineName + "-y");
                    
                    var inputVector2 = (CVRAdvancesAvatarSettingInputVector2) entity.setting;
                    
                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);
                    inputVector2.defaultValue = EditorGUI.Vector2Field(_rect, "Default", inputVector2.defaultValue);
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);
                    
                    EditorGUI.HelpBox(_rect, "This Settings does not provide a Setup Utility. "+
                                             "But it will create the necessary Animator Layers, Parameters and Animations. "+
                                             "So you can edit them to your liking after the animator was created.", MessageType.Info);
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.InputVector3:

                    animatorParameters.Add(entity.machineName + "-x");
                    animatorParameters.Add(entity.machineName + "-y");
                    animatorParameters.Add(entity.machineName + "-z");
                    
                    var inputVector3 = (CVRAdvancesAvatarSettingInputVector3) entity.setting;
                    
                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);
                    inputVector3.defaultValue = EditorGUI.Vector3Field(_rect, "Default", inputVector3.defaultValue);
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 3);
                    
                    EditorGUI.HelpBox(_rect, "This Settings does not provide a Setup Utility. "+
                                             "But it will create the necessary Animator Layers, Parameters and Animations. "+
                                             "So you can edit them to your liking after the animator was created.", MessageType.Info);
                    break;
            }
        }
        
        private void OnAdd(ReorderableList list)
        {
            _avatar.avatarSettings.settings.Add(new CVRAdvancedSettingsEntry());
            Repaint();
        }
   
        private void OnChanged(ReorderableList list)
        {
            //EditorUtility.SetDirty(target);
        }

        private float OnHeightElementTagging(int index)
        {
            if (index > _avatar.advancedTaggingList.Count) return EditorGUIUtility.singleLineHeight * 2.5f;
            
            return EditorGUIUtility.singleLineHeight * 
                   ((_avatar.advancedTaggingList[index].fallbackGameObject != null && 
                     _avatar.advancedTaggingList[index].fallbackGameObject.activeSelf) ? 5f : 3.75f);
        }

        private void OnDrawHeaderTagging(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "Tagged Gameobjects");
        }

        private void OnDrawElementTagging(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (index > _avatar.advancedTaggingList.Count) return;
            tagEntry = _avatar.advancedTaggingList[index];
            rect.y += 2;
            rect.x += 12;
            rect.width -= 12;
            Rect _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Tags");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            tagEntry.tags = (CVRAvatarAdvancedTaggingEntry.Tags) EditorGUI.EnumFlagsField(_rect, tagEntry.tags);

            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "GameObject");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            tagEntry.gameObject = (GameObject) EditorGUI.ObjectField(_rect, tagEntry.gameObject, typeof(GameObject), true);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Fallback GO");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            tagEntry.fallbackGameObject = (GameObject) EditorGUI.ObjectField(_rect, tagEntry.fallbackGameObject, typeof(GameObject), true);

            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
            
            if (tagEntry.fallbackGameObject != null && tagEntry.fallbackGameObject.activeSelf)
            {
                EditorGUI.HelpBox(_rect, "The Fallback needs to be disabled by default!", MessageType.Error);
            }
        }
        
        private void OnAddTagging(ReorderableList list)
        {
            _avatar.advancedTaggingList.Add(new CVRAvatarAdvancedTaggingEntry());
            Repaint();
        }
   
        private void OnChangedTagging(ReorderableList list)
        {
            EditorUtility.SetDirty(target);
        }
        
        private static void CreateAvatarSettings(CVRAvatar avatar)
        {
            string[] guids = AssetDatabase.FindAssets("AvatarAnimator t:animatorController", null);

            if (guids.Length < 1)
            {
                Debug.LogError("No Animator controller with the name \"AvatarAnimator\" was found. Please make sure that you CCK is installed properly.");
                return;
            }
            
            Type projectWindowUtilType = typeof(ProjectWindowUtil);
            MethodInfo getActiveFolderPath = projectWindowUtilType.GetMethod("GetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);
            object obj = getActiveFolderPath.Invoke(null, new object[0]);
            string pathToCurrentFolder = obj.ToString();
            
            avatar.avatarSettings = new CVRAdvancedAvatarSettings();
            avatar.avatarSettings.baseController = AssetDatabase.LoadAssetAtPath<AnimatorController>(AssetDatabase.GUIDToAssetPath(guids[0]));
            avatar.avatarSettings.settings = new List<CVRAdvancedSettingsEntry>();
            avatar.avatarSettings.initialized = true;
        }

        protected virtual void OnSceneGUI()
        {
            if (_avatar == null) _avatar = (CVRAvatar) target;
            if (_avatar != null)
            {
                var avatarTransform = _avatar.transform;
                var scale = avatarTransform.localScale;
                var inverseScale = new Vector3(1 / scale.x, 1 / scale.y, 1 / scale.z);

                //View Position
                GUIStyle style = new GUIStyle();
                style.normal.textColor = Color.green;
                style.fontSize = 20;
                Handles.BeginGUI();
                Vector3 pos = avatarTransform.TransformPoint(Vector3.Scale(_avatar.viewPosition, inverseScale));
                Vector2 pos2D = HandleUtility.WorldToGUIPoint(pos);
                GUI.Label(new Rect(pos2D.x + 20, pos2D.y - 10, 100, 20), "View Position", style);
                Handles.EndGUI();

                EditorGUI.BeginChangeCheck();
                Vector3 viewPos = Handles.PositionHandle(pos, avatarTransform.rotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(_avatar, "CVR View Position Change");
                    _avatar.viewPosition = Vector3.Scale(avatarTransform.InverseTransformPoint(viewPos), scale);
                }

                //Voice Position
                style.normal.textColor = Color.red;
                Handles.BeginGUI();
                pos = avatarTransform.TransformPoint(Vector3.Scale(_avatar.voicePosition, inverseScale));
                pos2D = HandleUtility.WorldToGUIPoint(pos);
                GUI.Label(new Rect(pos2D.x + 20, pos2D.y - 10, 100, 20), "Voice Position", style);
                Handles.EndGUI();

                EditorGUI.BeginChangeCheck();
                Vector3 voicePos = Handles.PositionHandle(pos, avatarTransform.rotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(_avatar, "CVR Voice Position Change");
                    _avatar.voicePosition = Vector3.Scale(avatarTransform.InverseTransformPoint(voicePos), scale);
                }
            }
        }
        
        void GetBlendShapeNames()
        {
            if (_avatar.bodyMesh != null)
            {
                _blendShapeNames = new List<string>();
                _blendShapeNames.Add("-none-");
                for (int i = 0; i < _avatar.bodyMesh.sharedMesh.blendShapeCount; ++i)
                    _blendShapeNames.Add(_avatar.bodyMesh.sharedMesh.GetBlendShapeName(i));
            }
            else
            {
                _blendShapeNames = new List<string>();
                _blendShapeNames.Add("-none-");
            }
        }

        void FindVisemes()
        {
            for (int i = 0; i < _visemeNames.Length; i++)
            {
                for (int j = 0; j < _blendShapeNames.Count; ++j)
                {
                    if (_blendShapeNames[j].ToLower().Contains("v_" + _visemeNames[i].ToLower()) ||
                        _blendShapeNames[j].ToLower().Contains("viseme_" + _visemeNames[i].ToLower()))
                    {
                        _avatar.visemeBlendshapes[i] = _blendShapeNames[j];
                    }
                }
            }
        }
    }
}