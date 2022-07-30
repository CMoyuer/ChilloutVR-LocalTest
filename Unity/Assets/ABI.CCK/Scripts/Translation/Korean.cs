using System.Collections.Generic;

namespace ABI.CCK.Scripts.Translation
{
    public class Korean
    {
        public static readonly Dictionary<string, string> Localization = new Dictionary<string, string>()
        {
            {"ABI_UI_BUILDPANEL_HEADING_BUILDER", "콘텐츠 빌더"},
            {"ABI_UI_BUILDPANEL_HEADING_SETTINGS", "설정 & 옵션"},
            {"ABI_UI_BUILDPANEL_HEADING_DOCUMENTATION", "안내 문서(Documentation) 읽기"},
            {"ABI_UI_BUILDPANEL_HEADING_FEEDBACK", "피드백 전송"},
            {"ABI_UI_BUILDPANEL_HEADING_FOUNDCONTENT", "활성화된 씬에서 콘텐츠 찾음"},
            {"ABI_UI_BUILDPANEL_HEADING_ACCOUNT_INFO", "계정 정보"},
            {"ABI_UI_BUILDPANEL_LOGIN_CREDENTIALS_INCORRECT", "로그인 정보가 정확하지 않습니다."},
            {"ABI_UI_BUILDPANEL_LOGIN_BUTTON", "로그인"},
            {"ABI_UI_BUILDPANEL_LOGOUT_BUTTON", "로그아웃"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_TITLE",
            "콘텐츠 제작 키트(CCK)의 로컬 계정 정보 제거"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_BODY",
            "이 컴퓨터에 저장된 정보를 제거합니다. 다시 로그인해야 합니다. 진행하시겠습니까?"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_ACCEPT", "네!"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_DECLINE", "아니오!"},

            {"ABI_UI_BUILDPANEL_UPLOADER_NO_AVATARS_FOUND",
            "아바타를 찾을 수 없습니다 - CVR Avatar 컴포넌트가 추가되었나요?"},
            {"ABI_UI_BUILDPANEL_UPLOADER_NO_SPAWNABLES_FOUND",
            "소환 가능 물체(프롭)을 찾을 수 없습니다 - CVRSpawnable 컴포넌트가 추가되었나요?"},

            {"ABI_UI_BUILDPANEL_WORLDS_WARNING_SPAWNPOINT",
            "월드에 스폰 장소가 지정되어 있지 않습니다. 하나 혹은 여러 개를 CVRWorld 컴포넌트에 추가하세요. 아니면, CVRWorld 오브젝트의 위치가 기본 스폰 장소가 됩니다."},
            {"ABI_UI_BUILDPANEL_WORLDS_INFO_REFERENCE_CAMERA",
            "월드에 리퍼런스 카메라(Reference camera)가 설정되어 있지 않습니다. 기본 카메라 설정이 사용됩니다."},
            {"ABI_UI_BUILDPANEL_WORLDS_INFO_RESPAWN_HEIGHT",
            "리스폰 위치가 -500보다 작습니다. 월드 밖으로 떨어질 때 까지 오랜 시간이 걸립니다. 아마 원하는 바가 아닐 가능성이 높습니다."},
            {"ABI_UI_BUILDPANEL_WORLDS_ERROR_MULTIPLE_CVR_WORLD",
            "여러 CVRWorld 오브젝트가 씬에 있습니다. 월드를 고장낼 가능성이 높습니다. CVRWorld 프리팹(Prefab)을 이용하거나, CVRWorld 컴포넌트를 하나만 남겨 주세요."},
            {"ABI_UI_BUILDPANEL_WORLDS_ERROR_WORLD_CONTAINS_AVATAR",
            "하나의 씬(Scene)에는 아바타와 월드 디스크립터(Descriptor)가 동시에 존재해서는 안 됩니다. 둘 중 하나는 제거해 주세요."},
            {"ABI_UI_BUILDPANEL_WORLDS_ERROR_NO_CONTENT",
            "현재 씬(Scene)에 아무 콘텐츠도 없습니다. 디스크립터(Descriptor) 컴포넌트나 게임 오브젝트를 추가하는 것을 잊으셨나요?"},
            {"ABI_UI_BUILDPANEL_WORLDS_ERROR_UNITY_UNSUPPORTED",
            "지원되지 않는 유니티 버전을 사용중입니다. 유니티 2019.3.14f1를 사용해 주세요. 유니티 허브를 사용하면 관리가 용이합니다."},
            {"ABI_UI_BUILDPANEL_UPLOAD_WORLD_BUTTON", "월드 업로드"},
            {"ABI_UI_BUILDPANEL_UPLOAD_AVATAR_BUTTON", "아바타 업로드"},
            {"ABI_UI_BUILDPANEL_UPLOAD_PROP_BUTTON", "소환 가능 물체(프롭, Prop) 업로드"},
            {"ABI_UI_BUILDPANEL_FIX_IMPORT_SETTINGS", "불러오기(Import) 설정 고치기"},
            {"ABI_UI_BUILDPANEL_UTIL_REMOVE_MISSING_SCRIPTS_BUTTON", "미발견 스크립트 제거"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_USERNAME", "유저 이름"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_ACCESSKEY", "접근 키(Access-Key)"},
            {
                "ABI_UI_BUILDPANEL_INFOTEXT_DOCUMENTATION",
                "안내 문서를 읽고, CVR에서 어떻게 콘텐츠를 만드는지 알아보세요. 게임에 대한 정보 엔진을 어떻게 사용하는지에 대한 유용한 튜토리얼도 찾을 수 있습니다."
            },
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN1", "콘텐츠 제작 키트(CCK) 계정 정보를 활용해 로그인 해 주세요."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN2", "hub.abinteractive.net에서 찾을 수 있습니다."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN3", "키 매니저에서 콘텐츠 제작 키트(CCK) 키를 생성해 주세요."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_AUTHENTICATED_AS", "로그인된 유저"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_USER_RANK", "API 유저 랭크"},

            {"ABI_UI_BUILDPANEL_SETTINGS_HEADER", "업로드 설정"},
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_ENCRYPTION", "연결 보안 방식 변경:"},
            {"ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_ENCRYPTION", "업로드에 문제가 생겼다면 http로 변경해 보세요."},
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_REGION", "선호하는 업로드 지역:"},
            {"ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_REGION", "업로드 속도를 빠르게 하기 위해, 선호하는 업로드 지역을 선택할 수 있습니다. 만약 선택한 지역에 업로드하지 못 할 경우, 다른 지역이 자동으로 선택됩니다. 어느 지역에 업로드해도 해당 콘텐츠는 어디서든 쓸 수 있습니다."},
            {"ABI_UI_BUILDPANEL_SETTINGS_CCK_LANGUAGE", "콘텐츠 제작 키트(CCK) 언어:"},
            {"ABI_UI_BUILDPANEL_SETTINGS_HINT_CCK_LANGUAGE", "콘텐츠 제작 키트(CCK) 선호하는 언어로 변경해 알림이나 UI 텍스트를 원하는 언어로 변경할 수 있습니다."},
            {"ABI_UI_BUILDPANEL_WARNING_FOLDERPATH", "콘텐츠 제작 키트(CCK) 폴더의 위치나 Mods의 폴더의 위치를 옮기지 마세요. 콘텐츠 제작 키트가 사용 불가능하게 됩니다."},
            {"ABI_UI_BUILDPANEL_WARNING_FEEDBACK", "기능을 제안하고 싶나요? 버그를 찾았나요? 피드백 플랫폼에 남겨 주세요!"},
            {"ABI_UI_BUILDPANEL_WARNING_MESH_FILTER_MESH_EMPTY", "메쉬(Mesh)가 없는 메쉬 필터(MeshFilter)를 찾았습니다."},
            {
                "ABI_UI_BUILDPANEL_AVATAR_ERROR_ANIMATOR",
                "아바타에 애니메이터(Animator)를 찾을 수 없습니다. CVRAvatar 컴포넌트(Component)가 달린 게임 오브젝트(GameObject)와 같은 게임 오브젝트에 애니메이터가 있는지 확인해 주세요."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_GENERIC",
                "아바타 애니메이터(Animator)의 아바타 슬롯이 비어 있습니다. 아바타가 일반 아바타(Generic Avatar)로 판정됩니다."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_POLYGONS",
                "경고: 이 아바타는 10만 개 (현재 {X}개) 이상 의 폴리곤을 가지고 있습니다. 이는 게임 내에서 성능 저하를 일으킵니다. 업로드가 불가능하지는 않습니다."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_SKINNED_MESH_RENDERERS",
                "경고: 이 아바타는 10개 (현재 {X}개) 이상의 스킨 메쉬 렌더러(SkinnedMeshRenderer) 컴포넌트를 가지고 있습니다. 이는 게임 내에서 성능 저하를 일으킵니다. 업로드가 불가능하지는 않습니다."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_MATERIALS",
                "경고: 이 아바타는 20개 (현재 {X}개) 이상의 머티리얼(Material) 슬롯을 사용중입니다. 이는 게임 내에서 성능 저하를 일으킵니다. 업로드가 불가능하지는 않습니다."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_VIEWPOINT",
                "경고: 아바타의 시점(View position)이 기본값인 X=0,Y=0,Z=0 으로 설정되어 있습니다. 아바타의 시점이 땅에 고정되어 있다는 의미입니다. 원하는 바가 아닐 가능성이 큽니다."
            },
            {"ABI_UI_BUILDPANEL_AVATAR_WARNING_NON_HUMANOID", "경고: 아바타가 휴머노이드로 설정되어 있지 않습니다."},
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_LEGACY_BLENDSHAPES",
                "경고: 아바타 불러오기 설정이 이전 블렌드 쉐이프 노말(Legacy blend shape normals) 사용으로 설정되어 있습니다. 아바타 파일의 크기가 커지고, 빛에 이상하게 영향받을 가능성이 큽니다."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_POLYGONS",
                "정보: 이 아바타는 5만 개 (현재 {X}개) 이상 의 폴리곤을 가지고 있습니다. 이는 게임 내에서 성능 저하를 일으킬 가능성이 있습니다. 업로드가 불가능하지는 않습니다."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SKINNED_MESH_RENDERERS",
                "정보: 이 아바타는 5개 (현재 {X}개) 이상의 스킨 메쉬 렌더러(SkinnedMeshRenderer) 컴포넌트를 가지고 있습니다. 이는 게임 내에서 성능 저하를 일으킬 가능성이 있습니다. 업로드가 불가능하지는 않습니다."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_MATERIALS",
                "정보: 이 아바타는 10개 (현재 {X}개) 이상의 머티리얼(Material) 슬롯을 사용중입니다. 이는 게임 내에서 성능 저하를 일으킬 가능성이 있습니다. 업로드가 불가능하지는 않습니다."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SMALL",
                "정보: 아바타의 시점 높이가 0.5보다 작습니다. 작은 아바타로 판정됩니다."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_HUGE",
                "정보: 아바타의 시점 높이가 5보다 큽니다. 큰 아바타로 판정됩니다."
            },
            {"ABI_UI_BUILDPANEL_AVATAR_UPLOAD_BUTTON", "아바타 업로드"},
            {
                "ABI_UI_BUILDPANEL_PROPS_ERROR_MISSING_SCRIPT",
                "소환 가능한 물체(프롭, Prop) 혹은 그 하위 오브젝트에 찾을 수 없는 스크립트가 있습니다. 업로드가 실패할 것입니다. 모든 미발견 스크립트를 제거하거나, 아래 모든 미발견 스크립트 제거 버튼을 눌러 자동으로 제거하세요."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_POLYGONS",
                "경고: 소환 가능한 물체(프롭, Prop)는 10만 개 (현재 {X}개) 이상 의 폴리곤을 가지고 있습니다. 이는 게임 내에서 성능 저하를 일으킵니다. 업로드가 불가능하지는 않습니다."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_SKINNED_MESH_RENDERERS",
                "경고: 이 소환 가능한 물체(프롭, Prop)는 10개 (현재 {X}개) 이상의 스킨 메쉬 렌더러(SkinnedMeshRenderer) 컴포넌트를 가지고 있습니다. 이는 게임 내에서 성능 저하를 일으킵니다. 업로드가 불가능하지는 않습니다."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_MATERIALS",
                "경고: 이 소환 가능한 물체(프롭, Prop)는 20개 ({X}) 이상의 머티리얼(material) 슬롯을 사용중입니다. 이는 게임 내에서 성능 저하를 일으킵니다. 업로드가 불가능하지는 않습니다."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_LEGACY_BLENDSHAPES",
                "경고: 소환 가능한 물체(프롭, Prop) 불러오기 설정이 이전 블렌드 쉐이프 노말(Legacy blend shape normals) 사용으로 설정되어 있습니다. 아바타 파일의 크기가 커지고, 빛에 이상하게 영향받을 가능성이 큽니다."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_POLYGONS",
                "정보: 이 소환 가능한 물체(프롭)는 5만 개 (현재 {X}개) 이상 의 폴리곤을 가지고 있습니다. 이는 게임 내에서 성능 저하를 일으킬 가능성이 있습니다. 업로드가 불가능하지는 않습니다."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_SKINNED_MESH_RENDERERS",
                "정보: 이 소환 가능한 물체(프롭)는 5개 (현재 {X}개) 이상의 스킨 메쉬 렌더러(SkinnedMeshRenderer) 컴포넌트를 가지고 있습니다. 이는 게임 내에서 성능 저하를 일으킬 가능성이 있습니다. 업로드가 불가능하지는 않습니다."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_MATERIALS",
                "정보: 이 소환 가능한 물체(프롭)는 10개 (현재 {X}개) 이상의 머티리얼(Material) 슬롯을 사용중입니다. 이는 게임 내에서 성능 저하를 일으킬 가능성이 있습니다. 업로드가 불가능하지는 않습니다."
            },
            {"ABI_UI_BUILDPANEL_PROPS_UPLOAD_BUTTON", "소환 가능한 물체(프롭) 업로드"},
            {
                "ABI_UI_BUILDPANEL_ERROR_WORLD_MISSING_SCRIPTS",
                "씬에 찾을 수 없는 스크립트가 있습니다. 업로드가 실패할 것입니다. 모든 미발견 스크립트를 제거하거나, 아래 모든 미발견 스크립트 제거 버튼을 눌러 자동으로 제거하세요."
            },
            {
                "ABI_UI_BUILDPANEL_ERROR_AVATAR_MISSING_SCRIPTS",
                "아바타 혹은 그 하위 오브젝트에 찾을 수 없는 스크립트가 있습니다. 업로드가 실패할 것입니다. 모든 미발견 스크립트를 제거하거나, 아래 모든 미발견 스크립트 제거 버튼을 눌러 자동으로 제거하세요."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_MULTIPLE_TRIGGER_HELPBOX",
                "한 오브젝트의 여러 개의 트리거(Triggers)를 배치하는 것은 예상치 못한 행동으로 이어집니다!"
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_POINTERS_HELPBOX",
                "\"Allowed Pointers\" 리스트에 포인터(Pointers)를 추가하면, 포함되지 않은 다른 모든 포인터가 무시됩니다."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_TYPES_HELPBOX",
                "\"Allowed Pointers\" 리스트에 새 타입을 추가하면, 리스트에 맞지 않는 모든 타입이 무시됩니다."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_PARTICLE_HELPBOX",
                "이 옵션을 활성화하면, 같은 게임 오브젝트(GameObject) 에 달린 포인터가 있는 파티클이 트리거를 작동시킬 수 있게 합니다. 파티클은 트리거 시작(On Enter Trigger) 이벤트만 실행할 수 있습니다."
            },
            {
                "ABI_UI_INFOTEXT_WORLDS_NO_AVATARS",
                "CVR 월드 오브젝트가 씬(Scene)에서 발견되었습니다. 아바타는 월드 오브젝트가 제거되기 전에 업로드가 불가능합니다. 아바타 / 소환 가능한 물체는 월드의 일부가 되며, 제거되기 전거나 비활성화 되기 전까지 보입니다."
            },
            {
                "ABI_UI_ASSET_INFO_HEADER_INFORMATION",
                "이 스크립트는 오브젝트의 메타데이터를 저장하기 위해 있습니다. 무엇을 하고 있는지 모른다면 데이터를 수정하지 마세요. 아바타를 다시 업로드하려면, 고유 식별자를 연결 해제한 뒤, 다시 업로드 하세요."
            },
            {"ABI_UI_ASSET_INFO_GUID_LABEL", "현재 부착된 아이디(Guid)는: "},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON", "고유 식별자 분리"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_TITLE", "아이디(Guid)를 에셋 정보 관리자(Asset Info Manager)에서 분리"},
            {
                "ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_BODY",
                "에셋의 고유 식별자가 분리됩니다. 이 의미는 이 콘텐츠가 새 오브젝트로 올라간다는 의미입니다. 계속합니까?"
            },
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_ACCEPT", "네!"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_DENY", "아니오!"},
            {"ABI_UI_ASSET_INFO_ATTACH_LABEL", "고유 식별자"},
            {
                "ABI_UI_ASSET_INFO_ATTACH_INFO",
                "기존 콘텐츠를 덮어씌우는 게 아니라면 아이디(Guid) 재부착을 할 필요가 없습니다. 아무 것도 부착되어 있지 않으면 새 것이 생성되어 업로드됩니다."
            },
            {"ABI_UI_ASSET_INFO_ATTACH_BUTTON", "아이디(Guid) 재부착"},
            {
                "ABI_UI_AVATAR_INFO_VIEWPOINT",
                "플레이어 리그(Rigs) 카메라 위치를 제어합니다. 눈 사이에 있어야 합니다."
            },
            {
                "ABI_UI_AVATAR_INFO_VOICE_POSITION",
                "플레이어 리그(Rigs) 음성 위치를 제어합니다. 입에 있어야 합니다."
            },
            {
                "ABI_UI_AVATAR_INFO_OVERRIDE_CONTROLLER",
                "오버라이드(Overrides)가 작동하려면, 아바타 오버라이드 컨트롤러(Override controller)에 맞는 애니메이터(Animator)가 적용되어 있는지 확인해 주세요. 그렇게 안 하면 애니메이터 슬롯에 덮어씌울 것이 안 보입니다. 예시가 CCK Player Prefabs 폴더에 있습니다."
            },
            {
                "ABI_UI_AVATAR_INFO_BLinking",
                "눈 깜박임 블렌드 쉐이프(Blend shape) 사용은 부가적입니다. 활성화할 경우, 게임 중에서 랜덤 깜박임이 생성됩니다."
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_MOVEMENT",
                "이 옵션을 설정할 경우, 조금 사실적인 눈 애니메이션이 적용됩니다."
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_VISEMES",
                "바이스메(Visemes) 자동 선택을 활성화 하려면, 얼굴에 해당하는 메쉬(Mesh)를 선택해야 합니다. 보통 body 메쉬에 해당됩니다."
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_TITLE",
                "종속 요소가 프로젝트에 없습니다!"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_WARNING_PREFACE",
                "종속 요소가 없습니다"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_FINAL_WARNING",
                "이 모듈을 사용하기 전에 모든 종속 요소들을 설치해 주세요!"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_DIALOG_ACCEPT",
                "이해했음"
            },
            { "ABI_UI_BUILD_RUNTIME_HEADER", "칠아웃VR(ChilloutVR)에 콘텐츠 업로드" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEXT", "다음 단계로" },
            { "ABI_UI_BUILD_RUNTIME_BTN_PREV", "이전 단계로" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEW_PICTURE", "이미지 교체" },
            { "ABI_UI_BUILDSTEP_FILTERTAGS", "태그 필터" },
            { "ABI_UI_BUILDSTEP_DETAILS", "자세히" },
            { "ABI_UI_BUILDSTEP_LEGAL", "법적 보증" },
            { "ABI_UI_BUILDSTEP_UPLOAD", "콘텐츠 업로드" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_ROW", "이름:" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_ROW", "설명:" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_PLACEHOLDER", "오브젝트 이름(필수)" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_PLACEHOLDER", "오브젝트 설명" },
            { "ABI_UI_BUILDSTEP_DETAILS_CHANGELOG_PLACEHOLDER", "오브젝트 변경사항 - 유저들에게 뭐가 바뀌거나 추가되었는지 알리세요" },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_NEW_OBJECT", 
                "이 오브젝트는 처음 업로드됩니다. 프로필 사진이 필요합니다. 이미지를 업로드하지 않는 옵션은 허용되지 않습니다."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_UPDATING_OBJECT", 
                "오브젝트를 업데이트하려고 합니다. 설명이나 이름을 바꾸는 건 업데이트 중에서는 불가능합니다. 필요할 경우, 허브에서 바꿔주세요."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_SET_ACTIVE_FILE", 
                "대상 플랫폼에서 사용 가능하도록 업로드"
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_PERMISSION", 
                "(번역에 차이가 있을 시 영어 원문을 따라갑니다.) 내가 업로드하는 콘텐츠가 내 것 혹은 내가 라이선스를 취득한 것임을 증명합니다. 원작자의 허락 없이 저작권이 있는 콘텐츠를 업로드하는 것은 내 계정이 제한되고 법적인 조치를 받을 수 있다는 것을 인지했습니다. Alpha Blend Interactive의 이용 규약에 언급된 콘텐츠 제작 규칙을 따라야 하는 것에 동의합니다."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_TAGS", 
                "(번역에 차이가 있을 시 영어 원문을 따라갑니다.) 태그가 정확하게 입려된 것을 확인합니다. 잘못된 태그를 입력하는 것이 타인에게 매우 공격적인 행동이 될 것을 인지했습니다. 태그를 지속적으로 잘못 설정하면 계정에 제재될 수 있다는 것을 인지했습니다."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_STEP_DETAILS", 
                "콘텐츠가 네트워크에 업로드되고 있습니다. 업로드 과정은 여러 단계를 거칩니다. 콘텐츠가 업로드되면, 파일은 자동적으로 보안 검사를 진행하고, 통과됐을 경우에 암호화 단계를 거쳐 CDN에 업로드됩니다. 아래에서 현재 상태를 확인할 수 있습니다."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_DETAILS_MISSING", 
                "콘텐츠를 업로드하기 위해서는 이름이 필요합니다. 새 오브젝트를 올릴 때는, 이름이 잘 붙어있는지 확인하세요. 이름을 입력하기 위해서 상세 정보 설정 페이지로 돌아갑니다."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_LEGAL_MISSING", 
                "콘텐츠를 업로드하기 위해서는, 태그가 정확해야 하고 이 콘텐츠를 업로드하기 위한 허락에 대한 확인이 필요합니다. 확인 페이지로 돌아갑니다."
            },
            { "ABI_UI_DETAILS_HEAD_CHANGELOG", "콘텐츠 변동사항" },
            { "ABI_UI_DETAILS_HEAD_STATISTICS", "파일 통계" },
            { "ABI_UI_LEGAL_HEAD_OWNERSHIP", "법적 보증: 소유권 & 저작권" },
            { "ABI_UI_LEGAL_HEAD_TAGS", "법적 보증: 태그" },
            { "ABI_UI_TAGS_HEADER_AUDIO", "소리(Audible Experience)" },
            { "ABI_UI_TAGS_HEADER_VISUAL", "시야(Visual Experience)" },
            { "ABI_UI_TAGS_HEADER_CONTENT", "콘텐츠" },
            { "ABI_UI_TAGS_HEADER_NSFW", "연령 제한 분류" },
            { "ABI_UI_TAGS_LOUD_AUDIO", "큰 소리" },
            { "ABI_UI_TAGS_LR_AUDIO", "멀리까지 들리는 소리" },
            { "ABI_UI_TAGS_SPAWN_AUDIO", "입장 음악" },
            { "ABI_UI_TAGS_CONTAINS_MUSIC", "음악을 포함함" },
            { "ABI_UI_TAGS_FLASHING_COLORS", "반짝이는 색" },
            { "ABI_UI_TAGS_FLASHING_LIGHTS", "반짝이는 빛" },
            { "ABI_UI_TAGS_EXTREMELY_BRIGHT", "매우 밝음" },
            { "ABI_UI_TAGS_SCREEN_EFFECTS", "화면 효과" },
            { "ABI_UI_TAGS_PARTICLE_SYSTEMS", "파티클 시스템" },
            { "ABI_UI_TAGS_VIOLENCE", "폭력" },
            { "ABI_UI_TAGS_GORE", "고어" },
            { "ABI_UI_TAGS_HORROR", "공포" },
            { "ABI_UI_TAGS_JUMPSCARE", "갑자기 툭 튀어나오는 것" },
            { "ABI_UI_TAGS_HUGE", "매우 큼" },
            { "ABI_UI_TAGS_SMALL", "매우 작음" },
            { "ABI_UI_TAGS_SUGGESTIVE", "선정적" },
            { "ABI_UI_TAGS_NUDITY", "벌거벗음" },
            { "ABI_UI_API_RESPONSE_HEAD", "현재 상태" },
            { "ABI_UI_API_RESPONSES_UPLOADED", "파일이 업로드되었습니다. 파일을 처리하고 있습니다." },
            { "ABI_UI_API_RESPONSES_SECURITY_CHECKING", "보안 시스템이 에셋 번들을 검사중입니다." },
            { "ABI_UI_API_RESPONSES_ENCRYPTING", "에셋 번들이 암호화되고 있습니다." },
            { "ABI_UI_API_RESPONSES_PUSHING", "확인이 완료되었습니다. 저장소로 파일을 전송하고 있습니다." },
            { "ABI_UI_API_RESPONSES_FINISHED", "업로드에 성공했습니다. 게임 내에서 확인 가능합니다." },
        };
    }
}