using System.Collections;
using UnityEngine;

namespace Sol {
    public class GameManager : MonoBehaviour_Singleton<GameManager> {
        #region Variable
        [field: SerializeField] public BallSpawner BallSpawner { get; private set; }
        [field: SerializeField] public ScoreSystem ScoreSystem { get; private set; }
        [field: SerializeField] public ComboSystem ComboSystem { get; private set; }
        [field: SerializeField] public Countdown Countdown { get; private set; }

        public int Score { get; private set; }
        public int Combo { get; private set; }
        private float _ballSpeed = 5;//3;
        public float BallSpeed {
            get {
                return _ballSpeed;
            }
            private set {
                _ballSpeed = value > 8 ? 8 : value;
            }
        }
        private float _ballSpawnDelay = 1;//1.5f;
        public float BallSpawnDelay {
            get {
                return _ballSpawnDelay;
            }
            private set {
                _ballSpawnDelay = value < 0.3f ? 0.3f : value;
            }
        }

        private WaitForSeconds _difficultyRisingDelay;
        private IEnumerator _difficultyChanger;
        #endregion

        #region Life Cycle
        private void Awake() {
            Caching();
        }

        private void Start() {
            Init();
        }
        #endregion

        #region Essential Function
        private void Caching() {
            _difficultyRisingDelay = new WaitForSeconds(3);
            _difficultyChanger = DifficultyChange();
        }

        private void Init() {
            GameStart();

            Time.timeScale = 1; // 임시
        }
        #endregion

        #region Definition Function
        public void GameStart() {
            BallSpawner.SetSpawnDelay();
            BallSpawner.CreateBall();
            Countdown.StartCountDown(3);

            StartCoroutine(_difficultyChanger);
        }

        public void GameOver() {
            UIManager.instance.ShowPanel(ePanelType.Result);
            Time.timeScale = 0; // 임시
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

        private IEnumerator DifficultyChange() {
            while (true) {
                yield return _difficultyRisingDelay;

                BallSpeed += 0.2f;
                BallSpawnDelay -= 0.1f;
                BallSpawner.SetSpawnDelay();
            }
        }
        #endregion
    }
}