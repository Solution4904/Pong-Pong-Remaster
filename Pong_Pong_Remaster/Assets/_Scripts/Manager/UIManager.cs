using System.Collections.Generic;
using UnityEngine;

namespace Sol {
    public class UIManager : MonoBehaviour_Singleton<UIManager> {
        #region Variable
        private GameObject _currentPanel;
        private Stack<GameObject> _panelStack = new();

        // # Objects
        [SerializeField] private GameObject _blockingPanel;
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private GameObject _gameStartPanel;
        #endregion

        #region Life Cycle
        private void Awake() {

        }

        private void Start() {
            Init();
        }
        #endregion

        #region Essential Function
        private void Init() {
            BlockingScreen(false);

            _blockingPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            _resultPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            _gameStartPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        #endregion

        #region Definition Function
        public void ShowPanel(ePanelType type) {
            switch (type) {
                case ePanelType.Result: _currentPanel = _resultPanel; break;
                case ePanelType.ClickToStart: _currentPanel = _gameStartPanel; break;
            }
            _currentPanel.SetActive(true);

            _panelStack.Push(_currentPanel);
        }

        public void HidePanel() {
            _panelStack.Pop().SetActive(false);
        }

        public void BlockingScreen(bool activate) {
            _blockingPanel.SetActive(activate);
        }
        #endregion
    }
}
