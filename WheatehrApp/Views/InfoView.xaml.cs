using WheatehrApp.ViewModels;

namespace WheatehrApp.Views;

public partial class InfoView : ContentPage
{
    


    public InfoView()
    {


 
        InitializeComponent();
        BindingContext = new WeatherViewModel();

    }
}
