using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
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
    public sealed partial class TexturesRegister : Page
    {
        Windows.Storage.Pickers.FileOpenPicker ImagePicker;
        Windows.Storage.Pickers.FileOpenPicker FilePicker;

        Windows.Storage.StorageFile xmlDoc;

        IRandomAccessStreamWithContentType stream;

        List<XmlEntry> xmlEntries;
        BitmapImage Atlasbimg;

        string ImagePath;

        public TexturesRegister()
        {
            this.InitializeComponent();
            this.Register_Picker();
            xmlEntries = new List<XmlEntry>();
        }

        private void Register_Picker()
        {
            ImagePicker = new Windows.Storage.Pickers.FileOpenPicker();
            ImagePicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            ImagePicker.FileTypeFilter.Add(".png");
            ImagePicker.FileTypeFilter.Add(".jpg");
            ImagePicker.FileTypeFilter.Add(".jpeg");

            FilePicker = new Windows.Storage.Pickers.FileOpenPicker();
            FilePicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            FilePicker.FileTypeFilter.Add(".xml");
            FilePicker.FileTypeFilter.Add(".txt");
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {   //Open a textures file
            xmlDoc = await FilePicker.PickSingleFileAsync();
            if (xmlDoc == null)
                return;
            await loadImages();
        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {   //Open atlas
            Windows.Storage.StorageFile file = await ImagePicker.PickSingleFileAsync();
            if (file == null)
                return;
            BitmapImage bimg = new BitmapImage();
            stream = await file.OpenReadAsync();
            await bimg.SetSourceAsync(stream);
            Atlasbimg = bimg;
            Atlas.Source = Atlasbimg;
        }

        private void ScaleBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double w;
            double.TryParse(((TextBox)sender).Text, out w);
            if (w > 0)
            {
                foreach(Sprite i in XmlEntry.sprites)
                {
                    i.Scale(w);
                }
            }
        }

        private void SnapBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double w;
            double.TryParse(((TextBox)sender).Text, out w);
            Sprite.SnapInterval = w;
        }

        private async Task loadImages()
        {
            var xmlstream = await xmlDoc.OpenStreamForReadAsync();
            XmlTextReader reader = new XmlTextReader(xmlstream);
            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "SubTexture":
                        int x, y, width, height;
                        int.TryParse(reader.GetAttribute("width"), out width);
                        int.TryParse(reader.GetAttribute("height"), out height);
                        int.TryParse(reader.GetAttribute("x"), out x);
                        int.TryParse(reader.GetAttribute("y"), out y);
                        string name = reader.GetAttribute("name");

                        WriteableBitmap wbmp = await BitmapFactory.FromStream(stream);
                        
                        WriteableBitmap cropped = wbmp.Clone().Crop(x,y,width, height);

                        XmlEntry xmle = new XmlEntry(x, y, canvas, cropped, name);
                        xmlEntries.Add(xmle);

                        //BitmapBounds bounds = new BitmapBounds();
                        //bounds.X = (uint)x;
                        //bounds.Y = (uint)y;
                        //bounds.Width = (uint)width;
                        //bounds.Height = (uint)height;

                        //BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                        //InMemoryRandomAccessStream ras = new InMemoryRandomAccessStream();
                        //BitmapEncoder enc = await BitmapEncoder.CreateForTranscodingAsync(ras, decoder);

                        //enc.BitmapTransform.Bounds = bounds;

                        //await enc.FlushAsync();

                        //WriteableBitmap tmpbimg = new WriteableBitmap(0, 0);
                        //tmpbimg.SetSource(ras);

                        //XmlEntry xmle = new XmlEntry(x, y, canvas, tmpbimg, name);
                        //xmlEntries.Add(xmle);
                        break;
                    case "TextureAtlas":
                        if (reader.HasAttributes)
                        {
                            ImagePath = reader.GetAttribute("imagePath");
                        }
                        break;
                }
            }
        }

        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFile file = await ImagePicker.PickSingleFileAsync();
            if (file == null)
                return;
            //WriteableBitmap bimg = new WriteableBitmap(1, 1).FromStream(tmp);
            var tmpstream = await file.OpenReadAsync();
            WriteableBitmap bimg = await BitmapFactory.FromStream(tmpstream);
            //await bimg.SetSourceAsync(tmpstream);

            XmlEntry xmle = new XmlEntry(0, 0, canvas, bimg, file.Name);
            xmlEntries.Add(xmle);
        }

        private async void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("TextureAtlas");
            root.SetAttribute("imagePath", ImagePath);
            xmlDoc.AppendChild(root);
            foreach(XmlEntry i in xmlEntries)
            {
                i.WriteDataToXML(xmlDoc, root);
            }
            var file = await FilePicker.PickSingleFileAsync();
            var stream = await file.OpenStreamForWriteAsync();
            stream.SetLength(0);
            xmlDoc.Save(stream);
            await stream.FlushAsync();
        }
        private async void SaveImageAtlas()
        {
            //var file = await ImagePicker.PickSingleFileAsync();
            //var stream = await file.OpenStreamForWriteAsync();

            //BitmapImage bimg = new BitmapImage();

            //InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
            //randomAccessStream.
            
            WriteableBitmap wbmp = new WriteableBitmap((int)XmlEntry.getWidth(), (int)XmlEntry.getHeight());
            

            //BitmapDecoder decoder = 
            //BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(stream);

        }
    }
}
