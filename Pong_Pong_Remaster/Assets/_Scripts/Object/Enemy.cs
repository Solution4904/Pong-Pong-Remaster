using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sol {
    public class Enemy : MonoBehaviour {
        #region Variable
        // # Values
        private Queue<Transform> _roadPointQueue;
        private Vector3 _direction;
        private Transform _destination;
        public eEnemyType EnemyType;
        private bool _goForword = true;

        // # Componets
        private Animator _animator;
        #endregion

        #region Life Cycle
        private void Awake() {
            Caching();
        }

        private void OnEnable() {
            Init();
        }

        private void LateUpdate() {
            Move();
        }
        #endregion

        #region Essential Function
        private void Init() {
            StartCoroutine(AnimationControl(eEnemyAnimationState.Move));
            _goForword = true;

            SetRoadPointQueue();
            SetNextDestinationAndDirection();
        }

        private void Caching() {
            _animator = GetComponent<Animator>();
        }
        #endregion

        #region Definition Function
        public void SetColor(eEnemyType value) {
            EnemyType = value;
        }

        private void SetRoadPointQueue() {
            _roadPointQueue = new Queue<Transform>();
            foreach (Transform point in RoadManager.instance.RoadPoints) {
                _roadPointQueue.Enqueue(point);
            }
        }
        private void Move() {
            if (!_goForword) return;

            if (CheckArriveDestination()) transform.localPosition += (_direction * GameManager.instance.EnemySpeed) * Time.deltaTime;
            else SetNextDestinationAndDirection();
        }

        public void Death() {
            _goForword = false;

            StartCoroutine(
                AnimationControl(eEnemyAnimationState.Death,
                () => {
                    gameObject.SetActive(false);
                }));
        }

        private bool CheckArriveDestination() {
            return Vector3.Distance(_destination.position, transform.localPosition) > 0.1f;
        }

        private void SetNextDestinationAndDirection() {
            if (_roadPointQueue.Count > 0) {
                _destination = _roadPointQueue.Dequeue();
                _direction = (_destination.position - transform.localPosition).normalized;
            } else {
                _goForword = false;
                GameManager.instance.GameOver();
            }
        }

        private IEnumerator AnimationControl(eEnemyAnimationState state, Action action = null) {
            switch (state) {
                case eEnemyAnimationState.Move:
                    _animator.SetInteger("Color", (int)EnemyType);
                    break;
                case eEnemyAnimationState.Death:
                    _animator.SetTrigger("Death");
                    yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && _animator.GetCurrentAnimatorStateInfo(0).IsName($"{EnemyType}_Death"));
                    action?.Invoke();
                    break;
                default: break;
            }

            yield return null;
        }
        #endregion
    }
}