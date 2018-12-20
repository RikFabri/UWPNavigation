using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPNavigation
{
    class Sprite
    {
        public BitmapImage bimg = new BitmapImage();

        int index;

        static int DragComponentIndex;
        static bool DragComponent;
        static double scale = 1;
        static int Xscroll, Yscroll = 0;

        public static double SnapInterval { get; set; } = 1;

        public double x { get; set; }
        public double y { get; set; }
        public double width { get; set; }
        public double height { get; set; }

        Canvas canvas;

        public Sprite(Canvas c, double x, double y)
        {
            canvas = c;
            this.x = x;
            this.y = y;
        }
        public void Scale(double factor)
        {
            scale = factor;

            canvas.Children[index].SetValue(Canvas.LeftProperty, x * factor);
            canvas.Children[index].SetValue(Canvas.TopProperty, y * factor);

            ((Image)canvas.Children[index]).Width = width * factor;
        }
        public void setSource(WriteableBitmap bimg)
        {
            this.height = bimg.PixelHeight;
            this.width = bimg.PixelWidth;
            ((Image)canvas.Children[index]).Source = bimg;
            ((Image)canvas.Children[index]).Width = width * scale;
        }
        public void AddToCanvas()
        {
            Image img = new Image();
            img.SetValue(Canvas.LeftProperty, x*scale);
            img.SetValue(Canvas.TopProperty, y*scale);
            img.PointerPressed += imagePressed;
            img.PointerReleased += imageReleased;
            canvas.Children.Add(img);
            index = canvas.Children.Count - 1;
        }
        private void imagePressed(object sender, RoutedEventArgs e)
        {
            DragComponentIndex = canvas.Children.IndexOf((Image)sender);
            DragComponent = true;
            canvas.PointerMoved += Atlas_PointerMoved;
        }
        private void imageReleased(object sender, RoutedEventArgs e)
        {
            DragComponent = false;
            canvas.PointerMoved -= Atlas_PointerMoved;
        }

        private void Atlas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (SnapInterval < 1)
                SnapInterval = 1;
            if (scale < 1)
                scale = 1;
            if (DragComponent)
                x = ((e.GetCurrentPoint(canvas).Position.X));
                y = ((e.GetCurrentPoint(canvas).Position.Y));
                x = (x - (x%(SnapInterval * scale)) + Xscroll) /scale;
                y = (y - (y%(SnapInterval * scale)) + Yscroll) /scale;
                canvas.Children[DragComponentIndex].SetValue(Canvas.TopProperty, y*scale);
                canvas.Children[DragComponentIndex].SetValue(Canvas.LeftProperty, x*scale);
        }
    }
}
