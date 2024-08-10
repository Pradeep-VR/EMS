using Management.Models;

namespace Management.Screens;

public partial class EditEmployee : ContentPage
{
    private Employee _employee;

    public EditEmployee(Employee employee)
	{
		InitializeComponent();

        _employee = employee;

        // Bind data to the Entry fields
        NameEntry.Text = _employee.Name;
        EmpIDEntry.Text = _employee.EmpID;
        EmailEntry.Text = _employee.Email;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Update employee details
        _employee.Name = NameEntry.Text;
        _employee.EmpID = EmpIDEntry.Text;
        _employee.Email = EmailEntry.Text;

        // Perform any additional save logic here

        await Navigation.PopAsync();
    }
}