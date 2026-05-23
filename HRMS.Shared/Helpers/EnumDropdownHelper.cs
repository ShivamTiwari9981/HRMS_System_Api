using HRMS.Shared.Dto;

namespace HRMS.Shared.Helpers
{
    public static class EnumHelper
    {
        public static List<EnumDto> GetEnumList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(x => new EnumDto
                {
                    Id = Convert.ToInt32(x),
                    Name = x.ToString()
                })
                .ToList();
        }
    }

}


