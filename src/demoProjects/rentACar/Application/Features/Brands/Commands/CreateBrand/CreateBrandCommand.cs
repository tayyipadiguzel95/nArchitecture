using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommand : IRequest<CreatedBrandDto>
{
    public string Name { get; set; }
}

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    private readonly BrandBusinessRules _brandBusinessRules;
    
    public CreateBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository, BrandBusinessRules brandBusinessRules)
    {
        _mapper = mapper;
        _brandRepository = brandRepository;
        _brandBusinessRules = brandBusinessRules;
    }
    
    public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);
        
        var mappedBrandEntity = _mapper.Map<Brand>(request);
        var createdBrandEntity = await _brandRepository.AddAsync(mappedBrandEntity);
        var createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrandEntity);
        
        return createdBrandDto;
    }
}