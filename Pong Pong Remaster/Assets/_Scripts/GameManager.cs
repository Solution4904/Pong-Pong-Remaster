using UnityEngine;

namespace Solution {
    public class GameManager : MonoBehaviour_Singleton<GameManager> {
        #region Variable
        [SerializeField] private GameObject Ball;
        #endregion

        #region Life Cycle
        private void Awake() {
            Init();
        }

        private void Start() {

        }
        #endregion

        #region Essential Function
        private void Init() {
            CreateBall();
        }
        #endregion

        #region Definition Function
        private void CreateBall() {
            for (int i = 0; i < 3; i++) {
                Ball.GetComponent<Ball>().SetColor((eColorType)i);
                ObjectPooling.instance.RequestPooling(Ball, (eColorType)i, 20);
            }
        }
        #endregion
    }
}