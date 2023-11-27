using Core.Shared;
using DataAccess.Repositories.Abstract;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using Models.Entities;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete; 

internal class ProductService : IProductService
{

    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Response<ProductResponseDto> Add(ProductAddRequest request)
    {
       Product product = ProductAddRequest.ConvertToEntity(request);
        _productRepository.Add(product);

  var data = ProductResponseDto.ConvertToResponse(product);

        return new Response<ProductResponseDto>()
        {
            Data = data,
            Message = "Ürün Eklendi",
            StatusCode = System.Net.HttpStatusCode.Created
        };
    }

    public Response<ProductResponseDto> Delete(Guid id)
    {
        var product = _productRepository.GetById(id);


        _productRepository.Delete(product);
        var data = ProductResponseDto.ConvertToResponse(product);
        return new Response<ProductResponseDto>()
        {
            Data = data,
            Message = "Ürün Silindi",
            StatusCode = System.Net.HttpStatusCode.OK

        };
    }

    public Response<List<ProductResponseDto>> GetAll()
    {
        var products = _productRepository.GetAll();
        var response = products.Select(x=> ProductResponseDto.ConvertToResponse(x)).ToList();

        return new Response<List<ProductResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<ProductResponseDto>> GetAllByPriceRange(decimal min, decimal max)
    {
        var products = _productRepository.GetAll(x => x.Price <= max && x.Price >= min);
        var response = products.Select(x => ProductResponseDto.ConvertToResponse(x)).ToList();

        return new Response<List<ProductResponseDto>>()
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };

    }

    public Response<List<ProductResponseDto>> GetAllByPriveRange(decimal min, decimal max)
    {
        throw new NotImplementedException();
    }

    public Response<List<ProductDetailDto>> GetAllDetails()
    {
        var details = _productRepository.GetAllProductDetails();
        return new Response<List<ProductDetailDto>>()
        {
            Data = details,
            StatusCode = System.Net.HttpStatusCode.OK

        };
    }

    public Response<List<ProductDetailDto>> GetAllDetailsByCategoryId(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Response<ProductResponseDto> GetByDetail(Guid id)
    {
        throw new NotImplementedException();
    }

    public Response<List<ProductDetailDto>> GetByDetailId(Guid id)
    {
        throw new NotImplementedException();
    }

    public Response<ProductResponseDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Response<ProductResponseDto> Update(ProductUpdateRequest request)
    {
        throw new NotImplementedException();
    }
}
