using Eduversity.com.Shared.Models.Data;

namespace Eduversity.com.Client.Services.MaritalStatusService
{
    public class MaritalStatusService: IMaritalStatusService
    {
        public List<MaritalStatus> Status => new()
            {
                new MaritalStatus { Name = "Single", Value = "S" },
                new MaritalStatus { Name = "Married", Value = "M" },
                new MaritalStatus { Name = "Widowed", Value = "W" },
                new MaritalStatus { Name = "Divorced", Value = "D" }
            };
    }
}
