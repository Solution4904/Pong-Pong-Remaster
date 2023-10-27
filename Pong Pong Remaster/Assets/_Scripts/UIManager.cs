using Solution;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour_Singleton<UIManager> {
    #region Variable
    private GameObject _currentPanel;
    private Stack<GameObject> _panelStack = new();
    [SerializeField] private GameObject _resultPanel;
    #endregion

    #region Life Cycle
    private void Awake() {

    }

    private void Start() {

    }
    #endregion

    #region Essential Function

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
    #endregion
}
