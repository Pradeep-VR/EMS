namespace Management.Screens;

public partial class SignUp : ContentPage
{

    public SignUp()
	{
		InitializeComponent();
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {		
		await Shell.Current.GoToAsync("//SignIn");        
    }
}