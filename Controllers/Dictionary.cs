using System.ComponentModel.Design;
using hospital_api.Modules;
using Microsoft.AspNetCore.Mvc;
using hospital_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace hospital_api.Controllers;


[Route("api/[controller]")]
public class Dictionary : Controller
{
    
    private readonly IDictionaryServic _dictionaryService;

    public Dictionary(IDictionaryServic dictionaryService)
    {
        _dictionaryService = dictionaryService;
    }
    
    [HttpGet("speciality")]
    public async Task<IActionResult> GetSpeciality(string name, int pageNumber, int pageSize)
    {
        
        //вот так нельзя вызывать. движок метода GetFullSpeciaityTable надо вынести в репозиторий
        var listSpeciality = await _dictionaryService.GetFullSpeciaityTable();

        // Это высчитывает значение count
        int totalPages = (int)Math.Ceiling(18 / (double)pageSize);
        
        
        //Почему не работает???
        // var items = listSpeciality.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        
        // Это уже сама пагинация
        var items = listSpeciality.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        
        var pagination = new PageInfoModel
        {
            size = pageSize,
            current = pageNumber,
            count = totalPages
        };

        var result = new SpecialtiesPagedListModel
        {
            specialties = items,
            pagintaion = pagination
        };

        return Ok(result);
    }

    [HttpGet("icd10")]
    public async Task<IActionResult> GetIcd10()

    {
        return Ok();
    }
}