using Eduversity.com.Shared.Models;

namespace Eduversity.com.Shared.Dtos.StateDto
{
    public class StatesResponse: IStateResponse
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public List<State> States { get; set; } = new List<State>();
    }
}
