﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XamlingCore.Portable.View;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TimeToShineClient.Controls
{
    public sealed partial class SubmitControlView : UserControl
    {
        private bool _isRunning;
        public SubmitControlView()
        {
            this.InitializeComponent();
           
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                _toggle();
            }
        }

        void _toggle()
        {
            if (_isRunning)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }

        public async void Start()
        {
            await Task.Delay(5000);
            EnterStory.BeginTime = TimeSpan.Zero;
            EnterStory.Begin();

            while (IsRunning)
            {
                await Task.Delay(8000);
                if (IsRunning)
                {
                    AttractSave.BeginTime = TimeSpan.Zero;
                    AttractSave.Begin();
                }
            }
        }

        public void Stop()
        {
            EnterStory.Stop();
        }

        
    }
}
