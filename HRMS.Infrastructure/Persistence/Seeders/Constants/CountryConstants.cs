namespace HRMS.Infrastructure.Persistence.Seeders.Constants
{
    public static class CountryConstants
    {
        public static readonly List<CountryModel> modelList = new()
        {
            new CountryModel
            {
                CountryName = "India",
            },
            new CountryModel
            {
               CountryName = "Chaina",
            },
            new CountryModel
            {
               CountryName = "Japan",
            },
        };
    }
    public class CountryModel : BaseModel
    {
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
