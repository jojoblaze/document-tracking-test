using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using TrackingAPI.Controllers;
using TrackingAPI.DTO;
using TrackingLib;
using TrackingLib.Models;
using TrackingMockStorage;
using Xunit;

namespace TrackingAPI.UnitTest
{
    public class DocumentTrackingControllerUnitTest
    {


        [Fact]
        public async void PostUploadInfoOk()
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();


            var request = new UploadInfoRequest();
            request.ID = "document-id";
            request.DocumentCategory = "document-category";
            request.Size = 1;

            var loggerMock = new Mock<ILogger<DocumentTrackingController>>();
            var repositoryMock = new Mock<ITrackingMemoryRepository>();
            var managerMock = new Mock<TrackingManager>(repositoryMock.Object);
            managerMock.Setup(m => m.TrackDocument(
                It.IsAny<DocumentInfo>()
                )
            ).ReturnsAsync("newid123456");
            var controller = new DocumentTrackingController(loggerMock.Object, managerMock.Object, mapper);


            var response = await controller.PostUploadInfo(request);


            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(response.Result);
            var uploadInfoResponse = Assert.IsType<UploadInfoResponse>(viewResult.Value);
            Assert.Equal("newid123456", uploadInfoResponse.ExternalID);
        }


        [Fact]
        public async void PostUploadInfoBadRequest()
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();


            var request = new UploadInfoRequest();

            // *** uncomment all lines below to fail test ***
            //request.ID = "document-id";
            //request.DocumentCategory = "document-category";
            //request.Size = 1;

            var loggerMock = new Mock<ILogger<DocumentTrackingController>>();
            //var repositoryMock = new Mock<ITrackingMemoryRepository>();
            var repository = new TrackingMemoryRepository();
            var managerMock = new Mock<TrackingManager>(repository);
            var controller = new DocumentTrackingController(loggerMock.Object, managerMock.Object, mapper);


            var response = await controller.PostUploadInfo(request);


            // Assert
            var viewResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal("document data incomplete", viewResult.Value);
        }
    }
}
