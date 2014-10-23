using System.Windows.Forms;

namespace Logger
{
    public static class TimerExtensions
    {
        public static void SetIntervalInMinutes(this Timer timer, int intervalInMinutes)
        {
            timer.Interval = intervalInMinutes*1000*60;
        }

        public static void ToggleEnabled(this Timer timer)
        {
            timer.Enabled = !timer.Enabled;
        }
    }
}