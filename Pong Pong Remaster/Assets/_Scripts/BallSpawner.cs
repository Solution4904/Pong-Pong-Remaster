using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sol {
    public class BallSpawner : MonoBehaviour_Singleton<BallSpawner> {
        #region Variable
        [SerializeField] private GameObject _ball;
        [SerializeField] private Transform _spawnTransform;
        private GameObject _spawnObject;
        private Queue<GameObject> _spawnedBallObject = new Queue<GameObject>();
        private Queue<eColorType> _spawnedBallOfColor = new Queue<eColorType>();

        private IEnumerator _spawnCoroutine = null;
        private WaitForSeconds _spawnDelay = new WaitForSeconds(1f);//new WaitForSeconds(0.3f);
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
            _spawnObject.transform.position = _spawnTransform.position;
            _spawnObject.SetActive(true);
            _spawnedBallObject.Enqueue(_spawnObject);
        }

        private IEnumerator Spawn() {
            while (true) {
                SpawnBall();

                yield return _spawnDelay;
            }
        }
        #endregion
    }
}