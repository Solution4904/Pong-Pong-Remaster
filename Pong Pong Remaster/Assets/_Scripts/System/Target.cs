using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Sol {
    public class Target : MonoBehaviour {
        #region Variable
        // # Values
        private float _time = 0.5f;
        private bool _isTrace = false;

        // # Objects
        private GameObject _target = null;
        private SpriteRenderer _spriteRenderer;

        private WaitForSeconds _blinkTime;
        private IEnumerator _blinkCoroutine;
        #endregion

        #region Life Cycle
        private void Awake() {
            Caching();
        }

        private void Start() {
            Init();
        }

        //private void FixedUpdate() {
        //    TraceTarget();
        //}
        #endregion

        #region Essential Function
        private void Caching() {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _blinkTime = new WaitForSeconds(_time);
        }

        private void Init() {
            _spriteRenderer.color = Color.clear;
        }
        #endregion

        #region Definition Function
        public void SetTarget(GameObject value) {
            _target = value;
            _isTrace = true;

            if (_blinkCoroutine != null) StopCoroutine(_blinkCoroutine);
            _blinkCoroutine = BlinkEffect();
            StartCoroutine(_blinkCoroutine);
        }

        private void TraceTarget() {
            if (!_isTrace || !_target.activeInHierarchy || _target == null) return;
            transform.position = _target.transform.position;
        }

        private IEnumerator BlinkEffect() {
            while (true) {
                _spriteRenderer.color = Color.white;
                yield return _blinkTime;
                _spriteRenderer.color = Color.clear;
            }
        }
        #endregion
    }
}