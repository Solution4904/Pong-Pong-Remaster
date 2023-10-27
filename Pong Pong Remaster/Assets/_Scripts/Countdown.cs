using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    #region Variable
    private IEnumerator CountingCoroutine;
    private WaitForSeconds _waitTime;
    private Action _action;
    #endregion

    #region Life Cycle
    private void Awake() {
        CountingCoroutine = Counting();
    }
    #endregion

    #region Essential Function

    #endregion

    #region Definition Function
    public void StartCountDown(float time, Action action) {
        _waitTime = new WaitForSeconds(time);
        _action = action;
        StartCoroutine(CountingCoroutine);
    }

    private IEnumerator Counting() {
        yield return null;
    }
    #endregion
}
