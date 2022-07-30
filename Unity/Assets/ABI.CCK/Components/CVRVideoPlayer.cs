using System;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ABI.CCK.Components
{
    public class CVRVideoPlayer : MonoBehaviour
    {
        public enum AudioMode
        {
            // Direct2D in CCK = 1
            Direct = 2,
            AudioSource = 3,
            RoomScale = 4,
        }
        
        [HideInInspector]
        public string playerId;
        
        public AudioMode audioPlaybackMode = AudioMode.Direct;
        public AudioSource customAudioSource;
        public List<VideoPlayerAudioSource> roomScaleAudioSources;
        [Range(0,1)] public float playbackVolume = 1f;
        public bool syncEnabled = true;
        [Range(0.5f,2)] public float localPlaybackSpeed = 1.0f;
        public CVRVideoPlayerPlaylistEntity playOnAwakeObject;

        public RenderTexture ProjectionTexture;
        public bool interactiveUI = true;
        public bool autoplay;
        public List<Text> subtitleTextComponents;

        public List<CVRVideoPlayerPlaylist> entities = new List<CVRVideoPlayerPlaylist>();
        
        [SerializeField]
        public UnityEvent startedPlayback; 
        [SerializeField]
        public UnityEvent finishedPlayback; 
        [SerializeField]
        public UnityEvent pausedPlayback; 
        [SerializeField]
        public UnityEvent setUrl;

        public Transform videoPlayerUIPosition;
        
        private void OnDrawGizmos()
        {
            if (videoPlayerUIPosition == null || !interactiveUI) return;
            
            Gizmos.color = Color.white;
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(videoPlayerUIPosition.position, videoPlayerUIPosition.rotation, videoPlayerUIPosition.lossyScale);
            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(new Vector3(0, 0.62f, 0), new Vector3(2.22f, 1.24f, 0f));
            Gizmos.DrawLine(new Vector3(-1.11f, 1.11f, 0f), new Vector3(1.11f, 1.11f, 0f));
            Gizmos.DrawLine(new Vector3(-0.78f, 1.24f, 0f), new Vector3(-0.78f, 1.11f, 0f));
            
            Gizmos.DrawLine(new Vector3(-0.3f, 0.75f, 0f), new Vector3(-0.3f, 0.25f, 0f));
            Gizmos.DrawLine(new Vector3(-0.3f, 0.75f, 0f), new Vector3(0.3f, 0.5f, 0f));
            Gizmos.DrawLine(new Vector3(-0.3f, 0.25f, 0f), new Vector3(0.3f, 0.5f, 0f));
            
            var scale = videoPlayerUIPosition.lossyScale;
            scale.Scale(new Vector3(1f, 1f, 0f));
            rotationMatrix = Matrix4x4.TRS(videoPlayerUIPosition.position, videoPlayerUIPosition.rotation, scale);
            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireSphere(new Vector3(1.04f, 1.174f, 0), 0.05f);
            Gizmos.DrawWireSphere(new Vector3(0.94f, 1.174f, 0), 0.05f);
            Gizmos.DrawWireSphere(new Vector3(0.84f, 1.174f, 0), 0.05f);
            Gizmos.DrawWireSphere(new Vector3(0.74f, 1.174f, 0), 0.05f);
            Gizmos.DrawWireSphere(new Vector3(0.64f, 1.174f, 0), 0.05f);
        }

        public void Play() {}
        public void Pause() {}
        public void Previous() {}
        public void Next() {}
        public void SetUrl(string url) {}
        public void SetNetworkSync(bool sync) {}
        public void SetAudioMode(AudioMode mode) {}
        public void SetAudioMode(int mode) => SetAudioMode((AudioMode)mode);

    }
    
    public enum AudioChannelMaskFlags : int
    {
        FrontLeft 			= 0x1,
        FrontRight 			= 0x2,
        FrontCenter 		= 0x4,
        LowFrequency 		= 0x8,
        BackLeft 			= 0x10,
        BackRight 			= 0x20,
        SideLeft 			= 0x200,
        SideRight 			= 0x400,
    }

    [Serializable]
    public class VideoPlayerAudioSource
    {
        public AudioChannelMaskFlags type;
        public AudioSource audioSource;
    }

    [System.Serializable]
    public class CVRVideoPlayerPlaylist
    {
        public string playlistThumbnailUrl;
        public string playlistTitle;
        public List<CVRVideoPlayerPlaylistEntity> playlistVideos = new List<CVRVideoPlayerPlaylistEntity>();
        
        #if UNITY_EDITOR
        private CVRVideoPlayerPlaylistEntity entity;
        public ReorderableList list;
        public bool isCollapsed;
        private CVRVideoPlayer videoPlayer;

        public void OnDrawHeader(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "Playlist - Videos");
        }

        public void OnDrawElement(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index > playlistVideos.Count) return;
            entity = playlistVideos[index];
            
            rect.y += 2;
            rect.x += 12;
            rect.width -= 12;
            Rect _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.BeginChangeCheck();
            bool isCollapsed = EditorGUI.Foldout(_rect, entity.isCollapsed, "Title", true);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(videoPlayer, "Video Expand");
                entity.isCollapsed = isCollapsed;
            }
            
            _rect.x += 80;
            _rect.width = rect.width - 80;
            
            EditorGUI.BeginChangeCheck();
            string videoTitle = EditorGUI.TextField(_rect, entity.videoTitle);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(videoPlayer, "Video Title");
                entity.videoTitle = videoTitle;
            }
            
            if (!entity.isCollapsed) return;
        
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Video Url");
            _rect.x += 80;
            _rect.width = rect.width - 80;
            
            EditorGUI.BeginChangeCheck();
            string videoUrl = EditorGUI.TextField(_rect, entity.videoUrl);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(videoPlayer, "Video Url");
                entity.videoUrl = videoUrl;
            }
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
        
            EditorGUI.LabelField(_rect, "Thumbnail Url");
            _rect.x += 80;
            _rect.width = rect.width - 80;
            
            EditorGUI.BeginChangeCheck();
            string thumbnailUrl = EditorGUI.TextField(_rect, entity.thumbnailUrl);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(videoPlayer, "Video Thumbnail Url");
                entity.thumbnailUrl = thumbnailUrl;
            }
        
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Start");
            _rect.x += 80;
            _rect.width = rect.width - 80;
            
            EditorGUI.BeginChangeCheck();
            int introEndInSeconds = EditorGUI.IntField(_rect, entity.introEndInSeconds);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(videoPlayer, "Video Intro End");
                entity.introEndInSeconds = introEndInSeconds;
            }
        
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "End");
            _rect.x += 80;
            _rect.width = rect.width - 80;
            
            EditorGUI.BeginChangeCheck();
            int creditsStartInSeconds = EditorGUI.IntField(_rect, entity.creditsStartInSeconds);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(videoPlayer, "Video Credits Start");
                entity.creditsStartInSeconds = creditsStartInSeconds;
            }
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            if (GUI.Button(_rect, "Set as Play On Awake Object"))
            {
                videoPlayer.playOnAwakeObject = entity;
            }
        }

        public float OnHeightElement(int index)
        {
            if (index > playlistVideos.Count) return EditorGUIUtility.singleLineHeight * 1.25f;
            entity = playlistVideos[index];
            
            if(!entity.isCollapsed) return EditorGUIUtility.singleLineHeight * 1.25f;
            
            return EditorGUIUtility.singleLineHeight * 7.5f;
        }

        public void OnAdd(ReorderableList reorderableList)
        {
            Undo.RecordObject(videoPlayer, "Add Video Entry");
            playlistVideos.Add(null);
        }

        public void OnChanged(ReorderableList reorderableList)
        {
            Undo.RecordObject(videoPlayer, "Video List changed");
        }

        public ReorderableList GetReorderableList(CVRVideoPlayer player)
        {
            videoPlayer = player;
            
            if (list == null)
            {
                list = new ReorderableList(playlistVideos, typeof(CVRVideoPlayerPlaylistEntity), true, true, true,
                    true);
                list.drawHeaderCallback = OnDrawHeader;
                list.drawElementCallback = OnDrawElement;
                list.elementHeightCallback = OnHeightElement;
                list.onAddCallback = OnAdd;
                list.onChangedCallback = OnChanged;
            }

            return list;
        }
        #endif
    } 
    
    [System.Serializable]
    public class CVRVideoPlayerPlaylistEntity
    {
        public string videoUrl;
        public string videoTitle;
        public int introEndInSeconds;
        public int creditsStartInSeconds;
        public string thumbnailUrl;
        public bool isCollapsed;
    }
    
}