using UnityEngine;
using UnityEngine.UI;

namespace Solution {
    public class TouchButton : MonoBehaviour {
        #region Variable
        public eColorType ColorType;
        private Button _button;
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
            bool correct = BallSpawner.instance.CheckTheBottomBallType(ColorType);

            if (correct) {
                BallSpawner.instance.DequeueSpawnedBallObject();
                BallSpawner.instance.DequeueSpawnedBallOfColor();

                GameManager.instance.GetScore();
                GameManager.instance.GetCombo();
            }
        }
        #endregion
    }
}