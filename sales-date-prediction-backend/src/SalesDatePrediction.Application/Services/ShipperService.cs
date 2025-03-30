

using AutoMapper;
using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Interfaces;

namespace SalesDatePrediction.Application.Services;

public class ShipperService : IShipperService
{
    private readonly IShipperRepository _repository;
    private readonly IMapper _mapper;

    public ShipperService(IShipperRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShipperDto>> GetAllAsync()
    {
        var shipper = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ShipperDto>>(shipper);
    }

}