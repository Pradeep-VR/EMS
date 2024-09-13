using Management.Database.Servicess;
using Management.Extras;
using Management.Extras.CustomAddons;
using Management.Managements;
using Management.Models;

namespace Management.Screens;

public partial class SignIn : ContentPage
{
    EmployeeManagement EmpMgnt = new EmployeeManagement();
    IEmployeeServices _Service = new EmployeeServices();
    Alerts Alerts = new Alerts();

    public SignIn()
    {           
        InitializeComponent();
        pageload();
        txtEmpId.Text = "TL0115";
        txtPassword.Text = "0115";
        lblCopyrights.Text = CmnVariables.CopyRights;
    }

    private async void pageload()
    {
        List<Employees> Empslist = await EmpMgnt.GetEmpService();
        if (Empslist != null)
        {
            foreach (var emp in Empslist)
            {
                var res = await this._Service.Chk_CreateUpdate(emp);
            }
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new SignUp());
        //await Shell.Current.GoToAsync("//SignUp");
    }


    private async void BtnSignIn_Clicked(object sender, EventArgs e)
    {
        string EmpId = txtEmpId.Text == null ? "" : txtEmpId.Text;
        string Passs = txtPassword.Text == null ? "" : txtPassword.Text;

        if (EmpId.Length > 0 && Passs.Length > 0)
        {
            var response = await this._Service.SignInAuthuntication(EmpId, Passs);
            if (response == true)
            {
                var Employes = await this._Service.GetEmployeeById(EmpId);
                if (Employes != null)
                {
                    var SessionEmp = new CreateEmployees()
                    {
                        empId = Employes.empId,
                        firstName = Employes.firstName,
                        department = Employes.department,
                        designation = Employes.designation,
                        empRole = Employes.empRole,                        
                    };
                    CmnVariables.Employees = SessionEmp;

                    if (Passs != "Welcome@123")
                    {
                        Alerts.show_SnackBar(Colors.Green, Colors.White, "Login Success.", 1);
                        await Navigation.PushAsync(new Home());
                    }
                    else
                    {
                        Alerts.show_SnackBar(Colors.Green, Colors.White, "Login Success.. Change Your Password.", 2);
                        await Navigation.PushAsync(new ForgotPassword());
                    }
                }

            }
            else
            {
                Alerts.show_SnackBar(Colors.Red, Colors.White, EmpId + " Not Found in System.\nContact Admin.", 2);
            }
        }
        else
        {
            Alerts.show_SnackBar(Colors.Red, Colors.White, "Please Enter the Details.", 2);
        }
    }

    private void chkShowPassword_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        txtPassword.IsPassword = !e.Value;
    }

    private void ImgBtnTogglePassword_Clicked(object sender, EventArgs e)
    {
        if (txtPassword.IsPassword)
        {
            txtPassword.IsPassword = false;
            imgBtnTogglePassword.Source = "eye_closed.png";
        }
        else
        {
            txtPassword.IsPassword = true;
            imgBtnTogglePassword.Source = "eye.png";
        }
    }

    private Entry entryField;
    private Button saveButton;
    private Button closeButton;

    private async void TapGus_Tapped(object sender, EventArgs e)
    {
        entryField = new Entry
        {
            Text = CmnVariables.baseUrl,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.Center,
            ZIndex = 2,
            IsEnabled = true,
        };

        saveButton = new Button
        {
            Text = "Save",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
        };
        saveButton.Clicked += SaveButton_Clicked;

        closeButton = new Button
        {
            Text = "Close",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
        };
        closeButton.Clicked += CloseButton_Clicked;

        StackLayout parentStackLayout = (StackLayout)sender;

        parentStackLayout.Children.Add(entryField);
        parentStackLayout.Children.Add(saveButton);
        parentStackLayout.Children.Add(closeButton);
        
        entryField.Focus();
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        string enteredData = entryField.Text;

        if (string.IsNullOrWhiteSpace(enteredData))
        {
            await DisplayAlert("Error", "The field cannot be empty.", "OK");
            return;
        }

        if (!IsValidUrl(enteredData))
        {
            await DisplayAlert("Error", "The entered URL is not valid.", "OK");
            return;
        }

        CmnVariables.baseUrl = enteredData;

        await DisplayAlert("Success", "Data saved successfully!", "OK");

        CloseButton_Clicked(sender, e);
    }

    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        StackLayout parentStackLayout = (StackLayout)entryField.Parent;
        parentStackLayout.Children.Remove(entryField);
        parentStackLayout.Children.Remove(saveButton);
        parentStackLayout.Children.Remove(closeButton);
    }

    // URL validation method
    private bool IsValidUrl(string url)
    {
        Uri uriResult;
        bool result = Uri.TryCreate(url + "api/Employees", UriKind.Absolute, out uriResult)
                      && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        return result;
    }

}