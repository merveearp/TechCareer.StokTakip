using Core.CrossCutingConcerns.Exceptions;
using DataAccess.Repositories.Abstract;
using Service.BusinessRules.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BusinessRules;

public class ProductRules : IProductRules

{
    private readonly IProductRepository _productRepository;

    public ProductRules(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void ProductNameMustBeUnique(string productName)
    {
        var product = _productRepository.GetByFilter(x=>x.Name==productName);
        if(product is not null)
        {
            throw new BusinessException("Ürün İsmi benzersiz olmalı");
        }
    }

    public void ProductIsPresent(Guid id)
    {
        var product = _productRepository.GetById(id);
        if(product is null)
        {
            throw new BusinessException($"Id si : {id} olan ürün bulunamadı");
        }
    }
}
