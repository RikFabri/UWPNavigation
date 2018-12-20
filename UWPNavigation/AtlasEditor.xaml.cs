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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPNavigation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapEditor : Page
    {
        public MapEditor()
        {
            this.InitializeComponent();

            //for (int i = 0; i < 20; i++)
            //{
            //    Image img = new Image();
            //    BitmapImage bimg = new BitmapImage(new Uri(this.BaseUri, "/Assets/StoreLogo.png"));
            //    img.Source = bimg;
            //    img.SetValue(Canvas.TopProperty, i * 20);
            //    img.SetValue(Canvas.LeftProperty, i * 20);
            //    canvas.Children.Add(img);
            //}
        }
    }
}
