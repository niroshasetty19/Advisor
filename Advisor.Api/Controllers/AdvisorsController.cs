using AdvisorApp.Application.DTOs;
using AdvisorApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdvisorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorsController : ControllerBase
    {
        private readonly IAdvisorService _advisorService;

        public AdvisorsController(IAdvisorService advisorService)
        {
            _advisorService = advisorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var advisor = await _advisorService.GetByIdAsync(id);
                return Ok(advisor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var advisors = await _advisorService.GetAllAsync();
                return Ok(advisors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvisorDto createAdvisorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var advisor = await _advisorService.CreateAsync(createAdvisorDto);
                return CreatedAtAction(nameof(GetById), new { id = advisor.Id }, advisor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AdvisorDto advisorDto)
        {
            if (id != advisorDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _advisorService.UpdateAsync(advisorDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _advisorService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
