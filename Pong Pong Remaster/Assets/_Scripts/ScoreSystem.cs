using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Sol {
    public class ScoreSystem : MonoBehaviour {
        #region Variable
        [SerializeField] private TextMeshProUGUI _scoreText;

        private Sequence _scoreTextTweeningSequence;
        private Vector3 _scoreTextOriginScale;
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
        private void Init() {
            ResetScore();
            SetScoreTextTweeningSequnce();
        }

        private void Caching() {
            _scoreTextOriginScale = _scoreText.transform.localScale;
        }
        #endregion

        #region Definition Function
        public void SetScore(int score) {
            _scoreText.text = $"{score}";

            _scoreTextTweeningSequence.Rewind();
            _scoreTextTweeningSequence.Play();
        }

        public void ResetScore() {
            _scoreText.text = "0";
        }

        private void SetScoreTextTweeningSequnce() {
            _scoreTextTweeningSequence = DOTween.Sequence()
                .OnComplete(() => {
                    _scoreText.transform.localScale = _scoreTextOriginScale;
                })
                .Append(_scoreText.transform.DOPunchScale(_scoreTextOriginScale, 0.3f, 3))
                .SetAutoKill(false)
                .Pause();
        }

        private void ActivateEffect() {

        }
        #endregion
    }
}
