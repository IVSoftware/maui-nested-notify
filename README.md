Looking at your code, the intention seems to be a `Settings` that is `ObservableObject` nested inside your `MainPageViewModel` which is also an `ObservableObject`. In xaml, the way you would bind to this is by referencing the nested object, e.g. `<CheckBox IsChecked="{Binding Settings.UseItalic}"/>`.

___

```<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:MauiNestedNotify"
             x:Class="MauiNestedNotify.MainPage">
    <ContentPage.BindingContext>
        <local:MainPageViewModel/>
    </ContentPage.BindingContext>
    <ScrollView>
        <VerticalStackLayout
            Padding="30,0"
            Spacing="25">
            <Image
                Source="dotnet_bot.png"
                HeightRequest="185"
                Aspect="AspectFit"
                SemanticProperties.Description="dot net bot in a race car number eight" />
            <Button
                Text="{Binding Text}" 
                FontAttributes="{Binding Settings.FontSetting}"
                SemanticProperties.Hint="Counts the number of times you click"
                Command="{Binding ButtonClickedCommand}"
                HorizontalOptions="Fill" />
            <Grid 
                HeightRequest="100">
                <Frame Margin="0,20,0,0">
                    <Grid
                        Margin="5"
                        ColumnDefinitions="25,*">
                        <CheckBox IsChecked="{Binding Settings.UseItalic}"/>
                        <Label Padding="5,0" Grid.Column="1" Text="Use Italics" VerticalTextAlignment="Center"/>
                    </Grid>
                </Frame>
                <Label 
                    Margin="10,5,0,0"
                    Background="White"
                    Padding="5"
                    Text="Settings"
                    WidthRequest="75"
                    HeightRequest="30"
                    VerticalOptions="Start"
                    HorizontalOptions="Start"/>
            </Grid>               
        </VerticalStackLayout>
    </ScrollView>
</ContentPage>
```

#### Main Page VM

``` 
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

    // This is 'not' an observable property itself!
    public Settings Settings { get; } = new Settings();
}
```

#### Settings

```
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
                // You 'could' use IValueConverter, or just do this:
                FontSetting = UseItalic ? FontAttributes.Italic : FontAttributes.None;
                break;
        }
    }
}
```

[![enter image description here][1]][1]


  [1]: https://i.sstatic.net/1KQO9am3.png