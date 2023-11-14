using UnityEngine;

namespace Sol {
    public class SoundManager : MonoBehaviour_Singleton<SoundManager> {
        #region Variable
        [SerializeField] private AudioListener _listener;
        private AudioClip _currentClip;

        // # BGM
        private AudioSource _bgmSource;
        [SerializeField] private AudioClip _bgmClip;

        // # SFX
        private AudioSource _sfxSource;
        [SerializeField] private AudioClip[] _sfxClips;
        #endregion

        #region Life Cycle
        private void Awake() {
            _listener = GetComponent<AudioListener>();
            _sfxSource = gameObject.AddComponent<AudioSource>();
            _bgmSource = gameObject.AddComponent<AudioSource>();
        }

        private void Start() {
            _sfxSource.loop = false;
            _sfxSource.playOnAwake = false;

            _bgmSource.loop = true;
            _bgmSource.playOnAwake = false;
        }
        #endregion

        #region Definition Function
        public void PlayOnes(eSFXSound type) {
            switch (type) {
                case eSFXSound.Correct: _currentClip = _sfxClips[0]; break;
                case eSFXSound.Wrong: _currentClip = _sfxClips[1]; break;
                case eSFXSound.GameOver: _currentClip = _sfxClips[2]; break;
            }

            _sfxSource.PlayOneShot(_currentClip);
        }

        public void PlayBGM() {
            _bgmSource.clip = _bgmClip;
            _bgmSource.Play();
        }

        public void StopBGM() {
            _bgmSource.Stop();
        }
        #endregion
    }
}