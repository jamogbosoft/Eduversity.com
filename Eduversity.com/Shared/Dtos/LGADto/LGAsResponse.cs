using Eduversity.com.Shared.Models;

namespace Eduversity.com.Shared.Dtos.LGADto
{
    public class LGAsResponse: ILGAResponse
    {
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public List<LGA> LGAs { get; set; } = new List<LGA>();
    }
}
