using UnityEngine;

public class FormationPiece : PieceData
{
    // インスペクターに表示する用
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
    // 駒の状態
    public bool Evolved { get; set; } = false;// 進化済みかどうか
    public bool Opponent { get; set; } = false;// 敵かどうか
    public bool StoppingAction { get; set; } = false;// 進化又は生成直後どうか

    public bool readyMove = false;
    public int StrategyPoint { get; set; } = 0;// 行動範囲の合計
    public bool Invasion = false;// 敵陣地に侵入したことがあるか
    private void Start()
    {
        CreateFormationPalette = GameObject.Find("Palette").GetComponent<CreateFormationPalette>();
    }
    private void Update()
    {
        // 駒が選択されている状態
        ToInspector();
    }
    // 行動範囲の初期化
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
    // 駒の座標の初期化
    public void InitPosition(int x, int y)
    {
        PositionX = x;
        PositionY = y;
    }
    // インスペクター用変数に代入していく
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
