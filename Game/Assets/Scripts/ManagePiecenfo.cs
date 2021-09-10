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
            // �G���[�����������ꍇ�ɓ��B
            // r.Error �͔���������O�I�u�W�F�N�g���i�[����Ă���
        }
                else
                {
                    Debug.Log(r.Result.Items); // list[ItemSet] �L����������{model_name}
            Debug.Log(r.Result.ItemModel.Name); // string �A�C�e�����f���̎�ޖ�
            Debug.Log(r.Result.ItemModel.Metadata); // string �A�C�e�����f���̎�ނ̃��^�f�[�^
            Debug.Log(r.Result.ItemModel.StackingLimit); // long �X�^�b�N�\�ȍő吔��
            Debug.Log(r.Result.ItemModel.AllowMultipleStacks); // boolean �X�^�b�N�\�ȍő吔�ʂ𒴂����������g�ɃA�C�e����ۊǂ��邱�Ƃ�������
            Debug.Log(r.Result.ItemModel.SortValue); // integer �\������
            Debug.Log(r.Result.Inventory.InventoryId); // string �C���x���g��
            Debug.Log(r.Result.Inventory.InventoryName); // string �C���x���g�����f����
            Debug.Log(r.Result.Inventory.CurrentInventoryCapacityUsage); // integer ���݂̃C���x���g���̃L���p�V�e�B�g�p��
            Debug.Log(r.Result.Inventory.CurrentInventoryMaxCapacity); // integer ���݂̃C���x���g���̍ő�L���p�V�e�B
            Debug.Log(r.Result.Body); // string �����Ώۂ̃A�C�e���Z�b�g���
            Debug.Log(r.Result.Signature); // string ����
        }
            },
            gameSession,    // GameSession ���O�C����Ԃ�\���Z�b�V�����I�u�W�F�N�g
            namespaceName,   //  �l�[���X�y�[�X��
            inventoryName,   //  �C���x���g���̎�ޖ�
            itemName,   //  �A�C�e�����f���̎�ޖ�
            keyId,   //  �����̔��s�Ɏg�p����Í��� ��GRN
            itemSetName   //  �A�C�e���Z�b�g�����ʂ��閼�O(�I�v�V�����l)
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
            // �G���[�����������ꍇ�ɓ��B
            // r.Error �͔���������O�I�u�W�F�N�g���i�[����Ă���
        }
        else
        {
            Debug.Log(r.Result.Item.Name); // string �t�H�[���̕ۑ��̈�̖��O
            Debug.Log(r.Result.Item.Index); // integer �ۑ��̈�̃C���f�b�N�X
            Debug.Log(r.Result.Item.Slots); // list[Slot] �X���b�g���X�g
            Debug.Log(r.Result.Mold.Name); // string �t�H�[���̕ۑ��̈�̖��O
            Debug.Log(r.Result.Mold.UserId); // string ���[�U�[ID
            Debug.Log(r.Result.Mold.Capacity); // integer ���݂̃L���p�V�e�B
            Debug.Log(r.Result.MoldModel.Name); // string �t�H�[���̕ۑ��̈於
            Debug.Log(r.Result.MoldModel.Metadata); // string ���^�f�[�^
            Debug.Log(r.Result.MoldModel.FormModel.Name); // string �t�H�[���̎�ޖ�
            Debug.Log(r.Result.MoldModel.FormModel.Metadata); // string �t�H�[���̎�ނ̃��^�f�[�^
            Debug.Log(r.Result.MoldModel.FormModel.Slots); // list[SlotModel] �X���b�g���X�g
            Debug.Log(r.Result.MoldModel.InitialMaxCapacity); // integer �t�H�[����ۑ��ł��鏉���L���p�V�e�B
            Debug.Log(r.Result.MoldModel.MaxCapacity); // integer �t�H�[����ۑ��ł���L���p�V�e�B
            Debug.Log(r.Result.FormModel.Name); // string �t�H�[���̎�ޖ�
            Debug.Log(r.Result.FormModel.Metadata); // string �t�H�[���̎�ނ̃��^�f�[�^
            Debug.Log(r.Result.FormModel.Slots); // list[SlotModel] �X���b�g���X�g
        }
    },
    gs2Client.profile.Gs2Session,    // GameSession ���O�C����Ԃ�\���Z�b�V�����I�u�W�F�N�g
    "t_namespace",   //  �l�[���X�y�[�X��
    "t_moldName",   //  �t�H�[���̕ۑ��̈�̖��O
    index,   //  �ۑ��̈�̃C���f�b�N�X
    slots,   //  �Ґ�����X���b�g�̃��X�g
    keyId,   //  �����̔��s�Ɏg�p���� GS2-Key �̈Í���GRN
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
