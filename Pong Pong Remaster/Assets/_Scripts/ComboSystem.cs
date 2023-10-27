using TMPro;
using UnityEngine;

public class ComboSystem : MonoBehaviour {
    #region Variable
    [SerializeField] private TextMeshProUGUI _comboText;
    #endregion

    #region Life Cycle
    private void Start() {
        Init();
    }
    #endregion

    #region Essential Function
    private void Init() {
        ResetCombo();
    }
    #endregion

    #region Definition Function
    public void SetCombo(int combo) {
        _comboText.text = $"{combo}";
    }

    public void ResetCombo() {
        _comboText.text = "0";
    }
    #endregion
}
