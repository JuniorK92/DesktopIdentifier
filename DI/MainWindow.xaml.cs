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
        MainWindowViewModel _viewModel;
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
        }

        #endregion

        #region Events
        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }

        private void ListBoxColorItem_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            MenuItem clickedItem = (MenuItem)sender;

            _viewModel.SetColorFromHex(new BrushConverter().ConvertToString(clickedItem.Background));
        }

        private void TextBoxColor_KeyUp(object sender, KeyEventArgs e)
        {
            string introducedHex = textBoxColor.Text;

            if (!string.IsNullOrEmpty(introducedHex) && introducedHex[0] != '#')
            {
                introducedHex = introducedHex.Insert(0, "#");
            }

            _viewModel.SetColorFromHex(introducedHex);
            textBoxColor.CaretIndex = introducedHex.Length;
        }
        private void TextBoxDisplayText_KeyUp(object sender, KeyEventArgs e)
        {
            _viewModel.Text = textBoxDisplayText.Text.ToUpper();
        }
        private void sizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var height = sizeSlider.Value;
            _viewModel.Height = height;

            if (displayedText != null && height > 8) { displayedText.FontSize = (_viewModel.Height - 8); }
        }
        private void opacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var opacity = opacitySlider.Value;
            _viewModel.Opacity = opacity;
        }

        private void borderWidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var borderWidth = borderWidthSlider.Value;
            _viewModel.BorderThickness = new Thickness(borderWidth, 0, borderWidth, borderWidth);
        }

        private void CloseLabel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
