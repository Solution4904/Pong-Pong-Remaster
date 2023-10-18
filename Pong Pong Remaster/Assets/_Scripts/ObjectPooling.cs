using System;
using System.Collections.Generic;
using UnityEngine;


namespace Solution {
    public class ObjectPooling : MonoBehaviour_Singleton<ObjectPooling> {
        #region Variable
        private Dictionary<Enum, Transform> _parentTransformDic = new Dictionary<Enum, Transform>();
        private Dictionary<Enum, Queue<GameObject>> _queueDic = new Dictionary<Enum, Queue<GameObject>>();
        #endregion

        #region Definition Function
        /// <summary>
        /// 오브젝트 풀링을 요청받음
        /// </summary>
        /// <param name="objs">생성시킬 오브젝트</param>
        /// <param name="type">생성시킬 오브젝트의 타입</param>
        /// <param name="amount">생성시킬 수량</param>
        public void RequestPoolingByArray(GameObject[] objs, Enum type, int amount) {
            PoolingByArray(type, amount, objs);
        }
        public void RequestPooling(GameObject obj, Enum type, int amount) {
            Pooling(type, amount, obj);
        }

        private void PoolingByArray(Enum type, int amount, GameObject[] objs = null) {
            Transform parent = GetParentToSameType(type);
            Queue<GameObject> queue = GetQueueToSameType(type);

            // 요청받았던 적 있는 타입의 오브젝트 추가 생성 케이스
            if (objs == null) {
                for (int i = 0; i < amount; i++) {
                    GameObject tempObj = Instantiate(queue.Dequeue(), parent, false);
                    //tempObj.transform.SetParent(parent);
                    tempObj.SetActive(false);
                    queue.Enqueue(tempObj);
                }
            }
            // 처음 요청 받은 타입의 오브젝트 생성 케이스
            else {
                foreach (GameObject go in objs) {
                    for (int i = 0; i < amount; i++) {
                        GameObject tempObj = Instantiate(go, parent, false);
                        //tempObj.transform.SetParent(parent);
                        tempObj.SetActive(false);
                        queue.Enqueue(tempObj);
                    }
                }
            }
        }
        private void Pooling(Enum type, int amount, GameObject obj = null) {
            Transform parent = GetParentToSameType(type);
            Queue<GameObject> queue = GetQueueToSameType(type);

            // 요청받았던 적 있는 타입의 오브젝트 추가 생성 케이스
            if (obj == null) {
                for (int i = 0; i < amount; i++) {
                    GameObject tempObj = Instantiate(queue.Dequeue(), parent, false);
                    //tempObj.transform.SetParent(parent);
                    tempObj.SetActive(false);
                    queue.Enqueue(tempObj);
                }
            }
            // 처음 요청 받은 타입의 오브젝트 생성 케이스
            else {
                for (int i = 0; i < amount; i++) {
                    GameObject tempObj = Instantiate(obj, parent, false);
                    //tempObj.transform.SetParent(parent);
                    tempObj.SetActive(false);
                    queue.Enqueue(tempObj);
                }
            }
        }

        /// <summary>
        /// 해당 타입의 부모 트랜스폼 획득
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Transform GetParentToSameType(Enum type) {
            Transform tr = null;

            if (_parentTransformDic.ContainsKey(type)) {
                tr = _parentTransformDic[type];
            } else {
                GameObject go = new GameObject($"Parent_{type}");
                go.transform.SetParent(transform.root);
                _parentTransformDic.Add(type, go.transform);
                tr = go.transform;
            }

            return tr;
        }

        /// <summary>
        /// 해당 타입의 Queue 획득
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Queue<GameObject> GetQueueToSameType(Enum type) {
            if (_queueDic.ContainsKey(type)) {
                return _queueDic[type];
            } else {
                Queue<GameObject> queue = new Queue<GameObject>();
                _queueDic.Add(type, queue);
                return queue;
            }
        }

        /// <summary>
        /// 오브젝트 반환 (배열)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public GameObject[] PopObject(Enum type, int amount, Transform parent = null) {
            if (!CheckExistPoolingObject(type)) {
                Debug.LogWarning($"{type} Never requested");
                return null;
            }
            if (!CheckPoolingObjectAmount(type, amount)) {
                Pooling(type, amount * 5);
            }

            GameObject[] objs = new GameObject[amount];
            for (int i = 0; i < amount; i++) {
                objs[i] = GetQueueToSameType(type).Dequeue();
            }
            return objs;
        }

        /// <summary>
        /// 오브젝트 반환
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public GameObject PopObject(Enum type, Transform parent = null) {
            if (!CheckExistPoolingObject(type)) {
                Debug.LogWarning($"{type} Never requested");
                return null;
            }
            if (!CheckPoolingObjectAmount(type, 1)) {
                Pooling(type, 5);
            }

            return GetQueueToSameType(type).Dequeue();
        }

        /// <summary>
        /// 해당하는 타입의 오브젝트 딕셔너리가 존재하는지 확인
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool CheckExistPoolingObject(Enum type) {
            return _queueDic.ContainsKey(type);
        }

        /// <summary>
        /// 해당하는 타입의 오브젝트가 필요한 수량만큼 존재하는지 확인
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private bool CheckPoolingObjectAmount(Enum type, int amount) {
            return _queueDic[type].Count - 1 >= amount;
        }
        #endregion
    }
}