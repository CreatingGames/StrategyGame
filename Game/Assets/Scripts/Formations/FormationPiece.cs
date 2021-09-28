using UnityEngine;

public class FormationPiece : PieceData
{
    // �C���X�y�N�^�[�ɕ\������p
    [SerializeField] int t_UpperLeft = 0;
    [SerializeField] int t_LowerLeft = 0;
    [SerializeField] int t_UpperRight = 0;
    [SerializeField] int t_LowerRight = 0;
    [SerializeField] int t_Left = 0;
    [SerializeField] int t_Right = 0;
    [SerializeField] int t_Forward = 0;
    [SerializeField] int t_Backward = 0;
    [SerializeField] int t_X = 0;
    [SerializeField] int t_Y = 0;
    [SerializeField] int t_StrategyPoint = 0;
    private CreateFormationPalette CreateFormationPalette;
    // ��̏��
    public bool Evolved { get; set; } = false;// �i���ς݂��ǂ���
    public bool Opponent { get; set; } = false;// �G���ǂ���
    public bool StoppingAction { get; set; } = false;// �i�����͐�������ǂ���

    public bool readyMove = false;
    public int StrategyPoint { get; set; } = 0;// �s���͈͂̍��v
    public bool Invasion = false;// �G�w�n�ɐN���������Ƃ����邩
    private void Start()
    {
        CreateFormationPalette = GameObject.Find("Palette").GetComponent<CreateFormationPalette>();
    }
    private void Update()
    {
        // ��I������Ă�����
        ToInspector();
    }
    // �s���͈͂̏�����
    public void InitActionRange(int UpperLeft, int LowerLeft, int UpperRight, int LowerRight, int Left, int Right, int Forward, int Backward)
    {
        this.UpperLeft = UpperLeft;
        this.LowerLeft = LowerLeft;
        this.UpperRight = UpperRight;
        this.LowerRight = LowerRight;
        this.Left = Left;
        this.Right = Right;
        this.Forward = Forward;
        this.Backward = Backward;
    }
    // ��̍��W�̏�����
    public void InitPosition(int x, int y)
    {
        PositionX = x;
        PositionY = y;
    }
    // �C���X�y�N�^�[�p�ϐ��ɑ�����Ă���
    public void ToInspector()
    {
        t_UpperLeft = UpperLeft;
        t_LowerLeft = LowerLeft;
        t_UpperRight = UpperRight;
        t_LowerRight = LowerRight;
        t_Left = Left;
        t_Right = Right;
        t_Forward = Forward;
        t_Backward = Backward;
        t_X = PositionX;
        t_Y = PositionY;
        t_StrategyPoint = StrategyPoint;
    }
    public void SetEvolveData(EvolveData evolve)
    {
        EvolveUpperLeft = evolve.EvolveUpperLeft;
        EvolveUpperRight = evolve.EvolveUpperRight;
        EvolveLowerLeft = evolve.EvolveLowerLeft;
        EvolveLowerRight = evolve.EvolveLowerRight;
        EvolveForward = evolve.EvolveForward;
        EvolveBackward = evolve.EvolveBackward;
        EvolveLeft = evolve.EvolveLeft;
        EvolveRight = evolve.EvolveRight;
        UpperLeft += EvolveUpperLeft;
        UpperRight += EvolveUpperRight;
        LowerLeft += EvolveLowerLeft;
        LowerRight += EvolveLowerRight;
        Forward += EvolveForward;
        Backward += EvolveBackward;
        Left += EvolveLeft;
        Right += EvolveRight;
        Evolved = true;
        StoppingAction = true;
        ToInspector();
    }
    public void ResetEvolveData()
    {
        UpperLeft -= EvolveUpperLeft;
        UpperRight -= EvolveUpperRight;
        LowerLeft -= EvolveLowerLeft;
        LowerRight -= EvolveLowerRight;
        Forward -= EvolveForward;
        Backward -= EvolveBackward;
        Left -= EvolveLeft;
        Right -= EvolveRight;
        EvolveUpperLeft = 0;
        EvolveUpperRight = 0;
        EvolveLowerLeft = 0;
        EvolveLowerRight = 0;
        EvolveForward = 0;
        EvolveBackward = 0;
        EvolveLeft = 0;
        EvolveRight = 0;
        Evolved = false;
        StoppingAction = false;
        ToInspector();
    }

    public void OnClicked()
    {
        CreateFormationPalette.OnPieceClicked(PositionX, PositionY, UpperRight, UpperLeft, LowerLeft, LowerRight, Forward, Backward, Right, Left);
    }
}
