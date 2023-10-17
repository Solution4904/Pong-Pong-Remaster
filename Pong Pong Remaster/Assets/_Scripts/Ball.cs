using UnityEngine;

namespace Solution {
    public class Ball : MonoBehaviour {
        #region Variable
        [SerializeField] private ColorType colorType;
        [SerializeField] private Sprite[] spriteArray;

        private SpriteRenderer spriteRenderer;
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
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Init() {
            SetSprite();
        }

        private void SetSprite() {
            spriteRenderer.sprite = spriteArray[(int)colorType];
        }
        public void SetType(ColorType type) {
            colorType = type;
        }
        #endregion

        #region Definition Function

        #endregion
    }
}