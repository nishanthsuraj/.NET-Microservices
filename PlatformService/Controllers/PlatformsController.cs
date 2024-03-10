using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Interfaces;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        #region Private Fields
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PlatformsController(IPlatformRepository platformRepository, IMapper mapper)
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
        }
        #endregion

        #region HttpGet
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            IEnumerable<Platform> platforms = _platformRepository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            Platform platform = _platformRepository.GetPlatformById(id);
            if (platform != null)
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            else
                return NotFound();
        }
        #endregion

        #region HttpPost
        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Platform platformModel = _mapper.Map<Platform>(platformCreateDto);
            _platformRepository.CreatePlatform(platformModel);
            _platformRepository.SaveChanges();

            PlatformReadDto platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }
        #endregion
    }
}
