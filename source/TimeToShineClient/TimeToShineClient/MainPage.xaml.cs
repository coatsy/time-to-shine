using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TimeToShineClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ColorSpectrum_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.ChangeHue(e.GetCurrentPoint(this.colorSpectrum).Position.Y);
            this.colorSpectrum.CapturePointer(e.Pointer);

            PointerEventHandler moved = null;
            moved = (s, args) =>
            {
                this.ChangeHue(args.GetCurrentPoint(this.colorSpectrum).Position.Y);
            };
            PointerEventHandler released = null;
            released = (s, args) =>
            {
                this.colorSpectrum.ReleasePointerCapture(args.Pointer);
                this.ChangeHue(args.GetCurrentPoint(this.colorSpectrum).Position.Y);
                this.colorSpectrum.PointerMoved -= moved;
                this.colorSpectrum.PointerReleased -= released;
            };
            this.colorSpectrum.PointerMoved += moved;
            this.colorSpectrum.PointerReleased += released;
        }

        /// <summary>
        /// Change color hue
        /// </summary>
        /// <param name="y">change point</param>
        private void ChangeHue(double y)
        {
            var py = Math.Max(0d, y);
            py = Math.Min(this.colorSpectrum.ActualHeight, py);

            var spectrum = Math.Round(py, MidpointRounding.AwayFromZero);

            // this.ViewModel.ColorSpectrumPoint = Math.Round(py, MidpointRounding.AwayFromZero);
        }
    }
}
