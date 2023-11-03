namespace Sol {
    public class GameManager : MonoBehaviour_Singleton<GameManager> {
        #region Variable
        public BallSpawner BallSpawner;
        public ScoreSystem ScoreSystem;
        public ComboSystem ComboSystem;
        public Timer Timer;
        public Countdown Countdown;

        public int Score { get; private set; }
        public int Combo { get; private set; }
        public float Time { get; private set; } = 10;
        #endregion

        #region Life Cycle
        private void Awake() {
            Init();
        }

        private void Start() {
            Init();
        }
        #endregion

        #region Essential Function
        private void Init() {
            GameStart();
        }
        #endregion

        #region Definition Function
        public void GameStart() {
            Timer.SetTimer(Time, null, GameOver);
            Timer.TimerControl(eTimerState.Activate);

            BallSpawner.CreateBall();
            Countdown.StartCountDown(3);
            /*
             * 1. 시간 설정
             * 2. 공 생성
             * 3. 게임 시작 대기 타이머
             * 3. 대기 시간 이후 게임 시작
             */

        }

        public void GameOver() {
            UIManager.instance.ShowPanel(ePanelType.Result);
        }

        public void GetScore(int score = 100) {
            Score += score;
            ScoreSystem.SetScore(Score);
        }

        public void GetCombo(int combo = 1) {
            Combo += combo;
            ComboSystem.PopComboText(Combo);
        }

        public void ResetCombo() {
            Combo = 0;
        }

        public void PlusTime(float time = 1f) {
            Timer.PlusTime(time);
        }
        #endregion
    }
}