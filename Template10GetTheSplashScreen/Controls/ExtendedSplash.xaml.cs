using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Template10GetTheSplashScreen.Controls
{
    public sealed partial class ExtendedSplash : UserControl
    {
        private Timer TheTimer = null;
        private object LockObject = new object();
        private double ProgressAmount = 0;
        public ExtendedSplash()
        {
            this.InitializeComponent();
            this.Loaded += (sender, e) =>
            {
                TimerCallback tcb = HandleTimerTick;
                TheTimer = new Timer(HandleTimerTick, null, 0, 30);
            };

            this.Unloaded += (sender, e) =>
            {
                lock (LockObject)
                {
                    if (TheTimer != null)
                        TheTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    TheTimer = null;
                }
            };
        }

        public void HandleTimerTick(Object state)
        {
            lock (LockObject)
            {
                ProgressControl.SetBarLength(ProgressAmount);
                ProgressAmount += 0.006;
                if (ProgressAmount > 1.5)
                    ProgressAmount = 0.0;
            }
        }
    }
}