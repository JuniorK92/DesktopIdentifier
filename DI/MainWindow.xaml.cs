using System;
using System.Collections.Generic;
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
        #region Global Variables
        
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Tick += DispatcherTimer_Tick; ;
            dispatcherTimer.Interval = new TimeSpan(1, 0, 0);
            dispatcherTimer.Start();
        }

        #endregion

        #region Methods

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.Topmost = true;
            this.Activate();            
        }

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

                        textBoxColor.Background = new SolidColorBrush(introducedColor);
                        textBoxColor.Foreground = Brushes.White;

                        grid.Background = displayedTextBox.Background = new SolidColorBrush(introducedColor);
                        label.Foreground = displayedTextBox.Foreground = Brushes.White;
                    }
                }
                catch (Exception)
                {
                    textBoxColor.Background = Brushes.White;
                    textBoxColor.Foreground = Brushes.Black;

                    grid.Background = displayedTextBox.Background = Brushes.White;
                    label.Foreground = displayedTextBox.Foreground = Brushes.Black;
                }
            }
        }

        #endregion

        #region Events

        #region Window

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;

            this.Left = (screenWidth / 2) - (this.Width / 2);
            this.Top = 0;
            this.Topmost = true;
        }

        #endregion

        #region Colors

        private void ListBoxColorItem_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            MenuItem clickedItem = (MenuItem)sender;
            
            grid.Background = displayedTextBox.Background = clickedItem.Background;
            label.Foreground = displayedTextBox.Foreground = Brushes.White;
        }
        
        private void TextBoxColor_KeyUp(object sender, KeyEventArgs e)
        {
            string introducedHex = textBoxColor.Text;

            if (!string.IsNullOrEmpty(introducedHex) && introducedHex[0] != '#')
            {
                introducedHex = introducedHex.Insert(0, "#");
            }

            SetColorFromHex(introducedHex);
        }

        private void TextBoxColor2_KeyUp(object sender, KeyEventArgs e)
        {
            string introducedHex = textBoxColor2.Text;

            if (!string.IsNullOrEmpty(introducedHex) && introducedHex[0] != '#')
            {
                introducedHex = introducedHex.Insert(0, "#");
            }

            SetColorFromHex(introducedHex);
        }

        #endregion

        #region Displayed Text

        private void TextBoxDisplayText_KeyUp(object sender, KeyEventArgs e)
        {
            displayedTextBox.Text = textBoxDisplayText.Text;            
        }

        private void TextBoxDisplayText2_KeyUp(object sender, KeyEventArgs e)
        {
            displayedTextBox.Text = textBoxDisplayText2.Text;
        }

        #endregion

        #region Close

        private void CloseLabel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #endregion
    }
}
