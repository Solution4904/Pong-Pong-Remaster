using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Solution {
    public class BallSpawner : MonoBehaviour_Singleton<BallSpawner> {
        #region Variable
        [SerializeField] private GameObject _ball;
        private GameObject _spawnObject = null;
        private Transform[] _spawnPositions;
        private Queue<GameObject> _spawnedBallObject = new Queue<GameObject>();
        private Queue<eColorType> _spawnedBallOfColor = new Queue<eColorType>();

        private IEnumerator _spawnCoroutine = null;
        private WaitForSeconds _spawnDelay = new WaitForSeconds(1f);
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
            _spawnPositions = transform.GetComponentsInChildren<Transform>();
            _spawnCoroutine = Spawn();
        }

        private void Init() {
            StartCoroutine(_spawnCoroutine);
        }
        #endregion

        #region Definition Function
        public void CreateBall() {
            for (int i = 0; i < 3; i++) {
                _ball.GetComponent<Ball>().SetColor((eColorType)i);
                ObjectPooling.instance.RequestPooling(_ball, (eColorType)i, 20);
            }
        }

        public bool CheckTheBottomBallType(eColorType type) {
            return type == _spawnedBallObject.Peek().GetComponent<Ball>().BallColor;
        }

        public void DequeueSpawnedBallObject() {
            GameObject obj = _spawnedBallObject.Dequeue();
            obj.SetActive(false);
        }

        public void DequeueSpawnedBallOfColor() {
            _spawnedBallOfColor.Dequeue();
        }

        public void EnqueueSpawnedBallOfColor(eColorType type) {
            _spawnedBallOfColor.Enqueue(type);
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

            _spawnObject = ObjectPooling.instance.PopObject(randomColor);
            _spawnObject.transform.position = _spawnedBallObject.Count % 2 > 0 ? _spawnPositions[1].position : _spawnPositions[2].position;
            _spawnObject.SetActive(true);
            _spawnedBallObject.Enqueue(_spawnObject);
        }

        private bool CheckCountInStage() {
            return _spawnedBallObject.Count < 8;
        }

        private IEnumerator Spawn() {
            while (true) {
                if (CheckCountInStage()) {
                    SpawnBall();
                }
                yield return _spawnDelay;
            }
        }
        #endregion
    }
}