using Management.Extras;
using Management.Models;
using Newtonsoft.Json;

namespace Management.Managements
{
    internal class EmployeeManagement
    {
        public async Task<List<Employees>>? GetEmpService()
        {
            try
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(CmnVariables.baseUrl)
                };
                var response = await httpClient.GetAsync("api/Employees");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Employees>>(content);
                }
                return new List<Employees>();
            }
            catch(Exception ex)
            {
                return new List<Employees>();
            }
        }
    }
}
