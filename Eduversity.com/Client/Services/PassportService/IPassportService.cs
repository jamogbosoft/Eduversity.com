using Microsoft.AspNetCore.Components.Forms;

namespace Eduversity.com.Client.Services.PassportService
{
    public interface IPassportService
    {
        string DefaultImage { get; set; }
        Task<string> UploadPassport(InputFileChangeEventArgs e);
    }
}
