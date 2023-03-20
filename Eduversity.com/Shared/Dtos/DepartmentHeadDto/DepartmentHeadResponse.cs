namespace Eduversity.com.Shared.Dtos.DepartmentHeadDto
{
    public  class DepartmentHeadResponse
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public int LecturerId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
    }
}
