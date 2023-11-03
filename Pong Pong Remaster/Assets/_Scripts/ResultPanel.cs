using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sol {
    public class ResultPanel : MonoBehaviour {
        #region Variable
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Button[] _buttons;
        #endregion

        #region Life Cycle
        private void Awake() {
            Caching();
        }

        private void Start() {
            Init();
        }

        private void OnEnable() {
            GetResult();
        }
        #endregion

        #region Essesntial Function
        private void Caching() {
            _buttons[0].onClick.AddListener(() => { });
            _buttons[1].onClick.AddListener(() => { });
        }

        private void Init() {
            gameObject.SetActive(false);
        }
        #endregion

        #region Definition Function
        private void GetResult() {
            _score.text = $"{GameManager.instance.Score}";
        }
        #endregion
    }
}