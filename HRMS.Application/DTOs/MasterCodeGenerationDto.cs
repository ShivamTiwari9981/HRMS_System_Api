namespace HRMS.Application.DTOs
{
    public class MasterCodeGenerationDto
    {
        public string TableName { get; set; }
        public string Prefix { get; set; }
        public int LastNumber { get; set; }
    }
}
