using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Runner
{
    internal sealed class AudioController /*: IAudioController*/
    {

        #region Fields

        [SerializeField] private AudioMixer _master;
        [SerializeField] private AudioMixerSnapshot _paused;
        [SerializeField] private AudioMixerSnapshot _unpaused;

        private float _minVolume = -80;
        private float _maxVolume = 0;
        private float _currentValue;
        private bool _isMuted;

        #endregion


        #region UnityMethods

        //private void Start() =>
        //    _master.GetFloat(MASTER_VOLUME, out _currentValue);

        //#endregion


        //#region IAudioController

        //public void SetEffectsVolume(float value) =>
        //    _master.SetFloat(EFFECTS_VOLUME, CheckValue(value));

        //public void SetMusicVolume(float value) =>
        //    _master.SetFloat(MUSIC_VOLUME, CheckValue(value));

        //public void SetMasterVolume(float value)
        //{
        //    _currentValue = CheckValue(value);
        //    _master.SetFloat(MASTER_VOLUME, _currentValue);
        //}

        //public void SetVoiceVolume(float value) =>
        //    _master.SetFloat(VOICE_VOLUME, CheckValue(value));

        //public void SetUiSoundsVolume(float value) =>
        //     _master.SetFloat(UISOUNDS_VOLUME, CheckValue(value));

        //public void MuteAllSounds()
        //{
        //    _isMuted = !_isMuted;
        //    if (_isMuted)
        //        _master.SetFloat(MASTER_VOLUME, _minVolume);
        //    else
        //        _master.SetFloat(MASTER_VOLUME, _currentValue);
        //}

        //public void SetStartVolumeLevel(float value) =>
        //    _master.SetFloat(MASTER_VOLUME, CheckValue(value));

        //#endregion


        //#region IAudioPauseGame

        //public void PausedGameAudio() =>
        //    _paused.TransitionTo(Time.deltaTime);

        //public void UnpausedGameAudio() =>
        //    _unpaused.TransitionTo(Time.deltaTime);

        //#endregion


        //#region Methods

        //private float CheckValue(float value)
        //{
        //    var translatedNumber = (Mathf.Abs(_minVolume) + Mathf.Abs(_maxVolume)) * value + _minVolume;
        //    if (translatedNumber < _minVolume)
        //        return _minVolume;
        //    if (translatedNumber > _maxVolume)
        //        return _maxVolume;

        //    return translatedNumber;
        //}

        #endregion


    }
}

