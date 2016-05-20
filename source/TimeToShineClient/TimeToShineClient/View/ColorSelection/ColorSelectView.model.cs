using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using TimeToShineClient.Util;
using XamlingCore.Portable.View.ViewModel;

namespace TimeToShineClient.View.ColorSelection
{
    public class ColorSelectViewModel : XViewModel
    {
        SolidColorBrush _brush = new SolidColorBrush(Colors.White);

        private float _chaseColor = 0;

        private bool _isAttract;

        public override void OnInitialise()
        {
            base.OnInitialise();
            StartAttract();
        }

        public void StartAttract()
        {
            _isAttract = true;
            _chase();
        }

        async void _chase()
        {
            while (_isAttract)
            {
                await Task.Delay(20);
                _chaseColor ++;
                var h = ColorUtils.FromHsv(_chaseColor, 1f, 1f);
                Brush = new SolidColorBrush(h);
            }
        }

        public void StopAttract()
        {
            _isAttract = false;
        }

        public void SetColor(Color c)
        {
            StopAttract();   
            Brush = new SolidColorBrush(c);
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
    }
}
