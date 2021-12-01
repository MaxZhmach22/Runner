using UnityEngine;


namespace Runner
{
    internal static class TimeShift
    {
        #region Methods

        public static void DeafultTimeScale()
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }

        public static void SlowMotionEffect()
        {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        } 

        #endregion
    }
}
