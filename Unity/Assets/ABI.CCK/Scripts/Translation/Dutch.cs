using System.Collections.Generic;

namespace ABI.CCK.Scripts.Translation
{
    public class Dutch
    {
        public static readonly Dictionary<string, string> Localization = new Dictionary<string, string>()
        {
            {"ABI_UI_BUILDPANEL_HEADING_BUILDER", "Content Bouwer"},
            {"ABI_UI_BUILDPANEL_HEADING_SETTINGS", "Instellingen en Opties"},
            {"ABI_UI_BUILDPANEL_HEADING_DOCUMENTATION", "Bekijk onze documentatie"},
            {"ABI_UI_BUILDPANEL_HEADING_FEEDBACK", "Stuur Feedback"},
            {"ABI_UI_BUILDPANEL_HEADING_FOUNDCONTENT", "Gevonden Content in Actieve Scene"},
            {"ABI_UI_BUILDPANEL_HEADING_ACCOUNT_INFO", "Account Informatie"},
            {"ABI_UI_BUILDPANEL_LOGIN_CREDENTIALS_INCORRECT", "De opgegeven inloggegevens zijn onjuist."},
            {"ABI_UI_BUILDPANEL_LOGIN_BUTTON", "Inloggen"},
            {"ABI_UI_BUILDPANEL_LOGOUT_BUTTON", "Uitloggen"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_TITLE", "Lokale inloggegevens voor CCK verwijderen"},
            {
                "ABI_UI_BUILDPANEL_LOGOUT_DIALOG_BODY",
                "Hiermee worden de lokaal opgeslagen referenties verwijderd. U zult zich opnieuw moeten verifiëren. Wil je doorgaan?"
            },
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_ACCEPT", "Ja!"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_DECLINE", "Nee!"},

            {"ABI_UI_BUILDPANEL_UPLOADER_NO_AVATARS_FOUND", "Geen geconfigureerde avatars gevonden in scène - CVRAvatar component toegevoegd?"},
            {
                "ABI_UI_BUILDPANEL_UPLOADER_NO_SPAWNABLES_FOUND",
                "Geen geconfigureerde spawnbare objecten gevonden in scene - CVRSpawnable component toegevoegd?"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_WARNING_SPAWNPOINT",
                "Je wereld heeft geen spawn-posities toegewezen. Voeg een of meerdere spawn posities toe in de CVRWorld-component, anders wordt de locatie van het standaard CVRWorld gebruikt."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_INFO_REFERENCE_CAMERA",
                "Je hebt geen referentiecamera toegewezen aan je wereld. Standaard camera-instellingen worden gebruikt."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_INFO_RESPAWN_HEIGHT",
                "De respawn-hoogte is minder dan -500. Het duurt lang om te respawnen als je uit de map valt. Dit is waarschijnlijk niet wat je wilt."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_MULTIPLE_CVR_WORLD",
                "Er zijn meerdere CVR World-objecten in de scène aanwezig. Dit zal de map breken. Zorg ervoor dat er slechts één CVR World-object in de scène is of gebruik onze prefab CVRWorld."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_WORLD_CONTAINS_AVATAR",
                "Geladen scènes mogen nooit zowel avatar- als werelddescriptorobjecten bevatten. Stel uw scènes dienovereenkomstig in."
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_NO_CONTENT",
                "Geen inhoud gevonden in huidige scène. Ben je vergeten een descriptorcomponent toe te voegen aan een game-object?"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_UNITY_UNSUPPORTED",
                "U gebruikt een unity-versie die niet wordt ondersteund. Gebruik een ondersteunde unity-versie. U kunt de ondersteunde versie controleren die overeenkomt met uw CCK-versie in onze documentatie."
            },
            {"ABI_UI_BUILDPANEL_UPLOAD_WORLD_BUTTON", "Upload Wereld"},
            {"ABI_UI_BUILDPANEL_UPLOAD_AVATAR_BUTTON", "Upload Avatar"},
            {"ABI_UI_BUILDPANEL_UPLOAD_PROP_BUTTON", "Upload Spawnbaar Object"},
            {"ABI_UI_BUILDPANEL_FIX_IMPORT_SETTINGS", "Importinstellingen herstellen"},
            {"ABI_UI_BUILDPANEL_UTIL_REMOVE_MISSING_SCRIPTS_BUTTON", "Verwijder ontbrekende scripts"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_USERNAME", "Gebruikersnaam"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_ACCESSKEY", "Toegangssleutel"},
            {
                "ABI_UI_BUILDPANEL_INFOTEXT_DOCUMENTATION",
                "Gebruik onze documentatie om meer te weten te komen over het maken van content voor onze games. Je vindt daar ook enkele handige tutorials over hoe je de meeste core engine-functies en core game-functies kunt gebruiken."
            },
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN1", "Verifieer alstublieft met uw CCK-inloggegevens."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN2", "Die vind je op hub.abinteractive.net."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN3", "Genereer een CCK-sleutel in de sleutelbeheerder."},
            {"ABI_UI_BUILDPANEL_INFOTEXT_AUTHENTICATED_AS", "Geauthenticeerd als"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_USER_RANK", "API-gebruikersrang"},
            {"ABI_UI_BUILDPANEL_SETTINGS_HEADER", "Instellingen uploaden"},
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_ENCRYPTION", "Kies Encryptie Verbinding:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_ENCRYPTION",
                "Als je problemen hebt met uploaden, probeer dan over te schakelen naar http."
            },
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_REGION", "Gewenste uploadregio:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_REGION",
                "U kunt de gewenste uploadregio wijzigen om uw uploadsnelheid te verhogen. Als de voorkeursregio niet beschikbaar is, wordt automatisch een andere regio geselecteerd. Uw inhoud is overal beschikbaar, ongeacht de regio die is gekozen om te uploaden."
            },
            {"ABI_UI_BUILDPANEL_SETTINGS_CCK_LANGUAGE", "CCK Language:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CCK_LANGUAGE",
                "U kunt hier uw CCK-taal wijzigen om meldingen en UI-teksten in uw voorkeurstaal te ontvangen."
            },
            {
                "ABI_UI_BUILDPANEL_WARNING_FOLDERPATH",
                "Verplaats de maplocatie van de CCK- of CCK Mods-map niet. Hierdoor wordt de CCK onbruikbaar."
            },
            {
                "ABI_UI_BUILDPANEL_WARNING_FEEDBACK",
                "Functie aanvragen? Een fout gevonden? Post op ons feedbackplatform!"
            },
            {"ABI_UI_BUILDPANEL_WARNING_MESH_FILTER_MESH_EMPTY", "MeshFilter with missing Mesh detected"},
            {
                "ABI_UI_BUILDPANEL_AVATAR_ERROR_ANIMATOR",
                "Er is geen animator gevonden voor deze avatar. Zorg ervoor dat er een animator aanwezig is op hetzelfde GameObject als de CVRAvatar-component."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_GENERIC",
                "Het Avatar Slot in uw Avatar Animator is niet gevuld. Uw Avatar wordt beschouwd als een algemene Avatar."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_POLYGONS",
                "Waarschuwing: deze avatar heeft in totaal meer dan 100.000 ({X}) polygonen. Dit kan prestatieproblemen veroorzaken in het spel. Dit weerhoudt je er niet van om te uploaden."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_SKINNED_MESH_RENDERERS",
                "Waarschuwing: deze avatar bevat meer dan 10 ({X}) SkinnedMeshRenderer-componenten. Dit kan prestatieproblemen veroorzaken in het spel. Dit weerhoudt je er niet van om te uploaden."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_MATERIALS",
                "Waarschuwing: deze avatar gebruikt meer dan 20 ({X}) materiële slots. Dit kan prestatieproblemen veroorzaken in het spel. Dit belet je niet om te uploaden."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_VIEWPOINT",
                "Waarschuwing: de weergavepositie van deze avatar is standaard X=0,Y=0,Z=0. Dit betekent dat uw kijkpositie op de grond is. Dit is waarschijnlijk niet wat je wilt."
            },
            {"ABI_UI_BUILDPANEL_AVATAR_WARNING_NON_HUMANOID", "Waarschuwing: je avatar is niet ingesteld als Humanoid."},
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_LEGACY_BLENDSHAPES",
                "Waarschuwing: deze avatar heeft geen legacy-blendshape normals Dit zal leiden tot een grotere bestandsgrootte en verlichtingsfouten"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_POLYGONS",
                "Info: deze avatar heeft in totaal meer dan 50.000 ({X}) polygonen. Dit kan prestatieproblemen veroorzaken in het spel. Dit weerhoudt je er niet van om te uploaden."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SKINNED_MESH_RENDERERS",
                "Info: deze avatar bevat meer dan 5 ({X}) SkinnedMeshRenderer-componenten. Dit kan prestatieproblemen veroorzaken in het spel. Dit weerhoudt je er niet van om te uploaden."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_MATERIALS",
                "Info: deze avatar gebruikt meer dan 10 ({X}) materiële slots. Dit kan prestatieproblemen veroorzaken in het spel. Dit weerhoudt je er niet van om te uploaden."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SMALL",
                "Info: de weergavepositie van deze avatar is minder dan 0,5 in hoogte. Deze avatar wordt als te klein beschouwd."
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_HUGE",
                "Info: de kijkpositie van deze avatar is meer dan 3,0 hoog. Deze avatar wordt als extreem groot beschouwd."
            },
            {"ABI_UI_BUILDPANEL_AVATAR_UPLOAD_BUTTON", "Upload Avatar"},
            {
                "ABI_UI_BUILDPANEL_PROPS_ERROR_MISSING_SCRIPT",
                "Spawnable Objects of de onderliggende items bevatten ontbrekende scripts. Het uploaden zal zo mislukken. Verwijder alle ontbrekende scriptreferenties voordat u uploadt of klik op Alle ontbrekende scripts verwijderen om dit automatisch voor u te laten doen."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_POLYGONS",
                "Waarschuwing: dit spawnbare object heeft in totaal meer dan 100k ({X}) polygonen. Dit kan prestatieproblemen veroorzaken in het spel. Dit weerhoudt je er niet van om te uploaden."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_SKINNED_MESH_RENDERERS",
                "Waarschuwing: dit spawnbare object bevat meer dan 10 ({X}) SkinnedMeshRenderer-componenten. Dit kan prestatieproblemen veroorzaken in het spel. Dit weerhoudt je er niet van om te uploaden."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_MATERIALS",
                "Waarschuwing: dit spawnbare object gebruikt meer dan 20 ({X}) materiaalvakken. Dit kan prestatieproblemen veroorzaken in het spel. Dit weerhoudt je er niet van om te uploaden."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_LEGACY_BLENDSHAPES",
                "Waarschuwing: dit spawnbare object heeft geen legacy normals. Dit zal leiden tot een grotere bestandsgrootte en verlichtingsfouten"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_POLYGONS",
                "Info: dit spawnbare object heeft in totaal meer dan 50k ({X}) polygonen. Dit kan prestatieproblemen veroorzaken in het spel. Dit weerhoudt je er niet van om te uploaden."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_SKINNED_MESH_RENDERERS",
                "Info: dit spawnbare object bevat meer dan 5 ({X}) SkinnedMeshRenderer-componenten. Dit kan prestatieproblemen veroorzaken in het spel. Dit weerhoudt je er niet van om te uploaden."
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_MATERIALS",
                "Info: dit spawnbare object gebruikt meer dan 10 ({X}) materiaalvakken. Dit kan prestatieproblemen veroorzaken in het spel. Dit weerhoudt je er niet van om te uploaden."
            },
            {"ABI_UI_BUILDPANEL_PROPS_UPLOAD_BUTTON", "Upload Spawnable Object (Prop)"},
            {
                "ABI_UI_BUILDPANEL_ERROR_WORLD_MISSING_SCRIPTS",
                "Scène bevat ontbrekende scripts. Het uploaden zal zo mislukken. Verwijder alle ontbrekende scriptreferenties voordat u uploadt of klik op Verwijder ontbrekende scripts om dit automatisch voor u te laten doen."
            },
            {
                "ABI_UI_BUILDPANEL_ERROR_AVATAR_MISSING_SCRIPTS",
                "Avatar of de sub-objecten bevatten ontbrekende scripts. Het uploaden zal zo mislukken. Verwijder alle ontbrekende scriptreferenties voordat u uploadt of klik op Verwijder ontbrekende scripts om dit automatisch voor u te laten doen."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_MULTIPLE_TRIGGER_HELPBOX",
                "Het hebben van meerdere triggers op hetzelfde GameObject leidt tot onvoorspelbaar gedrag!"
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_POINTERS_HELPBOX",
                "Pointers toevoegen aan de lijst \"Toegestane Pointers\" negeert alle andere Pointers die er niet in staan."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_TYPES_HELPBOX",
                "Door typen toe te voegen aan de lijst \"Toegestane typen\" worden alle verwijzingen genegeerd die niet overeenkomen met de typen in de lijst."
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_PARTICLE_HELPBOX",
                "Als u deze optie inschakelt, kunnen particles die een pointer op hetzelfde GameObject hebben, deze trigger activeren. Particle kan alleen On Enter Trigger activeren."
            },
            {
                "ABI_UI_INFOTEXT_WORLDS_NO_AVATARS",
                "Er is een ChilloutVR World-object gevonden in de scène. Avatars kunnen pas worden geüpload als het wereldobject is verwijderd. Avatars / spawnbare objecten zullen deel uitmaken van de wereld en zichtbaar zijn in de wereld, tenzij ze worden uitgeschakeld of verwijderd."
            },
            {
                "ABI_UI_ASSET_INFO_HEADER_INFORMATION",
                "Dit script wordt gebruikt om metadata van objecten op te slaan. Wijzig de gegevens erop niet, tenzij u weet wat u doet. Om een avatar opnieuw te uploaden, koppelt u de guid los en uploadt u hem opnieuw."
            },
            {"ABI_UI_ASSET_INFO_GUID_LABEL", "De momenteel opgeslagen guid is: "},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON", "Unieke ID (Guid) loskoppelen"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_TITLE", "Guid loskoppelen van Asset Info Manager"},
            {
                "ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_BODY",
                "De unieke ID van het item wordt losgekoppeld. Dit betekent dat uw inhoud hoogstwaarschijnlijk als nieuw wordt geüpload tijdens runtime. Doorgaan?"
            },
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_ACCEPT", "Ja!"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_DENY", "Nee!"},
            {"ABI_UI_ASSET_INFO_ATTACH_LABEL", "Unieke identificatie"},
            {
                "ABI_UI_ASSET_INFO_ATTACH_INFO",
                "U hoeft een Guid niet opnieuw toe te voegen als u niet van plan bent een oude upload te overschrijven. Een nieuwe wordt gegenereerd bij het uploaden als er geen is bijgevoegd."
            },
            {"ABI_UI_ASSET_INFO_ATTACH_BUTTON", "Re-Attach guid"},
            {
                "ABI_UI_AVATAR_INFO_VIEWPOINT",
                "Regelt de camerapositie van je speler in het spel. Dit moet tussen beide ogen geplaatst worden."
            },
            {
                "ABI_UI_AVATAR_INFO_VOICE_POSITION",
                "Regelt de stempositie van uw speler in het spel. Dit zou op je mond geplaatst moeten worden."
            },
            {
                "ABI_UI_AVATAR_INFO_OVERRIDE_CONTROLLER",
                "Om ervoor te zorgen dat de overrides werken, moet u ervoor zorgen dat de juiste animator is toegewezen in de override-controller. Anders ziet u geen animator-slots. Een voorbeeld hiervan staat in de map CCK Player Prefabs."
            },
            {
                "ABI_UI_AVATAR_INFO_BLinking",
                "Het gebruik van een knipperende ogen (blendshape) is optioneel. Het kan worden ingeschakeld om willekeurige knipperingen te genereren tijdens upload/runtime."
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_MOVEMENT",
                "Als u deze optie aanvinkt, wordt een semi-realistische animatie van de ogen gegenereerd."
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_VISEMES",
                "Om de functie voor automatisch selecteren van visemes te laten werken, moet u eerst de mesh selecteren die het gezicht bevat. Dit zal in de meeste gevallen de body mesh zijn."
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_TITLE",
                "Afhankelijkheden ontbreken in dit project!"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_WARNING_PREFACE",
                "Er wordt niet aan de volgende afhankelijkheden voldaan"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_FINAL_WARNING",
                "De volgende afhankelijkheden ontbreken, Installeer alle afhankelijkheden voordat u deze module installeert! Er wordt niet voldaan"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_DIALOG_ACCEPT",
                "Begrepen"
            },
            { "ABI_UI_BUILD_RUNTIME_HEADER", "Inhoud uploaden naar ChilloutVR" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEXT", "Ga verder naar de volgende stap" },
            { "ABI_UI_BUILD_RUNTIME_BTN_PREV", "Terug naar laatste stap" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEW_PICTURE", "Afbeelding vervangen" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_ASSETBUNDLE", "AssetBundle Bestandsgrootte" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_IMAGE", "Grootte afbeeldingsbestand" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_MANIFEST", "Manifest bestandsgrootte" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_PANO1K", "1080P Pano-bestandsgrootte" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_PANO4K", "4K Pano-bestandsgrootte" },
            { "ABI_UI_BUILD_RUNTIME_HINT_CLICK_TO_CAPTURE", "Klik op de kleinere afbeelding om de foto vast te leggen" },
            { "ABI_UI_BUILDSTEP_FILTERTAGS", "Tags filteren" },
            { "ABI_UI_BUILDSTEP_DETAILS", "Details" },
            { "ABI_UI_BUILDSTEP_LEGAL", "Juridische zekerheid" },
            { "ABI_UI_BUILDSTEP_UPLOAD", "Inhoud uploaden" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_ROW", "Naam:" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_ROW", "Beschrijving:" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_PLACEHOLDER", "Objectnaam (verplicht!)" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_PLACEHOLDER", "Objectbeschrijving" },
            { "ABI_UI_BUILDSTEP_DETAILS_CHANGELOG_PLACEHOLDER", "Object changelog - vertel gebruikers wat u hebt gewijzigd of toegevoegd" },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_NEW_OBJECT", 
                "Dit object wordt voor de eerste keer geüpload. Het uploaden van een profielfoto is vereist, daarom is de optie om geen afbeelding te uploaden niet beschikbaar."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_UPDATING_OBJECT", 
                "U staat op het punt dit object bij te werken. Het wijzigen van de beschrijving of naam is niet beschikbaar tijdens het bijwerken van dit object. Wijzig deze indien nodig op de hub."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_SET_ACTIVE_FILE", 
                "Stel deze upload in als het actieve bestand voor het doelplatform"
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_PERMISSION", 
                "Hierbij verklaar ik dat mijn geüploade inhoud toebehoort aan of in licentie is gegeven aan mij. Ik weet dat het uploaden van auteursrechtelijk beschermde inhoud zonder de toestemming van de auteur mijn account kan beperken en/of juridische gevolgen kan hebben. Ik weet dat ik me volledig moet houden aan alle regels voor het maken van inhoud die worden genoemd in de servicevoorwaarden van Alpha Blend Interactive."
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_TAGS", 
                "Hierbij verklaar ik dat de tags correct zijn ingesteld en passen bij de geüploade inhoud. Ik weet dat het willens en wetens instellen van de verkeerde tags een ernstige overtreding is. Ik weet dat mijn account wordt gestraft als ik voortdurend de verkeerde tags instel."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_STEP_DETAILS", 
                "Uw inhoud wordt nu geüpload naar ons netwerk. Het uploadproces is opgedeeld in verschillende stappen. Na het uploaden van het bestand naar ons netwerk, ondergaat het bestand automatische beveiligingscontroles, nadat ze zijn goedgekeurd, zullen we uw bundel versleutelen en naar ons CDN pushen. Je kunt hieronder de huidige status van je upload bekijken."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_DETAILS_MISSING", 
                "Om inhoud naar ons platform te uploaden, is een naam vereist. Zorg er bij het uploaden van een nieuw object voor dat u het dienovereenkomstig een naam geeft. U wordt nu teruggestuurd naar de detailpagina om een naam in te voeren."
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_LEGAL_MISSING", 
                "Om naar ons platform te uploaden, moet u bevestigen dat u de inhoud mag uploaden en dat alle ingestelde tags correct zijn. U wordt nu teruggestuurd naar de juridische pagina om de juridische verzekering te bekijken en te accepteren."
            },
            { "ABI_UI_DETAILS_HEAD_CHANGELOG", "Inhoudswijzigingslog" },
            { "ABI_UI_DETAILS_HEAD_STATISTICS", "Bestandsstatistieken" },
            { "ABI_UI_LEGAL_HEAD_OWNERSHIP", "Juridische zekerheid: eigendom en auteursrecht" },
            { "ABI_UI_LEGAL_HEAD_TAGS", "Juridische zekerheid: taggen" },
            { "ABI_UI_TAGS_HEADER_AUDIO", "Geluid ervaring" },
            { "ABI_UI_TAGS_HEADER_VISUAL", "Visuele ervaring" },
            { "ABI_UI_TAGS_HEADER_CONTENT", "Inhoud" },
            { "ABI_UI_TAGS_HEADER_NSFW", "Leeftijds grens classificatie" },
            { "ABI_UI_TAGS_LOUD_AUDIO", "Luid geluid" },
            { "ABI_UI_TAGS_LR_AUDIO", "Langeafstandsaudio" },
            { "ABI_UI_TAGS_SPAWN_AUDIO", "Spawn-audio" },
            { "ABI_UI_TAGS_CONTAINS_MUSIC", "Bevat Muziek" },
            { "ABI_UI_TAGS_FLASHING_COLORS", "Knipperende kleuren" },
            { "ABI_UI_TAGS_FLASHING_LIGHTS", "Flitsende lichten" },
            { "ABI_UI_TAGS_EXTREMELY_BRIGHT", "Extreem vel licht" },
            { "ABI_UI_TAGS_SCREEN_EFFECTS", "Schermeffecten" },
            { "ABI_UI_TAGS_PARTICLE_SYSTEMS", "Particle Systeem" },
            { "ABI_UI_TAGS_VIOLENCE", "Geweld" },
            { "ABI_UI_TAGS_GORE", "Gore" },
            { "ABI_UI_TAGS_HORROR", "Horror" },
            { "ABI_UI_TAGS_JUMPSCARE", "Jumpscare" },
            { "ABI_UI_TAGS_HUGE", "Extreem Groot" },
            { "ABI_UI_TAGS_SMALL", "Extreem Klein" },
            { "ABI_UI_TAGS_SUGGESTIVE", "Suggestief" },
            { "ABI_UI_TAGS_NUDITY", "Naaktheid" },
            { "ABI_UI_API_RESPONSE_HEAD", "Huidige status" },
            { "ABI_UI_API_RESPONSES_UPLOADED", "Bestand wordt geüpload. Bezig met verwerken van bestand." },
            { "ABI_UI_API_RESPONSES_SECURITY_CHECKING", "De activebundel wordt momenteel gecontroleerd door ons beveiligingssysteem." },
            { "ABI_UI_API_RESPONSES_ENCRYPTING", "Uw activebundelbestand wordt momenteel versleuteld." },
            { "ABI_UI_API_RESPONSES_PUSHING", "De controles zijn voltooid. Het bestand wordt momenteel overgebracht naar onze opslag." },
            { "ABI_UI_API_RESPONSES_FINISHED", "Upload compleet. Je inhoud is nu beschikbaar in de game." },
        };
    }
}