// -------------------------------------------------- //
/*
 * [ Info ]
 * Writer : Solution
 * Date Created : 2023-04-22
 */

/*
 * [ Update Log ]
 * 2023-04-22 : Add 'Color Log'
 * 2023-05-05 : Add 'Sington'
 * 2023-05-05 : Add 'GetRuntimePlatform'
 * 2023-05-05 : Add 'ModifyFixedDeltaTimeWithTargetFrame'
 * 2023-05-11 : Modify 'Singleton' -> 'MonoBehaviour_Singleton'
 * 2023-05-11 : Add 'Singleton' (Non MonoBehaviour)
 * 2023-07-10 : Modify 'Color Log' From 'Action' to 'Function'
*/
// -------------------------------------------------- //

using System;
using UnityEngine;

public enum LogColor {
    Defalut,
    Red,
    Green,
    Blue,
    Yellow,
    White,
    Black,
    Cyan,
    Brown
}

public class SolutionLibrary {
    /// <summary>
    /// 로그를 색상으로 표현.
    /// </summary>
    /// <param name="color">색상</param>
    /// <param name="text">내용</param>
    //public static Action<LogColor, String> Log = (color, text) => {
    //    Debug.Log($"<color={color}> {text}</color>");
    //};
    //public static Action<String> Log = (text) => {
    //    Debug.Log(text);
    //};
    public static void Log(LogColor color, string text) {
        Debug.Log($"<color={color}> {text}</color>");
    }
    public static void Log(string text) {
        Debug.Log(text);
    }

    /// https://docs.unity3d.com/ScriptReference/RuntimePlatform.html
    /// <summary>
    /// 런타임 플랫폼 반환.
    /// </summary>
    /// <return>RuntimePlatform</return>
    public static Func<RuntimePlatform> GetRuntimePlatform = () => {
        return Application.platform;
    };
}

public class MonoBehaviour_Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T _instance;
    public static T instance {
        get {
            _instance = FindObjectOfType<T>();
            if (_instance == null) {
                GameObject instObj = new GameObject(typeof(T).FullName, typeof(T));
                _instance = instObj.GetComponent<T>();
                DontDestroyOnLoad(instObj);
            }
            return _instance;
        }
    }
}

public class Singleton<T> where T : new() {
    private static T _instance;
    public static T instance {
        get {
            if (_instance == null) _instance = new T();
            return _instance;
        }
    }
}

public class Optimization {
    /// <summary>
    /// 최대 FPS 설정과 FixedTime 재설정
    /// </summary>
    /// <param name="targetFrame"></param>
    public static Action<float> ModifyFixedDeltaTimeWithTargetFrame = (targetFrame) => {
        Application.targetFrameRate = (int)targetFrame;
        Time.fixedDeltaTime = (1 / targetFrame);
    };
}