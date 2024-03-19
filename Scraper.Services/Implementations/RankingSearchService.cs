using AutoMapper;
using Microsoft.Data.SqlClient;
using Scraper.Core.NewFolder;
using Scraper.Data.Interfaces;
using Scraper.Services.Dtos;
using Scraper.Services.Dtos.ErrorDtos;
using Scraper.Services.Requests;
using Scraper.Services.SearchEngines;
using Scraper.Services.Services;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace Scraper.Services.Implementations
{
    public class RankingSearchService : IRankingSearchService
    {
        private readonly ISearchEngineRepository _searchEngineRepository;
        private readonly IMapper _mapper;
        public Func<string, SearchEngineBase> _searchEngineProvider;

        public RankingSearchService(ISearchEngineRepository searchEngineRepository, IMapper mapper, Func<string, SearchEngineBase> searchEngineProvider)
        {
            _searchEngineRepository = searchEngineRepository;
            _mapper = mapper;
            _searchEngineProvider = searchEngineProvider;
        }

        public async Task<GetResponseDto<SearchRankingDto>> GetSearchEngineRankings(GetSearchRankingRequest request)
        {
            var response = new GetResponseDto<SearchRankingDto>();
            try
            {
                var searchEngineResponse = await GetSearchEngineById(request.Id);
                var searchToUse = searchEngineResponse.Data;
                var searchUrl = searchToUse.Url.Replace("(amount)", HttpUtility.UrlEncode(request.PageSize.ToString()))
                                               .Replace("(searchToFind)", request.SearchText);

                //var client = _httpClient.CreateClient();
                var client = _searchEngineProvider(searchToUse.SearchEngineName).HandleCustomClientHeader();

                using var clientResponse = await client.GetAsync(searchUrl);

                var r = searchToUse.Regex;

                if (clientResponse.IsSuccessStatusCode)
                {
                    string html = await clientResponse.Content.ReadAsStringAsync();
                    var ranking = new SearchRankingDto
                    {
                        SearchText = request.SearchText,
                        SearchUrl = searchUrl
                    };

                    //filter out the urls
                    var matches = Regex.Matches(HttpUtility.HtmlDecode(html), r, RegexOptions.IgnoreCase);

                    var urls = matches.Select(x => x.Value).ToList();

                    // retrieve matching urls
                    Dictionary<string, List<int>> urlMap = [];

                    // get accurate ranking of duplicate ranking i.e if www.infotrack.co.uk/about is in position 5 and 9
                    int index = 0;
                    foreach (string url in urls)
                    {
                        if (url.Contains("www.infotrack.co.uk", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!urlMap.TryGetValue(url, out List<int>? value))
                            {
                                value = [];
                                urlMap[url] = value;
                            }

                            value.Add(index + 1);

                        }
                        index++;
                    }


                    // get rankings
                    ranking.Rankings = [.. urlMap.Values.SelectMany(x => x).OrderBy(index => index)];


                    if (ranking.Rankings.Count == 0)
                    {
                        ranking.Rankings.Add(0);
                    }

                    response.Data = ranking;

                    return response;
                }
                else
                {
                    throw new Exception($"Failed to scrape {searchUrl}");
                }
            }
            catch (SqlException e)
            {
                response.Error = new ErrorDto
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = e.Message,
                    Errors = new[] { new ErrorDetailDto { Type = ExceptionTypes.DataException.ToString() } }
                };

                return response;
            }
            catch (Exception e)
            {
                response.Error = new ErrorDto
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = e.Message
                };

                return response;
            }
        }

        public async Task<GetResponseDto<List<SearchEngineDto>>> GetSearchEngines()
        {
            var response = new GetResponseDto<List<SearchEngineDto>>();

            try
            {
                var searchEngines = await _searchEngineRepository.ReadSearchEngines();

                if (!searchEngines.Any())
                {
                    throw new NullReferenceException("No search engines found");
                }

                var mappedDto = _mapper.Map<List<SearchEngineDto>>(searchEngines);
                response.Data = mappedDto;
                return response;
            }
            catch (NullReferenceException e)
            {
                response.Error = new ErrorDto
                {
                    Code = (int)HttpStatusCode.NotFound,
                    Message = e.Message
                };

                return response;
            }
            catch (SqlException e)
            {
                response.Error = new ErrorDto
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = e.Message,
                    Errors = new[] { new ErrorDetailDto { Type = ExceptionTypes.DataException.ToString() } }
                };

                return response;
            }
            catch (Exception e)
            {
                response.Error = new ErrorDto
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = e.Message
                };

                return response;
            }
        }

        public async Task<GetResponseDto<SearchEngineDto>> GetSearchEngineById(Guid searchEngineId)
        {
            var response = new GetResponseDto<SearchEngineDto>();
            try
            {
                var searchEngines = await _searchEngineRepository.ReadSearchEngines();

                var selectedSearchEngine = searchEngines.FirstOrDefault(x => x.Id == searchEngineId);

                var mappedDto = _mapper.Map<SearchEngineDto>(selectedSearchEngine);
                response.Data = mappedDto;

                return response!;
            }
            catch (NullReferenceException e)
            {
                response.Error = new ErrorDto
                {
                    Code = (int)HttpStatusCode.NotFound,
                    Message = e.Message
                };

                return response;
            }
            catch (SqlException e)
            {
                response.Error = new ErrorDto
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = e.Message,
                    Errors = new[] { new ErrorDetailDto { Type = ExceptionTypes.DataException.ToString() } }
                };

                return response;
            }
            catch (Exception e)
            {
                response.Error = new ErrorDto
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = e.Message
                };

                return response;
            }
        }
    }
}
