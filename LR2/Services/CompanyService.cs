using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using LR2.Models;

namespace LR2.Services
{
    public interface ICompanyService
    {
        string GetCompanyWithMostEmployees();
    }

    public class CompanyService : ICompanyService
    {
        private readonly IConfiguration _configuration;

        public CompanyService(IConfiguration configuration)
        {
            _configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Config/json.json")
                .AddXmlFile("Config/xmlFile.xml")
                .AddIniFile("Config/textFile.ini")
                .Build();
            _configuration = builder;
        }

        public string GetCompanyWithMostEmployees()
        {
            var companies = _configuration.GetSection("Companies").Get<Dictionary<string, CompanyInfo>>();

            if (companies != null && companies.Any())
            {
                var maxEmployees = companies.Max(c => c.Value?.Employees ?? 0);
                var companyWithMostEmployees = companies.FirstOrDefault(c => c.Value?.Employees == maxEmployees).Key;
                return companyWithMostEmployees;
            }
            else
            {
                Console.WriteLine("No companies found in configuration files.");
                return "No companies found";
            }
        }
    }
}

