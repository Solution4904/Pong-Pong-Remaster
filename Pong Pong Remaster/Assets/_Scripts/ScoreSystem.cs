using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour {
    #region Variable
    [SerializeField] private TextMeshProUGUI _scoreText;
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
        ResetScore();
    }
    #endregion

    #region Definition Function
    public void SetScore(int score) {
        _scoreText.text = $"{score}";
    }

    public void ResetScore() {
        _scoreText.text = "0";
    }

    private void ActivateEffect() {

    }
    #endregion
}
