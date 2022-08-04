using System;
using System.Threading;

public class TimerExample
{
    // The method that is executed when the timer expires. Displays
    // a message to the console.
    private static void TimerHandler(object state)
    {
        Console.WriteLine("{0} : {1}",
            DateTime.Now.ToString("HH:mm:ss.ffff"), state);
    }

    public static void CreateXml()
    {
        TimerCallback handler = new TimerCallback(TimerHandler);

        string state = "Timer expired.";

        Console.WriteLine("{0} : Creating Timer.",
            DateTime.Now.ToString("HH:mm:ss.ffff"));

        using (Timer timer = new Timer(handler, state, 2000, 1000))
        {
            int period;

            do
            {
                try
                {
                    period = int.Parse(Console.ReadLine());
                }
                catch
                {
                    period = 0;
                }

                if (period > 0) timer.Change(0, period);

            }

            while (period > 0);
        }

        Console.WriteLine("Main method complete. Press Enter.");
        Console.ReadLine();
    }
}