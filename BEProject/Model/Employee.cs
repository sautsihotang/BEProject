using System.Text.Json.Serialization;

namespace BEProject.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNik { get; set; }
        public string EmployeeAddress { get; set; }
        public int PositionId { get; set; }
        [JsonIgnore]
        public virtual Position Position { get; set; }
    }
}
