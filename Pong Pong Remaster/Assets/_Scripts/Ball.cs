using System.Collections.Generic;
using UnityEngine;

namespace Sol {
    public class Ball : MonoBehaviour {
        #region Variable
        // # Values
        private Queue<Transform> _roadPointQueue;
        private Vector3 _direction;
        private Transform _destination;
        public eColorType BallColor;
        private bool _goForword = true;

        // # Components
        private SpriteRenderer _spriteRenderer;

        // # Resources
        [SerializeField] private Sprite[] _colorSprites;
        #endregion

        #region Life Cycle
        private void Awake() {
            Caching();
        }

        private void OnEnable() {
            Init();
        }

        private void Start() {
            _spriteRenderer.sprite = _colorSprites[(int)BallColor];
        }

        private void FixedUpdate() {
            Move();
        }
        #endregion

        #region Essential Function
        private void Init() {
            _goForword = true;

            SetRoadPointQueue();
            SetNextDestinationAndDirection();
        }

        private void Caching() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        #endregion

        #region Definition Function
        public void SetColor(eColorType color) {
            BallColor = color;
        }

        private void SetRoadPointQueue() {
            _roadPointQueue = new Queue<Transform>();
            foreach (Transform point in RoadManager.instance.RoadPoints) {
                _roadPointQueue.Enqueue(point);
            }
        }
        private void Move() {
            if (!_goForword) return;

            if (CheckArriveDestination()) transform.localPosition += (_direction * GameManager.instance.BallSpeed) * Time.deltaTime;
            else SetNextDestinationAndDirection();
        }

        private bool CheckArriveDestination() {
            return Vector3.Distance(_destination.position, transform.localPosition) > 0.05f;
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
        #endregion


















        /*#region Variable
        public eColorType BallColor;
        
        [SerializeField] private Sprite[] _spriteArray;
        private SpriteRenderer _spriteRenderer;
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
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Init() {
            SetSprite();
        }
        #endregion

        #region Definition Function
        private void SetSprite() {
            _spriteRenderer.sprite = _spriteArray[(int)BallColor];
        }

        public void SetColor(eColorType type) {
            BallColor = type;
        }
        #endregion*/
    }
}