using System.ComponentModel.DataAnnotations;

namespace hospital_api.Modules;

public class BlackListTokens
{
    [Key]
    public string doctorId { get; set; }
    public string token { get; set; }

    public BlackListTokens(string doctorId, string token)
    {
        this.doctorId = doctorId;
        this.token = token;
    }
}