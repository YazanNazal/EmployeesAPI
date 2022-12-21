using EmployeeMgt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmployeeMgt.Controllers

{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private static DataContext _context;
        public CompanyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Company>>> Get()
        {
            var companies = await _context.Companies.Include(e=>e.employees).ToListAsync();
            return Ok(companies);

        }
        [HttpGet("id")]
        public async Task<ActionResult<Company>> Get(int id)
        {
            var company = await _context.Companies.Include(e => e.employees).Where(c => c.Id == id).FirstOrDefaultAsync();
            if (company == null)
                return BadRequest("Company not Found");
            return Ok(company);
        }

        [HttpPost]
        public ActionResult<List<Company>> AddCompany([FromForm] Company Company)
        {
            _context.Companies?.Add(Company);
            _context.SaveChanges();
            return Ok("Success");
        }
        [HttpPut] //Update 
        public async Task<ActionResult<Company>> UpdateCompany([FromForm] Company company)
        {

            var MyCompany = await _context.Companies.Where(h => h.Id == company.Id).FirstOrDefaultAsync();
            if (MyCompany == null)
                return BadRequest("Company not Found");
            MyCompany.Name = company.Name;
            MyCompany.Address = company.Address;
            await _context.SaveChangesAsync();
            return Ok(MyCompany);
        }

        [HttpDelete("id")]
        public async Task<ActionResult<string>> DeleteCompany(int id)
        {
            var MyCompany = await _context.Companies.Where(h => h.Id == id).FirstOrDefaultAsync();
            if (MyCompany == null)
                return BadRequest("Company not Found");
            _context.Companies.Remove(MyCompany);
            await _context.SaveChangesAsync();
            return Ok("Success");
        }
    }
}





