using System.Collections.Generic;

namespace ABI.CCK.Scripts.Translation
{
    public class Chinese
    {
        public static readonly Dictionary<string, string> Localization = new Dictionary<string, string>()
        {
            {"ABI_UI_BUILDPANEL_HEADING_BUILDER", "内容构建"},
            {"ABI_UI_BUILDPANEL_HEADING_SETTINGS", "选项设置"},
            {"ABI_UI_BUILDPANEL_HEADING_DOCUMENTATION", "查看文档"},
            {"ABI_UI_BUILDPANEL_HEADING_FEEDBACK", "提交反馈"},
            {"ABI_UI_BUILDPANEL_HEADING_FOUNDCONTENT", "活动场景中的内容"},
            {"ABI_UI_BUILDPANEL_HEADING_ACCOUNT_INFO", "账号信息"},
            {"ABI_UI_BUILDPANEL_LOGIN_CREDENTIALS_INCORRECT", "提供的登录凭据不正确。"},
            {"ABI_UI_BUILDPANEL_LOGIN_BUTTON", "登录"},
            {"ABI_UI_BUILDPANEL_LOGOUT_BUTTON", "登出"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_TITLE", "移除CCK的本地登录凭据"},
            {
                "ABI_UI_BUILDPANEL_LOGOUT_DIALOG_BODY",
                "这将移除本地保存的凭据，你将会需要重新验证，确定继续吗？"
            },
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_ACCEPT", "是"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_DECLINE", "否"},

            {"ABI_UI_BUILDPANEL_UPLOADER_NO_AVATARS_FOUND", "在场景中找不到已经配置的Avatar - 是否添加了CVRAvatar？"},
            {
                "ABI_UI_BUILDPANEL_UPLOADER_NO_SPAWNABLES_FOUND",
                "在场景中找不到已经配置的可生成的对象 - 是否添加了CVRSpawnable？"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_WARNING_SPAWNPOINT",
                "你的世界没有指定任何出生点，请添加一个或者多个出生点在CVRWorld组件中，否则CVRWorld holder对象的位置将被使用。"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_INFO_REFERENCE_CAMERA",
                "你的世界没有指定参考摄像机，默认的摄像机配置将被使用。"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_INFO_RESPAWN_HEIGHT",
                "出生点高度低于-500，从地图上掉出要花很长时间才能重生，这可能不是你想要的。"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_MULTIPLE_CVR_WORLD",
                "场景中存在多个CVR世界对象。这将导致世界发生问题。请确保场景中仅有一个CVRWorld对象或使用我们的CVR世界预制体。"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_WORLD_CONTAINS_AVATAR",
                "被加载的场景不应该同时包含Avatar和世界描述符组件，请根据实际情况设置场景。"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_NO_CONTENT",
                "在当前场景中找不到任何内容，你是否忘了给游戏对象添加描述符对象？"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_UNITY_UNSUPPORTED",
                "你正在使用不受支持的unity版本。请使用受支持的unity版本，你可以在我们的文档中检查与你的CCK版本对应的支持版本。"
            },
            {"ABI_UI_BUILDPANEL_UPLOAD_WORLD_BUTTON", "上传世界"},
            {"ABI_UI_BUILDPANEL_UPLOAD_AVATAR_BUTTON", "上传Avatar"},
            {"ABI_UI_BUILDPANEL_UPLOAD_PROP_BUTTON", "上载可生成对象"},
            {"ABI_UI_BUILDPANEL_FIX_IMPORT_SETTINGS", "修复导入设置"},
            {"ABI_UI_BUILDPANEL_UTIL_REMOVE_MISSING_SCRIPTS_BUTTON", "删除所有丢失的脚本"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_USERNAME", "用户名"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_ACCESSKEY", "访问密钥"},
            {
                "ABI_UI_BUILDPANEL_INFOTEXT_DOCUMENTATION",
                "使用我们的文档了解有关如何创建游戏内容的更多信息，你还将找到一些关于如何使用多数的核心的引擎功能以及核心的游戏功能的方便教程。"
            },
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN1", "请使用您的CCK凭据进行验证。"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN2", "你可以在hub.abinteractive.net上找到它们。"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN3", "请在密钥管理器中生成CCK密钥。"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_AUTHENTICATED_AS", "身份验证为"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_USER_RANK", "API用户等级"},
            {"ABI_UI_BUILDPANEL_SETTINGS_HEADER", "上传设置"},
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_ENCRYPTION", "切换网络连接加密："},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_ENCRYPTION",
                "如果上传遇到问题，请尝试切换到http。"
            },
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_REGION", "首选上传地域："},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_REGION",
                "你可以切换首选上传地域来提高上传速度。如果首选地域不可用，则将自动选择其它地域。你的内容在所有地域内都可用，与选择的用于上传的地域无关。"
            },
            {"ABI_UI_BUILDPANEL_SETTINGS_CCK_LANGUAGE", "CCK语言:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CCK_LANGUAGE",
                "你可以在这里切换CCK语言，以便用所倾向的语言来获取通知及UI文本。"
            },
            {
                "ABI_UI_BUILDPANEL_WARNING_FOLDERPATH",
                "请不要移动CCK或CCK Mods文件夹的位置，这将导致CCK无法使用。"
            },
            {
                "ABI_UI_BUILDPANEL_WARNING_FEEDBACK",
                "想要请求新功能吗？找到了BUG？请在我们的反馈平台提交！"
            },
            {"ABI_UI_BUILDPANEL_WARNING_MESH_FILTER_MESH_EMPTY", "检测到缺少网格的网格过滤器"},
            {
                "ABI_UI_BUILDPANEL_AVATAR_ERROR_ANIMATOR",
                "在这个Avatar上没有找到Animator，确保Animator和CVRAvatar组件在同一个游戏对象上。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_GENERIC",
                "你的Avatar Animator上有没被填写的Avatar空位，你的Avatar将被视为普通Avatar。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_POLYGONS",
                "警告：此Avatar包含超过100k ({X}) 个多边形，这可能导致游戏中出现性能问题，但不会阻止你上传。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_SKINNED_MESH_RENDERERS",
                "警告：此Avatar包含超过10 ({X}) 个带蒙皮的网格渲染器组件，这可能导致游戏中出现性能问题，但不会阻止你上传。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_MATERIALS",
                "警告：此Avatar使用了超过20 ({X}) 个材质空位，这可能导致游戏中出现性能问题，但不会阻止你上传。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_VIEWPOINT",
                "警告：此Avatar的视角位置默认为X=0,Y=0,Z=0。这意味着你的视角在地面上，这可能不是你想要的。"
            },
            {"ABI_UI_BUILDPANEL_AVATAR_WARNING_NON_HUMANOID", "警告：你的Avatar没有设置为人形。"},
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_LEGACY_BLENDSHAPES",
                "警告：此Avatar没有传统的BlendShape法线，这将导致文件变大以及照明错误。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_POLYGONS",
                "信息：这个Avatar包含超过50k ({X}) 个多边形，这可能导致游戏中出现性能问题，但不会阻止你上传"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SKINNED_MESH_RENDERERS",
                "信息：此Avatar包含超过5 ({X}) 个带蒙皮的网格渲染器组件，这可能导致游戏中出现性能问题，但不会阻止你上传。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_MATERIALS",
                "信息：此Avatar使用了超过 10 ({X}) 个材质空位，这可能导致游戏中出现性能问题，但不会阻止你上传。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SMALL",
                "信息：此Avatar的视角高度低于0.5，这个Avatar将被认定为极小。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_HUGE",
                "信息：此Avatar的视角高度高于3.0，这个Avatar将被认定为极大。"
            },
            {"ABI_UI_BUILDPANEL_AVATAR_UPLOAD_BUTTON", "上传Avatar"},
            {
                "ABI_UI_BUILDPANEL_PROPS_ERROR_MISSING_SCRIPT",
                "可生成的对象或其子对象包含丢失的脚本。上传会因此失败。在上传之前删除所有丢失的脚本引用，或者单击“删除所有丢失的脚本”自动完成此操作。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_POLYGONS",
                "警告：这个可生成的对象包含超过100k ({X}) 个多边形，这可能导致游戏中出现性能问题，但不会阻止你上传。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_SKINNED_MESH_RENDERERS",
                "警告：此可生成对象包含超过 10 ({X}) 个带蒙皮的网格渲染器组件，这可能导致游戏中出现性能问题，但不会阻止你上传。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_MATERIALS",
                "警告：此可生成对象使用了超过 20 ({X}) 个材质空位，这可能导致游戏中出现性能问题，但不会阻止你上传。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_LEGACY_BLENDSHAPES",
                "警告：此可生成对象没有传统的BlendShape法线，这将导致文件变大以及照明错误。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_POLYGONS",
                "信息：此可生成对象包含超过 50k ({X}) 个多边形， 这可能导致游戏中出现性能问题，但不会阻止你上传。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_SKINNED_MESH_RENDERERS",
                "信息：此可生成对象包含超过 5 ({X}) 个带蒙皮的网格渲染器组件，这可能导致游戏中出现性能问题，但不会阻止你上传。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_MATERIALS",
                "信息：此可生成对象使用了超过 10 ({X}) 个材质空位，这可能导致游戏中出现性能问题，但不会阻止你上传。"
            },
            {"ABI_UI_BUILDPANEL_PROPS_UPLOAD_BUTTON", "上传可生成的物体 (Prop)"},
            {
                "ABI_UI_BUILDPANEL_ERROR_WORLD_MISSING_SCRIPTS",
                "场景包含丢失的脚本。上传会因此失败。在上传之前删除所有丢失的脚本引用，或者单击“删除所有丢失的脚本”自动完成此操作。"
            },
            {
                "ABI_UI_BUILDPANEL_ERROR_AVATAR_MISSING_SCRIPTS",
                "Avatar或其子对象包含丢失的脚本。上传会因此失败。在上传之前删除所有丢失的脚本引用，或者单击“删除所有丢失的脚本”自动完成此操作。"
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_MULTIPLE_TRIGGER_HELPBOX",
                "在同一个游戏对象上有多个触发器会导致不可预知的行为！"
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_POINTERS_HELPBOX",
                "添加Pointers到\"允许的Pointers\" 列表将忽略其中不包含的所有其他Pointers"
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_TYPES_HELPBOX",
                "添加类型到\"允许的类型\" 列表将忽略其中不包含的所有其他类型"
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_PARTICLE_HELPBOX",
                "启用此选项将允许在同一游戏对象上有pointer的粒子系统激活此触发器。粒子只能在输入触发器时触发。"
            },
            {
                "ABI_UI_INFOTEXT_WORLDS_NO_AVATARS",
                "在场景中发现一个ChilloutVR世界对象，在世界对象被移除之前，无法上传Avatars，Avatars / 可生成对象将是世界的一部分，并且在世界上可见，除非它们被禁用或移除。"
            },
            {
                "ABI_UI_ASSET_INFO_HEADER_INFORMATION",
                "此脚本用于存储对象元信息。请不要修改上面的数据，除非你知道你在做什么，要重新上传一个Avatar，请解绑Guid并重新上传。"
            },
            {"ABI_UI_ASSET_INFO_GUID_LABEL", "当前存储的Guid为: "},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON", "解绑asset唯一标识符"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_TITLE", "从Asset信息管理器中解绑Guid"},
            {
                "ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_BODY",
                "将解绑Asset唯一标识符，这意味着你内容很可能被运行时作为新内容上传，继续吗？"
            },
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_ACCEPT", "是"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_DENY", "否"},
            {"ABI_UI_ASSET_INFO_ATTACH_LABEL", "唯一标识符"},
            {
                "ABI_UI_ASSET_INFO_ATTACH_INFO",
                "如果你没有打算覆盖旧的上传内容，则不需要重新绑定Guid，当前没有绑定则会在上传时将生成一个。"
            },
            {"ABI_UI_ASSET_INFO_ATTACH_BUTTON", "重新绑定guid"},
            {
                "ABI_UI_AVATAR_INFO_VIEWPOINT",
                "控制玩家在游戏中视角相机绑定的位置，这个应该在你的两只眼睛之间。"
            },
            {
                "ABI_UI_AVATAR_INFO_VOICE_POSITION",
                "控制玩家在游戏中的声音绑定的位置。这个应该在你的嘴上。"
            },
            {
                "ABI_UI_AVATAR_INFO_OVERRIDE_CONTROLLER",
                "要使覆盖生效，请确保在覆盖控制器中指定了正确的animator，否则，将看不到要覆盖的animator空位，一个例子在CCK Player Prefabs文件夹中。"
            },
            {
                "ABI_UI_AVATAR_INFO_BLinking",
                "眨眼的形态键的使用是可选的，它可以在运行时生成随机眨眼。"
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_MOVEMENT",
                "选中此选项将开启眼睛的半真实的动画。"
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_VISEMES",
                "要使“自动选择发音嘴型”功能正常工作，必须首先选择包含face的网格。在大多数情况下，这将是body网格。"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_TITLE",
                "项目中缺少依赖项！"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_WARNING_PREFACE",
                "以下依赖项未满足"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_FINAL_WARNING",
                "请在安装此模块之前安装所有依赖项！"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_DIALOG_ACCEPT",
                "了解"
            },
            { "ABI_UI_BUILD_RUNTIME_HEADER", "上传内容到ChilloutVR" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEXT", "下一步" },
            { "ABI_UI_BUILD_RUNTIME_BTN_PREV", "上一步" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEW_PICTURE", "替换图像" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_ASSETBUNDLE", "资源包文件大小" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_IMAGE", "图片文件大小" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_MANIFEST", "清单文件大小" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_PANO1K", "1080P 全景文件大小" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_PANO4K", "4K 全景文件大小" },
            { "ABI_UI_BUILD_RUNTIME_HINT_CLICK_TO_CAPTURE", "单击较小的图像来捕获缩略图" },
            { "ABI_UI_BUILDSTEP_FILTERTAGS", "过滤标签" },
            { "ABI_UI_BUILDSTEP_DETAILS", "详情" },
            { "ABI_UI_BUILDSTEP_LEGAL", "法律保证" },
            { "ABI_UI_BUILDSTEP_UPLOAD", "上传内容" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_ROW", "名称：" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_ROW", "描述：" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_PLACEHOLDER", "对象名称（必填！）" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_PLACEHOLDER", "对象描述" },
            { "ABI_UI_BUILDSTEP_DETAILS_CHANGELOG_PLACEHOLDER", "对象更改日志 - 告诉用户你更改或添加了什么" },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_NEW_OBJECT", 
                "这个对象是第一次上传。必须上传资料图片，因此，不上传任何图像的选项不可用。"
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_UPDATING_OBJECT", 
                "你将要更新此对象。更新此对象时无法更改描述或名称。如果需要，请在hub上进行更改。"
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_SET_ACTIVE_FILE", 
                "将此次上传设置为目标平台的活动文件"
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_PERMISSION", 
                "本人承诺，我上传的内容属于我或已获得许可。我知道未经作者许可上传受版权保护的内容可能会导致我的帐户被限制，并且/或者产生法律后果。我知道我必须完全遵守Alpha Blend Interactive服务条款中提及的任何和所有内容创建规定。"
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_TAGS", 
                "本人承诺，所有标签设置正确，符合上传的内容。我知道故意设置错误的标签是严重的违规行为。我知道如果我连续不断地设置错误的标签将导致帐户受到惩罚。"
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_STEP_DETAILS", 
                "你的内容正在上传到我们的网络。上传过程分为几个步骤。文件上传到我们的网络后，该文件将自动进行安全检查，通过检查后，我们将加密你的资源包并推送到我们的CDN。你可以在下面检查当前的上传状态。"
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_DETAILS_MISSING", 
                "要将内容上传到我们的平台，需要一个名称。上传新对象时，请确保对它进行相应的命名。你将返回到详情页来输入一个名字。"
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_LEGAL_MISSING", 
                "要将内容上传到我们的平台，你必须证明你已被允许上传上述内容，并且设置的所有标签都是正确的。你将返回到法律信息页面查看并同意法律保证。"
            },
            { "ABI_UI_DETAILS_HEAD_CHANGELOG", "内容更改日志" },
            { "ABI_UI_DETAILS_HEAD_STATISTICS", "文件统计" },
            { "ABI_UI_LEGAL_HEAD_OWNERSHIP", "法律保证：所有权和版权" },
            { "ABI_UI_LEGAL_HEAD_TAGS", "法律保证：标签" },
            { "ABI_UI_TAGS_HEADER_AUDIO", "听觉体验" },
            { "ABI_UI_TAGS_HEADER_VISUAL", "视觉体验" },
            { "ABI_UI_TAGS_HEADER_CONTENT", "内容" },
            { "ABI_UI_TAGS_HEADER_NSFW", "年龄分级" },
            { "ABI_UI_TAGS_LOUD_AUDIO", "大音量音频" },
            { "ABI_UI_TAGS_LR_AUDIO", "广范围音频" },
            { "ABI_UI_TAGS_SPAWN_AUDIO", "生成时的音频" },
            { "ABI_UI_TAGS_CONTAINS_MUSIC", "包含音乐" },
            { "ABI_UI_TAGS_FLASHING_COLORS", "闪烁的颜色" },
            { "ABI_UI_TAGS_FLASHING_LIGHTS", "闪烁的灯光" },
            { "ABI_UI_TAGS_EXTREMELY_BRIGHT", "亮度极高" },
            { "ABI_UI_TAGS_SCREEN_EFFECTS", "屏幕特效" },
            { "ABI_UI_TAGS_PARTICLE_SYSTEMS", "粒子系统" },
            { "ABI_UI_TAGS_VIOLENCE", "暴力" },
            { "ABI_UI_TAGS_GORE", "血腥" },
            { "ABI_UI_TAGS_HORROR", "恐怖" },
            { "ABI_UI_TAGS_JUMPSCARE", "突发惊吓" },
            { "ABI_UI_TAGS_HUGE", "巨大" },
            { "ABI_UI_TAGS_SMALL", "微小" },
            { "ABI_UI_TAGS_SUGGESTIVE", "暗示性" },
            { "ABI_UI_TAGS_NUDITY", "裸露" },
            { "ABI_UI_API_RESPONSE_HEAD", "当前状态" },
            { "ABI_UI_API_RESPONSES_UPLOADED", "文件已上传。现在正在处理文件。" },
            { "ABI_UI_API_RESPONSES_SECURITY_CHECKING", "我们的安全系统目前正在检查资源包。" },
            { "ABI_UI_API_RESPONSES_ENCRYPTING", "你的资源包正在被加密。" },
            { "ABI_UI_API_RESPONSES_PUSHING", "检查完毕。该文件当前正在被传输到我们的存储。" },
            { "ABI_UI_API_RESPONSES_FINISHED", "上传完毕。你的内容现在在游戏中可用了。" },
        };
    }
}