using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Gs2.Core;
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Gs2Account.Result;
using Gs2.Unity.Util;

public class AccountPrefs : MonoBehaviour
{
	// アカウント情報を保存
	public static void SetAccountInfo(EzAccount account)
	{
		PlayerPrefs.SetString("AccountId", account.UserId);
		PlayerPrefs.SetString("AccountPassword", account.Password);
	}

	// ユーザ名を保存
	public static void SetAccountUsername(string username)
	{
		PlayerPrefs.SetString("AccountUsername", username);
	}

	// アカウントIDを取得
	public static string GetAccountId()
	{
		// 保存情報確認
		if (PlayerPrefs.HasKey("AccountId") == true)
		{
			// 保存あり
			return PlayerPrefs.GetString("AccountId"); ;
		}
		else
		{
			// 保存なし
			return null;
		}
	}

	// アカウントパスワードを取得
	public static string GetAccountPassword()
	{
		// 保存情報確認
		if (PlayerPrefs.HasKey("AccountPassword") == true)
		{
			// 保存あり
			return PlayerPrefs.GetString("AccountPassword"); ;
		}
		else
		{
			// 保存なし
			return null;
		}
	}

	// ユーザ名を取得
	public static string GetAccountUsername()
	{
		// 保存情報確認
		if (PlayerPrefs.HasKey("AccountUsername") == true)
		{
			// 保存あり
			return PlayerPrefs.GetString("AccountUsername");
		}
		else
		{
			// 保存なし
			return null;
		}
	}
}