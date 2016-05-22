using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Chat;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Media;
using TimeToShineClient.Model;
using TimeToShineClient.Model.Contract;
using TimeToShineClient.Model.Entity;
using TimeToShineClient.Model.Messages;
using TimeToShineClient.Util;
using uPLibrary.Networking.M2Mqtt;
using XamlingCore.Portable.Messages.XamlingMessenger;
using XamlingCore.Portable.View;
using XamlingCore.Portable.View.ViewModel;

namespace TimeToShineClient.View.ColorSelection
{
    public class ColorSelectViewModel : XViewModel
    {
        private readonly IColorService _colorService;

        SolidColorBrush _brush = new SolidColorBrush(Colors.White);

        public ICommand SaveCommand { get; set; }
        public ICommand StartSaveCommand { get; set; }
        private Visibility _attractVisible;
        private bool _attractRunning;

        private bool _saveRunning;
        private bool _colorSelectRunning;

        private float _chaseColor = 0;

        private string _firstName;
        private string _age;
        private string _suburb;

        int counter = 0;

        public ColorSelectViewModel(IColorService colorService)
        {
            _colorService = colorService;
            SaveCommand = new XCommand(_onSave);
            StartSaveCommand = new XCommand(_onStartSave);
            _attractTimer();
            this.Register<ResetMessage>(_onReset);
            this.Register<SettingsMessage>(_onSettings);
        }

        void _onSettings()
        {
            
        }

        void _onReset()
        {
            Dispatcher.Invoke(StartAttract);
            
        }

        async void _attractTimer()
        {
            while (true)
            {
                await Task.Delay(1000);
                if (counter > 45)
                {
                    counter = 0;
                    if (!AttractRunning)
                    {
                        StartAttract();
                    }
                    
                }
                
                counter ++;
            }
        }

        void _onStartSave()
        {
            _resetAttractTimer();
            StartSave();
        }

        void _resetAttractTimer()
        {
            counter = 0;
        }

        async void _onSave()
        {
            var s = "here";


            await Task.Delay(10000);

            StartAttract();
        }

        public override void OnInitialise()
        {
            base.OnInitialise();
            StartAttract();
        }

        public void StartSave()
        {
            StopColorSelect();
            StopAttract();
            SaveRunning = true;
        }

        public void StopSave()
        {
            SaveRunning = false;
        }

        public void StartColorSelect()
        {
            _resetAttractTimer();
            FirstName = "";
            Age = "";
            Suburb = "";
            if (ColorSelectRunning)
            {
                return;
            }
            StopAttract();
            StopSave();
            ColorSelectRunning = true;
        }

        public void StopColorSelect()
        {
            ColorSelectRunning = false;
        }

        public void StartAttract()
        {
            StopColorSelect();
            StopSave();
            AttractRunning = true;
            _chase();
        }

        async void _chase()
        {
            while (AttractRunning)
            {
                await Task.Delay(20);
                _chaseColor++;
                var h = ColorUtils.FromHsv(_chaseColor, 1f, 1f);
                Brush = new SolidColorBrush(h);
            }
        }

        public void StopAttract()
        {
            AttractRunning = false;
        }

        public void SetColor(Color c)
        {
            _resetAttractTimer();
            StartColorSelect();
            Brush = new SolidColorBrush(c);
            _publish(c);
        }


        void _publish(Color colour)
        {
            _colorService.PublishSampleColor(colour);
        }

        public SolidColorBrush Brush
        {
            get { return _brush; }
            set
            {
                _brush = value;
                OnPropertyChanged();
            }
        }

        public bool AttractRunning
        {
            get { return _attractRunning; }
            set
            {
                _attractRunning = value;
                OnPropertyChanged();
            }
        }

        public bool ColorSelectRunning
        {
            get { return _colorSelectRunning; }
            set
            {
                _colorSelectRunning = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public string Suburb
        {
            get { return _suburb; }
            set
            {
                _suburb = value; 
                OnPropertyChanged();
            }
        }

        public bool SaveRunning
        {
            get { return _saveRunning; }
            set
            {
                _saveRunning = value;
                OnPropertyChanged();
            }
        }
    }
}
