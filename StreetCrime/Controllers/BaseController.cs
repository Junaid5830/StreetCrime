using AutoMapper;
using DAL.IRepository;
using StreetCrime.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StreetCrime.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class BaseController : ControllerBase
  {
    protected readonly CustomResponse _customResponses = new CustomResponse();
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly ILogger<object> _logger;
    protected readonly IMapper _mapper;

    public BaseController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<object> logger)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
      _logger = logger;
    }
  }
}
