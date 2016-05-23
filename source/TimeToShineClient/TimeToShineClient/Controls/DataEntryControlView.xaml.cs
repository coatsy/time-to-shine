using System;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TimeToShineClient.Controls
{
    public sealed partial class DataEntryControlView : UserControl
    {
        public ICommand SaveCommand { get; set; }

        public DataEntryControlView()
        {
            this.InitializeComponent();
        }

        public bool IsRunning
        {
            get { return false; }
            set
            {
                _toggle(value);
            }
        }

        void _toggle(bool running)
        {
            if (running)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }


        public void Start()
        {
            var dc = this.DataContext;
            VisualStateManager.GoToState(this, "EnterColourNameState", true);
            FirstTextBox.Focus(FocusState.Programmatic);
            //EnterStory.BeginTime = TimeSpan.Zero;
            //EnterStory.Begin();
        }

        public void Stop()
        {
            VisualStateManager.GoToState(this, "BeforeState", true);
            // EnterStory.Stop();
        }

        private async void ButtonToEnterName_OnClick(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "EnterYourNameState", true);
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            SaveCommand?.Execute(null);
            VisualStateManager.GoToState(this, "ThanksState", true);
            await Task.Delay(6000);
            VisualStateManager.GoToState(this, "BackHomeState", true);
        }
    }
}
