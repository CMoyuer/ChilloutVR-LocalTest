using System.Collections.Generic;

namespace ABI.CCK.Scripts.Translation
{
    public class German
    {
        public static readonly Dictionary<string, string> Localization = new Dictionary<string, string>()
        {
            {"ABI_UI_BUILDPANEL_HEADING_BUILDER", "Inhaltserstellung"},
            {"ABI_UI_BUILDPANEL_HEADING_SETTINGS", "Einstellungen & Optionen"},
            {"ABI_UI_BUILDPANEL_HEADING_DOCUMENTATION", "Unsere Dokumentation anzeigen"},
            {"ABI_UI_BUILDPANEL_HEADING_FEEDBACK", "Gib uns Feedback"},
            {"ABI_UI_BUILDPANEL_HEADING_FOUNDCONTENT", "Inhalte, die in der aktuellen Szene gefunden wurden"},
            {"ABI_UI_BUILDPANEL_HEADING_ACCOUNT_INFO", "Account Informationen"},
            {"ABI_UI_BUILDPANEL_LOGIN_CREDENTIALS_INCORRECT", "Die Login-Daten sind fehlerhaft"},
            {"ABI_UI_BUILDPANEL_LOGIN_BUTTON", "Anmelden"},
            {"ABI_UI_BUILDPANEL_LOGOUT_BUTTON", "Abmelden"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_TITLE", "Lokale Anmeldeinformationen für die CCK entfernen"},
            {
                "ABI_UI_BUILDPANEL_LOGOUT_DIALOG_BODY",
                "Dies entfernt die lokal gespeicherten Anmeldeinformationen. Du musst dich danach erneut authentifizieren. Möchtest du fortfahren?"
            },
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_ACCEPT", "Ja!"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_DECLINE", "Nein!"},

            {"ABI_UI_BUILDPANEL_UPLOADER_NO_AVATARS_FOUND", "Keine konfigurierten Avatare in der Scene gefunden. - Wurde CVRAvatar hinzugefügt?"},
            {
                "ABI_UI_BUILDPANEL_UPLOADER_NO_SPAWNABLES_FOUND",
                "Keine konfigurierten spawnable objects in der Scene gefunden. - Wurde CVRSpawnable hinzugefügt?"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_WARNING_SPAWNPOINT",
                "Deine Welt hat keine spawn points festgelegt. Bitte füge einen oder mehrere spawn points in der CVRWorld Komponente hinzu. Ansonsten wird der Standort des CVRWorld holder objects genutzt."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_INFO_REFERENCE_CAMERA",
                "Du hast keine Referenz Kamera deiner Welt zugeordnet. Es werden die Standard-Einstellungen der Kamera genutzt."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_INFO_RESPAWN_HEIGHT",
                "Die Respawn-Höhe ist unter -500. Wenn man aus der Map gefallen ist, wird es lange dauern bis man zum Respawn-Punkt wiederkehrt. Das ist warscheinlich nicht etwas was du willst."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_MULTIPLE_CVR_WORLD",
                "Es sind mehrere CVR World Scripte in der scene vorhanden. Die Welt wird somit nicht funktionieren. Bitte stell sicher, dass sich nur ein CVR World object in der scene befindet oder verwende unser CVRWorld-Prefab."
            },
             {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_WORLD_CONTAINS_AVATAR",
                "Geladene Szenen sollten niemals Avatar- und Welten-Scripte gleichzeitig besitzen. Bitte richte deine scenes entsprechend ein."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_NO_CONTENT",
                "Es wurde kein Inhalt in der aktuellen scene gefunden. Hast du vergessen einen descriptor component zu einem game object hinzuzufügen?"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_UNITY_UNSUPPORTED",
                "Du nutzt eine Unity-Version, welche nicht mehr unterstützt wird. Bitte nutze Unity 2019.3.1f1 (das Verwenden vom Unity Hub erleichtert das Versions-Management)."
            },
            {"ABI_UI_BUILDPANEL_UPLOAD_WORLD_BUTTON", "Welt hochladen"},
            {"ABI_UI_BUILDPANEL_UPLOAD_AVATAR_BUTTON", "Avatar hochladen"},
            {"ABI_UI_BUILDPANEL_UPLOAD_PROP_BUTTON", "Prop hochladen"},
            {"ABI_UI_BUILDPANEL_FIX_IMPORT_SETTINGS", "Import-Einstellungen reparieren"},
            {"ABI_UI_BUILDPANEL_UTIL_REMOVE_MISSING_SCRIPTS_BUTTON", "Fehlende Scripts entfernen"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_USERNAME", "Benutzername"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_ACCESSKEY", "Zugangs-Schlüssel"},
            {
                "ABI_UI_BUILDPANEL_INFOTEXT_DOCUMENTATION",
                "Use our documentation to find out more about how to create content for our games. You will also find some handy tutorials on how to utilize most of the core engine features and core game features there."
            },
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN1", "Bitte melde dich mit deinen CCK Daten an."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN2", "Du findest diese auf hub.abinteractive.net."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN3", "Bitte generiere einen CCK Schlüssel im Schlüssel Manager."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_AUTHENTICATED_AS", "Angemeldet als"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_USER_RANK", "API Benutzerrang"},
            {"ABI_UI_BUILDPANEL_SETTINGS_HEADER", "Upload Einstellungen"},
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_ENCRYPTION", "Wechsel die Verbindungs Verschlüsselung:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_ENCRYPTION",
                "Wenn du Probleme mit dem Hochladen hast, versuche zu http zu wechseln."
            },
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_REGION", "Bevorzugte Upload Region:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_REGION",
                "Du kannst deine Bevorzugte Upload Region wechseln um schneller hochzuladen. Wenn deine bevorzugte Region nicht verfügbar ist, wird automatisch eine andere verwendet. Dein Inhlat ist unabhängig von der Upload Region überall erreichbar."
            },
            {"ABI_UI_BUILDPANEL_SETTINGS_CCK_LANGUAGE", "CCK Sprache:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CCK_LANGUAGE",
                "Du kannst hier deine CCK Sprache ändern um die Benutzeroberfläche und Benachrichtigungen in deiner bevorzugten Sprache zu erhalten."
            },
            {
                "ABI_UI_BUILDPANEL_WARNING_FOLDERPATH",
                "Bitte verschiebe nicht den CCK oder CCK Mods Ordner. Dies führt zu Fehlern im CCK."
            },
            {
                "ABI_UI_BUILDPANEL_WARNING_FEEDBACK",
                "Möchtest du eine neue Funktion vorschlagen oder du hast einen Bug gefunden? Schreibe uns auf unserer Feedback Plattform!"
            },
            {"ABI_UI_BUILDPANEL_WARNING_MESH_FILTER_MESH_EMPTY", "Ein MeshFilter ohne Mesh wurde gefunden"},
            {
                "ABI_UI_BUILDPANEL_AVATAR_ERROR_ANIMATOR",
                "Es wurde kein Animator für diesen Avatar gefunden. Stell sicher, dass ein Animator auf dem selben GameObject wie die CVRAvatar Komponente vorhanden ist."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_GENERIC",
                "Dein Avatar Animator hat keinen Avatar gesetzt. Dein Avatar wird als generischer Avatar behandelt."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_POLYGONS",
                "Warnung: Dieser Avatar hat mehr als 100k ({x}) gesamt Polygone. Dies kan zu Leistungsproblemen im Spiel führen. Dies hindert dich nicht am Hochladen."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_SKINNED_MESH_RENDERERS",
                "Warnung: Dieser Avatar hat mehr als 10 ({x}) SkinnedMeshRenderer Komponenten. Dies kan zu Leistungsproblemen im Spiel führen. Dies hindert dich nicht am Hochladen."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_MATERIALS",
                "Warnung: Dieser Avatar hat mehr als 20 ({x}) Material Plätze. Dies kan zu Leistungsproblemen im Spiel führen. Dies hindert dich nicht am Hochladen."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_VIEWPOINT",
                "Warnung: Die Blick Position dieses Avatars liegt bei X=0,Y=0,Z=0. Das bedeutet die Blick Position liegt auf dem Boden. Dies ist warscheinlich nicht was du möchtest."
            },
            {"ABI_UI_BUILDPANEL_AVATAR_WARNING_NON_HUMANOID", "Warnung: Dein Avatar ist nicht als Humanoid eingestellt."},
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_LEGACY_BLENDSHAPES",
                "Warnung: Dieser Avatar wurde ohne \"legacy blend shape normals\" importiert. Dies wird zu einer größeren Dateigröße und Fehler beim Schattenwurf führen."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_POLYGONS",
                "Info: Dieser Avatar hat mehr als 50k ({x}) gesamt Polygone. Dies kan zu Leistungsproblemen im Spiel führen. Dies hindert dich nicht am Hochladen."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SKINNED_MESH_RENDERERS",
                "Info: Dieser Avatar hat mehr als 5 ({x}) SkinnedMeshRenderer Komponenten. Dies kan zu Leistungsproblemen im Spiel führen. Dies hindert dich nicht am Hochladen."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_MATERIALS",
                "Info: Dieser Avatar hat mehr als 10 ({x}) Material Plätze. Dies kan zu Leistungsproblemen im Spiel führen. Dies hindert dich nicht am Hochladen."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SMALL",
                "Info: Die Blick Position bei diesem Avatar liegt tiefer als 0.5 meter. Dieser Avatar sollte als übermäßig klein eingestuft werden"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_HUGE",
                "Info: Die Blick Position bei diesem Avatar liegt höher als 3 meter. Dieser Avatar sollte als übermäßig groß eingestuft werden"
            },
            {"ABI_UI_BUILDPANEL_AVATAR_UPLOAD_BUTTON", "Avatar Hochladen"},
            {
                "ABI_UI_BUILDPANEL_PROPS_ERROR_MISSING_SCRIPT",
                "Das Requisit oder eines seiner unter Objekte enthält fehlende Skripte. Das Hochladen wird so fehlschlagen. Entferne diese Skripte vor dem Hochladen oder klicke den \"Entferne alle fehlenden Skripte\" Knopf, um dies automatisch zu erledigen."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_POLYGONS",
                "Warnung: Dieses Requisit hat mehr als 100k ({x}) gesamt Polygone. Dies kan zu Leistungsproblemen im Spiel führen. Dies hindert dich nicht am Hochladen."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_SKINNED_MESH_RENDERERS",
                "Warnung: Dieses Requisit hat mehr als 10 ({x}) SkinnedMeshRenderer Komponenten. Dies kan zu Leistungsproblemen im Spiel führen. Dies hindert dich nicht am Hochladen."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_MATERIALS",
                "Warnung: Dieses Requisit hat mehr als 20 ({x}) Material Plätze. Dies kan zu Leistungsproblemen im Spiel führen. Dies hindert dich nicht am Hochladen."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_LEGACY_BLENDSHAPES",
                "Warnung: Dieses Requisit wurde ohne \"legacy blend shape normals\" importiert. Dies wird zu einer größeren Dateigröße und Fehler beim Schattenwurf führen."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_POLYGONS",
                "Info: Dieses Requisit hat mehr als 50k ({x}) gesamt Polygone. Dies kan zu Leistungsproblemen im Spiel führen. Dies hindert dich nicht am Hochladen."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_SKINNED_MESH_RENDERERS",
                "Info: Dieses Requisit hat mehr als 5 ({x}) SkinnedMeshRenderer Komponenten. Dies kan zu Leistungsproblemen im Spiel führen. Dies hindert dich nicht am Hochladen."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_MATERIALS",
                "Info: Dieses Requisit hat mehr als 10 ({x}) Material Plätze. Dies kan zu Leistungsproblemen im Spiel führen. Dies hindert dich nicht am Hochladen."
            },
            {"ABI_UI_BUILDPANEL_PROPS_UPLOAD_BUTTON", "Requisit (Prop) hochladen"},
            {
                "ABI_UI_BUILDPANEL_ERROR_WORLD_MISSING_SCRIPTS",
                "Die Szene enthält fehlende Skripte. Das Hochladen wird so fehlschlagen. Entferne diese Skripte vor dem Hochladen oder klicke den \"Entferne alle fehlenden Skripte\" Knopf, um dies automatisch zu erledigen."
            },
            {
                "ABI_UI_BUILDPANEL_ERROR_AVATAR_MISSING_SCRIPTS",
                "Der Avatar oder eines seiner unter Objekte enthält fehlende Skripte. Das Hochladen wird so fehlschlagen. Entferne diese Skripte vor dem Hochladen oder klicke den \"Entferne alle fehlenden Skripte\" Knopf, um dies automatisch zu erledigen."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_MULTIPLE_TRIGGER_HELPBOX",
                "Mehrere \"Trigger\" auf dem selben GameObject führen zu unvorhersehbarem Verhalten."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_POINTERS_HELPBOX",
                "Wenn Pointer zu der \"Allowed Pointers\" Liste hinzugefügt werden, Werden alle pointer ignoriert, die nicht in der Liste sind."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_TYPES_HELPBOX",
                "Wenn Typen zu der \"Allowed Types\" Liste hinzugefügt werden, Werden alle pointer ignoriert, die nicht einen der angegebenen Typen haben."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_PARTICLE_HELPBOX",
                "Wenn diese Option ausgewählt ist, reagiert der Trigger mit dem \"On Enter Trigger\" auch auf Partikel Systeme, die einen Pointer besitzen."
            },
            {
                "ABI_UI_INFOTEXT_WORLDS_NO_AVATARS",
                "Ein ChilloutVR World Objekt wurde in der Scene gefunden. Avatare können nicht hochgeladen werden, bis das World Objekt entfernt wurde. Avatars und Requisiten werden teil der Welt sein, wenn sie nicht entfernt oder ausgeschaltet werden."
            },
            {
                "ABI_UI_ASSET_INFO_HEADER_INFORMATION",
                "Dieses Skript speichert die Meta Daten des Objekts. Bearbeite nicht diese Daten, wenn du nicht weißt was du tust. Um einen Avatar erneut hochzuladen, entferne die Objekt ID und lade neu hoch."
            },
            {"ABI_UI_ASSET_INFO_GUID_LABEL", "Die aktuell gespeicherte Objekt ID ist: "},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON", "Entferne Objekt ID"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_TITLE", "Entferne Objekt ID vom Asset Info Manager"},
            {
                "ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_BODY",
                "Die Objekt ID wird vom Objekt gelöst. Dies bedeutet, dass der Inhalt neu hochgeladen wird. Fortsetzen?"
            },
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_ACCEPT", "Ja!"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_DENY", "Nein!"},
            {"ABI_UI_ASSET_INFO_COPY_BUTTON", "Kopiere Objekt ID"},
            {"ABI_UI_ASSET_INFO_ATTACH_LABEL", "Objekt ID"},
            {
                "ABI_UI_ASSET_INFO_ATTACH_INFO",
                "Du musst keine neue Objekt ID hinzufügen, wenn du keinen ALten Inhalt aktualisieren möchtest. Eine neue ID wird generiert, wenn keine vorhanden ist."
            },
            {"ABI_UI_ASSET_INFO_ATTACH_BUTTON", "Objekt ID setzen"},
            {
                "ABI_UI_AVATAR_INFO_VIEWPOINT",
                "Dieser Punkt kontroliert die Kamera Position. Er sollte zwischen den Augen liegen."
            },
            {
                "ABI_UI_AVATAR_INFO_VOICE_POSITION",
                "Dieser Punkt kontroliert die Audio Position. Er sollte beim Mund liegen."
            },
            {
                "ABI_UI_AVATAR_INFO_OVERRIDE_CONTROLLER",
                "Damit die Animationen richtig funktionieren, stell sicher, dass der richtige animator in dem override controller zugewiesen ist. Einen beispiel Controller findest du im CCK Prefab Ordner."
            },
            {
                "ABI_UI_AVATAR_INFO_BLinking",
                "Blinzel blend shapes müssen nicht verwendet werden. Wenn eingeschaltet, werden zur Laufzeit zufällige intervalle generiert."
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_MOVEMENT",
                "Wenn diese Option eingeschalten ist, werden sie die die Augen nahezu realistisch bewegen."
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_VISEMES",
                "Damit die automatische viseme erkennung funktioniert, musst du zuerst das Mesh des Gesichtes auswählen. Dieses ist das Mesh mit dem namen \"body\" in den meisten Fällen."
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_TITLE",
                "Es fehlt eine Voraussätzungen im Projekt."
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_WARNING_PREFACE",
                "Die folgenden Voraussetzungen sind nicht erfüllt"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_FINAL_WARNING",
                "Bitte installiere alle Voraussetzungen, bevor du dieses Modul installierst!"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_DIALOG_ACCEPT",
                "Verstanden"
            },
            { "ABI_UI_BUILD_RUNTIME_HEADER", "Lade Inhalt zu ChilloutVR hoch" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEXT", "Weiter zum nächsten Schritt" },
            { "ABI_UI_BUILD_RUNTIME_BTN_PREV", "Zurück zum voherigen Schritt" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEW_PICTURE", "Bild ersetzen" },
            { "ABI_UI_BUILDSTEP_FILTERTAGS", "Filter Tags" },
            { "ABI_UI_BUILDSTEP_DETAILS", "Details" },
            { "ABI_UI_BUILDSTEP_LEGAL", "Rechtliche Bestätigung" },
            { "ABI_UI_BUILDSTEP_UPLOAD", "Inhalt hochladen" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_ROW", "Name:" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_ROW", "Beschreibung:" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_PLACEHOLDER", "Objekt Name (benötigt!)" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_PLACEHOLDER", "Objekt Beschreibung" },
            { "ABI_UI_BUILDSTEP_DETAILS_CHANGELOG_PLACEHOLDER", "Objekt Änderungen - Sage uns, was du geändert oder hinzugefügt hast" },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_NEW_OBJECT", 
                "Dieses Objekt wird zum ersten mal hochgeldaen. Dabei muss ein Bild hochgeladen werden. Es gibt keine Option das Objekt ohne Bild hochzuladen."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_UPDATING_OBJECT", 
                "Du bist dabei dieses Objekt zu aktualisieren. Die Beschreibung oder den Namen kannst du nicht während des aktualisieren ändern. Besuche hier für bitte den Hub."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_SET_ACTIVE_FILE", 
                "Markieren diesen Stand als den aktuell verwendeten für die aktuelle Plattform"
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_PERMISSION", 
                "Ich bestätige hiermit, dass der von mir hochgeladene Inhalt mir gehört oder ich entsprechende Nutzungslizenzen besitze. Ich weiß, dass das Hochladen von Kopiergeschützung Inhalten ohne die Zustimmung des Authors zu rechtlichen Konsequenzen und Beschränkungen meines Benutzerkontos führen können. Ich weiß, dass ich mich an die Inhalts erstellungs Regeln, die in den Konditiotionen von Alpha Blend Interactive geschrieben stehen halten muss."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_TAGS", 
                "Ich bestätige hiermit, dass ich meine Inhalt mit den korrekten Tags versehen habe. I weiß, das das bewusste fälschliche Taggen ein Schwerer Regelverstoß ist. Ich weiß, das mein Account dafür gestraft werden kann, dass ich bewusst falsche Tags setze."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_STEP_DETAILS", 
                "Dein Inhalt wird zu unserem Netzwerk hochgeladen. Der Prozess ist in mehrere Schritte unterteilt. Nachdem der Inhalt zu unserem Netzwerk hochgeladen wurde, wird dieser automatisch auf sicherheitsrisiken überprüft. Wenn der Inhalt diese Tests bestanden hat, wird der Inhalt verschlüsselt und auf unserCDN Netzwerk verteilt. Du kannst den aktuelle Status unten sehen."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_DETAILS_MISSING", 
                "Um Inhalt auf unsere Plattform hochzuladen, muss ein Name vergeben werden. Wenn du ein Objekt neu hochlädst, stell sicher, dass du einen sprechenden namen vergibst. Du wirst nun zurück zur Detailseite geleitet um einen Namen zu vergeben."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_LEGAL_MISSING", 
                "Um Inhalt auf unsere Plattform hochzuladen, musst du die Berechtigung haben diesen Inhalt hochladen zu dürfen und musst die Tags richtig setzen. Du wirst nun zur Rechtlichen Seite zurückgeleitet um die Rechtliche Bestätigung zu bestätigen."
            },
            { "ABI_UI_DETAILS_HEAD_CHANGELOG", "Inhalts Änderungen" },
            { "ABI_UI_DETAILS_HEAD_STATISTICS", "Datei Statistiken" },
            { "ABI_UI_LEGAL_HEAD_OWNERSHIP", "Rechtliche Bestätigung: Eigentum & Urheberrechte" },
            { "ABI_UI_LEGAL_HEAD_TAGS", "Rechtliche Bestätigung: Tagging" },
            { "ABI_UI_TAGS_HEADER_AUDIO", "Hörbare Erfahrung" },
            { "ABI_UI_TAGS_HEADER_VISUAL", "Visuelle Erfahrung" },
            { "ABI_UI_TAGS_HEADER_CONTENT", "Inhalt" },
            { "ABI_UI_TAGS_HEADER_NSFW", "Altersbeschränkung" },
            { "ABI_UI_TAGS_LOUD_AUDIO", "Lauter Klang" },
            { "ABI_UI_TAGS_LR_AUDIO", "Weit reichender Klang" },
            { "ABI_UI_TAGS_SPAWN_AUDIO", "Erscheinings Klang" },
            { "ABI_UI_TAGS_CONTAINS_MUSIC", "Enthält Musik" },
            { "ABI_UI_TAGS_FLASHING_COLORS", "Blitzende Farben" },
            { "ABI_UI_TAGS_FLASHING_LIGHTS", "Blitzende Lichter" },
            { "ABI_UI_TAGS_EXTREMELY_BRIGHT", "Sehr Hell" },
            { "ABI_UI_TAGS_SCREEN_EFFECTS", "Bildschirmeffekte" },
            { "ABI_UI_TAGS_PARTICLE_SYSTEMS", "Partikel Systeme" },
            { "ABI_UI_TAGS_VIOLENCE", "Gewalt" },
            { "ABI_UI_TAGS_GORE", "Blut und Eingeweide" },
            { "ABI_UI_TAGS_HORROR", "Grusel" },
            { "ABI_UI_TAGS_JUMPSCARE", "Jumpscare" },
            { "ABI_UI_TAGS_HUGE", "Extrem Groß" },
            { "ABI_UI_TAGS_SMALL", "Extrem Klein" },
            { "ABI_UI_TAGS_SUGGESTIVE", "anzüglich" },
            { "ABI_UI_TAGS_NUDITY", "Nacktheit" },
            { "ABI_UI_API_RESPONSE_HEAD", "Aktueller Status" },
            { "ABI_UI_API_RESPONSES_UPLOADED", "Datei ist hochgeladen. Sie wird nun weiterverarbeitet." },
            { "ABI_UI_API_RESPONSES_SECURITY_CHECKING", "Dein Inhalt wird gerade von unserem Sicherheitssystem überprüft." },
            { "ABI_UI_API_RESPONSES_ENCRYPTING", "Dein Inhalt wird gerade verschlüsselt." },
            { "ABI_UI_API_RESPONSES_PUSHING", "Die Überprüfungen sind vollständig. Dein Inhalt wird gerade zu unserem Speichernetwerk übertragen." },
            { "ABI_UI_API_RESPONSES_FINISHED", "Übertragung vollständig. Dein Inhalt ist jetzt im Spiel verfügbar." },
        };
    }
}