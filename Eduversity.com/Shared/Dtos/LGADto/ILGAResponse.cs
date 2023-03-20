namespace Eduversity.com.Shared.Dtos.LGADto
{
    public  interface ILGAResponse
    {
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
    }
}
