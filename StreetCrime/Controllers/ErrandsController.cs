using AutoMapper;
using Errands.Configurations;
using DAL.IRepository;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Data;
using Microsoft.AspNetCore.Authorization;

namespace Errands.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ErrandsController : ControllerBase
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ErrandsController> _logger;
    private readonly IMapper _mapper;
    private readonly CustomResponse _customResponse = new();

    public ErrandsController(IUnitOfWork unitOfWork, ILogger<ErrandsController> logger, IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _logger = logger;
      _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetErrands()
    {
      try
      {
        var errands = await _unitOfWork.Errands.GetAll(x => x.IsActive == true);

        var result = _mapper.Map<List<ErrandDto>>(errands);

        return Ok(_customResponse.SendSuccessResponse(result));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Something went wrong in the {nameof(GetErrands)}");
        return Problem("Internal server error. Please try again later");
      }
    }

    [Authorize]
    [HttpGet("{id:int}", Name = "GetErrand")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetErrand(int id)
    {
      try
      {
        var errand = await _unitOfWork.Errands.Get(x => x.Id == id && x.IsActive == true);

        if (errand == null)
        {
          return BadRequest(_customResponse.SendErrorResponse("Errand not found"));
        }

        var result = _mapper.Map<ErrandDto>(errand);

        return Ok(result);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Something went wrong in the {nameof(GetErrand)}");
        return StatusCode(500, "Internal server error. Please try again later");
      }
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateErrand([FromBody] CreateErrandDto errandDto)
    {
      if (!ModelState.IsValid)
      {
        _logger.LogError($"Invalid POST attempt in {nameof(CreateErrand)}");
        return BadRequest(ModelState);
      }
      try
      {
        var errand = _mapper.Map<Errand>(errandDto);
        errand.IsActive = true;
        errand.CreatedOn = DateTime.Now;
        await _unitOfWork.Errands.Insert(errand);
        await _unitOfWork.Save();

        return CreatedAtRoute("GetErrand", new { id = errand.Id }, errand);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Something went wrong in the {nameof(CreateErrand)}");
        return StatusCode(500, "Internal server error. Please try again later");
      }
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateErrand(int id, [FromBody] UpdateErrandDto errandDto)
    {
      if (!ModelState.IsValid || id < 1)
      {
        _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateErrand)}");
        return BadRequest(ModelState);
      }
      try
      {
        var errand = await _unitOfWork.Errands.Get(q => q.Id == id);
        if (errand == null)
        {
          _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateErrand)}");
          return BadRequest("Submitted data is invalid");
        }

        _mapper.Map(errandDto, errand);
        _unitOfWork.Errands.Update(errand);
        await _unitOfWork.Save();

        return NoContent();
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateErrand)}");
        return StatusCode(500, "Internal server error. Please try again later");
      }
    }
  }
}
