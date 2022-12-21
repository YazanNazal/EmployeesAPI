namespace EmployeeMgt
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime HireDate { get; set; } 
        public string Address { get; set; } = string.Empty;


        public Company? Company { get; set; }
    }
}
