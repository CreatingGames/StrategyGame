﻿using System;
 using Gs2.Sample.Core;
 using Gs2.Unity.Gs2Account.Model;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Gs2.Sample.AccountRegistrationLoginSample
{
    public class AccountRegistrationLoginScene : MonoBehaviour
    {
        /// <summary>
        /// アカウント操作をするためのコントローラー
        /// </summary>
        public AccountRegistrationLoginController controller;

        /// <summary>
        /// ログイン画面に表示するユーザID
        /// </summary>
        [SerializeField]
        public Text announceMsg;

        private InputField inputField;
        /// <summary>
        /// 発生したエラー
        /// </summary>
        [SerializeField]
        public Text errorMessage;


        /// <summary>
        /// ステートマシン
        /// </summary>
        private AccountRegistrationLoginStateMachine _stateMachine;

        private readonly AccountRepository _repository = new AccountRepository();

        private void Start()
        {
            controller.Initialize();
            
            if (controller.gs2AccountSetting == null)
            {
                throw new InvalidProgramException("'Gs2AccountSetting' is not null.");
            }
            if (string.IsNullOrEmpty(controller.gs2AccountSetting.accountNamespaceName))
            {
                throw new InvalidProgramException(
                    "'accountNamespaceName' of script 'Gs2AccountSetting' of 'Canvas' is not set. "+
                    "The value to be set for 'accountNamespaceName' can be created by uploading the 'initialize_account_template.yaml' bundled with the sample as a GS2-Deploy stack." +
                    "Please check README.md for details." +
                    " / " +
                    "'Canvas' の持つスクリプト 'Gs2AccountSetting' の 'accountNamespaceName' が設定されていません。" +
                    "'accountNamespaceName' に設定するべき値はサンプルに同梱されている 'initialize_account_template.yaml' を GS2-Deploy のスタックとしてアップロードすることで作成できます。" +
                    "詳しくは README.md をご確認ください。"
                    );
            }
            if (string.IsNullOrEmpty(controller.gs2AccountSetting.gatewayNamespaceName))
            {
                throw new InvalidProgramException(
                    "'gatewayNamespaceName' of script 'Gs2AccountSetting' of 'Canvas' is not set. "+
                    "The value to be set for 'gatewayNamespaceName' can be created by uploading the 'initialize_account_template.yaml' bundled with the sample as a GS2-Deploy stack." +
                    "Please check README.md for details." +
                    " / " +
                    "'Canvas' の持つスクリプト 'Gs2AccountSetting' の 'gatewayNamespaceName' が設定されていません。" +
                    "'gatewayNamespaceName' に設定するべき値はサンプルに同梱されている 'initialize_account_template.yaml' を GS2-Deploy のスタックとしてアップロードすることで作成できます。" +
                    "詳しくは README.md をご確認ください。"
                );
            }
            if (string.IsNullOrEmpty(controller.gs2AccountSetting.accountEncryptionKeyId))
            {
                throw new InvalidProgramException(
                    "'accountEncryptionKeyId' of script 'Gs2AccountSetting' of 'Canvas' is not set. "+
                    "The value to be set for 'accountEncryptionKeyId' can be created by uploading the 'initialize_account_template.yaml' bundled with the sample as a GS2-Deploy stack." +
                    "Please check README.md for details." +
                    " / " +
                    "'Canvas' の持つスクリプト 'Gs2AccountSetting' の 'accountEncryptionKeyId' が設定されていません。" +
                    "'accountEncryptionKeyId' に設定するべき値はサンプルに同梱されている 'initialize_account_template.yaml' を GS2-Deploy のスタックとしてアップロードすることで作成できます。" +
                    "詳しくは README.md をご確認ください。"
                );
            }
        
            if (controller.gs2Client == null)
            {
                controller.gs2Client = Gs2Util.LoadGlobalGameObject<Gs2Client>("Gs2Client");
                if (controller.gs2Client == null)
                {
                    throw new InvalidProgramException(
                        "Unable to find GS2 Client" +
                        "You need to set GS2 Client on 'AccountRegistrationLoginController' or place a GameObject named 'Gs2Client' in the scene." +
                        "Please check README.md for details." +
                        " / " +
                        "GS2 Client を見つけられません。" +
                        "'AccountRegistrationLoginController' に GS2 Client を設定するか、'Gs2Client' という名前の GameObject をシーン内に配置する必要があります。" +
                        "詳しくは README.md をご確認ください。"
                    );
                }
            }

            var animator = GetComponent<Animator>();
            if (animator == null)
            {
                throw new InvalidProgramException(
                    "'AccountRegistrationLoginStateMachine' that controls the state is not registered." +
                    "Check if Animator is registered in 'Canvas', if the correct controller is set, or if the script is set in the animator's Behavior" +
                    " / " + 
                    "ステートをコントロールする 'AccountRegistrationLoginStateMachine' が登録されていません." +
                    "'Canvas' に Animator が登録されているか、正しいコントローラーが設定されているか、アニメーターの Behaviour にスクリプトが設定されているかを確認してください"
                    );
            }
            _stateMachine = animator.GetBehaviour<AccountRegistrationLoginStateMachine>();
            if (_stateMachine == null)
            {
                throw new InvalidProgramException(
                    "'AccountRegistrationLoginStateMachine' that controls the state is not registered." +
                    "Check if Animator is registered in 'Canvas', if the correct controller is set, or if the script is set in the animator's Behavior" +
                    " / " + 
                    "ステートをコントロールする 'AccountRegistrationLoginStateMachine' が登録されていません." +
                    "'Canvas' に Animator が登録されているか、正しいコントローラーが設定されているか、アニメーターの Behaviour にスクリプトが設定されているかを確認してください"
                    );
            }

            _stateMachine.controller = controller;
            _stateMachine.onChangeState.AddListener(
                (_, state) =>
                {
                    InActiveAll();
                    ActiveMenuGameObject(state);
                }
            );
            controller.gs2AccountSetting.onError.AddListener(
                e => { errorMessage.text = e.Message; }
            );

            // 画面の初期状態を設定
            InActiveAll();
        }

        /// <summary>
        /// メニューパネルをすべて非表示にする
        /// </summary>
        private void InActiveAll()
        {

            transform.Find("CreateBtn").gameObject.SetActive(false);
            transform.Find("LoginBtn").gameObject.SetActive(false);
            transform.Find("RemoveBtn").gameObject.SetActive(false);
            transform.Find("BackBtn").gameObject.SetActive(false);
            transform.Find("AnnounceMsg").gameObject.SetActive(false);
            transform.Find("ErrMsg").gameObject.SetActive(false);
            transform.Find("UsernameInput").gameObject.SetActive(false);
            Debug.Log("InActiveAll_done");
        }

        /// <summary>
        /// ステートに対応したメニューパネルをアクティブ化
        /// </summary>
        /// <param name="state">ステート</param>
        /// <returns>メニューパネル</returns>
        private void ActiveMenuGameObject(AccountRegistrationLoginStateMachine.State state)
        {
            switch (state)
            {
                case AccountRegistrationLoginStateMachine.State.CreateAccountMenu:
                    transform.Find("CreateBtn").gameObject.SetActive(true);
                    transform.Find("UsernameInput").gameObject.SetActive(true);
                    announceMsg.text = "ユーザー名を入力して下さい";
                    transform.Find("AnnounceMsg").gameObject.SetActive(true);
                    return ;
                case AccountRegistrationLoginStateMachine.State.LoginMenu:
                    if (_stateMachine.account != null)
                    {
                        announceMsg.text = "Username: " + _repository.GetUsername() + "でログインしますか？";
                    }
                    Debug.Log(_repository.GetUsername());
                    transform.Find("AnnounceMsg").gameObject.SetActive(true);
                    transform.Find("LoginBtn").gameObject.SetActive(true);
                    transform.Find("RemoveBtn").gameObject.SetActive(true);
                    return ;
                case AccountRegistrationLoginStateMachine.State.LoginComplete:
                    Debug.Log("Login_complete");
                    announceMsg.text = "ログインしました";
                    transform.Find("AnnounceMsg").gameObject.SetActive(true);
                    SceneManager.LoadScene("MainScene");
                    return ;
                case AccountRegistrationLoginStateMachine.State.Error:
                    Debug.Log("Error_occured");
                    announceMsg.text = "エラーが発生しました";
                    transform.Find("ErrMsg").gameObject.SetActive(true);
                    transform.Find("BackBtn").gameObject.SetActive(true);
                    return ;
                case AccountRegistrationLoginStateMachine.State.CreateAccountProcessing:
                    _repository.LoadUsername(transform.Find("UsernameInput").gameObject.GetComponent<InputField>().text);
                    announceMsg.text = "処理中";
                    transform.Find("AnnounceMsg").gameObject.SetActive(true);
                    return;
                default:
                    Debug.Log("Default");
                    announceMsg.text = "処理中";
                    transform.Find("AnnounceMsg").gameObject.SetActive(true);
                    return ;
            }
        }

        /// <summary>
        /// アカウントの作成ボタンをクリック
        /// </summary>
        public void ClickToCreateAccount()
        {
            string UsernameBuf = transform.Find("UsernameInput").gameObject.GetComponent<InputField>().text;
            Debug.Log(UsernameBuf.Length);
            if ( UsernameBuf == "" || UsernameBuf == null || UsernameBuf.Length > 20)
            {
                announceMsg.text = "ユーザー名は20文字以内の文字列で入力してください";
                return;
            }
            var stateMachine = GetComponent<Animator>();
            stateMachine.SetTrigger(AccountRegistrationLoginStateMachine.Trigger.SelectCreateAccount.ToString());
        }

        /// <summary>
        /// ログインボタンをクリック
        /// </summary>
        public void ClickToLogin()
        {
            var stateMachine = GetComponent<Animator>();
            stateMachine.SetTrigger(AccountRegistrationLoginStateMachine.Trigger.SelectLogin.ToString());
        }

        /// <summary>
        /// アカウントの削除ボタンをクリック
        /// </summary>
        public void ClickToRemoveAccount()
        {
            var stateMachine = GetComponent<Animator>();
            stateMachine.SetTrigger(AccountRegistrationLoginStateMachine.Trigger.SelectRemoveAccount.ToString());
        }

        /// <summary>
        /// エラー内容の確認ボタンをクリック
        /// </summary>
        public void ClickToConfirmError()
        {
            var stateMachine = GetComponent<Animator>();
            stateMachine.SetTrigger(AccountRegistrationLoginStateMachine.Trigger.ConfirmError.ToString());
        }
    }
}