namespace EmployeeMgt
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public List<Employee>? employees { get; set; } = new List<Employee>();

    }
}
