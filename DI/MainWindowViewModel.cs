using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DI
{
    public class MainWindowViewModel : ViewModelBase
    {
        double _width;
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        double _height;
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        double _position;
        public double Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        Thickness _borderThickness;
        public Thickness BorderThickness
        {
            get => _borderThickness;
            set => SetProperty(ref _borderThickness, value);
        }

        double _opacity;
        public double Opacity
        {
            get => _opacity;
            set => SetProperty(ref _opacity, value);
        }

        Color _backColor;
        public Color BackColor
        {
            get => _backColor;
            set => SetProperty(ref _backColor, value);
        }

        Color _foreColor;
        public Color ForeColor
        {
            get => _foreColor;
            set => SetProperty(ref _foreColor, value);
        }

        string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        readonly DelegateCommand _updateWidthCommand;
        public ICommand UpdateWidthCommand => _updateWidthCommand;
        public ICommand UpdateHeightCommand { get; private set; }
        public ICommand UpdatePositionCommand { get; private set; }
        public ICommand UpdateBorderThicknessCommand { get; private set; }
        public ICommand UpdateOpacityCommand { get; private set; }

        readonly DelegateCommand _updateBackColorCommand;
        public ICommand UpdateBackColorCommand => _updateBackColorCommand;
        public ICommand UpdateForeColorColorCommand { get; private set; }

        public MainWindowViewModel()
        {
            Width = 150;
            Height = 100;

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            Position = ((screenWidth / 2d) - (Width / 2d));

            _updateBackColorCommand = new DelegateCommand(OnBackColorUpdate, CanUpdateBackColor);            
            
        }

        SolidColorBrush ContrastColor(SolidColorBrush value)
        {
            var c = value.Color;
            var l = 0.2126 * c.ScR + 0.7152 * c.ScG + 0.0722 * c.ScB;

            return l < 0.5 ? Brushes.White : Brushes.Black;
        }

        public void SetColorFromHex(string hex)
        {
            if (!string.IsNullOrEmpty(hex))
            {
                try
                {
                    if (!string.IsNullOrEmpty(hex))
                    {
                        Color introducedColor = new Color();

                        introducedColor = (Color)ColorConverter.ConvertFromString(hex);

                        BackColor = new SolidColorBrush(introducedColor).Color;
                        ForeColor = ContrastColor(new SolidColorBrush(introducedColor)).Color;
                    }
                }
                catch (Exception)
                {
                    BackColor = Brushes.White.Color;
                    ForeColor = Brushes.Black.Color;
                }
            }
        }

        void OnBackColorUpdate(object commandParameter)
        {
            Width = 150;
            _updateWidthCommand.InvokeCanExecuteChanged();
        }        
        bool CanUpdateBackColor(object commandParameter)
        {
            return Width == 150;
        }



    }
}
