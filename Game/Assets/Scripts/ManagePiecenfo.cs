using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Gs2.Core.Model.Region;
//using Gs2.Core.Model.BasicGs2Credential;
//using Gs2.Core.Net.Gs2RestSession;
//using Gs2.Core.Exception.Gs2Exception;
//using Gs2.Core.AsyncResult;
//using Gs2.Gs2Formation.Gs2FormationRestClient;
//using Gs2.Gs2Formation.Request.CreateNamespaceRequest;
//using Gs2.Gs2Formation.Result.CreateNamespaceResult;
using Gs2.Core;
using Gs2.Gs2Formation.Model;
using Gs2.Gs2Formation.Result;
using Gs2.Gs2Formation;
using Gs2.Sample.Core;
using Gs2.Gs2Inventory.Model;
using Gs2.Gs2Inventory.Result;


public class ManagePiecenfo : MonoBehaviour
{
    [SerializeField]
    public Gs2Client gs2Client;

    public IEnumerator LoadData()
    {
        //AsyncResult < AsyncResult < Gs2.Gs2Account.Result.CreateNamespaceResult > asyncResult = null;
        yield return Gs2.Gs2Inventory.FormModel(
            r => {
                if (r.Error != null)
                {
            // エラーが発生した場合に到達
            // r.Error は発生した例外オブジェクトが格納されている
        }
                else
                {
                    Debug.Log(r.Result.Items); // list[ItemSet] 有効期限毎の{model_name}
            Debug.Log(r.Result.ItemModel.Name); // string アイテムモデルの種類名
            Debug.Log(r.Result.ItemModel.Metadata); // string アイテムモデルの種類のメタデータ
            Debug.Log(r.Result.ItemModel.StackingLimit); // long スタック可能な最大数量
            Debug.Log(r.Result.ItemModel.AllowMultipleStacks); // boolean スタック可能な最大数量を超えた時複数枠にアイテムを保管することを許すか
            Debug.Log(r.Result.ItemModel.SortValue); // integer 表示順番
            Debug.Log(r.Result.Inventory.InventoryId); // string インベントリ
            Debug.Log(r.Result.Inventory.InventoryName); // string インベントリモデル名
            Debug.Log(r.Result.Inventory.CurrentInventoryCapacityUsage); // integer 現在のインベントリのキャパシティ使用量
            Debug.Log(r.Result.Inventory.CurrentInventoryMaxCapacity); // integer 現在のインベントリの最大キャパシティ
            Debug.Log(r.Result.Body); // string 署名対象のアイテムセット情報
            Debug.Log(r.Result.Signature); // string 署名
        }
            },
            gameSession,    // GameSession ログイン状態を表すセッションオブジェクト
            namespaceName,   //  ネームスペース名
            inventoryName,   //  インベントリの種類名
            itemName,   //  アイテムモデルの種類名
            keyId,   //  署名の発行に使用する暗号鍵 のGRN
            itemSetName   //  アイテムセットを識別する名前(オプション値)
        );
    }
    /*
    AsyncResult<Gs2.Gs2Account.Result.CreateNamespaceResult> asyncResult = null;
    yield return gs2Client.client.GetMoldModel(
        new EzGetMoldModelResult()
            .withNamespaceName("namespace1")
            .withMoldName(""),
        r => asyncResult = r
    );
    if (asyncResult.Error != null)
    {
        throw asyncResult.Error;
    }
    var result = asyncResult.Result;
    var item = result.Item;
    */

    /*
    yield return gs2.Formation.SetForm(
    r => {
        if (r.Error != null)
        {
            // エラーが発生した場合に到達
            // r.Error は発生した例外オブジェクトが格納されている
        }
        else
        {
            Debug.Log(r.Result.Item.Name); // string フォームの保存領域の名前
            Debug.Log(r.Result.Item.Index); // integer 保存領域のインデックス
            Debug.Log(r.Result.Item.Slots); // list[Slot] スロットリスト
            Debug.Log(r.Result.Mold.Name); // string フォームの保存領域の名前
            Debug.Log(r.Result.Mold.UserId); // string ユーザーID
            Debug.Log(r.Result.Mold.Capacity); // integer 現在のキャパシティ
            Debug.Log(r.Result.MoldModel.Name); // string フォームの保存領域名
            Debug.Log(r.Result.MoldModel.Metadata); // string メタデータ
            Debug.Log(r.Result.MoldModel.FormModel.Name); // string フォームの種類名
            Debug.Log(r.Result.MoldModel.FormModel.Metadata); // string フォームの種類のメタデータ
            Debug.Log(r.Result.MoldModel.FormModel.Slots); // list[SlotModel] スリットリスト
            Debug.Log(r.Result.MoldModel.InitialMaxCapacity); // integer フォームを保存できる初期キャパシティ
            Debug.Log(r.Result.MoldModel.MaxCapacity); // integer フォームを保存できるキャパシティ
            Debug.Log(r.Result.FormModel.Name); // string フォームの種類名
            Debug.Log(r.Result.FormModel.Metadata); // string フォームの種類のメタデータ
            Debug.Log(r.Result.FormModel.Slots); // list[SlotModel] スリットリスト
        }
    },
    gs2Client.profile.Gs2Session,    // GameSession ログイン状態を表すセッションオブジェクト
    "t_namespace",   //  ネームスペース名
    "t_moldName",   //  フォームの保存領域の名前
    index,   //  保存領域のインデックス
    slots,   //  編成するスロットのリスト
    keyId,   //  署名の発行に使用した GS2-Key の暗号鍵GRN
    */

    // Start is called before the first frame update
    void Start()
    {
        LoadData();

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
