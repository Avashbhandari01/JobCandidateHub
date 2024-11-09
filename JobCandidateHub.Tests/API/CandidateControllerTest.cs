using JobCandidateHub.API.Controllers;
using JobCandidateHub.Application.DTOs.Base;
using JobCandidateHub.Application.DTOs.Candidate;
using JobCandidateHub.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace JobCandidateHub.Tests.API
{
    public class CandidateControllerTest
    {
        [Fact]
        public async Task Run_AddUpdateCandidate_When_Success_Returns_Ok()
        {
            //Arrange
            CandidateDto model = new CandidateDto()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Comment = ""
            };

            var candidateServiceMock = new Mock<ICandidateService>();

            candidateServiceMock.Setup(m => m.InsertOrUpdateCandidate(It.IsAny<CandidateDto>())).ReturnsAsync(true);

            var candidateController = new CandidateController(candidateServiceMock.Object);

            var expectedResult = new ResponseDto<CandidateDto>
            {
                StatusMessage = "Success",
                Message = "Successfully Added/Updated",
                StatusCode = HttpStatusCode.OK,
                ResponseData = model
            };

            //Act 
            var response = await candidateController.InsertOrUpdateCandidate(model);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var responsee = Assert.IsType<ResponseDto<CandidateDto>>(okResult.Value);
            Assert.Equal("Success", responsee.StatusMessage);
            Assert.Equal("Successfully Added/Updated", responsee.Message);
            Assert.Equal(HttpStatusCode.OK, responsee.StatusCode);
            Assert.Equal(model, responsee.ResponseData);

        }

        [Fact]
        public async Task Run_AddUpdateCandidate_When_Fail_Returns_BadRequest()
        {
            //Arrange
            CandidateDto model = new CandidateDto()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Comment = ""
            };

            var candidateServiceMock = new Mock<ICandidateService>();

            candidateServiceMock.Setup(m => m.InsertOrUpdateCandidate(It.IsAny<CandidateDto>())).ReturnsAsync(false);

            var candidateController = new CandidateController(candidateServiceMock.Object);

            var expectedResult = new ResponseDto<object>
            {
                StatusMessage = "Error",
                Message = "Failed to add/update candidate",
                StatusCode = HttpStatusCode.InternalServerError,
                ResponseData = ""
            };

            //Act 
            var response = await candidateController.InsertOrUpdateCandidate(model);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            var responsee = Assert.IsType<ResponseDto<object>>(badRequestResult.Value);
            Assert.Equal("Error", responsee.StatusMessage);
            Assert.Equal("Failed to add/update candidate", responsee.Message);
            Assert.Equal(HttpStatusCode.InternalServerError, responsee.StatusCode);
            Assert.Equal("", responsee.ResponseData);
        }
    }
}