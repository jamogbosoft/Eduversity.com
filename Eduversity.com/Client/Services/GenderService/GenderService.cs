
using Eduversity.com.Shared.Models.Data;

namespace Eduversity.com.Client.Services.GenderService
{
    public class GenderService : IGenderService
    {
        public List<Gender> Genders => new()
            {
                new Gender { Name = "Male", Value = "M" },
                new Gender { Name = "Female", Value = "F" }
            };
    }
}
