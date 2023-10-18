using System.Collections.Generic;
using UnityEngine;


namespace Solution {
    public class SpawnManager : MonoBehaviour_Singleton<SpawnManager> {
        #region Variable
        private GameObject spawnObject = null;
        [SerializeField] private Transform[] spawnPositions;
        private Queue<ColorType> spawnedBallHistory = new Queue<ColorType>();
        private Queue<GameObject> spawnedBallObject = new Queue<GameObject>();
        #endregion

        #region Life Cycle
        private void Awake() {
            Caching();
        }

        private void Start() {
            Init();
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SpawnBall();
            }
        }
        #endregion

        #region Essential Function
        private void Caching() {
            spawnPositions = transform.GetComponentsInChildren<Transform>();
        }

        private void Init() {

        }
        #endregion

        #region Definition Function
        public bool CheckTheBottomBallType(ColorType type) {
            return type == spawnedBallHistory.Peek();
        }

        public void DequeueSpawnedBall() {
            spawnedBallObject.Dequeue().SetActive(false);
        }

        public void EnqueueSpawnedBallHistory(ColorType type) {
            spawnedBallHistory.Enqueue(type);
        }

        private ColorType RandomSelectionOfItems() {
            int rand = Random.Range(0, (int)GameManager.instance.GameLevel > 0 ? 5 : 3);
            return (ColorType)rand;
        }

        public void SpawnBall() {
            ColorType randomType = RandomSelectionOfItems();
            EnqueueSpawnedBallHistory(randomType);

            spawnObject = ObjectPooling.instance.PopObject(randomType);
            spawnObject.transform.position = spawnedBallHistory.Count % 2 > 0 ? spawnPositions[1].position : spawnPositions[2].position;
            spawnObject.SetActive(true);

            spawnedBallObject.Enqueue(spawnObject);
        }
        #endregion
    }

    /*
     * 1. ball 오브젝트는 미리 생성.
     * 2. 생성과 동시에 색상을 랜덤하게 선정.
     * 3. 선정된 색상을 Queue에 넣어둠.
     * 4. 버튼 클릭 시 Queue Peek()으로 확인하고 맞다면 Dequeue
     */
}