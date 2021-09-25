using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateFormationController : MonoBehaviour
{
    [System.Serializable]
    public struct FormationDatas
    {
        public int StrategyPoint;
        [System.Serializable]
        public struct Formation
        {
            public int x;
            public int y;
            public int UL;
            public int UR;
            public int LL;
            public int LR;
            public int L;
            public int R;
            public int F;
            public int B;
            public bool King;
        }
        public Formation[] Formations;
        public void print()
        {
            foreach (var item in Formations)
            {
                Debug.Log("[x:" + item.x +
                          "][y:" + item.y + "]");
            }
        }
    }
    private string _dataPath;
    private void Start()
    {
        _dataPath = Path.Combine(Application.persistentDataPath, "Formations.json");
        print(_dataPath);
    }
    public void OnSave()
    {
        var obj = new FormationDatas
        {
            StrategyPoint = 0
        };
        obj.Formations = new FormationDatas.Formation[1];
        obj.Formations[0].x = 0;
        obj.Formations[0].y = 1;
        obj.Formations[0].UL = 0;
        obj.Formations[0].UR = 0;
        obj.Formations[0].LL = 0;
        obj.Formations[0].LR = 0;
        obj.Formations[0].L = 0;
        obj.Formations[0].R = 0;
        obj.Formations[0].F = 0;
        obj.Formations[0].B = 0;
        obj.Formations[0].King = true;

        // JSON�`���ɃV���A���C�Y
        var json = JsonUtility.ToJson(obj, false);

        // JSON�f�[�^���t�@�C���ɕۑ�
        File.WriteAllText(_dataPath, json);
    }
    // JSON�`�������[�h���ăf�V���A���C�Y
    public void OnLoad()
    {
        // �O�̂��߃t�@�C���̑��݃`�F�b�N
        if (!File.Exists(_dataPath)) return;

        // JSON�f�[�^�Ƃ��ăf�[�^��ǂݍ���
        var json = File.ReadAllText(_dataPath);

        // JSON�`������I�u�W�F�N�g�Ƀf�V���A���C�Y
        var obj = JsonUtility.FromJson<FormationDatas>(json);

        obj.print();

    }
}
