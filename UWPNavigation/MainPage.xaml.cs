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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPNavigation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

   public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(MapEditor));
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {

            }
            else
            {
                NavigationViewItem nvi = args.SelectedItem as NavigationViewItem;
                switch (nvi.Tag.ToString())
                {
                    case "e_atlas":
                        ContentFrame.Navigate(typeof(AtlasEditor));
                        break;
                    case "e_map":
                        ContentFrame.Navigate(typeof(MapEditor));
                        break;
                    case "r_terrain":
                        ContentFrame.Navigate(typeof(TerrainRegister));
                        break;
                    case "r_textures":
                        ContentFrame.Navigate(typeof(TexturesRegister));
                        break;
                }
            }
            NavView.IsBackEnabled = ContentFrame.CanGoBack;
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if(ContentFrame.CanGoBack)
                ContentFrame.GoBack();
        }
    }
}