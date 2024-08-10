using Management.Extras;

namespace Management.Screens;

public partial class Home : ContentPage
{
    public Home()
    {
        InitializeComponent();

        lblCopyrights.Text = CmnVariables.CopyRights;
        
        if (CmnVariables.Employees != null)
        {
            lblEmpId.Text = CmnVariables.Employees.empId;
            lblEmpRole.Text = CmnVariables.Employees.empRole;
            lblEmpEmail.Text = CmnVariables.Employees.firstName + "@teamliftss.com";
            lblEmpDesignation.Text = CmnVariables.Employees.department + " / " + CmnVariables.Employees.designation;
        }
        else
        {
            Shell.Current.GoToAsync("//SignIn");
        }
    }

    private async void OnToolbarItemClicked(object sender, EventArgs e)
    {
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            var options = new string[] { "Profile", "Settings", "Logout" };
            string action = await DisplayActionSheet("Options", "Cancel", null, options);
            HandleMenuAction(action);
        }
        else
        {
            // Handle other platforms if needed
        }
    }

    private async void HandleMenuAction(string action)
    {
        switch (action)
        {
            case "Profile":
                //await Navigation.PushAsync(new ProfilePage());
                break;
            case "Settings":
                await Navigation.PushAsync(new Attendance());
                break;
            case "Logout":
                await Shell.Current.GoToAsync("//SignIn");
                break;
            default:
                break;
        }
    }

    //private async void OnToolbarItemClicked(object sender, EventArgs e)
    //{
    //    string action = await DisplayActionSheet("Options", "Cancel", null, "Profile", "Settings", "Logout");
    //    switch (action)
    //    {
    //        case "Profile":
    //            // Navigate to Profile page
    //            //await Navigation.PushAsync(new ProfilePage());
    //            break;
    //        case "Settings":
    //            // Navigate to Settings page
    //            await Navigation.PushAsync(new Attendance());
    //            break;
    //        case "Logout":
    //            // Handle logout
    //            await Shell.Current.GoToAsync("//SignIn");
    //            break;
    //        default:
    //            break;
    //    }
    //}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//SignIn");
    }

    private async void icbtnAttendance_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Attendance());
    }

    private async void icbtnRequest_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Request());
    }

    private async void icbtnManageEmployee_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManageEmployees());
    }

    private async void icbtnReports_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Reports());
    }

    private async void icbtnTaskAssigin_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TaskAssigning());
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