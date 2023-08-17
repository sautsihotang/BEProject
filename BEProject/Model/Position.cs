using System.Text.Json.Serialization;

namespace BEProject.Model
{
    public class Position
    {
        public int PositionId { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }

        public int TitleId { get; set; } //foreignkey
        [JsonIgnore]
        public virtual Title Title { get; set; }
        [JsonIgnore]
        public List<Employee> Employees { get; set; }
    }
}
