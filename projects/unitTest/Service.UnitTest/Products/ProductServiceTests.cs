using Core.CrossCutingConcerns.Exceptions;
using DataAccess.Repositories.Abstract;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using Models.Entities;
using Moq;
using Service.BusinessRules.Abstract;
using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.UnitTest.Products;

public class ProductServiceTests
{
    private ProductService _service;
    private Mock<IProductRepository> _mockRepository;
    private Mock<IProductRules> _mockRules;
   
    private ProductAddRequest productAddRequest;
    private ProductUpdateRequest productUpdateRequest;
    private Product product;
    private ProductResponseDto productResponseDto;


    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IProductRepository>();
        _mockRules = new Mock<IProductRules>();
        _service = new ProductService(_mockRepository.Object,_mockRules.Object);
        productAddRequest = new ProductAddRequest(Name:"Test", Stock : 25 , Price:2500,CategoryId:1);
        productUpdateRequest = new ProductUpdateRequest(Id: new Guid(), Name: "Test", Stock: 25, Price: 2500, CategoryId: 1);
        product = new Product
        {
            Id = new Guid(),
            Name = "Test",
            CategoryId = 1,
            Price = 2500,
            Stock = 25,
            Category = new Category() { Id = 1, Name = "Teknoloji",Products = new List<Product>() { new Product() } }
        };


        productResponseDto = new ProductResponseDto(Id: new Guid(), Name: "Test", Stock: 25, Price: 2500, CategoryId: 1);
    }

    [Test]
    public void Add_WhenProductNameIsUnique_ReturnsOk()
    {
        //Arrange
        _mockRules.Setup(x => x.ProductNameMustBeUnique(productAddRequest.Name));
        _mockRepository.Setup(x => x.Add(product));

        //Act
        var result = _service.Add(productAddRequest);

        //Assert
        Assert.AreEqual(result.StatusCode,HttpStatusCode.Created);
        Assert.AreEqual(result.Data,productResponseDto);



    }
    [Test]
    public void Add_WhenProductNameIsNotUnique_ReturnsBadRequest()
    {
        //Arrange
        _mockRules.Setup(x=>x.ProductNameMustBeUnique(productAddRequest.Name))
            .Throws(new BusinessException("Ürün İsmi benzersiz olmalı"));

        //Act
        var result = _service.Add(productAddRequest) ;

        //Assert
        Assert.AreEqual(result.Message, "Ürün İsmi benzersiz olmalı");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);

    }




}
