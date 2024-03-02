using AutoMapper;
using Microsoft.Data.SqlClient;
using Scraper.Core.NewFolder;
using Scraper.Data.Interfaces;
using Scraper.Services.Dtos;
using Scraper.Services.Dtos.ErrorDtos;
using Scraper.Services.Requests;
using Scraper.Services.Services;
using System.Net;

namespace Scraper.Services.Implementations
{
    public class RankingSearchHistoryService : IRankingSearchHistoryService
    {
        private readonly IRankingHistoryRepository _rankingHistoryRepository;
        private readonly IMapper _mapper;
        public RankingSearchHistoryService(IRankingHistoryRepository rankingHistoryRepository, IMapper mapper)
        {
            _rankingHistoryRepository = rankingHistoryRepository;
            _mapper = mapper;
        }

        public async Task<GetResponseDto<List<SearchHistoryDto>>> GetSearchHistory(GetSearchHistoryRequest request)
        {
            var response = new GetResponseDto<List<SearchHistoryDto>>();
            try
            {
                var data = await _rankingHistoryRepository.ReadSearchHistory();

                var filtered = data.Where(x => (!request.Id.HasValue || x.Id == request.Id) &&
                (string.IsNullOrEmpty(request.KeyWords) || x.SearchText.Contains(request.KeyWords)) &&
                (string.IsNullOrEmpty(request.Ranking) || x.Rankings.Contains(request.Ranking)) &&
                (!request.SearchDate.HasValue || (x.SearchDate >= request.SearchDate && x.SearchDate <= request.SearchEndDate)));

                var mappedDto = _mapper.Map<List<SearchHistoryDto>>(filtered.ToList());

                response.Data = mappedDto;

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

        public async Task<GetResponseDto<CreatedSearchHistoryDto>> CreateSearchHistory(StoreSearchHistoryRequest request)
        {
            var response = new GetResponseDto<CreatedSearchHistoryDto>();

            try
            {
                await _rankingHistoryRepository.CreateSearch(request.SearchText, request.URL, request.Rankings, request.SearchEngineId);

                response.Data = new CreatedSearchHistoryDto
                {
                    Rankings = request.Rankings,
                    URL = request.URL,
                    SearchText = request.SearchText,
                    SearchEngineName = request.SearchEngineName
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
