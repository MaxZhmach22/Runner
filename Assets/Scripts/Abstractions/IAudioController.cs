namespace Runner
{
    internal interface IAudioController 
    {
        void SetMasterVolume(float value);

        void SetMusicVolume(float value);

        void SetVoiceVolume(float value);

        void SetEffectsVolume(float value);

        void SetUiSoundsVolume(float value);

        void SetStartVolumeLevel(float value);

        void MuteAllSounds();
    }
}