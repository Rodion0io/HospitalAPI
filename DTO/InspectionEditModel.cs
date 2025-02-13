using System.ComponentModel.DataAnnotations;
using hospital_api.Enums;

namespace hospital_api.Modules;

public class InspectionEditModel
{
    [MaxLength(5000)]
    public string anamnesis { get; set; }
    [Required]
    [MinLength(1)]
    [MaxLength(5000)]
    public string complaints { get; set; }
    [Required]
    [MinLength(1)]
    [MaxLength(5000)]
    public string treatment { get; set; }
    [Required]
    public Conclusion conclusion { get; set; }
    public DateTime nextVisitDate { get; set; }
    public DateTime deathDate { get; set; }
    [Required]
    [MinLength(1)]
    public DiagnosisCreateModel[] diagnosis { get; set; }
}