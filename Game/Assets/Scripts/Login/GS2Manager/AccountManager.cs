using System;
using System.Collections;
using System.Collections.Generic;

using Gs2.Core;
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Gs2Account.Result;
using Gs2.Unity.Util;

using UnityEngine;

public class AccountManager : MonoBehaviour
{
	private static Gs2.Unity.Util.Profile profile;          // GS2 SDK Profile情報
	private static Gs2.Unity.Client gs2;                    // GS2 Client情報
	private static EzAccount account = null;                // GS2 アカウント情報
	private static GameSession session = null;              // GS2 セッション情報

	public const int ACCOUNTSTATE_INIT = 0;     // 未ログイン状態
	public const int ACCOUNTSTATE_NOW = 1;      // ログイン処理中	
	public const int ACCOUNTSTATE_FAILED = 2;       // ログイン失敗
	public const int ACCOUNTSTATE_SUCCESS = 3;  // ログイン成功

	private static int accountState = ACCOUNTSTATE_INIT;    // アカウント状態

	// アカウント情報の作成
	public static IEnumerator CreateAccount()
	{
		string applicationClientId = AccountInfo.GetApplicationClientId();
		string applicationClientSecret = AccountInfo.GetApplicationClientSecret();
		AsyncResult<object> asyncResultSDKInit = null;
		AsyncResult<EzCreateResult> asyncResultCreateAccount = null;
		AsyncResult<GameSession> asyncResultLogin = null;

		// 状態更新(ログイン処理中)
		accountState = ACCOUNTSTATE_NOW;

		// SDK情報の初期化
		profile = new Gs2.Unity.Util.Profile(
			clientId: applicationClientId,
			clientSecret: applicationClientSecret,
			reopener: new Gs2BasicReopener()
		);

		// profileの初期化
		var current = profile.Initialize(
		   r => {
			   // コールバック処理
			   asyncResultSDKInit = r;
		   }
	   );

		yield return current;

		// コルーチンの実行が終了した時点で、コールバックは必ず呼ばれています

		// エラーが発生したかを確認する
		if (asyncResultSDKInit.Error != null)
		{
			// エラー発生

			// エラー登録(SDK初期化異常)
			ErrorInfo.SetErrorStatus(ErrorInfo.ERROR_ID_SDKINITFAILED);

			// 状態更新(ログイン失敗)
			accountState = ACCOUNTSTATE_FAILED;

			yield break;
		}

		// クライアント初期化
		gs2 = new Gs2.Unity.Client(profile);

		current = gs2.Account.Create(
		   r => {
			   // コールバック処理
			   asyncResultCreateAccount = r;
		   },
		   AccountInfo.GetAccountNameSpaceName()
   	);

		yield return current;

		// コルーチンの実行が終了した時点で、コールバックは必ず呼ばれています

		// エラーが発生したかを確認する
		if (asyncResultCreateAccount.Error != null)
		{
			// エラー発生

			// エラー登録(SDK初期化異常)
			ErrorInfo.SetErrorStatus(ErrorInfo.ERROR_ID_SDKINITFAILED);

			// 状態更新(ログイン失敗)
			accountState = ACCOUNTSTATE_FAILED;

			yield break;
		}
		else
		{
			// 作成したアカウント情報を取得
			account = asyncResultCreateAccount.Result.Item;

			// アカウント情報を保存
			AccountPrefs.SetAccountInfo(account);
		}

		current = profile.Login(
		   authenticator: new Gs2AccountAuthenticator(
			   session: profile.Gs2Session,
			   accountNamespaceName: AccountInfo.GetAccountNameSpaceName(),
			   keyId: AccountInfo.GetKeyAccountAuthenticationKeyId(),
			   userId: account.UserId,
			   password: account.Password
		   ),
		   r => { asyncResultLogin = r; }
	   );

		yield return current;

		// コルーチンの実行が終了した時点で、コールバックは必ず呼ばれています

		// エラーが発生したかを確認する
		if (asyncResultLogin.Error != null)
		{
			// エラー発生

			// エラー登録(ログイン異常)
			ErrorInfo.SetErrorStatus(ErrorInfo.ERROR_ID_LOGINFAILED);

			// 状態更新(ログイン失敗)
			accountState = ACCOUNTSTATE_FAILED;

			yield break;
		}

		// ログイン状態を表すゲームセッションオブジェクトを取得
		session = asyncResultLogin.Result;

		// 状態更新(ログイン成功)
		accountState = ACCOUNTSTATE_SUCCESS;
	}

	// ログイン処理
	public static IEnumerator LoginAccount()
	{
		string applicationClientId = AccountInfo.GetApplicationClientId();
		string applicationClientSecret = AccountInfo.GetApplicationClientSecret();
		AsyncResult<object> asyncResultSDKInit = null;
		AsyncResult<GameSession> asyncResultLogin = null;

		// 状態更新(ログイン処理中)
		accountState = ACCOUNTSTATE_NOW;

		// SDK情報の初期化
		profile = new Gs2.Unity.Util.Profile(
			clientId: applicationClientId,
			clientSecret: applicationClientSecret,
			reopener: new Gs2BasicReopener()
		);

		// profileの初期化
		var current = profile.Initialize(
		   r => {
			   // コールバック処理
			   asyncResultSDKInit = r;
		   }
	   );

		yield return current;

		// コルーチンの実行が終了した時点で、コールバックは必ず呼ばれています

		// エラーが発生したかを確認する
		if (asyncResultSDKInit.Error != null)
		{
			// エラー発生

			// エラー登録(SDK初期化異常)
			ErrorInfo.SetErrorStatus(ErrorInfo.ERROR_ID_SDKINITFAILED);

			// 状態更新(ログイン失敗)
			accountState = ACCOUNTSTATE_FAILED;

			yield break;
		}

		// クライアント初期化
		gs2 = new Gs2.Unity.Client(profile);

		current = profile.Login(
			authenticator: new Gs2AccountAuthenticator(
				session: profile.Gs2Session,
				accountNamespaceName: AccountInfo.GetAccountNameSpaceName(),
				keyId: AccountInfo.GetKeyAccountAuthenticationKeyId(),
				userId: AccountPrefs.GetAccountId(),
				password: AccountPrefs.GetAccountPassword()
			),
			r => { asyncResultLogin = r; }
		);

		yield return current;

		// コルーチンの実行が終了した時点で、コールバックは必ず呼ばれています

		// エラーが発生したかを確認する
		if (asyncResultLogin.Error != null)
		{
			// エラー発生

			// エラー登録(ログイン異常)
			ErrorInfo.SetErrorStatus(ErrorInfo.ERROR_ID_LOGINFAILED);

			// 状態更新(ログイン失敗)
			accountState = ACCOUNTSTATE_FAILED;

			yield break;
		}

		// ログイン状態を表すゲームセッションオブジェクトを取得
		session = asyncResultLogin.Result;

		// 状態更新(ログイン成功)
		accountState = ACCOUNTSTATE_SUCCESS;
	}

	//ログアウト処理
	public static IEnumerator LogoutAccount()
    {
		//GS2からのログアウト処理は必要であれば今後記述する
		//代わりにnullを返す
		yield return null;
		//ログイン画面に移行
		accountState = ACCOUNTSTATE_INIT;
    }

	// GS2 SDKの終了処理
	public static void FinalizeGS2SDK()
	{
		// ゲームを終了するときなどに呼び出してください。
		// 頻繁に呼び出すことは想定していません。
		if (profile != null)
		{
			var current = profile.Finalize();
		}
	}

	// ログインシーケンスの状態取得
	public static int GetAccountState()
	{
		return accountState;
	}
}