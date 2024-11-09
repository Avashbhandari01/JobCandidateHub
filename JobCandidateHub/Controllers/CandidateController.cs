using JobCandidateHub.Application.DTOs.Base;
using JobCandidateHub.Application.DTOs.Candidate;
using JobCandidateHub.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobCandidateHub.API.Controllers
{
    [Route("api/candidate")]
    [ApiController]
    public class CandidateController(ICandidateService candidateService) : Controller
    {
        [HttpPost("add-update-candidate")]
        public async Task<IActionResult> InsertOrUpdateCandidate(CandidateDto candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<object>
                {
                    StatusMessage = "Error",
                    Message = "Invalid candidate data",
                    StatusCode = HttpStatusCode.BadRequest,
                    ResponseData = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var result = await candidateService.InsertOrUpdateCandidate(candidate);

                if (result)
                {
                    var response = new ResponseDto<CandidateDto>
                    {
                        StatusMessage = "Success",
                        Message = "Successfully Added/Updated",
                        StatusCode = HttpStatusCode.OK,
                        ResponseData = candidate
                    };

                    return Ok(response);
                }

                var errorResponse = new ResponseDto<object>
                {
                    StatusMessage = "Error",
                    Message = "Failed to add/update candidate",
                    StatusCode = HttpStatusCode.InternalServerError,
                    ResponseData = ""
                };

                return BadRequest(errorResponse);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
