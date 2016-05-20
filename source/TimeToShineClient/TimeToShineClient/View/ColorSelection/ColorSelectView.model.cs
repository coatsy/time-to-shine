using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Media;
using TimeToShineClient.Model;
using TimeToShineClient.Model.Contract;
using TimeToShineClient.Model.Entity;
using TimeToShineClient.Util;
using uPLibrary.Networking.M2Mqtt;
using XamlingCore.Portable.View.ViewModel;

namespace TimeToShineClient.View.ColorSelection
{
    public class ColorSelectViewModel : XViewModel
    {
        private readonly IColorService _colorService;

        SolidColorBrush _brush = new SolidColorBrush(Colors.White);

        private Visibility _attractVisible;
        private bool _attractRunning;


        private bool _colorSelectRunning;

        private float _chaseColor = 0;

       

        public ColorSelectViewModel(IColorService colorService)
        {
            _colorService = colorService;
        }

        public override void OnInitialise()
        {
            base.OnInitialise();
            StartAttract();
        }

        public void StartColorSelect()
        {
            if (ColorSelectRunning)
            {
                return;
            }
            StopAttract();
            ColorSelectRunning = true;
        }

        public void StopColorSelect()
        {
            ColorSelectRunning = false;
        }

        public void StartAttract()
        {
            StopColorSelect();
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
    }
}
