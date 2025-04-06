using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using stock.management.API.Controllers;
using stock.management.BusinessAccess.Interfaces;
using stock.management.BusinessAccess.Services;
using stock.management.DataAccess.DataModels;
using stock.management.DataAccess;
using Microsoft.Extensions.Logging;

 
namespace stock.management.Tests;

[TestClass]
public sealed class ProductControllerTest
{
    private ProductController _controller;
    private AppDbContext _context;
    private Mock<ILogger<ProductController>> _mockLogger;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new AppDbContext(options);
        var productService = new ProductService(_context);

        _mockLogger = new Mock<ILogger<ProductController>>();
        _controller = new ProductController(productService, _mockLogger.Object);
        SeedDatabase();
    }

    private void SeedDatabase()
    {
        _context.ProductDetails.AddRange(
            new ProductDetail { ProductId = 1, ProductName = "Product1", Quantity = 10, Priceperunit = 50 },
            new ProductDetail { ProductId = 2, ProductName = "Product2", Quantity = 20, Priceperunit = 50 }
        );
        _context.SaveChanges();
    }

    [TestMethod]
    public async Task PostProduct_ReturnsOkResult_WithCreatedProduct()
    {
        // Arrange
        var product = new ProductDetail { ProductId = 212556, ProductName = "Product2", Quantity = 20, Priceperunit = 50 };
        var mockProductService = new Mock<IProductService>();
        mockProductService.Setup(service => service.CreateProductAsync(It.IsAny<ProductDetail>()))
                          .ReturnsAsync(product);

        var controller = new ProductController(mockProductService.Object, _mockLogger.Object);

        // Act
        var result = await controller.PostProduct(product);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.IsInstanceOfType(okResult.Value, typeof(ProductDetail));
        Assert.AreEqual(product, okResult.Value);
    }

    [TestMethod]
    public async Task GetProducts_ReturnsOkResult_WithListOfProducts()
    {
        // Act
        var result = await _controller.GetProducts();

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var returnProducts = (List<ProductDetail>)((OkObjectResult)result).Value;
        Assert.AreEqual(2, returnProducts.Count);
    }

    [TestMethod]
    public async Task GetProduct_ReturnsOkResult_WithProduct()
    {
        // Act
        var result = await _controller.GetProduct(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var returnProduct = (ProductDetail)((OkObjectResult)result).Value;
        Assert.AreEqual(1, returnProduct.ProductId);
    }

    [TestMethod]
    public async Task UpdateProduct_ReturnsOkResult_WithUpdatedProduct()
    {
        // Arrange
        var product = new ProductDetail { ProductId = 1, ProductName = "Updated Product", Priceperunit = 20.0m, Quantity = 50 };
        var mockProductService = new Mock<IProductService>();
        mockProductService.Setup(service => service.UpdateProductAsync(It.IsAny<int>(), It.IsAny<ProductDetail>()))
                          .ReturnsAsync(product);

        var controller = new ProductController(mockProductService.Object, _mockLogger.Object);

        // Act
        var result = await controller.UpdateProduct(product.ProductId, product);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.IsInstanceOfType(okResult.Value, typeof(ProductDetail));
        Assert.AreEqual(product, okResult.Value);
    }

    [TestMethod]
    public async Task DeleteProduct_ReturnsOkResult()
    {
        // Act
        var result = await _controller.DeleteProduct(2);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task DecrementStock_ReturnsOkResult_WithUpdatedProduct()
    {
        // Act
        var result = await _controller.DecrementStock(1, 5);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var returnProduct = (ProductDetail)((OkObjectResult)result).Value;
        Assert.AreEqual(5, returnProduct.Quantity);
    }

    [TestMethod]
    public async Task AddToStock_ReturnsOkResult_WithUpdatedProduct()
    {
        // Act
        var result = await _controller.AddToStock(2, 5);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var returnProduct = (ProductDetail)((OkObjectResult)result).Value;
        Assert.AreEqual(25, returnProduct.Quantity);  
    }
}
 