using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class FormationData : MonoBehaviour
{
    public PieceData[,] MyFormationBoard;
    public PieceData[,] OpponentFormationBoard;
    public bool InitializedMyformation = false;
    public bool InitializedOpponentformation = false;
    public void InitMyFormationData()
    {
        string _dataPath;
        _dataPath = Path.Combine(Application.persistentDataPath, "Formations.json");
        // 念のためファイルの存在チェック
        if (!File.Exists(_dataPath))
        {
            return;
        }

        // JSONデータとしてデータを読み込む
        var json = File.ReadAllText(_dataPath);

        // JSON形式からオブジェクトにデシリアライズ
        var obj = JsonUtility.FromJson<CreateFormationController.FormationDatas>(json);

        int pieces = obj.Formations.Length;
        MyFormationBoard = new PieceData[2, 5];
        for (int i = 0; i < pieces; i++)
        {
            int x = obj.Formations[i].x;
            int y = obj.Formations[i].y;
            MyFormationBoard[y, x] = gameObject.AddComponent<PieceData>();
            MyFormationBoard[y, x].UpperLeft = obj.Formations[i].UL;
            MyFormationBoard[y, x].UpperRight = obj.Formations[i].UR;
            MyFormationBoard[y, x].LowerLeft = obj.Formations[i].LL;
            MyFormationBoard[y, x].LowerRight = obj.Formations[i].LR;
            MyFormationBoard[y, x].Forward = obj.Formations[i].F;
            MyFormationBoard[y, x].Backward = obj.Formations[i].B;
            MyFormationBoard[y, x].Right = obj.Formations[i].R;
            MyFormationBoard[y, x].Left = obj.Formations[i].L;
            MyFormationBoard[y, x].King = obj.Formations[i].King;
        }

        InitializedMyformation = true;

    }
    public void InitOpponentFormationData()
    {
        string _dataPath;
        _dataPath = Path.Combine(Application.persistentDataPath, "OppnentFormations.json");
        // 念のためファイルの存在チェック
        if (!File.Exists(_dataPath))
        {
            return;
        }

        // JSONデータとしてデータを読み込む
        var json = File.ReadAllText(_dataPath);

        // JSON形式からオブジェクトにデシリアライズ
        var obj = JsonUtility.FromJson<CreateFormationController.FormationDatas>(json);

        int pieces = obj.Formations.Length;
        OpponentFormationBoard = new PieceData[2, 5];
        for (int i = 0; i < pieces; i++)
        {
            int x = obj.Formations[i].x;
            int y = obj.Formations[i].y;
            OpponentFormationBoard[y, x] = gameObject.AddComponent<PieceData>();
            OpponentFormationBoard[y, x].UpperLeft = obj.Formations[i].UL;
            OpponentFormationBoard[y, x].UpperRight = obj.Formations[i].UR;
            OpponentFormationBoard[y, x].LowerLeft = obj.Formations[i].LL;
            OpponentFormationBoard[y, x].LowerRight = obj.Formations[i].LR;
            OpponentFormationBoard[y, x].Forward = obj.Formations[i].F;
            OpponentFormationBoard[y, x].Backward = obj.Formations[i].B;
            OpponentFormationBoard[y, x].Right = obj.Formations[i].R;
            OpponentFormationBoard[y, x].Left = obj.Formations[i].L;
            OpponentFormationBoard[y, x].King = obj.Formations[i].King;
        }
        
        InitializedOpponentformation = true;
    }
}
