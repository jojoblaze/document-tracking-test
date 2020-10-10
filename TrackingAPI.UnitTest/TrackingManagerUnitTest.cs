using Moq;
using System;
using TrackingLib;
using TrackingLib.Models;
using Xunit;

namespace TrackingAPI.UnitTest
{
    public class TrackingManagerUnitTest
    {

        [Fact]
        public async void GetLatestUploadsReportShouldRaiseExceptionWithWrongParameter()
        {
            int hours = 0;

            var repositoryMock = new Mock<ITrackingMemoryRepository>();
            repositoryMock.Setup(mock => mock.GetItemsInLastNHoursAsync(It.IsAny<int>()));

            TrackingManager tm = new TrackingManager(repositoryMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await tm.GetLatestUploadsReport(hours, ReportUnit.Hours));

            repositoryMock.Verify(mock => mock.GetItemsInLastNHoursAsync(hours), Times.Never);
        }


        [Fact]
        public async void GetLatestUploadsReportShouldApplyTimeFilter()
        {
            int hours = 4;

            var repositoryMock = new Mock<ITrackingMemoryRepository>();
            repositoryMock.Setup(mock => mock.GetItemsInLastNHoursAsync(It.IsAny<int>()));

            TrackingManager tm = new TrackingManager(repositoryMock.Object);

            await tm.GetLatestUploadsReport(hours, ReportUnit.Hours);

            repositoryMock.Verify(mock => mock.GetItemsInLastNHoursAsync(hours), Times.Once);
        }

        [Fact]
        public async void GetLatestUploadsReportShouldApplyUnitFilter()
        {
            int hours = 4;

            var repositoryMock = new Mock<ITrackingMemoryRepository>();
            repositoryMock.Setup(mock => mock.GetItemsInLastNHoursAsync(It.IsAny<int>()));

            TrackingManager tm = new TrackingManager(repositoryMock.Object);

            await tm.GetLatestUploadsReport(hours, ReportUnit.Units);

            repositoryMock.Verify(mock => mock.GetLastNItemsAsync(hours), Times.Once);
        }


    }
}
