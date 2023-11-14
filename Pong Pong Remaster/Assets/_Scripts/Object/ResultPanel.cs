using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sol {
    public class ResultPanel : MonoBehaviour {
        #region Variable
        [SerializeField] private TextMeshProUGUI[] _texts;
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
            // Retry
            _buttons[0].onClick.AddListener(() => { SceneManager.LoadScene(0); });

            // Exit
            _buttons[1].onClick.AddListener(() => { Application.Quit(); });
        }

        private void Init() {
            gameObject.SetActive(false);
        }
        #endregion

        #region Definition Function
        private void GetResult() {
            _texts[0].text = $"{GameManager.instance.Score}";
            _texts[1].text = $"{GameManager.instance.MaxCombo}";
        }
        #endregion
    }
}