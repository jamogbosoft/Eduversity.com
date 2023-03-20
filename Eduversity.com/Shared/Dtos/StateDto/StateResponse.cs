using Eduversity.com.Shared.Models;

namespace Eduversity.com.Shared.Dtos.StateDto
{
    public class StateResponse: IStateResponse
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public State State { get; set; } = new State();
    }
}
