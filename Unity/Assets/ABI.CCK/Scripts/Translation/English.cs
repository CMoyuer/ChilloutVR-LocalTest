using System.Collections.Generic;

namespace ABI.CCK.Scripts.Translation
{
    public class English
    {
        public static readonly Dictionary<string, string> Localization = new Dictionary<string, string>()
        {
            {"ABI_UI_BUILDPANEL_HEADING_BUILDER", "Content Builder"},
            {"ABI_UI_BUILDPANEL_HEADING_SETTINGS", "Settings & Options"},
            {"ABI_UI_BUILDPANEL_HEADING_DOCUMENTATION", "View our documentation"},
            {"ABI_UI_BUILDPANEL_HEADING_FEEDBACK", "Submit feedback"},
            {"ABI_UI_BUILDPANEL_HEADING_FOUNDCONTENT", "Content found in active scene"},
            {"ABI_UI_BUILDPANEL_HEADING_ACCOUNT_INFO", "Account Information"},
            {"ABI_UI_BUILDPANEL_LOGIN_CREDENTIALS_INCORRECT", "The provided login credentials are incorrect."},
            {"ABI_UI_BUILDPANEL_LOGIN_BUTTON", "Log in"},
            {"ABI_UI_BUILDPANEL_LOGOUT_BUTTON", "Log out"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_TITLE", "Remove local credentials for CCK"},
            {
                "ABI_UI_BUILDPANEL_LOGOUT_DIALOG_BODY",
                "This will remove the locally stored credentials. You will have to re-authenticate. Do you want to continue?"
            },
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_ACCEPT", "Yes!"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_DECLINE", "No!"},

            {"ABI_UI_BUILDPANEL_UPLOADER_NO_AVATARS_FOUND", "No configured avatars found in scene - CVRAvatar added?"},
            {
                "ABI_UI_BUILDPANEL_UPLOADER_NO_SPAWNABLES_FOUND",
                "No configured spawnable objects found in scene - CVRSpawnable added?"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_WARNING_SPAWNPOINT",
                "Your world does not have any spawn points assigned. Please add one or multiple spawn points in the CVRWorld component or the location of the CVRWorld holder object will be used."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_INFO_REFERENCE_CAMERA",
                "You do not have a reference camera assigned to your world. Default camera settings will be used."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_INFO_RESPAWN_HEIGHT",
                "The respawn height is under -500. It will take a long time to respawn when falling out of the map. This is probably not what you want."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_MULTIPLE_CVR_WORLD",
                "Multiple CVR World objects are present in the scene. This will break the world. Please ensure that there is only one CVR World object in the scene or use our CVRWorld prefab."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_WORLD_CONTAINS_AVATAR",
                "Loaded scenes should never contain both avatar and world descriptor objects. Please setup your scenes accordingly."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_NO_CONTENT",
                "No content found in present scene. Did you forget to add a descriptor component to a game object?"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_UNITY_UNSUPPORTED",
                "You are using a unity version that is not supported. Please use a supported unity version. You can check the supported version corresponding to your CCK version in our documentation."
            },
            {"ABI_UI_BUILDPANEL_UPLOAD_WORLD_BUTTON", "Upload World"},
            {"ABI_UI_BUILDPANEL_UPLOAD_AVATAR_BUTTON", "Upload Avatar"},
            {"ABI_UI_BUILDPANEL_UPLOAD_PROP_BUTTON", "Upload Spawnable Object"},
            {"ABI_UI_BUILDPANEL_FIX_IMPORT_SETTINGS", "Fix import settings"},
            {"ABI_UI_BUILDPANEL_UTIL_REMOVE_MISSING_SCRIPTS_BUTTON", "Remove missing scripts"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_USERNAME", "Username"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_ACCESSKEY", "Access-Key"},
            {
                "ABI_UI_BUILDPANEL_INFOTEXT_DOCUMENTATION",
                "Use our documentation to find out more about how to create content for our games. You will also find some handy tutorials on how to utilize most of the core engine features and core game features there."
            },
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN1", "Please authenticate using your CCK credentials."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN2", "You can find those on hub.abinteractive.net."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN3", "Please generate a CCK Key in the key manager."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_AUTHENTICATED_AS", "Authenticated as"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_USER_RANK", "API user rank"},
            {"ABI_UI_BUILDPANEL_SETTINGS_HEADER", "Upload Settings"},
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_ENCRYPTION", "Switch Connection Encryption:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_ENCRYPTION",
                "If you have Problems uploading try switching to http."
            },
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_REGION", "Preferred Upload Region:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_REGION",
                "You can switch the preferred Upload Region to increase your upload speed. If the preferred region is unavailable, another region will be selected automatically. Your content is available everywhere, independently of the region chosen to upload."
            },
            {"ABI_UI_BUILDPANEL_SETTINGS_CCK_LANGUAGE", "CCK Language:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CCK_LANGUAGE",
                "You can switch your CCK language here in order to get notifications and UI texts in your preferred language."
            },
            {
                "ABI_UI_BUILDPANEL_WARNING_FOLDERPATH",
                "Please do not move the folder location of the CCK or CCK Mods folder. This will render the CCK unusable."
            },
            {
                "ABI_UI_BUILDPANEL_WARNING_FEEDBACK",
                "Want to request a feature? Found a bug? Post on our feedback platform!"
            },
            {"ABI_UI_BUILDPANEL_WARNING_MESH_FILTER_MESH_EMPTY", "MeshFilter with missing Mesh detected"},
            {
                "ABI_UI_BUILDPANEL_AVATAR_ERROR_ANIMATOR",
                "No Animator was detected for this Avatar. Make sure, that an Animator is present on the same GameObject as the CVRAvatar Component."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_GENERIC",
                "The Avatar Slot in your Avatar Animator is not filled. Your Avatar will be considered as generic Avatar."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_POLYGONS",
                "Warning: This avatar has more than 100k ({X}) polygons in total. This can cause performance problems in game. This does not prevent you from uploading."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_SKINNED_MESH_RENDERERS",
                "Warning: This avatar contains more than 10 ({X}) SkinnedMeshRenderer components. This can cause performance problems in game. This does not prevent you from uploading."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_MATERIALS",
                "Warning: This avatar utilizes more than 20 ({X}) material slots. This can cause performance problems in game. This does not prevent you from uploading."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_VIEWPOINT",
                "Warning: The view position of this avatar defaults to X=0,Y=0,Z=0. This means your view position is on the ground. This is probably not what you want."
            },
            {"ABI_UI_BUILDPANEL_AVATAR_WARNING_NON_HUMANOID", "Warning: Your Avatar is not setup as Humanoid."},
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_LEGACY_BLENDSHAPES",
                "Warning: This Avatar has none legacy blend shape normals. This will lead to an increased filesize and lighting errors"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_POLYGONS",
                "Info: This avatar has more than 50k ({X}) polygons in total. This can cause performance problems in game. This does not prevent you from uploading."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SKINNED_MESH_RENDERERS",
                "Info: This avatar contains more than 5 ({X}) SkinnedMeshRenderer components. This can cause performance problems in game. This does not prevent you from uploading."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_MATERIALS",
                "Info: This avatar utilizes more than 10 ({X}) material slots. This can cause performance problems in game. This does not prevent you from uploading."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SMALL",
                "Info: The view position of this avatar is under 0.5 in height. This avatar is considered excessively small."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_HUGE",
                "Info: The view position of this avatar is over 3.0 in height. This avatar is considered excessively huge."
            },
            {"ABI_UI_BUILDPANEL_AVATAR_UPLOAD_BUTTON", "Upload Avatar"},
            {
                "ABI_UI_BUILDPANEL_PROPS_ERROR_MISSING_SCRIPT",
                "Spawnable Objects or its children contains missing scripts. The upload will fail like this. Remove all missing script references before uploading or click Remove all missing scripts to automatically have this done for you."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_POLYGONS",
                "Warning: This spawnable object has more than 100k ({X}) polygons in total. This can cause performance problems in game. This does not prevent you from uploading."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_SKINNED_MESH_RENDERERS",
                "Warning: This spawnable object contains more than 10 ({X}) SkinnedMeshRenderer components. This can cause performance problems in game. This does not prevent you from uploading."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_MATERIALS",
                "Warning: This spawnable object utilizes more than 20 ({X}) material slots. This can cause performance problems in game. This does not prevent you from uploading."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_LEGACY_BLENDSHAPES",
                "Warning: This spawnable object has none legacy blend shape normals. This will lead to an increased filesize and lighting errors"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_POLYGONS",
                "Info: This spawnable object has more than 50k ({X}) polygons in total. This can cause performance problems in game. This does not prevent you from uploading."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_SKINNED_MESH_RENDERERS",
                "Info: This spawnable object contains more than 5 ({X}) SkinnedMeshRenderer components. This can cause performance problems in game. This does not prevent you from uploading."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_MATERIALS",
                "Info: This spawnable object utilizes more than 10 ({X}) material slots. This can cause performance problems in game. This does not prevent you from uploading."
            },
            {"ABI_UI_BUILDPANEL_PROPS_UPLOAD_BUTTON", "Upload Spawnable Object (Prop)"},
            {
                "ABI_UI_BUILDPANEL_ERROR_WORLD_MISSING_SCRIPTS",
                "Scene contains missing scripts. The upload will fail like this. Remove all missing script references before uploading or click Remove all missing scripts to automatically have this done for you."
            },
            {
                "ABI_UI_BUILDPANEL_ERROR_AVATAR_MISSING_SCRIPTS",
                "Avatar or its children contains missing scripts. The upload will fail like this. Remove all missing script references before uploading or click Remove all missing scripts to automatically have this done for you."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_MULTIPLE_TRIGGER_HELPBOX",
                "Having multiple Triggers on the same GameObject will lead to unpredictable behavior!"
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_POINTERS_HELPBOX",
                "Adding Pointers to the \"Allowed Pointers\" List will Ignore all other Pointers that are not contained in it."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_TYPES_HELPBOX",
                "Adding Types in the \"Allowed Types\" List will ignore all Pointers that do not match the Types in the List."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_PARTICLE_HELPBOX",
                "Enabling this option will allow particle systems that have a pointer on the same GameObject to activate this trigger. Particle can only trigger On Enter Trigger."
            },
            {
                "ABI_UI_INFOTEXT_WORLDS_NO_AVATARS",
                "A ChilloutVR World object has been found in the scene. Avatars can not be uploaded until the world object has been removed. Avatars / spawnable objects will be part of the world and visible in-world unless they are disabled or removed."
            },
            {
                "ABI_UI_ASSET_INFO_HEADER_INFORMATION",
                "This script is used to store object metadata. Please do not modify the data on it unless you know what you are doing. To reupload an avatar, detach the Guid and reupload."
            },
            {"ABI_UI_ASSET_INFO_GUID_LABEL", "The currently stored Guid is: "},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON", "Detach asset unique identifier"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_TITLE", "Detach Guid from Asset Info Manager"},
            {
                "ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_BODY",
                "The asset unique identifier will be detached. This means that your content will most likely be uploaded as new on runtime. Continue?"
            },
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_ACCEPT", "Yes!"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_DENY", "No!"},
            {"ABI_UI_ASSET_INFO_COPY_BUTTON", "Copy asset unique identifier"},
            {"ABI_UI_ASSET_INFO_ATTACH_LABEL", "Unique identifier"},
            {
                "ABI_UI_ASSET_INFO_ATTACH_INFO",
                "You do not need to re-attach a Guid if you do not plan to overwrite any old upload. A new one will be generated on upload if none is attached."
            },
            {"ABI_UI_ASSET_INFO_ATTACH_BUTTON", "Re-Attach guid"},
            {
                "ABI_UI_AVATAR_INFO_VIEWPOINT",
                "Controls your player rigs camera position in game. This should be between both eyes."
            },
            {
                "ABI_UI_AVATAR_INFO_VOICE_POSITION",
                "Controls your player rigs voice position in game. This should be on your mouth."
            },
            {
                "ABI_UI_AVATAR_INFO_OVERRIDE_CONTROLLER",
                "For the overrides to work, please make sure, that the correct animator is assigned in the override controller. Otherwise you will not see animator slots to override. An example for this is in the CCK Player Prefabs folder."
            },
            {
                "ABI_UI_AVATAR_INFO_BLinking",
                "Blinking blend shape usage is optional. It can be enabled to generate random blinks on runtime."
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_MOVEMENT",
                "Checking this option will enable a semi realistic animation of the eyes."
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_VISEMES",
                "For the auto-select visemes feature to work, you will have to select the mesh that includes the face first. This will be the body mesh in most cases."
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_TITLE",
                "Dependencies missing in project!"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_WARNING_PREFACE",
                "The following dependencies are not met"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_FINAL_WARNING",
                "Please install all dependencies before installing this module!"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_DIALOG_ACCEPT",
                "Understood"
            },
            { "ABI_UI_BUILD_RUNTIME_HEADER", "Upload content to ChilloutVR" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEXT", "Continue to next step" },
            { "ABI_UI_BUILD_RUNTIME_BTN_PREV", "Back to last step" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEW_PICTURE", "Replace image" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_ASSETBUNDLE", "AssetBundle File Size" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_IMAGE", "Image File Size" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_MANIFEST", "Manifest File Size" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_PANO1K", "1080P Pano File Size" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_PANO4K", "4K Pano File Size" },
            { "ABI_UI_BUILD_RUNTIME_HINT_CLICK_TO_CAPTURE", "Click smaller image to capture thumbnail" },
            { "ABI_UI_BUILDSTEP_FILTERTAGS", "Filter Tags" },
            { "ABI_UI_BUILDSTEP_DETAILS", "Details" },
            { "ABI_UI_BUILDSTEP_LEGAL", "Legal Assurance" },
            { "ABI_UI_BUILDSTEP_UPLOAD", "Upload Content" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_ROW", "Name:" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_ROW", "Description:" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_PLACEHOLDER", "Object name (required!)" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_PLACEHOLDER", "Object description" },
            { "ABI_UI_BUILDSTEP_DETAILS_CHANGELOG_PLACEHOLDER", "Object changelog - tell users what you have changed or added" },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_NEW_OBJECT", 
                "This object is uploaded for the first time. Uploading a profile picture is required, as such the option to not upload any image is not available."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_UPDATING_OBJECT", 
                "You are about to update this object. Changing description or name is not available while updating this object. Please change it on the hub if required."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_SET_ACTIVE_FILE", 
                "Set this upload as the active file for the target platform"
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_PERMISSION", 
                "I hereby certify that my uploaded content belongs to or is licensed to me. I know that uploading copyrighted content without the authors permission can get my account restricted and / or have legal consequences. I know that i have to fully adhere to any and all content creation rules mentioned in the terms of service of Alpha Blend Interactive."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_TAGS", 
                "I hereby certify that the tags are set correctly and fit to the uploaded content. I know that knowingly setting the wrong tags is a serious offense. I know that my account will be punished if i continuously set the wrong tags."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_STEP_DETAILS", 
                "Your content is now being uploaded to our network. The upload process is split into various steps. After uploading the file to our network, the file will undergo automatic security checks, after they were passed, we will encrypt your bundle and push it to our CDN. You can check the current status of your upload below."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_DETAILS_MISSING", 
                "To upload content to our platform, a name is required. When uploading a new object, make sure to name it accordingly. You will now be sent back to the details page to enter a name."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_LEGAL_MISSING", 
                "To upload to our platform, you have to certify that you are permitted to upload said content and that all set tags are correct. You will now be sent back to the legal page to review and accept the legal assurance."
            },
            { "ABI_UI_DETAILS_HEAD_CHANGELOG", "Content Changelog" },
            { "ABI_UI_DETAILS_HEAD_STATISTICS", "File Statistics" },
            { "ABI_UI_LEGAL_HEAD_OWNERSHIP", "Legal Assurance: Ownership & Copyright" },
            { "ABI_UI_LEGAL_HEAD_TAGS", "Legal Assurance: Tagging" },
            { "ABI_UI_TAGS_HEADER_AUDIO", "Audible Experience" },
            { "ABI_UI_TAGS_HEADER_VISUAL", "Visual Experience" },
            { "ABI_UI_TAGS_HEADER_CONTENT", "Content" },
            { "ABI_UI_TAGS_HEADER_NSFW", "Age Gate Classification" },
            { "ABI_UI_TAGS_LOUD_AUDIO", "Loud Audio" },
            { "ABI_UI_TAGS_LR_AUDIO", "Long-Range Audio" },
            { "ABI_UI_TAGS_SPAWN_AUDIO", "Spawn Audio" },
            { "ABI_UI_TAGS_CONTAINS_MUSIC", "Contains Music" },
            { "ABI_UI_TAGS_FLASHING_COLORS", "Flashing Colors" },
            { "ABI_UI_TAGS_FLASHING_LIGHTS", "Flashing Lights" },
            { "ABI_UI_TAGS_EXTREMELY_BRIGHT", "Extremely Bright" },
            { "ABI_UI_TAGS_SCREEN_EFFECTS", "Screen Effects" },
            { "ABI_UI_TAGS_PARTICLE_SYSTEMS", "Particle Systems" },
            { "ABI_UI_TAGS_VIOLENCE", "Violence" },
            { "ABI_UI_TAGS_GORE", "Gore" },
            { "ABI_UI_TAGS_HORROR", "Horror" },
            { "ABI_UI_TAGS_JUMPSCARE", "Jumpscare" },
            { "ABI_UI_TAGS_HUGE", "Excessively Huge" },
            { "ABI_UI_TAGS_SMALL", "Excessively Small" },
            { "ABI_UI_TAGS_SUGGESTIVE", "Suggestive" },
            { "ABI_UI_TAGS_NUDITY", "Nudity" },
            { "ABI_UI_API_RESPONSE_HEAD", "Current Status" },
            { "ABI_UI_API_RESPONSES_UPLOADED", "File is uploaded. Now processing file." },
            { "ABI_UI_API_RESPONSES_SECURITY_CHECKING", "The asset bundle is currently being checked by our security system." },
            { "ABI_UI_API_RESPONSES_ENCRYPTING", "Your asset bundle file is currently being encrypted." },
            { "ABI_UI_API_RESPONSES_PUSHING", "Checks are complete. The file is currently being transferred to our storage." },
            { "ABI_UI_API_RESPONSES_FINISHED", "Upload complete. Your content is now available in the game." },
            { "ABI_UI_API_RESPONSES_FLAGGED_BY_SECURITY_SYSTEM", "Upload complete. Your file was flagged by our security system and might not be available to some users." }
        };
    }
}