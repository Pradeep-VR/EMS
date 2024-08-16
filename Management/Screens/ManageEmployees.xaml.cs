using Management.Models;
using System.Collections.ObjectModel;

namespace Management.Screens;

public partial class ManageEmployees : ContentPage
{

    //public ObservableCollection<Employee> _employees { get; set; }
    //public ObservableCollection<Employee> _filteredEmployees { get; set; }
    private List<Employee> _employees;
    private List<Employee> _filteredEmployees;


    public ManageEmployees()
    {
        InitializeComponent();

        InitializeData();

        // Set the initial filter to "Name"
        FilterPicker.SelectedIndex = 0;

        // Bind the ListView to the filtered list
        EmployeeListView.ItemsSource = _filteredEmployees;
    }





    private void InitializeData()
    {
        // Initialize with sample data
        _employees = new List<Employee>
        {
            new Employee { SerialNo = 1, Name = "John Doe", EmpID = "E001", Email = "john@example.com" },
            new Employee { SerialNo = 2, Name = "Jane Smith", EmpID = "E002", Email = "jane@example.com" },
            // Add more employees here...
        };

        _filteredEmployees = new List<Employee>(_employees);
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        FilterEmployees();
    }

    private void FilterEmployees()
    {
        var searchText = SearchBar.Text;
        var filterBy = FilterPicker.SelectedItem.ToString();

        if (string.IsNullOrEmpty(searchText))
        {
            _filteredEmployees = new List<Employee>(_employees);
        }
        else
        {
            if (filterBy == "Name")
            {
                _filteredEmployees = _employees
                    .Where(emp => emp.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }
            else if (filterBy == "EmpID")
            {
                _filteredEmployees = _employees
                    .Where(emp => emp.EmpID.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }
        }

        EmployeeListView.ItemsSource = _filteredEmployees;
    }

    private async void OnEmployeeSelected(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null) return;

        var selectedEmployee = e.Item as Employee;

        // Navigate to EditEmployeePage with the selected employee data
        await Navigation.PushAsync(new EditEmployee(selectedEmployee));

        ((ListView)sender).SelectedItem = null;
    }
}

