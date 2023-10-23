using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Solution {
    public class BallSpawner : MonoBehaviour_Singleton<BallSpawner> {
        #region Variable
        private GameObject spawnObject = null;
        private Transform[] spawnPositions;
        private Queue<GameObject> spawnedBallObject = new Queue<GameObject>();
        private Queue<eColorType> spawnedBallOfColor = new Queue<eColorType>();

        private IEnumerator spawnCoroutine = null;
        private WaitForSeconds spawnDelay = new WaitForSeconds(1f);
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
            spawnCoroutine = Spawn();
        }

        private void Init() {
            StartCoroutine(spawnCoroutine);
        }
        #endregion

        #region Definition Function
        public bool CheckTheBottomBallType(eColorType type) {
            return type == spawnedBallObject.Peek().GetComponent<Ball>().BallColor;
        }

        public void DequeueSpawnedBallObject() {
            GameObject obj = spawnedBallObject.Dequeue();
            obj.SetActive(false);
        }

        public void DequeueSpawnedBallOfColor() {
            spawnedBallOfColor.Dequeue();
        }

        public void EnqueueSpawnedBallOfColor(eColorType type) {
            spawnedBallOfColor.Enqueue(type);
        }

        private eColorType RandomColorNumber() {
            int rand = Random.Range(0, 3);
            return (eColorType)rand;
        }

        /// <summary>
        /// 공 생성.
        /// </summary>
        public void SpawnBall() {
            eColorType randomColor = RandomColorNumber();
            EnqueueSpawnedBallOfColor(randomColor);

            spawnObject = ObjectPooling.instance.PopObject(randomColor);
            spawnObject.transform.position = spawnedBallObject.Count % 2 > 0 ? spawnPositions[1].position : spawnPositions[2].position;
            spawnObject.SetActive(true);
            spawnedBallObject.Enqueue(spawnObject);
        }

        private bool CheckCountInStage() {
            return spawnedBallObject.Count < 8;
        }

        private IEnumerator Spawn() {
            while (true) {
                if (CheckCountInStage()) {
                    SpawnBall();
                }
                yield return spawnDelay;
            }
        }
        #endregion
    }
}