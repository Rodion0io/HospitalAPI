using System.ComponentModel.Design;
using hospital_api.Modules;
using hospital_api.Services;
using Microsoft.AspNetCore.Mvc;
using hospital_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace hospital_api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class Consultation : Controller
{
    
    private readonly IConsultationService _consultationService;
    private readonly IJWTService _jwtService;
    private readonly IInputOptions _inputOpstions;

    public Consultation(IConsultationService consultationService, IJWTService jwtService,
        IInputOptions inputOptions)
    {
        _consultationService = consultationService;
        _jwtService = jwtService;
        _inputOpstions = inputOptions;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<InspectionPagedListModel>> GetListConsultation([FromQuery] bool grouped, [FromQuery] List<Guid> icdRoots, [FromQuery(Name = "pageNuber")] int pageNumber = 1,
        [FromQuery(Name = "pageSize")] int pageSize = 5)
    {
     
        var authHeader = HttpContext.Request.Headers["Authorization"];
        string token = authHeader.ToString().Split(" ")[1];
        Guid Id = Guid.Parse(_jwtService.DecodeToken(token).Claims.ToArray()[2].Value);
        
        var result = await _consultationService.GetListInspectionForConsultation(Id);

        if (grouped != null)
        {
            result = _inputOpstions.GetFilteringGroupInspection(result, grouped);
        }

        if (icdRoots.Count != 0)
        {
            result = await _inputOpstions.GetFilteringByParentCode(result, icdRoots);
        }
        
        int totalPages = (int)Math.Ceiling(result.Count / (double)pageSize);
        
        var items = result.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize).ToList();

        PageInfoModel pagination = new PageInfoModel
        {
            size = pageSize,
            current = pageNumber,
            count = totalPages
        };

        InspectionPagedListModel res = new InspectionPagedListModel
        {
            inspections = items.ToArray(),
            pagination = pagination
        };
        
        return Ok(res);
    }
    
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ConsultationModel>> GetConsultation([FromQuery] string id)
    {

        var result = await _consultationService.GetConcreteConsultation(Guid.Parse(id));
        return Ok(result);
    }

    [HttpPost("{id}/comment")]
    [Authorize]
    public async Task<IActionResult> AddCommentToConcreneteConsultation(Guid consultationId,
        [FromBody] CommentCreateModel model)
    {
        
        var authHeader = HttpContext.Request.Headers["Authorization"];
        string token = authHeader.ToString().Split(" ")[1];
        Guid Id = Guid.Parse(_jwtService.DecodeToken(token).Claims.ToArray()[2].Value);
        string name = _jwtService.DecodeToken(token).Claims.ToArray()[0].Value;
        
        await _consultationService.AddCommentConsultation(model, consultationId, Id, name);
        return Ok();
    }

    [HttpPut("comment/{id}")]
    [Authorize]
    public async Task<IActionResult> RedactComment(Guid id, [FromBody] InspectionCommentCreateModel model)
    {

        
        var authHeader = HttpContext.Request.Headers["Authorization"];
        string token = authHeader.ToString().Split(" ")[1];
        Guid Id = Guid.Parse(_jwtService.DecodeToken(token).Claims.ToArray()[2].Value);
        
        await _consultationService.UpdateComment(model, id, Id);
        return Ok();
    }
}