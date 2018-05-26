using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace LWS
{
    /// <summary>
    /// WpfSample.xaml の相互作用ロジック
    /// </summary>
    public partial class WpfSample : Window
    {
        public WpfSample()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as MenuItem).Header)
            {
                case "Japanese":
                    ResourceManager.Current.ChangeCulture("ja-JP");
                    break;
                case "English":
                    ResourceManager.Current.ChangeCulture("en-US");
                    break;
            }

            Console.WriteLine(CultureInfo.CurrentCulture.DisplayName);

            
        }
    }
}
