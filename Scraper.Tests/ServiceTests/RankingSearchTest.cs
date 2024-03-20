using AutoFixture;
using Moq;
using Scraper.Data.Interfaces;
using Scraper.Services.Implementations;
using Scraper.Services.Requests;
using Scraper.Tests.MockData;

namespace Scraper.Tests.ServiceTests
{
    public class RankingSearchTest : TestBase
    {
        private readonly RankingSearchService _rankingSearchService;
        private readonly Mock<ISearchEngineRepository> _mockSearchRepository;

        public RankingSearchTest()
        {
            _mockSearchRepository = _fixture.Freeze<Mock<ISearchEngineRepository>>();
            _rankingSearchService = _fixture.Create<RankingSearchService>();
        }

        [Fact]
        public async Task Ranking_EnsureSearchListIsReturned()
        {
            var expectedData = SearchEngineMock.SearchEngines;

            _mockSearchRepository.Setup(repo => repo.GetAll()).ReturnsAsync(expectedData);

            // Act
            var response = await _rankingSearchService.GetSearchEngines();
            var actualData = response.Data;

            Assert.NotEmpty(actualData);
            Assert.Equal(expectedData.Count, actualData.Count);
            Assert.Null(response.Error);
        }

        [Fact]
        public async Task Ranking_EnsureSearchEngineRankingIsRetrieved()
        {
            var expectedData = SearchEngineMock.SearchEngines.First();

            _mockSearchRepository.Setup(repo => repo.ReadSearchById(It.IsAny<Guid>())).ReturnsAsync(expectedData);

            var req = new GetSearchRankingRequest { Id = Guid.Parse("84008369-7A16-4E19-9D79-2BD9332D98C5"), SearchText = "infotrack", PageSize = 100 };

            // Act
            var response = await _rankingSearchService.GetSearchEngineRankings(req);
            var actualData = response.Data;

            _mockSearchRepository.Verify(repo => repo.ReadSearchById(It.IsAny<Guid>()), Times.Once);
            Assert.NotNull(actualData);
            Assert.Contains("https://www.bing.com", actualData.SearchUrl);
            Assert.Null(response.Error);
        }

        [Fact]
        public async Task Ranking_EnsureSearchEngineRankingForJibberishRetrievesZero()
        {
            var expectedData = SearchEngineMock.SearchEngines.First();

            _mockSearchRepository.Setup(repo => repo.ReadSearchById(It.IsAny<Guid>())).ReturnsAsync(expectedData);

            var req = new GetSearchRankingRequest { Id = Guid.Parse("84008369-7A16-4E19-9D79-2BD9332D98C5"), SearchText = "dtfsdjhr", PageSize = 100 };

            // Act
            var response = await _rankingSearchService.GetSearchEngineRankings(req);
            var actualData = response.Data;

            _mockSearchRepository.Verify(repo => repo.ReadSearchById(It.IsAny<Guid>()), Times.Once);
            Assert.NotNull(actualData);
            Assert.Single(actualData.Rankings);
            Assert.StrictEqual(0, actualData.Rankings.Select(x => x).First());
            Assert.Contains("https://www.bing.com", actualData.SearchUrl);
            Assert.Null(response.Error);
        }
    }
}
