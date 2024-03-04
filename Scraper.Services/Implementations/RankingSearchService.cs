using AutoMapper;
using Microsoft.Data.SqlClient;
using Scraper.Core.NewFolder;
using Scraper.Data.Interfaces;
using Scraper.Services.Dtos;
using Scraper.Services.Dtos.ErrorDtos;
using Scraper.Services.Requests;
using Scraper.Services.Services;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace Scraper.Services.Implementations
{
    public class RankingSearchService : IRankingSearchService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ISearchEngineRepository _searchEngineRepository;
        private readonly IRankingSearchHistoryService _searchHistoryRepository;
        private readonly IMapper _mapper;

        public RankingSearchService(IHttpClientFactory httpClient, ISearchEngineRepository searchEngineRepository, IRankingSearchHistoryService searchHistoryRepository, IMapper mapper)
        {
            _httpClient = httpClient;
            _searchEngineRepository = searchEngineRepository;
            _searchHistoryRepository = searchHistoryRepository;
            _mapper = mapper;
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

                var client = _httpClient.CreateClient();
                client.BaseAddress = new Uri("https://www.google.co.uk");

                if (searchToUse.SearchEngineName == "Bing")
                {
                    client.DefaultRequestHeaders.Add("User-Agent",
                       "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36");
                }


                if (searchToUse.SearchEngineName == "Google")
                {
                    var cookieContainer = new CookieContainer();
                    using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
                    cookieContainer.Add(client.BaseAddress!, new Cookie("CONSENT", "PENDING+986"));
                }

                using var clientResponse = await client.GetAsync(searchUrl);

                string r = searchToUse.Regex;

                if (clientResponse.IsSuccessStatusCode)
                {
                    string html = await clientResponse.Content.ReadAsStringAsync();
                    var ranking = new SearchRankingDto
                    {
                        SearchText = request.SearchText
                    };
                    var matches = Regex.Matches(HttpUtility.HtmlDecode(html), r, RegexOptions.IgnoreCase);

                    var urls = matches.Select(x => x.Value).ToList();

                    foreach (var i in urls)
                    {
                        if (i.Contains("www.infotrack.co.uk", StringComparison.OrdinalIgnoreCase))
                        {
                            ranking.Rankings!.Add(urls.IndexOf(i) + 1);
                        }
                    }

                    if (ranking.Rankings.Count == 0)
                    {
                        ranking.Rankings.Add(0);
                    }

                    var rankingConcat = string.Join(",", ranking.Rankings!.Select(x => x.ToString()));

                    var createResponse = await _searchHistoryRepository.CreateSearchHistory(new StoreSearchHistoryRequest
                    {
                        SearchText = request.SearchText,
                        SearchEngineName = searchToUse.SearchEngineName,
                        URL = searchUrl,
                        Rankings = rankingConcat,
                        SearchEngineId = searchToUse.Id
                    });

                    if (createResponse.Data != null)
                    {
                        response.Data = ranking;
                    }
                    else
                    {
                        response.Error = createResponse.Error;
                    }

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
