
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;

namespace MauiNestedNotify
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
    partial class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
            ButtonClickedCommand = new Command((o) =>
            {
                _count++;
                string plural = _count > 1 ? "s" : string.Empty; 
                Text = $"You clicked {_count} time{plural}";
            });
        }
        int _count = 0;

        public ICommand ButtonClickedCommand { get; }

        [ObservableProperty]
        string _text = "Click Me";
        public Settings Settings { get; } = new Settings();
    }

    public partial class Settings : ObservableObject
    {
        [ObservableProperty]
        bool _useItalic;

        [ObservableProperty]
        FontAttributes _fontSetting;

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            switch (e.PropertyName)
            {
                case nameof(UseItalic):
                    FontSetting = UseItalic ? FontAttributes.Italic : FontAttributes.None;
                    break;
            }
        }
    }
}
