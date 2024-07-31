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
        txtEmpId.Text = "TL073";
        txtPassword.Text = "0115";
    }

    private async void pageload()
    {
        List<Employees> Empslist = await EmpMgnt.GetEmpService();
        if (Empslist != null)
        {
            foreach (var emp in Empslist)
            {
                var res = await this._Service.CreateEmployee(emp);
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
                }
                CmnVariables.Employees = 
                Alerts.show_SnackBar(Colors.Green, Colors.White, "Login Success.", 1);
                await Navigation.PushAsync(new Home());
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
            imgBtnTogglePassword.Source = "eye_closed.png"; // Image for hiding password
        }
        else
        {
            txtPassword.IsPassword = true;
            imgBtnTogglePassword.Source = "eye.png"; // Image for showing password
        }
    }

}