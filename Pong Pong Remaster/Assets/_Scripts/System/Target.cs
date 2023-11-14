using UnityEngine;

namespace Sol {
    public class Target : MonoBehaviour {
        #region Variable
        // # Objects
        private GameObject _target;
        #endregion

        #region Life Cycle
        private void Start() {
            gameObject.SetActive(false);
        }

        private void FixedUpdate() {
            TraceTarget();
        }
        #endregion

        #region Definition Function
        public void SetTraceTarget(GameObject obj) {
            _target = obj;
            gameObject.SetActive(true);
        }

        private void TraceTarget() {
            if (!_target || !_target.activeInHierarchy) return;

            transform.position = _target.transform.position;
        }
        #endregion
    }
}