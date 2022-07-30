using ABI.CCK.Components;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


[CustomEditor(typeof(CVRVideoPlayer))]
public class CCK_CVR_VideoPlayerEditor : Editor
{
    private ReorderableList _reorderableList;
    private CVRVideoPlayer _player;
    private static bool _showGeneral = true;
    private static bool _showAudio = true;
    private static bool _showPlaylists = true;
    private static bool _showEvents = true;

    private const string TypeLabel = "Playlists";

    private void OnEnable()
    {
        if (_player == null) _player = (CVRVideoPlayer)target;

        _reorderableList = new ReorderableList(_player.entities, typeof(CVRVideoPlayerPlaylist), true, true, true, true);
        _reorderableList.drawHeaderCallback = OnDrawHeader;
        _reorderableList.drawElementCallback = OnDrawElement;
        _reorderableList.elementHeightCallback = OnHeightElement;
        _reorderableList.onAddCallback = OnAdd;
        _reorderableList.onChangedCallback = OnChanged;
    }

    private float OnHeightElement(int index)
    {
        var height = 0f;

        if (!_player.entities[index].isCollapsed) return EditorGUIUtility.singleLineHeight * 1.25f;

        height += EditorGUIUtility.singleLineHeight * (3f + 2.5f);

        if (_player.entities[index].playlistVideos.Count == 0) height += 1.25f * EditorGUIUtility.singleLineHeight;

        foreach (var entry in _player.entities[index].playlistVideos)
        {
            if (entry == null)
            {
                height += 1.25f * EditorGUIUtility.singleLineHeight;
            }
            else
            {
                height += (entry.isCollapsed ? 7.5f : 1.25f) * EditorGUIUtility.singleLineHeight;
            }
        }

        return height;
    }

    private void OnDrawHeader(Rect rect)
    {
        Rect rectA = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

        GUI.Label(rectA, TypeLabel);
    }

    private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        if (index > _player.entities.Count) return;

        rect.y += 2;
        rect.x += 12;
        rect.width -= 12;
        Rect rectA = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

        EditorGUI.BeginChangeCheck();

