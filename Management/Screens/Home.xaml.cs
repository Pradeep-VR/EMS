namespace Management.Screens;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//SignIn");
    }
}