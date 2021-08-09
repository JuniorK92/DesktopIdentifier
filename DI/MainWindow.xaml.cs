using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            displayedText.Content = string.Empty;
        }

        #endregion

        #region Methods
        private void SetColorFromHex(string hex)
        {
            if (!string.IsNullOrEmpty(hex))
            {
                try
                {
                    if (!string.IsNullOrEmpty(hex))
                    {
                        Color introducedColor = new Color();

                        introducedColor = (Color)ColorConverter.ConvertFromString(hex);

                        textBoxColor.Background = border.Background /*= border.BorderBrush*/ = new SolidColorBrush(introducedColor);
                        textBoxColor.Foreground = displayedText.Foreground = ContrastColor(new SolidColorBrush(introducedColor));
                    }
                }
                catch (Exception)
                {
                    border.Background = textBoxColor.Background = Brushes.White;
                    /*border.BorderBrush =*/
                    textBoxColor.Foreground = displayedText.Foreground = Brushes.Black;
                }

                textBoxColor.Text = hex;
            }
        }
        public SolidColorBrush ContrastColor(SolidColorBrush value)
        {
            var c = value.Color;
            var l = 0.2126 * c.ScR + 0.7152 * c.ScG + 0.0722 * c.ScB;

            return l < 0.5 ? Brushes.White : Brushes.Black;
        }
        #endregion

        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;

            this.Left = (screenWidth / 2) - (this.Width / 2);
            this.Top = 0;
            this.Topmost = true;
        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }

        private void ListBoxColorItem_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            MenuItem clickedItem = (MenuItem)sender;

            SetColorFromHex(new BrushConverter().ConvertToString(clickedItem.Background));
        }

        private void TextBoxColor_KeyUp(object sender, KeyEventArgs e)
        {
            string introducedHex = textBoxColor.Text;

            if (!string.IsNullOrEmpty(introducedHex) && introducedHex[0] != '#')
            {
                introducedHex = introducedHex.Insert(0, "#");
            }

            SetColorFromHex(introducedHex);
            textBoxColor.CaretIndex = introducedHex.Length;
        }
        private void TextBoxDisplayText_KeyUp(object sender, KeyEventArgs e)
        {
            displayedText.Content = textBoxDisplayText.Text.ToUpper();
        }
        private void sizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var height = sizeSlider.Value;
            Height = height;

            if (displayedText != null && height > 8) { displayedText.FontSize = (height - 8); }
        }
        private void opacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var opacity = opacitySlider.Value;
            Opacity = opacity;
        }

        private void borderWidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var borderWidth = borderWidthSlider.Value;
            border.BorderThickness = new Thickness(borderWidth, 0, borderWidth, borderWidth); ;
        }

        private void CloseLabel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
