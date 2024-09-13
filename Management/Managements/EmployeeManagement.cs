using Management.Extras;
using Management.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

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
            catch (Exception ex)
            {
                return new List<Employees>();
            }
        }

        public async Task<Response> ForgetPassword(Employee emp)
        {
            try
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(CmnVariables.baseUrl)
                };

                var response = await httpClient.PostAsJsonAsync("api/ForgetPassword", emp);
                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    var cont = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Response>(cont);
                }
                else
                {
                    return new Response { isSuccess = false, Result = Convert.ToString(response.StatusCode) };
                }
            }
            catch (Exception ex)
            {
                return new Response { isSuccess = false, Result = ex.Message };
            }
        }
    }
}
