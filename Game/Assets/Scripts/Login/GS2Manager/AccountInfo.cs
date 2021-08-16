using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountInfo : MonoBehaviour
{
    
    // クレデンシャルの定義情報
    private readonly static string applicationClientId = "GKIBT3C-iAYWbW7CuI8nCh0QhxPH0gjHlJcAtI2t1TACE7cPFBWuCYXgLKamIHW4gM-";
    private readonly static string applicationClientSecret = "RmJkQnY3TWI2S3pabEg0clVwOTMyem5LeUN0UmRGQmE=";


    // アカウントの定義情報
    private readonly static string accountNameSpaceName = "game-0001";
    private readonly static string keyAccountAuthenticationKeyId = "grn:gs2:ap-northeast-1:iy1YogLz-StrategyGame:key:account-encryption-key-namespace:key:account-encryption-key";


    // ClientIdの取得処理
    public static string GetApplicationClientId()
    {
        return applicationClientId;
    }

    // ClientSecretの取得処理
    public static string GetApplicationClientSecret()
    {
        return applicationClientSecret;
    }

    // AccountNameSapceNameの取得処理
    public static string GetAccountNameSpaceName()
    {
        return accountNameSpaceName;
    }

    // AccountNameSapceNameの取得処理
    public static string GetKeyAccountAuthenticationKeyId()
    {
        return keyAccountAuthenticationKeyId;
    }
}