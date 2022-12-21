using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmployeeMgt.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private static DataContext _context;
        public EmployeeController(DataContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            
            var Employees = await _context.Employees.Include(c=>c.Company).AsNoTracking().ToListAsync();
            return Ok(Employees);
        }
        [HttpGet("id")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var Employees = await _context.Employees.Include(e=>e.Company).AsNoTracking().Where(c => c.Id == id).FirstOrDefaultAsync();
            if (Employees == null)
                return BadRequest("Employee not Found");
            return Ok(Employees);

        }

        [HttpGet("GetEmployeesByCompanyId")]
        public async Task<ActionResult<List<Employee>>> GetEmployeesByCompanyId(int id)
        {
            var Employees = await _context.Employees.Include(e => e.Company).AsNoTracking().Where(c => c.Company.Id == id).FirstOrDefaultAsync();
            if (Employees == null)
                return BadRequest("Employee not Found");
            return Ok(Employees);

        }

        [HttpPost("AddEmployee")]
        public ActionResult<List<Employee>> AddEmployee([FromBody] Employee employee)
        {
            _context.Employees?.Add(employee);
            _context.SaveChanges();
            return Ok("Success");

        }

        [HttpPut] //Update 
        public async Task<ActionResult<Employee>> UpdateEmployee([FromBody] Employee employee)
        {
            var MyEmployee = await _context.Employees.Where(h => h.Id == employee.Id).FirstOrDefaultAsync();
            if (MyEmployee == null)
                return BadRequest("Employee not Found");
            MyEmployee.Name = employee.Name;
            MyEmployee.Address = employee.Address;
            await _context.SaveChangesAsync();
            return Ok(MyEmployee);

        }
        [HttpDelete("id")]
        public async Task<ActionResult<string>> DeleteEmployee(int id)
        {
            var MyEmployee = await _context.Employees.Where(h => h.Id == id).FirstOrDefaultAsync();
            if (MyEmployee == null)
                return BadRequest("Employee not Found");
            _context.Employees.Remove(MyEmployee);
            await _context.SaveChangesAsync();
            return Ok("Success");
        }

        [HttpPost("UpdateCompany")]
        public async Task<ActionResult<List<Employee>>> UpdateCompany(int EmployeeId, int CompanyId)
        {
            try
            {
                var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == CompanyId);
                if (company == null)
                    return BadRequest("Company not Found");
                var employee = await _context.Employees.Where(h => h.Id == EmployeeId).FirstOrDefaultAsync();
                employee.Company = company;
                await _context.SaveChangesAsync();
                return Ok("Transfered");
            }catch(Exception ex)
            {
                // save ex to db
                return BadRequest(ex.Message);
            }
        }
    }
}







