using System.Collections.Generic;
using UnityEngine;

namespace Sol {
    public class UIManager : MonoBehaviour_Singleton<UIManager> {
        #region Variable
        private GameObject _currentPanel;
        private Stack<GameObject> _panelStack = new();
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private GameObject _blockingPanel;
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

            _resultPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            _blockingPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        #endregion

        #region Definition Function
        public void ShowPanel(ePanelType type) {
            switch (type) {
                case ePanelType.Result:
                    _currentPanel = _resultPanel;
                    break;
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
