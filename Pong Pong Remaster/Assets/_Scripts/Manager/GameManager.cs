using System.Collections;
using UnityEngine;

namespace Sol {
    public class GameManager : MonoBehaviour_Singleton<GameManager> {
        #region Variable
        // # Components
        [field: SerializeField] public ScoreSystem ScoreSystem { get; private set; }
        [field: SerializeField] public ComboSystem ComboSystem { get; private set; }
        [field: SerializeField] public Countdown Countdown { get; private set; }
        [field: SerializeField] public GameStartPanel GameStartPanel { get; private set; }

        // # Values
        public int Score { get; private set; }
        public int Combo { get; private set; }
        private float _enemySpeed = 3;
        public float EnemySpeed {
            get {
                return _enemySpeed;
            }
            private set {
                _enemySpeed = value > 8 ? 8 : value;
            }
        }
        private float _enemySpawnDelay = 1;
        public float EnemySpawnDelay {
            get {
                return _enemySpawnDelay;
            }
            private set {
                _enemySpawnDelay = value < 0.3f ? 0.3f : value;
                EnemySpawner.instance.SetSpawnDelay();
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
            WaitingToStart();
        }
        #endregion

        #region Definition Function
        private void WaitingToStart() {
            Time.timeScale = 1; // 임시
            GameStartPanel.SetClickEvent(() => {
                GameStart();
                UIManager.instance.HidePanel();
            });

            UIManager.instance.ShowPanel(ePanelType.ClickToStart);
        }

        public void GameStart() {
            EnemySpawner.instance.SetSpawnDelay();
            EnemySpawner.instance.CreateEnemys();
            EnemySpawner.instance.SpawnStart();
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

                EnemySpeed += 0.2f;
                EnemySpawnDelay -= 0.1f;
            }
        }
        #endregion
    }
}