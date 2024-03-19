using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Scraper.Tests.AutoMapper;

namespace Scraper.Tests.ServiceTests
{
    public class TestBase
    {
        protected readonly IFixture _fixture;
        protected readonly IMapper _mapper;

        public TestBase()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _mapper = RankingMapperConfig.Configure();
            _fixture.Inject(_mapper);
        }
    }
}
