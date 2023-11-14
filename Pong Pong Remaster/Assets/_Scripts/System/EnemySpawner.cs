using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Sol {
    public class EnemySpawner : MonoBehaviour_Singleton<EnemySpawner> {
        #region Variable
        // # Components
        [SerializeField] private Target _target;

        // # Objects
        [SerializeField] private GameObject _enemy;
        [SerializeField] private Transform _spawnTransform;
        private GameObject _spawnEnemy;
        private Queue<GameObject> _spawnedEnemys = new();
        private Queue<eEnemyType> _spawnedEnemyOfColor = new();

        private IEnumerator _spawnCoroutine = null;
        private WaitForSeconds _spawnDelay;
        #endregion

        #region Life Cycle
        private void Awake() {
            Caching();
        }
        #endregion

        #region Essential Function
        private void Caching() {
            _spawnCoroutine = Spawn();
        }
        #endregion

        #region Definition Function
        public void SpawnStart() {
            StartCoroutine(_spawnCoroutine);

            _target.SetTraceTarget(_spawnedEnemys.Peek());
        }

        public void CreateEnemys() {
            for (int i = 0; i < 3; i++) {
                _enemy.GetComponent<Enemy>().SetColor((eEnemyType)i);
                ObjectPooling.instance.RequestPooling(_enemy, (eEnemyType)i, 20);
            }
        }

        public void SetSpawnDelay() {
            _spawnDelay = new WaitForSeconds(GameManager.instance.EnemySpawnDelay);
        }

        public bool CheckEnemyType(eEnemyType type) {
            return type == _spawnedEnemys.Peek().GetComponent<Enemy>().EnemyType;
        }

        public void DequeueSpawnedEnemyObject() {
            GameObject obj = _spawnedEnemys.Dequeue();
            obj.SetActive(false);

            _target.SetTraceTarget(_spawnedEnemys.Peek());
        }

        public void DequeueSpawnedEnemyOfColor() {
            _spawnedEnemyOfColor.Dequeue();
        }

        public void EnqueueSpawnedEnemyOfColor(eEnemyType type) {
            _spawnedEnemyOfColor.Enqueue(type);
        }

        private eEnemyType RandomTypeToNumber() {
            int rand = Random.Range(0, 3);
            return (eEnemyType)rand;
        }

        public void SpawnEnemy() {
            eEnemyType randomColor = RandomTypeToNumber();
            EnqueueSpawnedEnemyOfColor(randomColor);

            _spawnEnemy = ObjectPooling.instance.PopObject(randomColor);
            _spawnEnemy.transform.position = _spawnTransform.position;
            _spawnEnemy.SetActive(true);
            _spawnedEnemys.Enqueue(_spawnEnemy);
        }

        private IEnumerator Spawn() {
            while (true) {
                SpawnEnemy();

                yield return _spawnDelay;
            }
        }
        #endregion
    }
}