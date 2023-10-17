using UnityEngine;

namespace Solution {
    public class GameManager : MonoBehaviour_Singleton<GameManager> {
        #region Variable
        public GameLevel GameLevel { get; private set; }

        [SerializeField] private GameObject Ball;
        #endregion

        #region Life Cycle
        private void Awake() {
            CreateBall();
        }

        private void Start() {

        }
        #endregion

        #region Essential Function
        private void CreateBall() {
            int n = (int)GameLevel > 0 ? 5 : 3;
            for (int i = 0; i < n; i++) {
                Ball.GetComponent<Ball>().SetType((ColorType)i);
                ObjectPooling.instance.RequestPooling(Ball, (ColorType)i, 20);
            }
        }
        #endregion

        #region Definition Function

        #endregion
    }
}