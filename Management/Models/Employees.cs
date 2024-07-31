using SQLite;

namespace Management.Models;

class Employees
{
    [PrimaryKey]
    public string? empId { get; set; }
    public string? firstName { get; set; }
    public string? middleName { get; set; }
    public string? lastName { get; set; }
    public string? secretCode { get; set; }
    public bool active { get; set; }
    public string? department { get; set; }
    public string? designation { get; set; }
    public string? empRole { get; set; }
    public DateTime registeredDate { get; set; }
    public string? registeredBy { get; set; }
    public string? deviceDetails { get; set; }
    public bool empViolation { get; set; }
    public int deviceViolation { get; set; }
    public string? remarks { get; set; }
}

class CreateEmployees
{
    public string? empId { get; set; }
    public string? firstName { get; set; }
    public string? secretCode { get; set; }
    public bool active { get; set; }
    public string? department { get; set; }
    public string? designation { get; set; }
    public string? empRole { get; set; }
    public DateTime registeredDate { get; set; }
    public string? registeredBy { get; set; }   
}

/*  
 
{
"empid": "TL073",
"firstname": "Pradeep",
"middlename": "",
"lastname": "",
"secretcode": "0115",
"active": true,
"department": "Software",
"designation": "Developer",
"emprole": "Admin",
"registereddate": "2024-07-19T10:26:07.773",
"registeredby": "Admin",
"devicedetails": "",
"empviolation": false,
"deviceviolation": 0,
"remarks": ""
}
 
 */
