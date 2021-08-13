using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorInfo : MonoBehaviour
{
	// エラーステータス定義情報
	readonly public static int NON_ERROR = 0;                       // エラー無し
	readonly public static int ERROR_ID_SDKINITFAILED = 1;          // SDK初期化異常
	readonly public static int ERROR_ID_CREATEACCOUNTFAILED = 2;    // アカウント生成失敗
	readonly public static int ERROR_ID_LOGINFAILED = 3;            // ログイン失敗	

	private static int errorStatus = 0;

	// エラー情報を登録
	public static void SetErrorStatus(int errStatus)
	{
		Debug.Log("ErrorDetection:" + errorStatus);

		errorStatus = errStatus;
	}

	// エラー情報を取得
	public static int GetErrorStatus()
	{
		return errorStatus;
	}
}