using UnityEngine;

public class RoadManager : MonoBehaviour_Singleton<RoadManager> {
    #region Variable
    // # Values
    [field: SerializeField] public Transform[] RoadPoints { get; private set; }
    #endregion
}
