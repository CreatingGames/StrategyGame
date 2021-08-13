using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountConnectSequence : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	// アカウント作成要求
	public void AccountCreateRequest()
	{
		StartCoroutine(AccountManager.CreateAccount());
	}

	// ログイン要求
	public void LoginRequest()
	{
		StartCoroutine(AccountManager.LoginAccount());
	}

	// ログイン要求
	public void LogoutRequest()
	{
		StartCoroutine(AccountManager.LogoutAccount());
	}

	// 終了処理
	private void OnApplicationQuit()
	{
		AccountManager.FinalizeGS2SDK();
	}
}