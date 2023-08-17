namespace BEProject.Model.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nik { get; set; }
        public string Address { get; set; }
        public int PositionId { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }

        public string TitleCode { get; set; }
        public string TitleName { get; set; }
    }
}
