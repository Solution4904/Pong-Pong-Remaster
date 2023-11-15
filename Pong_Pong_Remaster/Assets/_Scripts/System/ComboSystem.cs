using DamageNumbersPro;
using UnityEngine;

namespace Sol {
    public class ComboSystem : MonoBehaviour {
        #region Variable
        [SerializeField] private RectTransform _parent;
        [SerializeField] private DamageNumber _comboTextPrefab;
        private DamageNumber _comboText;
        #endregion

        #region Life Cycle

        #endregion

        #region Essential Function

        #endregion

        #region Definition Function
        public void PopComboText(int combo) {
            _comboText = _comboTextPrefab.Spawn(Vector3.zero);
            _comboText.leftText = combo.ToString();
            _comboText.SetAnchoredPosition(_parent, Vector2.zero);
        }
        #endregion
    }
}
