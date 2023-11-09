using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            _buttons[0].onClick.AddListener(() => { SceneManager.LoadScene(0); });
            _buttons[1].onClick.AddListener(() => { Application.Quit(); });
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