using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStartPanel : MonoBehaviour {
    #region Variable
    // # Objects
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _text;

    // # Values
    private float _blinkTime = 0.5f;
    private WaitForSeconds _waitBlinkTime;

    private IEnumerator _blinkCoroutine;
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
    private void Caching() {
        _blinkCoroutine = BlinkEffect();
    }

    private void Init() {
        _waitBlinkTime = new WaitForSeconds(_blinkTime);

        StartCoroutine(_blinkCoroutine);
    }
    #endregion

    #region Definition Function
    public void SetClickEvent(Action action) {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => action());
    }

    private IEnumerator BlinkEffect() {
        while (true) {
            _text.gameObject.SetActive(!_text.gameObject.activeInHierarchy);
            yield return _waitBlinkTime;
        }
    }
    #endregion
}
