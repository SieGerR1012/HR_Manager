using HR_ManagerUI.Models;
using System.Net.Http.Json;

namespace HR_ManagerUI.Services;

public class EmployeeService
{
    private readonly HttpClient _http;

    public EmployeeService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<EmployeeDto>> GetEmployees()
    {
        var result = await _http.GetFromJsonAsync<List<EmployeeDto>>("api/employee");
        return result ?? new List<EmployeeDto>();
    }
}