using Management.Extras;

namespace Management.Screens;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
        //var EmpDtls = CmnVariables.Employees();
        if(CmnVariables.Employees != null)
        {
            lblEmpId.Text = CmnVariables.Employees.empId.ToString();
            lblEmpRole.Text = CmnVariables.Employees.empRole.ToString();
            lblEmpEmail.Text = CmnVariables.Employees.firstName.ToString() + "@teamliftss.com";
            lblEmpDesignation.Text = CmnVariables.Employees.designation.ToString();
        }
        else
        {
            Shell.Current.GoToAsync("//SignIn");
        }
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//SignIn");
    }

   
    //private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    //{
    //    var data = DatePicker.Date;
    //    if (btnExit.IsVisible == true)
    //    {
    //        btnExit.IsVisible = false;
    //    }
    //    else
    //    {
    //        btnExit.IsVisible = true;
    //    }
    //}
}