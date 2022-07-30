using System.Collections.Generic;
using ABI.CCK.Components;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using AnimatorControllerParameterType = UnityEngine.AnimatorControllerParameterType;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRSpawnable), true)]
    public class CCK_CVRSpawnableEditor : UnityEditor.Editor
    {
        private CVRSpawnable _spawnable;
        private ReorderableList reorderableList;
        private ReorderableList subSyncList;
        private CVRSpawnableValue entity;
        private CVRSpawnableSubSync subSyncEntity;

        private float SubSyncCosts = 0f;
        private bool outOfBoundsError = false;
        
        private void InitializeList()
        {
            if (!_spawnable.useAdditionalValues) return;
            
            reorderableList = new ReorderableList(_spawnable.syncValues, typeof(CVRAdvancedSettingsEntry), true, true, true, true);
            reorderableList.drawHeaderCallback = OnDrawHeader;
            reorderableList.drawElementCallback = OnDrawElement;
            reorderableList.elementHeightCallback = OnHeightElement;
            reorderableList.onAddCallback = OnAdd;
            reorderableList.onChangedCallback = OnChanged; 
        }
        
        private void InitializeSubSyncList()
        {
            if (!_spawnable.useAdditionalValues) return;
            
            subSyncList = new ReorderableList(_spawnable.subSyncs, typeof(CVRSpawnableSubSync), false, true, true, true);
            subSyncList.drawHeaderCallback = OnDrawHeaderSubSync;
            subSyncList.drawElementCallback = OnDrawElementSubSync;
            subSyncList.elementHeightCallback = OnHeightElementSubSync;
            subSyncList.onAddCallback = OnAddSubSync;
            subSyncList.onChangedCallback = OnChangedSubSync;
        }

        private void OnDrawHeaderSubSync(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "Sub Sync Transforms");
        }

        private void OnDrawElementSubSync(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index > _spawnable.subSyncs.Count) return;
            subSyncEntity = _spawnable.subSyncs[index];

            rect.y += 2;
            Rect _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Transform");
            _rect.x += 120;
            _rect.width = rect.width - 120;
            subSyncEntity.transform = (Transform) EditorGUI.ObjectField(_rect, subSyncEntity.transform, typeof(Transform));
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Synced Properties");
            _rect.x += 120;
            _rect.width = rect.width - 120;
            subSyncEntity.syncedValues = (CVRSpawnableSubSync.SyncFlags) EditorGUI.EnumFlagsField(_rect, subSyncEntity.syncedValues);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Sync Precision");
            _rect.x += 120;
            _rect.width = rect.width - 120;
            subSyncEntity.precision = (CVRSpawnableSubSync.SyncPrecision) EditorGUI.EnumPopup(_rect, subSyncEntity.precision);

            if (subSyncEntity.precision == CVRSpawnableSubSync.SyncPrecision.Full) return;
            
            if (subSyncEntity.transform != null &&
                (Mathf.Abs(subSyncEntity.transform.localPosition.x) > subSyncEntity.syncBoundary ||
                 Mathf.Abs(subSyncEntity.transform.localPosition.y) > subSyncEntity.syncBoundary ||
                 Mathf.Abs(subSyncEntity.transform.localPosition.z) > subSyncEntity.syncBoundary))
            {
                outOfBoundsError = true;
            }
                
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);
        
            EditorGUI.LabelField(_rect, "Sync Boundary");
            _rect.x += 120;
            _rect.width = rect.width - 120;
            subSyncEntity.syncBoundary = EditorGUI.FloatField(_rect, subSyncEntity.syncBoundary);
        }

        private float OnHeightElementSubSync(int index)
        {
            if (_spawnable.subSyncs[index].precision == CVRSpawnableSubSync.SyncPrecision.Full) return EditorGUIUtility.singleLineHeight * 3.75f;
            return EditorGUIUtility.singleLineHeight * 5f;
        }

        private void OnAddSubSync(ReorderableList list)
        {
            if (_spawnable.subSyncs.Count < 40)
            {
                _spawnable.subSyncs.Add(new CVRSpawnableSubSync());
                Repaint();
            }
        }

        private void OnChangedSubSync(ReorderableList list)
        {
            //EditorUtility.SetDirty(target);
        }

        private void OnChanged(ReorderableList list)
        {
            //EditorUtility.SetDirty(target);
        }

        private void OnAdd(ReorderableList list)
        {
            if (_spawnable.syncValues.Count < 40)
            {
                _spawnable.syncValues.Add(new CVRSpawnableValue());
                Repaint();
            }
        }

        private float OnHeightElement(int index)
        {
            return EditorGUIUtility.singleLineHeight * 7.5f;
        }

        private void OnDrawElement(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index > _spawnable.syncValues.Count) return;
            entity = _spawnable.syncValues[index];
            
            rect.y += 2;
            Rect _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Name");
            _rect.x += 120;
            _rect.width = rect.width - 120;
            entity.name = EditorGUI.TextField(_rect, entity.name);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Start Value");
            _rect.x += 120;
            _rect.width = rect.width - 120;
            entity.startValue = EditorGUI.FloatField(_rect, entity.startValue);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Update Type");
            _rect.x += 120;
            _rect.width = rect.width - 120;
            entity.updatedBy = (CVRSpawnableValue.UpdatedBy) EditorGUI.EnumPopup(_rect, entity.updatedBy);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Update Method");
            _rect.x += 120;
            _rect.width = rect.width - 120;
            entity.updateMethod = (CVRSpawnableValue.UpdateMethod) EditorGUI.EnumPopup(_rect, entity.updateMethod);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Connected Animator");
            _rect.x += 120;
            _rect.width = rect.width - 120;
            entity.animator = (Animator) EditorGUI.ObjectField(_rect, entity.animator, typeof(Animator));
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Animator Parameter");
            _rect.x += 120;
            _rect.width = rect.width - 120;

            var parameters = new List<string>();
            parameters.Add("-none-");
            var parameterIndex = 0;
            if (entity.animator != null)
            {
                if (entity.animator.runtimeAnimatorController != null)
                {
                    foreach (var parameter in ((UnityEditor.Animations.AnimatorController) entity.animator.runtimeAnimatorController).parameters)
                    {
                        if (parameter.type == AnimatorControllerParameterType.Float)
                        {
                            parameters.Add(parameter.name);
                        }
                    }

                    parameterIndex = parameters.FindIndex(match => match == entity.animatorParameterName);
                }
            }

            if (parameterIndex < 0) parameterIndex = 0;
            
            parameterIndex = EditorGUI.Popup(_rect, parameterIndex, parameters.ToArray());
            entity.animatorParameterName = parameters[parameterIndex];
        }

        private void OnDrawHeader(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "Values");
        }

        public void UpdateSubSyncCosts()
        {
            SubSyncCosts = 0f;
            
            foreach (var subSync in _spawnable.subSyncs)
            {
                if (subSync.syncedValues.HasFlag(CVRSpawnableSubSync.SyncFlags.TransformX))
                    SubSyncCosts += (int) subSync.precision / 4f;
                if (subSync.syncedValues.HasFlag(CVRSpawnableSubSync.SyncFlags.TransformY))
                    SubSyncCosts += (int) subSync.precision / 4f;
                if (subSync.syncedValues.HasFlag(CVRSpawnableSubSync.SyncFlags.TransformZ))
                    SubSyncCosts += (int) subSync.precision / 4f;
                if (subSync.syncedValues.HasFlag(CVRSpawnableSubSync.SyncFlags.RotationX))
                    SubSyncCosts += (int) subSync.precision / 4f;
                if (subSync.syncedValues.HasFlag(CVRSpawnableSubSync.SyncFlags.RotationY))
                    SubSyncCosts += (int) subSync.precision / 4f;
                if (subSync.syncedValues.HasFlag(CVRSpawnableSubSync.SyncFlags.RotationZ))
                    SubSyncCosts += (int) subSync.precision / 4f;
            }
        }
        
        public override void OnInspectorGUI()
        {
            if (_spawnable == null) _spawnable = (CVRSpawnable) target;

            EditorGUI.BeginChangeCheck();
            
            _spawnable.spawnHeight = EditorGUILayout.FloatField("Spawn Height", _spawnable.spawnHeight);

            _spawnable.propPrivacy = (CVRSpawnable.PropPrivacy) EditorGUILayout.EnumPopup("Prop Usage", _spawnable.propPrivacy);
            
            GUILayout.BeginVertical("HelpBox");
            
            GUILayout.BeginHorizontal ();
            _spawnable.useAdditionalValues = EditorGUILayout.Toggle (_spawnable.useAdditionalValues, GUILayout.Width(16));
            EditorGUILayout.LabelField ("Enable Sync Values", GUILayout.Width(250));
            GUILayout.EndHorizontal ();

            if (_spawnable.useAdditionalValues)
            {
                UpdateSubSyncCosts();
                
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical("GroupBox");

                var SyncCost = _spawnable.syncValues.Count + SubSyncCosts;
                
                Rect _rect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight);
                EditorGUI.ProgressBar(_rect, SyncCost / 40f, Mathf.CeilToInt(SyncCost) + " of 40 Synced Parameter Slots used");
                EditorGUILayout.Space();

                if (SubSyncCosts > 0f)
                {
                    EditorGUILayout.HelpBox(SubSyncCosts.ToString("F2")+" Values are used for Sub Sync Transforms", MessageType.Info);
                    EditorGUILayout.Space();
                }
                
                if (reorderableList == null) InitializeList();
                reorderableList.displayAdd = SyncCost <= 39f;
                reorderableList.DoLayoutList();
                
                EditorGUILayout.Space();

                outOfBoundsError = false;
                if (subSyncList == null) InitializeSubSyncList();
                subSyncList.displayAdd = SyncCost <= 39f;
                subSyncList.DoLayoutList();
                
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
            
            GUILayout.EndVertical();

            if (outOfBoundsError)
            {
                EditorGUILayout.HelpBox("A Sub Sync Transform is out of bounds by default. This object will snap to its bounds, when it is being synced.", MessageType.Error);
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(_spawnable);
            }
        }
    }
}