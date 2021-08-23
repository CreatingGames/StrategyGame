﻿﻿using System.Collections.Generic;
using System.IO;
using Gs2.Unity.Gs2Account.Model;
using LitJson;
using UnityEngine;

 namespace Gs2.Sample.AccountRegistrationLoginSample
 {
	 [System.Serializable]
	public class AccountRepository
	{
        public AccountRegistrationLoginScene _scene;
        private static string Username; //アカウントのユーザー名

        /// <summary>
        /// セーブデータを保存するディレクトリを作成する
        /// </summary>
        private void MakeAccountDataDirectory()
        {
            
            Directory.CreateDirectory(Application.persistentDataPath + "/Gs2/");
        }

        /// <summary>
        /// セーブデータを保存するファイルパス
        /// </summary>
        /// <returns></returns>
        private string AccountDataPath()
        {
            return Application.persistentDataPath + "/Gs2/account.json";   
        }

        /// <summary>
        /// アカウント情報が存在するか確認する
        /// </summary>
        /// <returns></returns>
        public bool IsRegistered()
        {
            Debug.Log(Application.persistentDataPath + "/Gs2/");
            return File.Exists(AccountDataPath());
        }

        /// <summary>
        /// アカウント情報を永続化する
        /// </summary>
        /// <param name="account"></param>
        public void SaveAccount(EzAccount account)
        {
            if (File.Exists(AccountDataPath()))
            {
                File.Delete(AccountDataPath());
            }

            MakeAccountDataDirectory();

            var json = new JsonData
            {
                ["username"] = Username,
	            ["userId"] = account.UserId, 
	            ["password"] = account.Password
            };
            var writer = new BinaryWriter(new FileStream(AccountDataPath(), FileMode.Create, FileAccess.Write));
            writer.Write(json.ToJson());
            writer.Close();
        }
        
        /// <summary>
        /// アカウント情報を読み込む
        /// </summary>
        public EzAccount LoadAccount()
        {
            var reader = new BinaryReader(new FileStream(AccountDataPath(), FileMode.Open, FileAccess.Read));
            var json = JsonMapper.ToObject(reader.ReadString());
            
            reader.Close ();
            try
            {
                Username = (string)json["username"];
                return new EzAccount
	            {
		            UserId = (string)json["userId"],
		            Password = (string)json["password"],
	            };
            }
            catch (KeyNotFoundException)
            {
	            DeleteAccount();
	            throw;
            }
        }
        
	    /// <summary>
	    /// アカウント情報を削除
	    /// </summary>
	    public void DeleteAccount()
	    {
	        if (File.Exists(AccountDataPath()))
	        {
	            File.Delete(AccountDataPath());
	        }
            Username = null;
	    }

        //ユーザー名を返す
        public string GetUsername()
        {
            return Username;
        }

        //ユーザー名を保存する為の処理
        public void LoadUsername(string username)
        {
            if(username == null || username == "")
            {
                Debug.Log("username is null");
            }
            Username = username;
        }
	}
}
