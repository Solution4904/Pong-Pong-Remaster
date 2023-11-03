using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Sol {
    public class Countdown : MonoBehaviour {
        #region Variable
        private IEnumerator CountingCoroutine;
        private WaitForSeconds _waitTime;
        private Action _action;

        [SerializeField] private TextMeshProUGUI _countDownText;
        private float _originSize, _increasingSize;
        #endregion

        #region Life Cycle
        private void Awake() {
            Caching();

            CountingCoroutine = Counting();
        }

        private void Start() {

        }

        private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                StartCoroutine(IncreasingAndDecreasingEffectToText());
            }
        }
        #endregion

        #region Essential Function
        private void Caching() {
            _originSize = _countDownText.fontSize;
            _increasingSize = (_originSize * 0.5f) + _originSize;
        }

        private void Init() {

        }
        #endregion

        #region Definition Function
        public void StartCountDown(float time, Action action) {
            _waitTime = new WaitForSeconds(time);
            _action = action;
            StartCoroutine(CountingCoroutine);
        }

        private IEnumerator Counting() {
            yield return null;
        }

        private IEnumerator IncreasingAndDecreasingEffectToText() {
            while (_countDownText.fontSize < _increasingSize) {
                _countDownText.fontSize += 3.0f;
                yield return null;
            }
            while (_countDownText.fontSize > _originSize) {
                _countDownText.fontSize -= 3.0f;
                yield return null;
            }
        }
        #endregion
    }
}