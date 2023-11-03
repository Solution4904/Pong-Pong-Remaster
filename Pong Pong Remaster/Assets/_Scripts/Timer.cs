using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Sol {
    /*
     * 
     * 01. Called 'SetTimer(time, successAction, failAction)'
     *              time = float Type
     *              successAction = positive case
     *              failAction = negative case
     *              
     * 02. Called 'TimerControl(eTimerState)'
     *              Idle = Standby
     *              Activate = In Game
     *              Deactivate = Gameover
     *              
     * Clear Case   = Called 'TimeOver(true)'
     * Fail Case    = Called 'TimeOver(false)'
     * 
     * ##. Called 'SubscribeEventByTime(float time, Action action)'
     *              time = execute event time
     *              action = event
     *              
     */
    public class Timer : MonoBehaviour {
        #region Variable
        [SerializeField] private Slider _timerSlider;
        private float _timeValue;

        private eTimerState _timerState;
        private ReactiveProperty<float> _timerValueProperty = new();
        private IConnectableObservable<int> _countDownObservable;
        private IObservable<int> CountDownObservable => _countDownObservable.AsObservable();
        private Action _successAction, _failAction;
        #endregion

        #region Life Cycle
        private void Start() {
            Init();
        }

        //private void OnDestroy() {
        //    StreamDispose();
        //}
        #endregion

        #region Essential Function
        private void Init() {
            SubscribeTimeValue();
            SubscribeUpdateUI();
        }
        #endregion

        #region Definition Function
        /// <summary>
        /// 타이머 상태에 따른 스트림 구독
        /// </summary>
        private void SubscribeTimeValue() {
            this.UpdateAsObservable()
                .Where(_ => _timerState == eTimerState.Activate)
                .Subscribe(_ => {
                    _timeValue -= Time.deltaTime;
                    _timerValueProperty.Value = _timeValue;
                });
        }

        /// <summary>
        /// 남은 시간 값에 따른 스트림 구독
        /// </summary>
        private void SubscribeUpdateUI() {
            _timerValueProperty
                .Skip(1)
                .Subscribe(x => {
                    _timerSlider.value = x;
                    if (x <= 0) TimeOver(false);
                });
        }

        /// <summary>
        /// 특정 시간에 이벤트 발생을 등록
        /// </summary>
        /// <param name="time">발생될 시간(초)</param>
        /// <param name="action">발생 이벤트</param>
        private void SubscribeEventByTime(float time, Action action) {
            float value = _timerValueProperty.Value - time;
            _timerValueProperty
                .Where(x => x <= value)
                .First()
                .Subscribe(_ => {
                    action.Invoke();
                });
        }

        /// <summary>
        /// 특정 주기 마다 발생되는 이벤트 등록
        /// </summary>
        /// <param name="repeatTime">발생 주기(초)</param>
        /// <param name="action">발생시킬 이벤트</param>
        private void SubscribeRepeatEventByTime(float repeatTime, Action action) {
            _timerValueProperty
                .Buffer(TimeSpan.FromSeconds(repeatTime))
                .Subscribe(_ => action.Invoke());
        }

        /// <summary>
        /// 스트림 구독 종료
        /// </summary>
        private void StreamDispose() {
            this.OnDisableAsObservable();
            _timerValueProperty.Dispose();
        }

        /// <summary>
        /// 타이머 상태 제어
        /// </summary>
        /// <param name="state"></param>
        public void TimerControl(eTimerState state) {
            _timerState = state;
        }

        /// <summary>
        /// 타이머 초기화
        /// </summary>
        /// <param name="time">시간 값</param>
        /// <param name="action">클리어 실패 시 액션</param>
        public void SetTimer(float time, Action success, Action fail) {
            _timerState = eTimerState.Idle;

            _timeValue = time;

            _timerSlider.maxValue = time;
            _timerSlider.value = time;

            _timerValueProperty.Value = time;

            _successAction = success;
            _failAction = fail;
        }

        /// <summary>
        /// 반복 이벤트 등록
        /// </summary>
        public void SetRepeatEvent(float reatingTime, Action action) {
            SubscribeRepeatEventByTime(reatingTime, action);
        }

        /// <summary>
        /// 게임 오버
        /// </summary>
        /// <param name="inCase">true = 성공, false = 실패<param>
        public void TimeOver(bool inCase) {
            StreamDispose();

            _timerState = eTimerState.Idle;

            if (inCase) _successAction.Invoke();
            else _failAction.Invoke();
        }

        /// <summary>
        /// 특정 시간에 이벤트 발생을 등록
        /// </summary>
        /// <param name="time">발생될 시간(초)</param>
        /// <param name="action">발생 이벤트</param>
        public void AddEventByTime(float time, Action action) {
            SubscribeEventByTime(time, action);
        }

        /// <summary>
        /// 전달받은 시간초를 mm:ss로 변환
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string ConvertTime(float time) {
            return TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
        }

        public void PlusTime(float time) {
            _timeValue += time;

            SL.Log($"PlusTime\n추가 전 : {_timeValue}");
            SL.Log($"PlusTime\n추가 시간 : {time}");
            SL.Log($"PlusTime\n추가 후 : {_timeValue}");
        }
        #endregion
    }
}