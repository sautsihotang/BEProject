using System.Text.Json.Serialization;

namespace BEProject.Model
{
    public class Title
    {
        public int TitleId { get; set; }
        public string TitleCode { get; set; }
        public string TitleName { get; set; }
        [JsonIgnore]
        public List<Position> Positions { get; set; }

    }
}
