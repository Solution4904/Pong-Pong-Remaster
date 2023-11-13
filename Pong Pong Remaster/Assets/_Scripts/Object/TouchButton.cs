using UnityEngine;
using UnityEngine.UI;

namespace Sol {
    public class TouchButton : MonoBehaviour {
        #region Variable
        // # Components
        private Button _button;

        // # Values
        public eEnemyType ColorType;        
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
            _button = GetComponent<Button>();
        }

        private void Init() {
            _button.onClick.AddListener(TouchEvent);
        }
        #endregion

        #region Definition Function
        private void TouchEvent() {
            bool correct = EnemySpawner.instance.CheckEnemyType(ColorType);

            if (correct) {
                EnemySpawner.instance.DequeueSpawnedEnemyObject();
                EnemySpawner.instance.DequeueSpawnedEnemyOfColor();

                GameManager.instance.GetScore();
                GameManager.instance.GetCombo();
            } else {
                GameManager.instance.ResetCombo();
            }
        }
        #endregion
    }
}