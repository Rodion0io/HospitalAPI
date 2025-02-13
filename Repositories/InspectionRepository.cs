using hospital_api.Dates;
using hospital_api.Modules;
using hospital_api.Repositories.repositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace hospital_api.Repositories;

public class InspectionRepository : IInspectionRepository
{
    private readonly AccountsContext _context;

    public InspectionRepository(AccountsContext context)
    {
        _context = context;
    }

    public async Task<Inspection> GetInspection(Guid id)
    {
        var result = await _context.Inspections.FindAsync(id);

        if (result != null)
        {
            return result;
        }
        else
        {
            return null;
        }
    }

    public async Task<DiagnosisModel[]> GetDiagnosis(Guid inspectionId)
    {
        return (await _context.Diagnosis.Where(i => i.inspectionId == inspectionId)
            .Select(i => new DiagnosisModel
            {
                id = i.id,
                createTime = i.createTime,
                code = i.code,
                name = i.name,
                description = i.description,
                type = i.type
            }).ToListAsync()).ToArray();
    }

    public async Task<InspectionConsultationModel[]> GetInspectionConsultations(Guid inspectionId)
    {
        return (await _context.Consultations.Where(i => i.inspectionId == inspectionId)
            .Select(i => new InspectionConsultationModel
            {
                id = i.id,
                createTime = i.createTime,
                inspectionId = i.inspectionId,
                speciality = _context.Specialities.FirstOrDefault(s => s.id == i.specialityId),
                rootComment = _context.Comments.FirstOrDefault(c => c.consultationId == i.id)
            }).ToListAsync()).ToArray();
    }

    public async Task SaveChangesInspection(Inspection changesInspection)
    {
        _context.Inspections.Update(changesInspection);
        await _context.SaveChangesAsync();
    }

    public async Task ClearDiagnosis(Guid inspectionId)
    {
        var removeDiagnosis = await _context.Diagnosis.Where(i => i.inspectionId == inspectionId).ToListAsync();

        _context.Diagnosis.RemoveRange(removeDiagnosis);
        await _context.SaveChangesAsync();
    }

    public async Task<Inspection> GetInspectionByPrevId(Guid? prevId)
    {
        var result = await _context.Inspections.FirstOrDefaultAsync(i => i.previousInspectionId == prevId);

        return result;
    }

    // public async Task UpdateData(Inspection updatedModel)
    // {
    //     await _context.Inspections.
    // }
    //
    // public async Task UpdatedDiagnosis(Diagnosis updatedDiagnosis)
    // {
    //     
    // }
}