        bool collapse = EditorGUI.Foldout(rectA, _player.entities[index].isCollapsed, "Playlist Title", true);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Playlist Expand");
            _player.entities[index].isCollapsed = collapse;
        }

        rectA.x += 80;
        rectA.width = rect.width - 80;

        EditorGUI.BeginChangeCheck();

        string playlistTitle = EditorGUI.TextField(rectA, _player.entities[index].playlistTitle);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Playlist Title");
            _player.entities[index].playlistTitle = playlistTitle;
        }

        if (!_player.entities[index].isCollapsed) return;

        rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
        rectA = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

        EditorGUI.LabelField(rectA, "Thumbnail Url");
        rectA.x += 80;
        rectA.width = rect.width - 80;

        EditorGUI.BeginChangeCheck();

        string playlistThumbnailUrl = EditorGUI.TextField(rectA, _player.entities[index].playlistThumbnailUrl);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Playlist Thumbnail Url");
            _player.entities[index].playlistThumbnailUrl = playlistThumbnailUrl;
        }

        rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
        //_rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

        var videoList = _player.entities[index].GetReorderableList(_player);
        videoList.DoList(new Rect(rect.x, rect.y, rect.width, 20f));
    }

    private void OnAdd(ReorderableList list)
    {
        Undo.RecordObject(target, "Add Playlist Entry");
        _player.entities.Add(null);
    }

    private void OnChanged(ReorderableList list)
    {
        Undo.RecordObject(target, "Playlist List changed");
    }

    public override void OnInspectorGUI()
    {
        #region General settings

        _showGeneral = EditorGUILayout.BeginFoldoutHeaderGroup(_showGeneral, "General");
        if (_showGeneral)
        {
            _player.syncEnabled = EditorGUILayout.Toggle("Network Sync", _player.syncEnabled);
            _player.autoplay = EditorGUILayout.Toggle("Play On Awake", _player.autoplay);
            _player.interactiveUI = EditorGUILayout.Toggle("Use Interactive Library UI", _player.interactiveUI);
            _player.videoPlayerUIPosition = (Transform)EditorGUILayout.ObjectField("UI Position/Parent", _player.videoPlayerUIPosition, typeof(Transform), true);
            _player.localPlaybackSpeed = EditorGUILayout.Slider("Playback Speed", _player.localPlaybackSpeed, 0.5f, 2.0f);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            _player.ProjectionTexture = (RenderTexture)EditorGUILayout.ObjectField("Projection Texture", _player.ProjectionTexture, typeof(RenderTexture), true);
            EditorGUILayout.Space();
            
            if (_player.ProjectionTexture == null)
            {
                EditorGUILayout.HelpBox("The video player output texture is empty, please fill it or no video will be drawn.", MessageType.Warning);
                if (GUILayout.Button("Create Sample Render Texture"))
                {
                    RenderTexture tex = new RenderTexture(1920, 1080, 24);
                    if (!AssetDatabase.IsValidFolder("Assets/ABI.Generated"))
                        AssetDatabase.CreateFolder("Assets", "ABI.Generated");
                    if (!AssetDatabase.IsValidFolder("Assets/ABI.Generated/VideoPlayer"))
                        AssetDatabase.CreateFolder("Assets/ABI.Generated", "VideoPlayer");
                    if (!AssetDatabase.IsValidFolder("Assets/ABI.Generated/VideoPlayer/RenderTextures"))
                        AssetDatabase.CreateFolder("Assets/ABI.Generated/VideoPlayer", "RenderTextures");
                    AssetDatabase.CreateAsset(tex,
                        "Assets/ABI.Generated/VideoPlayer/RenderTextures/" + _player.gameObject.GetInstanceID() +
                        ".renderTexture");
                    _player.ProjectionTexture = tex;
                }
            }
            
            EditorGUILayout.Space();
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        #endregion

        #region Audio settings

        _showAudio = EditorGUILayout.BeginFoldoutHeaderGroup(_showAudio, "Audio");
        
        if (_showAudio)
        {
            _player.playbackVolume = EditorGUILayout.Slider("Playback Volume", _player.playbackVolume, 0.0f, 1.0f);
            _player.audioPlaybackMode = (CVRVideoPlayer.AudioMode)EditorGUILayout.EnumPopup("Audio Playback Mode ", _player.audioPlaybackMode);
            _player.customAudioSource = (AudioSource)EditorGUILayout.ObjectField("Custom Audio Source", _player.customAudioSource, typeof(AudioSource), true);

            var list = serializedObject.FindProperty("roomScaleAudioSources");
            EditorGUILayout.PropertyField(list, new GUIContent("Room Scale Audio Sources"), true);
            serializedObject.ApplyModifiedProperties();
            
            EditorGUILayout.Space();
        }
        
        EditorGUILayout.EndFoldoutHeaderGroup();

        #endregion

        #region Playlists

        _showPlaylists = EditorGUILayout.BeginFoldoutHeaderGroup(_showPlaylists, "Playlists");

        if (_showPlaylists)
        {
            EditorGUILayout.LabelField(new GUIContent("Play On Awake Object", "Default video to play on start/awake"), new GUIContent(_player.playOnAwakeObject?.videoTitle));
            if (GUILayout.Button("Remove Play on Awake Object")) _player.playOnAwakeObject = null;
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            _reorderableList.DoLayoutList();
            EditorGUILayout.Space();
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        #endregion

        #region Events

        _showEvents = EditorGUILayout.BeginFoldoutHeaderGroup(_showEvents, "Events");

        if (_showEvents)
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("startedPlayback"), true);
            serializedObject.ApplyModifiedProperties();

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("finishedPlayback"), true);
            serializedObject.ApplyModifiedProperties();

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("pausedPlayback"), true);
            serializedObject.ApplyModifiedProperties();

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("setUrl"), true);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space();
        }
        
        EditorGUILayout.EndFoldoutHeaderGroup();

        #endregion
    }
}