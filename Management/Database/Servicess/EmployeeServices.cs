using Management.Extras;
using Management.Models;
using SQLite;

namespace Management.Database.Servicess;

internal interface IEmployeeServices
{
    public Task<List<Employees>> GetEmployees();
    public Task<Employees> GetEmployeeById(string strEmpId);
    public Task<bool> SignInAuthuntication(string strEmpId, string strSecretCode);
    public Task<Response> SignUpAuthentication(Employees employees);
    public Task<bool> CreateEmployee(Employees employee);
    public Task<bool> UpdateEmployee(Employees employee);
    public Task<bool> DeleteEmployee(Employees employee);
    public Task<bool> Chk_CreateUpdate(Employees employee);
}

public class EmployeeServices : IEmployeeServices
{
    private readonly IEmployeeServices _services;
    private SQLiteAsyncConnection _dbConnection;
    public EmployeeServices()
    {
        SetUpLocalDb();
    }

    private async void SetUpLocalDb()
    {
        if (_dbConnection == null)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EMPMGMT.db3");
            _dbConnection = new SQLiteAsyncConnection(dbPath);
            await _dbConnection.CreateTableAsync<Employees>();
        }
    }

    async Task<List<Employees>> IEmployeeServices.GetEmployees()
    {
        return await _dbConnection.Table<Employees>().ToListAsync();
    }


    async Task<Employees> IEmployeeServices.GetEmployeeById(string strEmpId)
    {
        return await _dbConnection.Table<Employees>().Where(x => x.empId == strEmpId && x.active == true).FirstOrDefaultAsync();
    }

    async Task<bool> IEmployeeServices.CreateEmployee(Employees employee)
    {
        var emp = await this._dbConnection.Table<Employees>().Where(x => x.empId == employee.empId).CountAsync();
        if (emp == 0)
        {
            int res = await this._dbConnection.InsertAsync(employee);
            return res == 0 ? true : false;
        }
        else
        {
            return false;
        }

    }

    async Task<bool> IEmployeeServices.Chk_CreateUpdate(Employees employee)
    {
        var emp = await this._dbConnection.Table<Employees>().Where(x => x.empId == employee.empId).CountAsync();
        if (emp > 0)
        {            
            int ress = await this._dbConnection.UpdateAsync(employee);
            return ress == 0 ? true : false;
        }
        else
        {
            int res = await this._dbConnection.InsertAsync(employee);
            return res == 0 ? true : false;            
        }
    }
    async Task<bool> IEmployeeServices.UpdateEmployee(Employees employee)
    {
        var emp = await this._dbConnection.Table<Employees>().Where(x => x.empId == employee.empId).CountAsync();
        if (emp == 0)
        {
            int res = await this._dbConnection.UpdateAsync(employee);
            return res == 0 ? true : false;
        }
        else
        {
            return false;
        }

    }

    async Task<bool> IEmployeeServices.DeleteEmployee(Employees employee)
    {
        var emp = await this._dbConnection.Table<Employees>().Where(x => x.empId == employee.empId).CountAsync();
        if (emp == 0)
        {
            int res = await this._dbConnection.DeleteAsync(employee);
            return res == 0 ? true : false;
        }
        else
        {
            return false;
        }

    }

    async Task<bool> IEmployeeServices.SignInAuthuntication(string strEmpId, string strSecretCode)
    {
        var tryHash = Utils.HashPassword(strSecretCode.Trim().ToString());
        var res = await this._dbConnection.Table<Employees>().Where(x => x.empId == strEmpId && x.active == true && x.empViolation == false && x.deviceViolation < 3).FirstOrDefaultAsync();
        if (res == null)
        {
            return false;
        }
        else
        {
            bool isVerify = Utils.VerifyPassword(res.secretCode, strSecretCode);
            return isVerify;
        }
    }

    async Task<Response> IEmployeeServices.SignUpAuthentication(Employees employees)
    {
        Response retRes = new Response();
        try
        {
            if (employees != null)
            {
                var RealEmp = new Employees()
                {
                    empId = employees.empId,
                    firstName = employees.firstName,
                    lastName = employees.lastName,
                    middleName = employees.middleName,
                    secretCode = Utils.HashPassword(employees.secretCode.Trim().ToString()),
                    active = true,
                    department = employees.department,
                    designation = employees.designation,
                    empRole = employees.empRole,
                    empViolation = false,
                    registeredBy = employees.registeredBy,
                    registeredDate = employees.registeredDate,
                    deviceViolation = 0,
                    remarks = employees.remarks,

                };
                var emp = await this._services.CreateEmployee(RealEmp);
                if (emp == true)
                {
                    return retRes = new Response()
                    {
                        isSuccess = emp,
                        Result = "Employee Added Success."
                    };
                }
                else
                {
                    return retRes = new Response()
                    {
                        isSuccess = emp,
                        Result = employees.empId + " Is Already Available."
                    };
                }
            }
            else
            {
                return retRes = new Response()
                {
                    isSuccess = false,
                    Result = "Employee is Null Here."
                };
            }
        }
        catch (Exception ex)
        {
            return retRes = new Response()
            {
                isSuccess = false,
                Result = ex.ToString()
            };
        }
    }


}
