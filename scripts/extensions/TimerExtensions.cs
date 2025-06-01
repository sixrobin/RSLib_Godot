namespace RSLib.GE
{
    using Godot;

    public static class TimerExtensions
    {
        /// <summary>
        /// Returns the completion percentage of a timer.
        /// </summary>
        /// <param name="timer">Timer to get the completion percentage of.</param>
        /// <returns>Completion percentage in [0,1] clamped range.</returns>
        public static float GetCompletionPercentage(this Timer timer)
        {
            return (float)Mathf.Clamp(1f - timer.TimeLeft / timer.WaitTime, 0, 1);
        }
    }
}