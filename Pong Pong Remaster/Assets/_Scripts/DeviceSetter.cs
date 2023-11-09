using UnityEngine;

public class DeviceSetter : MonoBehaviour {
    private void Awake() {
        Optimization.ModifyFixedDeltaTimeWithTargetFrame(60);
    }
}