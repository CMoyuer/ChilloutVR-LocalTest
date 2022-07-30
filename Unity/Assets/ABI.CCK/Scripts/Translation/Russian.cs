using System.Collections.Generic;

namespace ABI.CCK.Scripts.Translation
{
    public class Russian
    {
        public static readonly Dictionary<string, string> Localization = new Dictionary<string, string>()
        {
            {"ABI_UI_BUILDPANEL_HEADING_BUILDER", "Content Builder"},
            {"ABI_UI_BUILDPANEL_HEADING_SETTINGS", "Настройки и опции"},
            {"ABI_UI_BUILDPANEL_HEADING_DOCUMENTATION", "Документация"},
            {"ABI_UI_BUILDPANEL_HEADING_FEEDBACK", "Обратная связь"},
            {"ABI_UI_BUILDPANEL_HEADING_FOUNDCONTENT", "Контент в активной сцене"},
            {"ABI_UI_BUILDPANEL_HEADING_ACCOUNT_INFO", "Информация об аккаунте"},
            {"ABI_UI_BUILDPANEL_LOGIN_CREDENTIALS_INCORRECT", "Неверный логин и/или ключ"},
            {"ABI_UI_BUILDPANEL_LOGIN_BUTTON", "Войти"},
            {"ABI_UI_BUILDPANEL_LOGOUT_BUTTON", "Выйти"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_TITLE", "Удалить сохраненные логин и ключ для CCK"},
            {
                "ABI_UI_BUILDPANEL_LOGOUT_DIALOG_BODY",
                "Это удалит сохраненные логин и ключ. Их нужно будет ввести заново. Продолжить?"
            },
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_ACCEPT", "Да!"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_DECLINE", "Нет!"},

            {"ABI_UI_BUILDPANEL_UPLOADER_NO_AVATARS_FOUND", "В сцене нет настроенных аватаров - Добавлен ли CVRAvatar?"},
            {
                "ABI_UI_BUILDPANEL_UPLOADER_NO_SPAWNABLES_FOUND",
                "В сцене нет настроенных предметов - Добавлен ли CVRSpawnable?"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_WARNING_SPAWNPOINT",
                "В мире не заданы стартовые позиции. Пожалуйста, добавьте одну или несколько позиций в компоненте CVRWorld, иначе будет использоваться позиция объекта с CVRWorld."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_INFO_REFERENCE_CAMERA",
                "Не задана эталонная камера для мира. Будут использованы настройки камеры по умолчанию."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_INFO_RESPAWN_HEIGHT",
                "Высота респавна ниже чем -500. Падать до нее будет очень долго. Вероятно это не то чего Вы хотите."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_MULTIPLE_CVR_WORLD",
                "В сцене есть несколько объектов с CVRWorld. Это поломает мир. Пожалуйста, убедитесь что в сцене есть только один объект с CVRWorld, или используйте наш префаб."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_WORLD_CONTAINS_AVATAR",
                "Загруженные сцены не должны содержать одновременно аватары и миры. Пожалуйста, настройте Ваши сцены соответственно."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_NO_CONTENT",
                "В текущей сцене нет контента. Возможно Вы забыли добавить компонент-дескриптор на объект?"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_UNITY_UNSUPPORTED",
                "Вы используете неподдерживаемую версию Unity. Пожалуйста, используйте Unity 2019.3.1f1 (Unity Hub сильно упрощает установку разных версий)."
            },
            {"ABI_UI_BUILDPANEL_UPLOAD_WORLD_BUTTON", "Загрузить мир"},
            {"ABI_UI_BUILDPANEL_UPLOAD_AVATAR_BUTTON", "Загрузить аватар"},
            {"ABI_UI_BUILDPANEL_UPLOAD_PROP_BUTTON", "Загрузить предмет"},
            {"ABI_UI_BUILDPANEL_FIX_IMPORT_SETTINGS", "Исправить настройки импорта"},
            {"ABI_UI_BUILDPANEL_UTIL_REMOVE_MISSING_SCRIPTS_BUTTON", "Удалить отсутствующие скрипты"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_USERNAME", "Имя пользователя"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_ACCESSKEY", "Ключ доступа"},
            {
                "ABI_UI_BUILDPANEL_INFOTEXT_DOCUMENTATION",
                "Используйте нашу документацию, чтобы научиться создавать контент для наших игр. Также там находятся туториалы по использованию всех основных элементов Unity и наших игр."
            },
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN1", "Пожалуйста войдите используя Ваши логин и ключ доступа для CCK."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN2", "Их можно найти на hub.abinteractive.net."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN3", "Пожалуйста создайте ключ для CCK в менеджере ключей"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_AUTHENTICATED_AS", "Вы вошли как"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_USER_RANK", "Ранг пользователя в API"},
            {"ABI_UI_BUILDPANEL_SETTINGS_HEADER", "Настройки загрузки"},
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_ENCRYPTION", "Переключить шифрование подключения:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_ENCRYPTION",
                "Если Вы испытываете проблемы с загрузкой, попробуйте переключиться на http"
            },
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_REGION", "Предпочтительный Регион для загрузки:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_REGION",
                "Вы можете выбрать предпочтительный регион, чтобы увеличить скорость загрузки. Если выбранный регион недоступен, другой регион будет выбран автоматически. Ваш контент будет доступен везде, независимо от региона, выбранного для загрузки."
            },
            {"ABI_UI_BUILDPANEL_SETTINGS_CCK_LANGUAGE", "Язык CCK:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CCK_LANGUAGE",
                "Вы можете выбрать язык CCK для нотификаций и пользовательского интерфейса."
            },
            {
                "ABI_UI_BUILDPANEL_WARNING_FOLDERPATH",
                "Пожалуйста не перемещайте папки CCK и CCK Mods. Это может поломать CCK."
            },
            {
                "ABI_UI_BUILDPANEL_WARNING_FEEDBACK",
                "Хотите запросить новую фичу? Нашли баг? Пишите нам на нашей платформе обратной связи!"
            },
            {"ABI_UI_BUILDPANEL_WARNING_MESH_FILTER_MESH_EMPTY", "Обнаружен MeshFilter с отсутствующим Mesh"},
            {
                "ABI_UI_BUILDPANEL_AVATAR_ERROR_ANIMATOR",
                "На данном аватаре нет компоненты Animator. Убедитесь что Animator находится на том же объекте что и CVRAvatar."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_GENERIC",
                "Поле Avatar в компоненте Animator не заполнено. Ваш аватар будет считаться как generic (не humanoid)"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_POLYGONS",
                "Предупреждение: Этот аватар содержит более 100000 ({X}) полигонов. Это может ухудшить производительность в игре. Вы все еще может загрузить этот аватар."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_SKINNED_MESH_RENDERERS",
                "Предупреждение: Этот аватар содержит более чем 10 ({X}) компонент SkinnedMeshRenderer. Это может ухудшить производительность в игре. Вы все еще может загрузить этот аватар."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_MATERIALS",
                "Предупреждение: Этот аватар использует более чем 20 ({X}) слотов материалов. Это может ухудшить производительность в игре. Вы все еще может загрузить этот аватар."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_VIEWPOINT",
                "Предупреждение: Позиция камеры на аватаре задана на X=0,Y=0,Z=0 по умолчанию. Это означает, что она находится на земле. Вероятно это не то чего Вы хотите."
            },
            {"ABI_UI_BUILDPANEL_AVATAR_WARNING_NON_HUMANOID", "Предупреждение: Ваш аватар не настроен как humanoid."},
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_LEGACY_BLENDSHAPES",
                "Предупреждение: Этот аватар использует non-legacy blend shape normals (настройка импорта модели). Это увеличит размер файла и приведет к проблемам с освещением на аватаре."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_POLYGONS",
                "Информация: Этот аватар содержит более 50000 ({X}) полигонов. Это может ухудшить производительность в игре. Вы все еще может загрузить этот аватар."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SKINNED_MESH_RENDERERS",
                "Информация: Этот аватар содержит более 5 ({X}) компонент SkinnedMeshRenderer. Это может ухудшить производительность в игре. Вы все еще может загрузить этот аватар."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_MATERIALS",
                "Информация: Этот аватар использует более 10 ({X}) слотов материалов. Это может ухудшить производительность в игре. Вы все еще может загрузить этот аватар."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SMALL",
                "Информация: Позиция камеры на этом аватаре ниже чем 0.5 по высоте. Этот аватар считается слишком маленьким."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_HUGE",
                "Информация: Позиция камеры на этом аватаре выше чем 3.0 по высоте. Этот аватар считается слишком большим."
            },
            {"ABI_UI_BUILDPANEL_AVATAR_UPLOAD_BUTTON", "Загрузить аватар"},
            {
                "ABI_UI_BUILDPANEL_PROPS_ERROR_MISSING_SCRIPT",
                "Предмет или его дети содержат отсутствующие скрипты. Загрузить такой предмет не получится. Удалите все отсутствующие скрипты прежде чем загружать этот предмет, или нажмите \"Удалить отсутствующие скрипты\", чтобы сделать это автоматически."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_POLYGONS",
                "Предупреждение: Этот предмет содержит более 100000 ({X}) полигонов. Это может ухудшить производительность в игре. Вы все еще может загрузить этот предмет."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_SKINNED_MESH_RENDERERS",
                "Предупреждение: Этот предмет содержит более 10 ({X}) компонент SkinnedMeshRenderer. Это может ухудшить производительность в игре. Вы все еще может загрузить этот предмет."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_MATERIALS",
                "Предупреждение: Этот предмет использует более 20 ({X}) слотов материалов. Это может ухудшить производительность в игре. Вы все еще может загрузить этот предмет."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_LEGACY_BLENDSHAPES",
                "Предупреждение: Этот предмет использует non-legacy blend shape normals (настройка импорта модели). Это увеличит размер файла и приведет к проблемам с освещением на предмете."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_POLYGONS",
                "Информация: Этот предмет содержит более 50000 ({X}) полигонов. Это может ухудшить производительность в игре. Вы все еще может загрузить этот предмет."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_SKINNED_MESH_RENDERERS",
                "Информация: Этот предмет содержит более 5 ({X}) компонент SkinnedMeshRenderer. Это может ухудшить производительность в игре. Вы все еще может загрузить этот предмет."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_MATERIALS",
                "Информация: Этот предмет использует более 10 ({X}) слотов материалов. Это может ухудшить производительность в игре. Вы все еще может загрузить этот предмет."
            },
            {"ABI_UI_BUILDPANEL_PROPS_UPLOAD_BUTTON", "Загрузить предмет (Spawnable Object/Prop)"},
            {
                "ABI_UI_BUILDPANEL_ERROR_WORLD_MISSING_SCRIPTS",
                "Сцена содержит отсутствующие скрипты. Загрузить такой мир не получится. Удалите все отсутствующие скрипты прежде чем загружать этот мир, или нажмите \"Удалить отсутствующие скрипты\", чтобы сделать это автоматически."
            },
            {
                "ABI_UI_BUILDPANEL_ERROR_AVATAR_MISSING_SCRIPTS",
                "Аватар или его дети содержат отсутствующие скрипты. Загрузить такой аватар не получится. Удалите все отсутствующие скрипты прежде чем загружать этот аватар, или нажмите \"Удалить отсутствующие скрипты\", чтобы сделать это автоматически."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_MULTIPLE_TRIGGER_HELPBOX",
                "Наличие нескольких триггеров на одном объекте приводит к непредсказуемому поведению!"
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_POINTERS_HELPBOX",
                "Добавление Pointer'ов в список \"Allowed Pointers\" приведет к игнорированию всех Pointer'ов которых нет в списке."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_TYPES_HELPBOX",
                "Добавление типов в список \"Allowed Types\" приведет к игнорированию всех Pointer'ов которые не соответствуют типам в списке."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_PARTICLE_HELPBOX",
                "Эта опция разрешает Particle System с Pointer на том же объекте активировать этот триггер. Частицы могут активировать только On Enter Trigger."
            },
            {
                "ABI_UI_INFOTEXT_WORLDS_NO_AVATARS",
                "В сцене есть объект с ChilloutVR World. Аватары не смогут быть загружены до тех пор пока объект с CVRWorld не будет удален. Аватары / предметы будут частью мира и видимы в нем если их не выключить или удалить."
            },
            {
                "ABI_UI_ASSET_INFO_HEADER_INFORMATION",
                "Этот скрипт используется, чтобы хранить метаданные об объекте. Пожалуйста, не меняйте данные в нем если вы не знаете что вы делаете. Чтобы загрузить еще одну копию аватара, отсоедините Guid и загрузите аватар."
            },
            {"ABI_UI_ASSET_INFO_GUID_LABEL", "Сохраненный Guid: "},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON", "Отсоединить уникальный идентификатор"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_TITLE", "Отсоединить Guid от компоненты Asset Info Manager"},
            {
                "ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_BODY",
                "Уникальный идентификатор будет отсоединен. Это означает что Ваш контент будет загружен как еще одна копия (вместо перезаписи существующего). Продолжить?"
            },
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_ACCEPT", "Да!"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_DENY", "Нет!"},
            {"ABI_UI_ASSET_INFO_ATTACH_LABEL", "Уникальный идентификатор"},
            {
                "ABI_UI_ASSET_INFO_ATTACH_INFO",
                "Присоединение Guid нужно только для того, чтобы перезаписать уже загруженный контент. Новый Guid будет создан при загрузке контента без присоединенного Guid."
            },
            {"ABI_UI_ASSET_INFO_ATTACH_BUTTON", "Присоединить guid"},
            {
                "ABI_UI_AVATAR_INFO_VIEWPOINT",
                "Настраивает позицию в которой будет находиться камера игрока. Она должна быть между глаз."
            },
            {
                "ABI_UI_AVATAR_INFO_VOICE_POSITION",
                "Настраивает позицию из которой слышен голос в игре. Она должна быть около рта."
            },
            {
                "ABI_UI_AVATAR_INFO_OVERRIDE_CONTROLLER",
                "Чтобы переопределения работали, убедитесь что в override controller указан правильный базовый аниматор. Иначе вы не увидите слоты для переопределения анимаций. Пример этого можно найти в папке CCK Player Prefabs."
            },
            {
                "ABI_UI_AVATAR_INFO_BLinking",
                "Blend shape для мигания необязательный. Его можно включить чтобы аватар иногда мигал глазами в игре."
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_MOVEMENT",
                "Эта опция включает относительно реалистичную анимацию глаз."
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_VISEMES",
                "Для использования автоматического выбора визем нужно сперва выбрать меш включающий в себя лицо аватара. Обычно это меш с названием Body."
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_TITLE",
                "В проекте отсутствуют зависимости!"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_WARNING_PREFACE",
                "Следующие зависимости не выполняются"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_FINAL_WARNING",
                "Пожалуйста, установите все зависимости перед установкой этого модуля!"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_DIALOG_ACCEPT",
                "Понятно"
            },
            { "ABI_UI_BUILD_RUNTIME_HEADER", "Загрузить контент в ChilloutVR" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEXT", "Перейти к следующему шагу" },
            { "ABI_UI_BUILD_RUNTIME_BTN_PREV", "Вернуться к предыдущему шагу" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEW_PICTURE", "Заменить изображение" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_ASSETBUNDLE", "Размер пакета с активами" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_IMAGE", "Размер файла изображения" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_MANIFEST", "Размер файла манифеста" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_PANO1K", "Размер файла панорамного изображения (1080P)" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_PANO4K", "Размер файла панорамного изображения (4K)" },
            { "ABI_UI_BUILD_RUNTIME_HINT_CLICK_TO_CAPTURE", "Нажмите на уменьшенное изображение, чтобы сделать миниатюру" },
            { "ABI_UI_BUILDSTEP_FILTERTAGS", "Теги Фильтров" },
            { "ABI_UI_BUILDSTEP_DETAILS", "Детали" },
            { "ABI_UI_BUILDSTEP_LEGAL", "Юридическая гарантия" },
            { "ABI_UI_BUILDSTEP_UPLOAD", "Загрузка контента" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_ROW", "Имя:" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_ROW", "Описание:" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_PLACEHOLDER", "Имя объекта (обязательно!)" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_PLACEHOLDER", "Описание объекта" },
            { "ABI_UI_BUILDSTEP_DETAILS_CHANGELOG_PLACEHOLDER", "Журнал изменений - расскажите пользователям о том, что вы изменили или добавили" },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_NEW_OBJECT",
                "Этот объект загружается впервые. Загрузка фотографии профиля обязательна, поэтому вам необходимо загрузить изображение."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_UPDATING_OBJECT",
                "Вы собираетесь обновить этот объект. Изменение описания или названия недоступно при обновлении этого объекта. При необходимости измените их в Хабе."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_SET_ACTIVE_FILE",
                "Установите эту загрузку в качестве активного файла для целевой платформы"
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_PERMISSION",
                "Настоящим я подтверждаю, что загружаемый мной контент принадлежит мне или имеет лицензию. Я знаю, что загрузка защищенного авторским правом контента без разрешения авторов может привести к блокировке моего аккаунта и/или юридическим последствиям. Я знаю, что должен полностью соблюдать все правила создания контента, указанные в условиях предоставления услуг Alpha Blend Interactive."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_TAGS",
                "Настоящим я подтверждаю, что теги установлены правильно и соответствуют загруженному содержимому. Я знаю, что сознательная установка неправильных тегов является серьезным правонарушением. Я знаю, что мой аккаунт будет наказан, если я буду постоянно устанавливать неправильные теги."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_STEP_DETAILS",
                "Сейчас ваш контент загружается в нашу сеть. Процесс загрузки разбит на различные этапы. После загрузки файла в нашу сеть, файл пройдет автоматические проверки безопасности, после их прохождения мы зашифруем ваш пакет и отправим его на наш CDN. Вы можете проверить текущий статус вашей загрузки ниже."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_DETAILS_MISSING",
                "Для загрузки контента на нашу платформу требуется имя. При загрузке нового объекта не забудьте дать ему соответствующее имя. Вы будете отправлены обратно на страницу деталей для ввода имени."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_LEGAL_MISSING",
                "Для загрузки на нашу платформу вы должны подтвердить, что вам разрешено загружать указанный контент и что все установленные теги верны. Вы будете отправлены обратно на юридическую страницу, чтобы просмотреть и принять юридическую гарантию."
            },
            { "ABI_UI_DETAILS_HEAD_CHANGELOG", "Изменения в контенте" },
            { "ABI_UI_DETAILS_HEAD_STATISTICS", "Статистика файлов" },
            { "ABI_UI_LEGAL_HEAD_OWNERSHIP", "Юридическая гарантия: Право собственности и авторское право" },
            { "ABI_UI_LEGAL_HEAD_TAGS", "Юридическая гарантия: Теги" },
            { "ABI_UI_TAGS_HEADER_AUDIO", "Аудиальный опыт" },
            { "ABI_UI_TAGS_HEADER_VISUAL", "Визуальный опыт" },
            { "ABI_UI_TAGS_HEADER_CONTENT", "Контент" },
            { "ABI_UI_TAGS_HEADER_NSFW", "Возрастная классификация" },
            { "ABI_UI_TAGS_LOUD_AUDIO", "Громкое аудио" },
            { "ABI_UI_TAGS_LR_AUDIO", "Аудио дальнего радиуса действия" },
            { "ABI_UI_TAGS_SPAWN_AUDIO", "Спаун аудио" },
            { "ABI_UI_TAGS_CONTAINS_MUSIC", "Содержит музыку" },
            { "ABI_UI_TAGS_FLASHING_COLORS", "Мигающие цвета" },
            { "ABI_UI_TAGS_FLASHING_LIGHTS", "Мигающий свет" },
            { "ABI_UI_TAGS_EXTREMELY_BRIGHT", "Очень яркий" },
            { "ABI_UI_TAGS_SCREEN_EFFECTS", "Экранные эффекты" },
            { "ABI_UI_TAGS_PARTICLE_SYSTEMS", "Системы частиц" },
            { "ABI_UI_TAGS_VIOLENCE", "Насилие" },
            { "ABI_UI_TAGS_GORE", "Гуро" },
            { "ABI_UI_TAGS_HORROR", "Ужас" },
            { "ABI_UI_TAGS_JUMPSCARE", "Джампскейр" },
            { "ABI_UI_TAGS_HUGE", "Чрезмерно большой" },
            { "ABI_UI_TAGS_SMALL", "Чрезмерно маленький" },
            { "ABI_UI_TAGS_SUGGESTIVE", "Суггестивный" },
            { "ABI_UI_TAGS_NUDITY", "Нагота" },
            { "ABI_UI_API_RESPONSE_HEAD", "Текущее состояние" },
            { "ABI_UI_API_RESPONSES_UPLOADED", "Файл загружен. Теперь обрабатываем файл." },
            { "ABI_UI_API_RESPONSES_SECURITY_CHECKING", "Пакет с активами сейчас проверяется нашей системой безопасности." },
            { "ABI_UI_API_RESPONSES_ENCRYPTING", "Ваш файл пакета сейчас зашифровывается." },
            { "ABI_UI_API_RESPONSES_PUSHING", "Проверка завершена. Сейчас файл передается в наше хранилище." },
            { "ABI_UI_API_RESPONSES_FINISHED", "Загрузка завершена. Ваш контент теперь доступен в игре." },
        };
    }
}