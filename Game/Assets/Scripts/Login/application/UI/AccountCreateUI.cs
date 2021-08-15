using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AccountCreateUI : MonoBehaviour
{
	private AccountConnectSequence gs2Manager;

	private GameObject errTextObj, inputFieldObj, loginButtonObj, createButtonObj, logoutButtonObj, toMainSceneButtonObj;
	private Text titleText, announceText, errText;
	private InputField inputField;
	private Button loginButton, createButton, logoutButton, toMainSceneButton;

	readonly private int MODE_LOGIN = 0;
	readonly private int MODE_CREATEACCOUNT = 1;
	private int mode = 0;

	// Start is called before the first frame update
	void Start()
	{
		// 各オブジェクト取得
		gs2Manager = GameObject.Find("GS2Manager").GetComponent<AccountConnectSequence>();

		errTextObj = GameObject.Find("ErrMsg");
		inputFieldObj = GameObject.Find("UsernameInput");
		loginButtonObj = GameObject.Find("LoginBtn");
		createButtonObj = GameObject.Find("CreateBtn");
		logoutButtonObj = GameObject.Find("LogoutBtn");
		toMainSceneButtonObj = GameObject.Find("ToMainSceneBtn");

		titleText = GameObject.Find("Title").gameObject.GetComponent<Text>();
		announceText = GameObject.Find("AnnounceMsg").gameObject.GetComponent<Text>();
		errText = errTextObj.gameObject.GetComponent<Text>();

		inputField = inputFieldObj.GetComponent<InputField>();

		loginButton = loginButtonObj.GetComponent<Button>();
		createButton = createButtonObj.GetComponent<Button>();
		logoutButton = loginButtonObj.GetComponent<Button>();
		toMainSceneButton = loginButtonObj.GetComponent<Button>();


		// エラー情報はエラー発生時に表示するのでここでは無効にする。
		errTextObj.SetActive(false);

		//メイン画面へのボタンはここでは無効にする。
		toMainSceneButtonObj.SetActive(false);

		// 既にアカウント作成済みかチェックする。
		if (CheckAccountCreated() == true)
		{
			// 作成済(ログイン処理を行う)
			createButtonObj.SetActive(false);   // アカウント作成ボタンは不要なので無効にする。
			inputFieldObj.SetActive(false); // ユーザ名入力フィールドは不要なので無効にする。
			loginButtonObj.SetActive(false);    // 再ログインボタンはログイン失敗時に表示するのでここでは無効にする。
			logoutButtonObj.SetActive(false);   //ログアウトボタンは不要なので無効にする

			titleText.text = "ログイン処理";
			announceText.text = "ログイン中...";

			// モードをログイン処理に設定
			mode = MODE_LOGIN;

			// ログイン要求
			gs2Manager.LoginRequest();
		}
		else
		{
			// 未作成(アカウント作成処理を行う)
			loginButtonObj.SetActive(false);    // ログインボタンは不要なので無効にする。
			logoutButtonObj.SetActive(false);   //ログアウトボタンは不要なので無効にする

			titleText.text = "ユーザ登録";
			announceText.text = "ユーザ名を入力してください。";

			// モードをアカウント生成に設定
			mode = MODE_CREATEACCOUNT;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (mode == MODE_LOGIN)
		{
			switch (AccountManager.GetAccountState())
			{
				case AccountManager.ACCOUNTSTATE_NOW:
					announceText.text = "ログイン中...";
					break;

				case AccountManager.ACCOUNTSTATE_FAILED:
					announceText.text = "ログイン失敗。";

					errTextObj.SetActive(true);
					errText.text = "エラー：ログインに失敗しました。";
					loginButtonObj.SetActive(true);
					break;

				case AccountManager.ACCOUNTSTATE_SUCCESS:
					announceText.text = "ログインしました。" + AccountPrefs.GetAccountUsername();
					logoutButtonObj.SetActive(true);
					toMainSceneButtonObj.SetActive(true);
					break;

				case AccountManager.ACCOUNTSTATE_INIT:
					announceText.text = "ユーザー名を入力してください。 ";
					logoutButtonObj.SetActive(false);
					loginButtonObj.SetActive(true);
					inputFieldObj.SetActive(true);
					break;

				default:
					break;
			}
		}
		else if (mode == MODE_CREATEACCOUNT)
		{
			switch (AccountManager.GetAccountState())
			{
				case AccountManager.ACCOUNTSTATE_NOW:
					announceText.text = "アカウント作成中...";
					break;

				case AccountManager.ACCOUNTSTATE_FAILED:
					announceText.text = "アカウント作成失敗。";

					errTextObj.SetActive(true);
					errText.text = "エラー：アカウント作成に失敗しました。";

					createButtonObj.SetActive(true);
					break;

				case AccountManager.ACCOUNTSTATE_SUCCESS:
					announceText.text = "アカウント作成しました。";
					break;

				default:
					break;
			}
		}
	}

	/* アカウント作成クリックイベント */
	public void OnClickCreateButton()
	{
		if (inputField.text == "")
		{
			errTextObj.SetActive(true);
			errText.text = "エラー：ユーザ名を入力してください。";
		}
		else
		{
			errTextObj.SetActive(false);
			if (AccountManager.GetAccountState() == AccountManager.ACCOUNTSTATE_INIT ||
				AccountManager.GetAccountState() == AccountManager.ACCOUNTSTATE_FAILED)
			{
				gs2Manager.AccountCreateRequest();
				AccountPrefs.SetAccountUsername(inputField.text);
				createButtonObj.SetActive(false);
			}
		}
	}

	/* ログインボタンクリックイベント */
	public void OnClickLoginButton()
	{
		if (AccountManager.GetAccountState() == AccountManager.ACCOUNTSTATE_INIT ||
				AccountManager.GetAccountState() == AccountManager.ACCOUNTSTATE_FAILED)
		{
			if (inputField.text != AccountPrefs.GetAccountUsername())
			{
				errTextObj.SetActive(true);
				errText.text = "エラー：ユーザ名が無効です。";
			}
            else
            {
				errTextObj.SetActive(false);
				gs2Manager.LoginRequest();
				loginButtonObj.SetActive(false);
			}

		}
	}

	/* ログアウトボタンクリックイベント */
	public void OnClickLogoutButton()
	{
		if (AccountManager.GetAccountState() == AccountManager.ACCOUNTSTATE_SUCCESS)
		{
			errTextObj.SetActive(false);
			gs2Manager.LogoutRequest();
			logoutButtonObj.SetActive(false);
		}
	}

	/* アカウント生成状態をチェック */
	private bool CheckAccountCreated()
	{
		if (AccountPrefs.GetAccountId() == null || AccountPrefs.GetAccountPassword() == null)
		{
			// アカウントID, パスワードが設定されていない場合、falseを返す。
			return false;
		}
		else
		{
			// アカウントID, パスワードが設定済みの場合、trueを返す。
			return true;
		}
	}
}