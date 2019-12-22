using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Sirena.Books.Api.Controllers;
using Sirena.Books.Api.Models;
using Sirena.Books.Domain.Entities;
using Sirena.Books.Domain.Interfaces;

namespace Sirena.Books.Tests
{
    public class BooksControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Success()
        {
            Mock<IBooksService> serviceMock = new Mock<IBooksService>();
            serviceMock.Setup(x => x.GetByParamsAsync(
                It.IsAny<bool?>(), It.IsAny<int[]>(), It.IsAny<decimal?>(),
                It.IsAny<decimal?>(), It.IsAny<string>(), It.IsAny<string?>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(new Book[]
            {
                new Book(), new Book(),
            });
            var loggerMock = new Mock<ILogger<BooksController>>();
            BooksController controller = new BooksController(serviceMock.Object, loggerMock.Object);
            var filterModel = new FilterModel
            {
                Author = "x",
                Name = "y",
                Exists = null,
                MaxCost = null,
                MinCost = null,
                Types = null
            };

            var getResult = (await controller.Get(filterModel, CancellationToken.None)) as OkObjectResult;
            Assert.IsNotNull(getResult);
            var booksArray = getResult.Value as BookModel[];
            Assert.IsNotNull(booksArray);
            Assert.AreEqual(2, booksArray.Length);
        }

        [Test]
        public async Task ServerError()
        { 
            Mock<IBooksService> serviceMock = new Mock<IBooksService>();
            serviceMock.Setup(x => x.GetByParamsAsync(
                It.IsAny<bool?>(), It.IsAny<int[]>(), It.IsAny<decimal?>(),
                It.IsAny<decimal?>(), It.IsAny<string>(), It.IsAny<string?>(),
                It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());
            var loggerMock = new Mock<ILogger<BooksController>>();

            BooksController controller = new BooksController(serviceMock.Object, loggerMock.Object);
            var filterModel = new FilterModel
            {
                Author = "x",
                Name = "y",
                Exists = null,
                MaxCost = null,
                MinCost = null,
                Types = null
            };

            var getResult = (await controller.Get(filterModel, CancellationToken.None)) as OkObjectResult;
            Assert.IsNotNull(getResult);
            var errorResult = getResult.Value as ProblemDetails;
            Assert.IsNotNull(errorResult);
        }
        [Test]
        public async Task BadRequest()
        {
            Mock<IBooksService> serviceMock = new Mock<IBooksService>();
            var loggerMock = new Mock<ILogger<BooksController>>();
            BooksController controller = new BooksController(serviceMock.Object, loggerMock.Object);

            var getResult = (await controller.Get(null, CancellationToken.None)) as OkObjectResult;
            Assert.IsNotNull(getResult);
            var badRequestResult = getResult.Value as ProblemDetails;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.Status);
        }
    }
}