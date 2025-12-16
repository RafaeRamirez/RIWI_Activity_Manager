using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    /// <summary>
    /// Controller for managing organizations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(IOrganizationRepository repository, IMapper mapper, ILogger<OrganizationController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all organizations
        /// </summary>
        /// <returns>List of all organizations</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrganizationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<OrganizationDto>>> GetAll()
        {
            try
            {
                var organizations = await _repository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<OrganizationDto>>(organizations));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all organizations");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving organizations",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Get organization by ID
        /// </summary>
        /// <param name="id">Organization ID</param>
        /// <returns>Organization details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrganizationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrganizationDto>> GetById(long id)
        {
            try
            {
                var organization = await _repository.GetByIdAsync(id);
                if (organization == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Organization with ID {id} not found"
                    });
                }
                return Ok(_mapper.Map<OrganizationDto>(organization));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving organization {OrganizationId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving the organization",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Create a new organization
        /// </summary>
        /// <param name="dto">Organization creation data</param>
        /// <returns>Created organization</returns>
        [HttpPost]
        [ProducesResponseType(typeof(OrganizationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrganizationDto>> Create(CreateOrganizationDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse
                    {
                        StatusCode = 400,
                        Message = "Invalid request data",
                        Details = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
                    });
                }

                var organization = _mapper.Map<Organization>(dto);
                await _repository.AddAsync(organization);
                return CreatedAtAction(nameof(GetById), new { id = organization.OrgId }, _mapper.Map<OrganizationDto>(organization));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating organization");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while creating the organization",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Update an existing organization
        /// </summary>
        /// <param name="id">Organization ID</param>
        /// <param name="dto">Organization update data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(long id, UpdateOrganizationDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse
                    {
                        StatusCode = 400,
                        Message = "Invalid request data",
                        Details = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
                    });
                }

                var organization = await _repository.GetByIdAsync(id);
                if (organization == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Organization with ID {id} not found"
                    });
                }

                _mapper.Map(dto, organization);
                await _repository.UpdateAsync(organization);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating organization {OrganizationId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating the organization",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete an organization
        /// </summary>
        /// <param name="id">Organization ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var organization = await _repository.GetByIdAsync(id);
                if (organization == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Organization with ID {id} not found"
                    });
                }

                await _repository.DeleteAsync(organization);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting organization {OrganizationId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while deleting the organization",
                    Details = ex.Message
                });
            }
        }
    }
}
