using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountInfo : MonoBehaviour
{
    
    // クレデンシャルの定義情報
    private readonly static string applicationClientId = "GKI-HJRpsKueh64OQPohmRCew4f-Ic7fp43_ZlPdjnUDg2W5WXRe523vJ6oD-xNv3j8";
    private readonly static string applicationClientSecret = "NGZWSmJJMU5TMVFxMnlwUzdnd0NnQ0RtSjltaDVXeHk=";


    // アカウントの定義情報
    private readonly static string accountNameSpaceName = "game-0001";
    private readonly static string keyAccountAuthenticationKeyId = "grn:gs2:ap-northeast-1:iy1YogLz-tryal:key:account-encryption-key-namespace:key:account-encryption-key";
 

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