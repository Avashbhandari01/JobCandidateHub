using JobCandidateHub.Application.DTOs.Candidate;
using JobCandidateHub.Application.Interfaces.Repositories;
using JobCandidateHub.Domain.Entities.Models;
using JobCandidateHub.Infrastructure.Implementation.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Linq.Expressions;

namespace JobCandidateHub.Tests.Core.Services
{
    public class CandidateServiceTest
    {
        [Fact]
        public async Task Run_AddUpdateCandidate_CacheMiss_UpdateSuccess_Returns_true()
        {
            //Arrange
            CandidateDto model = new CandidateDto()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Comment = ""
            };

            var genericRepositoryMock = new Mock<IGenericRepository>();

            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

            Candidate candidateInfo = new Candidate() { };

            object dummy = candidateInfo;

            genericRepositoryMock.Setup(m => m.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Candidate, bool>>>())).ReturnsAsync(candidateInfo);

            var candidateService = new CandidateService(genericRepositoryMock.Object, cache);

            //Act
            var response = await candidateService.InsertOrUpdateCandidate(model);

            //Assert
            Assert.True(response);


        }

        [Fact]
        public async Task Run_AddUpdateCandidate_CacheMiss_UpdateFailed_Returns_false()
        {
            //Arrange
            CandidateDto model = new CandidateDto()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Comment = ""
            };

            var genericRepositoryMock = new Mock<IGenericRepository>();

            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

            Candidate candidate = new Candidate() { };

            object dummy = candidate;

            genericRepositoryMock.Setup(m => m.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Candidate, bool>>>())).ReturnsAsync(candidate);

            Exception ex = new Exception();
            genericRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<Candidate>())).ThrowsAsync(ex);

            var candidateService = new CandidateService(genericRepositoryMock.Object, cache);

            //Act
            var response = await candidateService.InsertOrUpdateCandidate(model);

            //Assert
            Assert.False(response);
        }
    }
}
