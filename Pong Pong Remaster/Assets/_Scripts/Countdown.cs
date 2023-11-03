using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Sol {
    public class Countdown : MonoBehaviour {
        #region Variable
        // # Components
        [SerializeField] private TextMeshProUGUI _countDownText;

        // # Values
        private float _waitTime;
        private Sequence _countDownTextSequence;

        public Ease ease;
        #endregion

        #region Life Cycle
        private void Start() {
            Init();
        }

        //public void Update() {
        //    if (Input.GetMouseButtonDown(1)) {
        //        _countDownTextSequence.SetEase(ease);
        //        _countDownTextSequence.Play();
        //    }
        //}
        #endregion

        #region Essential Function
        private void Init() {
            _countDownText.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            _countDownTextSequence = DOTween.Sequence()
                .Append(_countDownText.transform.DOScale(new Vector3(2, 2, 2), 0.5f))
                .Append(_countDownText.transform.DOScale(Vector3.one, 0.5f))
                .SetLoops(-1)
                .SetEase(Ease.OutSine);
        }
        #endregion

        #region Definition Function
        public void StartCountDown(float time) {
            _waitTime = time;
            StartCoroutine(Counting());
        }

        private IEnumerator Counting() {
            yield return null;
            UIManager.instance.BlockingScreen(true);

            _countDownTextSequence.Play();
            float divideTime = _waitTime;
            while (divideTime > 0) {
                _countDownText.text = divideTime.ToString();

                yield return new WaitForSeconds(1);
                divideTime--;
            }
            _countDownTextSequence.Kill();

            _countDownText.gameObject.SetActive(false);
            UIManager.instance.BlockingScreen(false);
        }
        #endregion
    }
}