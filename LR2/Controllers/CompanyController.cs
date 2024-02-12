using LR2.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    public ActionResult<string> GetCompanyWithMostEmployees()
    {
        var company = _companyService.GetCompanyWithMostEmployees();
        return Ok($"Company with most employees: {company}");
    }

    [HttpGet("/")]
    public ActionResult<string> Index()
    {
        var companyWithMostEmployees = _companyService.GetCompanyWithMostEmployees();
        return Ok($"Company with most employees: {companyWithMostEmployees}");
    }
}