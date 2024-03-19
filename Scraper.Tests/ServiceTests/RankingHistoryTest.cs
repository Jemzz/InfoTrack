using AutoFixture;
using Moq;
using Scraper.Data.Entities;
using Scraper.Data.Interfaces;
using Scraper.Services.Implementations;
using Scraper.Services.Requests;
using Scraper.Tests.MockData;

namespace Scraper.Tests.ServiceTests
{
    public class RankingHistoryTest : TestBase
    {
        private readonly RankingSearchService _rankingSearchService;
        private readonly RankingSearchHistoryService _rankingSearchHistoryService;
        private readonly Mock<ISearchEngineRepository> _mockSearchRepository;
        private readonly Mock<IRankingHistoryRepository> _mockSearchHistoryRepository;

        public RankingHistoryTest()
        {
            _mockSearchRepository = _fixture.Freeze<Mock<ISearchEngineRepository>>();
            _mockSearchHistoryRepository = _fixture.Freeze<Mock<IRankingHistoryRepository>>();
            _rankingSearchService = _fixture.Create<RankingSearchService>();
            _rankingSearchHistoryService = _fixture.Create<RankingSearchHistoryService>();
        }

        [Fact]
        public async Task Ranking_EnsureServiceReturnsRankingSearchHistory()
        {
            var expectedData = SearchHistoryMock.SearchHistory;

            _mockSearchHistoryRepository.Setup(repo => repo.ReadSearchHistory()).ReturnsAsync(expectedData);

            // Act
            var response = await _rankingSearchHistoryService.GetSearchHistory(new Services.Requests.GetSearchHistoryRequest());
            var actualData = response.Data;

            Assert.NotEmpty(actualData);
            Assert.Equal(expectedData.Count, actualData.Count);
            Assert.Null(response.Error);
        }

        [Fact]
        public async Task Ranking_EnsureServiceReturnsFilteredRankingSearchHistory()
        {
            var expectedData = SearchHistoryMock.SearchHistory;

            _mockSearchHistoryRepository.Setup(repo => repo.ReadSearchHistory()).ReturnsAsync(expectedData);

            // Act
            var filterReq = new GetSearchHistoryRequest
            {
                KeyWords = "conveyancing",
                Id = Guid.Parse("0FEFF982-E9AA-4403-B6B8-9A6FD5B01B6D")
            };

            var response = await _rankingSearchHistoryService.GetSearchHistory(filterReq);
            var actualData = response.Data;

            var isTrue = actualData.TrueForAll(item => item.SearchText.Contains("conveyancing"));

            Assert.NotEmpty(actualData);
            Assert.True(isTrue);
            Assert.StrictEqual(2, actualData.Count);
            Assert.Null(response.Error);
        }

        [Fact]
        public async Task Ranking_EnsureSearchEngineRankingHistoryIsStored()
        {
            var expectedData = SearchHistoryMock.SearchHistory.First();

            _mockSearchHistoryRepository.Setup(repo => repo.CreateSearch(It.IsAny<CreateSearch>())).ReturnsAsync(expectedData);

            // Act

            var request = new StoreSearchHistoryRequest
            {
                SearchText = "infotrack",
                URL = "https://www.bing.com/search?count=100&q=infotrack",
                Rankings = new List<int> { 1, 2, 3, 5, 6, 8, 9, 10 },
                SearchEngineId = Guid.Parse("84008369-7A16-4E19-9D79-2BD9332D98C5"),
            };

            var response = await _rankingSearchHistoryService.CreateSearchHistory(request);
            var actualData = response.Data;

            Assert.NotNull(actualData);
            Assert.Equal(expectedData.Id, actualData.Id);
            _mockSearchHistoryRepository.Verify(repo => repo.CreateSearch(It.IsAny<CreateSearch>()), Times.Once);
        }
    }
}
