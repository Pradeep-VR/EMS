using Management.Extras;
using System.Collections.ObjectModel;

namespace Management.Screens;

public partial class Attendance : ContentPage
{
    public class AttendanceRecord
    {
        public int SNo { get; set; }
        public DateTime DateTime { get; set; }
        public string? Attendance { get; set; }
        public string? Place { get; set; }
    }
    public ObservableCollection<AttendanceRecord> AttendanceRecords { get; set; }

    public Attendance()
    {
        InitializeComponent();
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

        AttendanceRecords = new ObservableCollection<AttendanceRecord>
            {
                new AttendanceRecord { SNo = 1, DateTime = DateTime.Now, Attendance = "Present", Place = "Office" },
                new AttendanceRecord { SNo = 2, DateTime = DateTime.Now, Attendance = "Absent", Place = "Home" },
                new AttendanceRecord { SNo = 3, DateTime = DateTime.Now, Attendance = "Present", Place = "WFH" },
                // Add more records as needed
            };

        attendanceListView.ItemsSource = AttendanceRecords;
    }

    private void Icncalendar_Clicked(object sender, EventArgs e)
    {
        datePicker.Focus();
    }

    private void Calendar_Tapped(object sender, TappedEventArgs e)
    {
        datePicker.Focus();
    }
}