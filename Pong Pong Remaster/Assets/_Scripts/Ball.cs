using UnityEngine;

namespace Solution {
    public class Ball : MonoBehaviour {
        #region Variable
        public eColorType BallColor;
        
        [SerializeField] private Sprite[] _spriteArray;
        private SpriteRenderer _spriteRenderer;
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
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Init() {
            SetSprite();
        }
        #endregion

        #region Definition Function
        private void SetSprite() {
            _spriteRenderer.sprite = _spriteArray[(int)BallColor];
        }

        public void SetColor(eColorType type) {
            BallColor = type;
        }
        #endregion
    }
}