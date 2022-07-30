using System.Collections.Generic;

namespace ABI.CCK.Scripts.Translation
{
    public class Japanese
    {
        public static readonly Dictionary<string, string> Localization = new Dictionary<string, string>()
        {
            {"ABI_UI_BUILDPANEL_HEADING_BUILDER", "コンテンツビルダー"},
            {"ABI_UI_BUILDPANEL_HEADING_SETTINGS", "設定とオプション"},
            {"ABI_UI_BUILDPANEL_HEADING_DOCUMENTATION", "ドキュメントを見る"},
            {"ABI_UI_BUILDPANEL_HEADING_FEEDBACK", "フィードバックを送信"},
            {"ABI_UI_BUILDPANEL_HEADING_FOUNDCONTENT", "アクティブなシーンで見つけたコンテンツ"},
            {"ABI_UI_BUILDPANEL_HEADING_ACCOUNT_INFO", "アカウント情報"},
            {"ABI_UI_BUILDPANEL_LOGIN_CREDENTIALS_INCORRECT", "入力されたログイン情報が正しくありません。"},
            {"ABI_UI_BUILDPANEL_LOGIN_BUTTON", "ログイン"},
            {"ABI_UI_BUILDPANEL_LOGOUT_BUTTON", "ログアウト"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_TITLE", "CCKのローカル認証情報を削除"},
            {
                "ABI_UI_BUILDPANEL_LOGOUT_DIALOG_BODY",
                "これにより、ローカルに保存されていた認証情報が削除されます。再度認証を行う必要がありますが、続けますか？"
            },
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_ACCEPT", "はい！"},
            {"ABI_UI_BUILDPANEL_LOGOUT_DIALOG_DECLINE", "いいえ！"},

            {"ABI_UI_BUILDPANEL_UPLOADER_NO_AVATARS_FOUND", "シーン内には設定されたアバターが見つかりません - CVR Avatarを追加しましたか？"},
            {
                "ABI_UI_BUILDPANEL_UPLOADER_NO_SPAWNABLES_FOUND",
                "シーン内には設定されたSpawnable object(Prop)が見つかりません - CVR Spawnableを追加しましたか？"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_WARNING_SPAWNPOINT",
                "このワールドにはスポーンポイントが割り当てられていません。CVR Worldコンポーネントに1つまたは複数のスポーンポイントを追加してください。"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_INFO_REFERENCE_CAMERA",
                "ワールドにReferenceCameraが割り当てられていません。デフォルトのカメラ設定が使用されます。"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_INFO_RESPAWN_HEIGHT",
                "リスポーンの高さが-500以下です。マップから落下した際にリスポーンするのに時間がかかります。これは恐らくあなたが望むものではありません。"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_MULTIPLE_CVR_WORLD",
                "複数のCVR Worldオブジェクトがシーンに存在しています。これはワールドを壊します。シーン内にCVR Worldオブジェクトが1つだけ存在するようにするか、CCKに含まれているCVRWorldプレハブを利用してください。"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_WORLD_CONTAINS_AVATAR",
                "読み込まれたシーンは、アバターとワールド記述子の両方のオブジェクトを含むことは出来ません。シーンを適切に設定し直してください。"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_NO_CONTENT",
                "現在のシーンにはコンテンツが見つかりません。GameObjectに記述子―コンポーネントを追加するのを忘れていませんか？"
            },
            {
                "ABI_UI_BUILDPANEL_WORLDS_ERROR_UNITY_UNSUPPORTED",
                "サポートされていないバージョンのUnityを使用しています。サポートされているUnityのバージョンを使用してください。使用中のCCKのバージョンに対応しているバージョンは、ドキュメントから確認出来ます。"
            },
            {"ABI_UI_BUILDPANEL_UPLOAD_WORLD_BUTTON", "ワールドアップロード"},
            {"ABI_UI_BUILDPANEL_UPLOAD_AVATAR_BUTTON", "アバターアップロード"},
            {"ABI_UI_BUILDPANEL_UPLOAD_PROP_BUTTON", "Spawnable object(Prop)アップロード"},
            {"ABI_UI_BUILDPANEL_FIX_IMPORT_SETTINGS", "インポート設定の修正"},
            {"ABI_UI_BUILDPANEL_UTIL_REMOVE_MISSING_SCRIPTS_BUTTON", "不足しているスクリプトを削除"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_USERNAME", "Username"},
            {"ABI_UI_BUILDPANEL_LOGIN_TEXT_ACCESSKEY", "Access-Key"},
            {
                "ABI_UI_BUILDPANEL_INFOTEXT_DOCUMENTATION",
                "ゲームのコンテンツを作成する方法については、ドキュメントを参照してください。また、エンジンのコア機能やゲームのコア機能を活用するための便利なチュートリアルも用意されています。"
            },
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN1", "あなたのCCK認証情報を使用し、認証してください。"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN2", "これらは、hub.abinteractive.netで確認出来ます。"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN3", "Key managerでCCK Keyを生成してください。"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_AUTHENTICATED_AS", "認証されたユーザ"},
            {"ABI_UI_BUILDPANEL_INFOTEXT_USER_RANK", "APIユーザランク"},
            {"ABI_UI_BUILDPANEL_SETTINGS_HEADER", "アップロード設定"},
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_ENCRYPTION", "接続暗号化の変更:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_ENCRYPTION",
                "アップロードに問題がある場合は、httpに切り替えてから試してください。"
            },
            {"ABI_UI_BUILDPANEL_SETTINGS_CONTENT_REGION", "優先アップロード地域:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_REGION",
                "アップロード速度を上げる為に、優先アップロード地域を切り替える事が出来ます。選択された地域が利用出来ない場合は、他地域が自動的に選択されます。あなたのコンテンツは、選択されたアップロード先地域に関係なく、世界中で利用可能です。"
            },
            {"ABI_UI_BUILDPANEL_SETTINGS_CCK_LANGUAGE", "CCKの言語:"},
            {
                "ABI_UI_BUILDPANEL_SETTINGS_HINT_CCK_LANGUAGE",
                "ここでCCKの言語を切り替える事で、通知やUIのテキストを任意の言語で利用する事が可能です。"
            },
            {
                "ABI_UI_BUILDPANEL_WARNING_FOLDERPATH",
                "CCKやCCK Modsのフォルダ位置を移動しないでください。移動するとCCKが使用出来なくなります。"
            },
            {
                "ABI_UI_BUILDPANEL_WARNING_FEEDBACK",
                "機能をリクエストしたい？バグを見つけた？フィードバックプラットフォームに投稿をお願いします。"
            },
            {"ABI_UI_BUILDPANEL_WARNING_MESH_FILTER_MESH_EMPTY", "メッシュが見つからないMeshFilterを検出"},
            {
                "ABI_UI_BUILDPANEL_AVATAR_ERROR_ANIMATOR",
                "このアバターにはAnimatorが検出されませんでした。CVR Avatarコンポーネントと同じGameObjectにAnimatorが存在する事を確認してください。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_GENERIC",
                "アバターAnimatorのAvatarが埋まっていません。あなたのアバターは、GenericAvatarとみなされます。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_POLYGONS",
                "警告：このアバターは総ポリゴン数が100k({X})を超過しています。これはゲーム内でのパフォーマンス問題を引き起こす可能性があります。このメッセージはアップロードを妨げるものではありません。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_SKINNED_MESH_RENDERERS",
                "警告：このアバターには10個以上({X})のSkinnedMeshRendererコンポーネントが含まれています。これはゲーム内でのパフォーマンス問題を引き起こす可能性があります。このメッセージはアップロードを妨げるものではありません。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_MATERIALS",
                "警告：このアバターは20個以上({X})のMaterialスロットを使用しています。これはゲーム内でのパフォーマンス問題を引き起こす可能性があります。このメッセージはアップロードを妨げるものではありません。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_VIEWPOINT",
                "警告：このアバターの表示位置は、デフォルトではX=0,Y=0,Z=0になっています。これは恐らくあなたが望むものではありません。"
            },
            {"ABI_UI_BUILDPANEL_AVATAR_WARNING_NON_HUMANOID", "警告：あなたのアバターはHumanoidとして設定されていません。"},
            {
                "ABI_UI_BUILDPANEL_AVATAR_WARNING_LEGACY_BLENDSHAPES",
                "警告：このアバターには、LegacyBlendShapeの法線がありません。これにより、ファイルサイズが肥大化し、Lighthingエラーが発生します。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_POLYGONS",
                "情報：このアバターは、総ポリゴン数が50k({X})を超過しています。これはゲーム内でのパフォーマンス問題を引き起こす可能性があります。このメッセージはアップロードを妨げるものではありません。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SKINNED_MESH_RENDERERS",
                "情報：このアバターには5個以上({X})のSkinnedMeshRendererコンポーネントが含まれています。これはゲーム内でのパフォーマンス問題を引き起こす可能性があります。このメッセージはアップロードを妨げるものではありません。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_MATERIALS",
                "情報：このアバターは10個以上({X})のMaterialスロットを使用しています。これはゲーム内でのパフォーマンス問題を引き起こす可能性があります。このメッセージはアップロードを妨げるものではありません。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_SMALL",
                "情報：このアバターの表示位置は、高さが0.5未満です。このアバターは過度に小さいと思われます。"
            },
            {
                "ABI_UI_BUILDPANEL_AVATAR_INFO_HUGE",
                "情報：このアバターの表示位置は、高さが3.0を超過しています。このアバターは過度に大きいと思われます。"
            },
            {"ABI_UI_BUILDPANEL_AVATAR_UPLOAD_BUTTON", "アバターアップロード"},
            {
                "ABI_UI_BUILDPANEL_PROPS_ERROR_MISSING_SCRIPT",
                "Spawnable objects(Prop)またはその子に、不足しているスクリプトがあります。このような場合、アップロードに失敗します。アップロードする前に、不足しているスクリプトの参照を全て削除するか、［不足しているスクリプトを全て削除/Remove all missing scripts］をクリックすると、自動的に削除されます。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_POLYGONS",
                "警告：このSpawnable object(Prop)は総ポリゴン数が100k({X})を超過しています。これはゲーム内でのパフォーマンス問題を引き起こす可能性があります。このメッセージはアップロードを妨げるものではありません。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_SKINNED_MESH_RENDERERS",
                "警告：このSpawnable object(Prop)には10個以上({X})のSkinnedMeshRendererコンポーネントが含まれています。これはゲーム内でのパフォーマンス問題を引き起こす可能性があります。このメッセージはアップロードを妨げるものではありません。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_MATERIALS",
                "警告：このSpawnable object(Prop)は20個以上({X})のMaterialスロットを使用しています。これはゲーム内でのパフォーマンス問題を引き起こす可能性があります。このメッセージはアップロードを妨げるものではありません。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_WARNING_LEGACY_BLENDSHAPES",
                "警告：このSpawnable object(Prop)はLegacyBlendShapeの法線がありません。これにより、ファイルサイズが肥大化し、Lighthingエラーが発生します。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_POLYGONS",
                "情報：このSpawnable object(Prop)は、総ポリゴン数が50k({X})を超過しています。これはゲーム内でのパフォーマンス問題を引き起こす可能性があります。このメッセージはアップロードを妨げるものではありません。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_SKINNED_MESH_RENDERERS",
                "情報：このSpawnable object(Prop)には5個以上({X})のSkinnedMeshRendererコンポーネントが含まれています。これはゲーム内でのパフォーマンス問題を引き起こす可能性があります。このメッセージはアップロードを妨げるものではありません。"
            },
            {
                "ABI_UI_BUILDPANEL_PROPS_INFO_MATERIALS",
                "情報：このSpawnable object(Prop)は10個以上({X})のMaterialスロットを使用しています。これはゲーム内でのパフォーマンス問題を引き起こす可能性があります。このメッセージはアップロードを妨げるものではありません。"
            },
            {"ABI_UI_BUILDPANEL_PROPS_UPLOAD_BUTTON", "Spawnable Object(Prop)アップロード"},
            {
                "ABI_UI_BUILDPANEL_ERROR_WORLD_MISSING_SCRIPTS",
                "シーンに不足しているスクリプトがあります。このような場合、アップロードに失敗します。アップロードする前に、不足しているスクリプトの参照を全て削除するか、［不足しているスクリプトを全て削除/Remove all missing scripts］をクリックすると、自動的に削除されます。"
            },
            {
                "ABI_UI_BUILDPANEL_ERROR_AVATAR_MISSING_SCRIPTS",
                "アバターまたはその子に不足しているスクリプトがあります。このような場合、アップロードに失敗します。アップロードする前に、不足しているスクリプトの参照を全て削除するか、［不足しているスクリプトを全て削除/Remove all missing scripts］をクリックすると、自動的に削除されます。"
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_MULTIPLE_TRIGGER_HELPBOX",
                "同じGameObjectに複数のTriggersを設定すると、予測不可能な挙動を起こします。"
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_POINTERS_HELPBOX",
                "\"Allowed Pointers\"リストにPointerを追加すると、そのリストに含まれていない他のPointerは無視されます。"
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_ALLOWED_TYPES_HELPBOX",
                "\"Allowed Types\"リストにTypeを追加すると、 そのリストに含まれていない他のTypeは無視されます。"
            },
            {
                "ABI_UI_ADVAVTR_TRIGGER_PARTICLE_HELPBOX",
                "このオプションを有効にすると、同じGameObjectにPointerがあるパーティクルシステムがこのTriggerを起動する事が出来ます。パーティクルは、On Enter Triggerでしか起動出来ません。"
            },
            {
                "ABI_UI_INFOTEXT_WORLDS_NO_AVATARS",
                "シーン内にChilloutVRのワールドオブジェクトが見つかりました。ワールドオブジェクトが削除されるまで、アバターのアップロードは出来ません。アバター/Spawnable object(Prop)は、無効化または削除されない限り、ワールドの一部となり、ワールド内で表示されます。"
            },
            {
                "ABI_UI_ASSET_INFO_HEADER_INFORMATION",
                "このスクリプトは、オブジェクトのメタデータを保存するために使用されます。何をしているのか分からない場合は、データを変更しないでください。アバターを更新せずに別途アップロード場合、GUIDをDetachしてからアップロードしてください。"
            },
            {"ABI_UI_ASSET_INFO_GUID_LABEL", "現在保存されているGUID："},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON", "GUIDのDetach"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_TITLE", "アセット情報マネージャからGUIDをDetach"},
            {
                "ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_BODY",
                "GUIDをDetachします。既存データの更新ではなく、コンテンツが新規にアップロードされる事になります。よろしいですか？"
            },
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_ACCEPT", "はい！"},
            {"ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_DENY", "いいえ！"},
            {"ABI_UI_ASSET_INFO_ATTACH_LABEL", "Unique identifier"},
            {
                "ABI_UI_ASSET_INFO_ATTACH_INFO",
                "既存のデータを上書きしない場合、GUIDを再設定する必要はありません。GUIDが未設定の場合、アップロード時に新たにGUIDが生成されます。"
            },
            {"ABI_UI_ASSET_INFO_ATTACH_BUTTON", "GUIDの再設定"},
            {
                "ABI_UI_AVATAR_INFO_VIEWPOINT",
                "ゲーム内のプレイヤー操作のカメラ位置をコントロールします。通常は両目の間に配置します。"
            },
            {
                "ABI_UI_AVATAR_INFO_VOICE_POSITION",
                "ゲーム内でのプレイヤー操作の声の位置をコントロールします。通常は口元に配置します。"
            },
            {
                "ABI_UI_AVATAR_INFO_OVERRIDE_CONTROLLER",
                "Overrideを機能させるためには、Override controllerに正しいAnimatorが割り当てられている事を確認してください。そうしないと、OverrideするAnimatorのスロットが表示されません。この例は、CCK Playerプレハブフォルダにあります。"
            },
            {
                "ABI_UI_AVATAR_INFO_BLinking",
                "Blinking blend shapeの使用はオプションです。これを有効にすると、ランダムなまばたきを有効にする事が出来ます。"
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_MOVEMENT",
                "このオプションをチェックすると、目のややリアルなアニメーションが可能になります。"
            },
            {
                "ABI_UI_AVATAR_INFO_EYE_VISEMES",
                "Visemesの自動選択機能を有効にするには、まず顔を含むメッシュを選択する必要があります。殆どの場合、これはBodyのメッシュになります。"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_TITLE",
                "プロジェクト内の依存関係が欠落しています!"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_WARNING_PREFACE",
                "以下の依存関係が満たされていません。"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_FINAL_WARNING",
                "このモジュールをインストールする前に、全ての依存パッケージをインストールしてください。!"
            },
            {
                "ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_DIALOG_ACCEPT",
                "了解しました。"
            },
            { "ABI_UI_BUILD_RUNTIME_HEADER", "コンテンツをChilloutVRにアップロード" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEXT", "次のステップに進む" },
            { "ABI_UI_BUILD_RUNTIME_BTN_PREV", "最後のステップに戻る" },
            { "ABI_UI_BUILD_RUNTIME_BTN_NEW_PICTURE", "画像の置換" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_ASSETBUNDLE", "アセットバンドルのサイズ" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_IMAGE", "画像ファイルのサイズ" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_MANIFEST", "マニフェストファイルのサイズ" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_PANO1K", "1080Pパノラマファイルのサイズ" },
            { "ABI_UI_BUILD_RUNTIME_FILEINFO_PANO4K", "4Kパノラマファイルのサイズ" },
            { "ABI_UI_BUILD_RUNTIME_HINT_CLICK_TO_CAPTURE", "画像をクリックするとサムネイルが表示されます" },
            { "ABI_UI_BUILDSTEP_FILTERTAGS", "タグでフィルター" },
            { "ABI_UI_BUILDSTEP_DETAILS", "詳細" },
            { "ABI_UI_BUILDSTEP_LEGAL", "法的保証" },
            { "ABI_UI_BUILDSTEP_UPLOAD", "コンテンツをアップロード" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_ROW", "名前:" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_ROW", "説明:" },
            { "ABI_UI_BUILDSTEP_DETAILS_NAME_PLACEHOLDER", "オブジェクト名(必須です!)" },
            { "ABI_UI_BUILDSTEP_DETAILS_DESC_PLACEHOLDER", "オブジェクトの説明" },
            { "ABI_UI_BUILDSTEP_DETAILS_CHANGELOG_PLACEHOLDER", "オブジェクトの変更ログ - ユーザに追加/変更点を伝える事が出来ます。" },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_NEW_OBJECT", 
                "このオブジェクトは初めてアップロードされます。プロフィール画像のアップロードが必須な為、画像をアップロードしないというオプションは利用出来ません。"
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_WARNING_UPDATING_OBJECT", 
                "このオブジェクトを更新しようとしています。このオブジェクトの更新中は、説明や名前の変更は出来ません。必要に応じてHubで変更してください。"
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_SET_ACTIVE_FILE", 
                "このアップロードファイルをターゲットプラットフォームのアクティブファイルとして設定する"
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_PERMISSION", 
                "私はここに、私のアップロードしたコンテンツが私に帰属するか、または私にライセンスされている事を証明します。私は、著作権のあるコンテンツを作者の許可なくアップロードすると、私のアカウントが制限されたり、法的な影響を受ける可能性がある事を承知します。私は、Alpha Blend Interactiveの利用規約に記載されている全てのコンテンツ作成ルールを完全に遵守しなければならない事を承知します。"
            },
            {
                "ABI_UI_BUILDSTEP_DETAILS_LEGAL_TAGS", 
                "私はここに、タグが正しく設定され、アップロードされたコンテンツに適合していることを証明します。私は、故意に間違ったタグを設定する事が重大な規約違反であることを承知します。私は、誤ったタグを設定し続けると、私のアカウントが処罰される事を承知します。"
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_STEP_DETAILS", 
                "あなたのコンテンツがネットワークにアップロードされています。アップロード処理は様々なステップに分かれています。ファイルをネットワークにアップロードした後、ファイルは自動的にセキュリティチェックを受け、チェックを通過した後、データ一式を暗号化してCDNに反映します。現在のアップロード状況は、以下でご確認出来ます。"
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_DETAILS_MISSING", 
                "当社のプラットフォームにコンテンツをアップロードするには、名前が必要です。新しいオブジェクトをアップロードする際には、必ず適切な名前を付けてください。ここから名前を入力する為の詳細ページに戻ります。"
            },
            {
                "ABI_UI_BUILDSTEP_UPLOAD_LEGAL_MISSING", 
                "当社のプラットフォームにアップロードするには、当該コンテンツをアップロードする事が許可されており、設定されているタグが全て正しい事を証明する必要があります。その後、法定ページに戻り、法的保証を確認・承諾していただきます。"
            },
            { "ABI_UI_DETAILS_HEAD_CHANGELOG", "コンテンツの変更履歴" },
            { "ABI_UI_DETAILS_HEAD_STATISTICS", "ファイルの統計情報" },
            { "ABI_UI_LEGAL_HEAD_OWNERSHIP", "法的保証: 所有者 & 著作権" },
            { "ABI_UI_LEGAL_HEAD_TAGS", "法的保証: タグ付け" },
            { "ABI_UI_TAGS_HEADER_AUDIO", "音響に関する項目" },
            { "ABI_UI_TAGS_HEADER_VISUAL", "視覚に関する項目" },
            { "ABI_UI_TAGS_HEADER_CONTENT", "コンテンツに関する項目" },
            { "ABI_UI_TAGS_HEADER_NSFW", "年齢制限に関する項目" },
            { "ABI_UI_TAGS_LOUD_AUDIO", "大音量" },
            { "ABI_UI_TAGS_LR_AUDIO", "広域音" },
            { "ABI_UI_TAGS_SPAWN_AUDIO", "音の発生" },
            { "ABI_UI_TAGS_CONTAINS_MUSIC", "音楽" },
            { "ABI_UI_TAGS_FLASHING_COLORS", "点滅を伴う発色" },
            { "ABI_UI_TAGS_FLASHING_LIGHTS", "点滅を伴うライト" },
            { "ABI_UI_TAGS_EXTREMELY_BRIGHT", "過度な照明" },
            { "ABI_UI_TAGS_SCREEN_EFFECTS", "画面効果" },
            { "ABI_UI_TAGS_PARTICLE_SYSTEMS", "パーティクルシステム" },
            { "ABI_UI_TAGS_VIOLENCE", "暴力表現" },
            { "ABI_UI_TAGS_GORE", "流血や残虐を伴う表現" },
            { "ABI_UI_TAGS_HORROR", "ホラー表現" },
            { "ABI_UI_TAGS_JUMPSCARE", "音や画像で驚かせるホラー表現" },
            { "ABI_UI_TAGS_HUGE", "過度に大きい" },
            { "ABI_UI_TAGS_SMALL", "過度に小さい" },
            { "ABI_UI_TAGS_SUGGESTIVE", "性的に際どい表現" },
            { "ABI_UI_TAGS_NUDITY", "裸体表現" },
            { "ABI_UI_API_RESPONSE_HEAD", "現在の状況" },
            { "ABI_UI_API_RESPONSES_UPLOADED", "ファイルがアップロードされました。現在ファイルを処理中です。" },
            { "ABI_UI_API_RESPONSES_SECURITY_CHECKING", "アセットバンドルは現在、当社のセキュリティシステムによってチェック中です。" },
            { "ABI_UI_API_RESPONSES_ENCRYPTING", "アセットバンドルファイルは現在暗号化中です。" },
            { "ABI_UI_API_RESPONSES_PUSHING", "チェックは完了しました。ファイルは現在、当社のストレージに転送中です。" },
            { "ABI_UI_API_RESPONSES_FINISHED", "アップロードが完了しました。コンテンツはゲームで利用可能です。" },
        };
    }
}