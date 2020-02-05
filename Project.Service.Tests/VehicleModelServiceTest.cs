using Moq;
using Project.Common;
using Project.Model.Common;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using Xunit;

namespace Project.Service.Tests
{
    public class VehicleModelServiceTest
    {
        Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
        Mock<IVehicleModelRepository> repoMock = new Mock<IVehicleModelRepository>();

        [Fact]
        public async void GetModelAsync_Returns_IVehicleModel()
        {
            repoMock.Setup(s => s.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Mock<IVehicleModel>().Object));
            uowMock.SetupGet(s => s.Models).Returns(repoMock.Object);

            var modelService = new VehicleModelService(uowMock.Object);
            var model = await modelService.GetModelAsync(Guid.NewGuid());

            //Assert  
            Assert.NotNull(model);
            Assert.IsAssignableFrom<IVehicleModel>(model);
        }

        [Fact]
        public async void AddModelAsync_Returns_Model_Guid()
        {
            Guid guid = Guid.NewGuid();
            var modelr = new Mock<IVehicleModel>();
            modelr.Setup(s => s.Id).Returns(guid);
            repoMock.Setup(s => s.AddAsync(It.IsAny<IVehicleModel>())).Returns(Task.FromResult(modelr.Object));
            uowMock.SetupGet(s => s.Models).Returns(repoMock.Object);

            var modelService = new VehicleModelService(uowMock.Object);
            var model = await modelService.AddModelAsync(modelr.Object);

            //Assert  
            Assert.NotNull(model);
            Assert.Equal(guid, model.Id);
        }

        [Fact]
        public async void UpdateModelAsync_Returns_1()
        {

            repoMock.Setup(s => s.UpdateAsync(It.IsAny<IVehicleModel>()));
            uowMock.SetupGet(s => s.Models).Returns(repoMock.Object);
            uowMock.Setup(s => s.CommitAsync()).Returns(Task.FromResult(1));


            var modelService = new VehicleModelService(uowMock.Object);
            var result = await modelService.UpdateModelAsync(new Mock<IVehicleModel>().Object);

            //Assert  
            Assert.Equal(1, result);
        }

        [Fact]
        public async void RemoveModelAsync_Returns_1()
        {
            repoMock.Setup(s => s.RemoveAsync(It.IsAny<Guid>())).Returns(Task.FromResult(1));
            uowMock.SetupGet(s => s.Models).Returns(repoMock.Object);
            uowMock.Setup(s => s.CommitAsync()).Returns(Task.FromResult(1));


            var modelService = new VehicleModelService(uowMock.Object);
            var result = await modelService.RemoveModelAsync(Guid.NewGuid());

            //Assert  
            Assert.Equal(1, result);
        }

        [Fact]
        public async void FindModelAsync_Returns_PagedList_Models()
        {
            var sortMock = new Mock<ISortingParams>();
            var filterMock = new Mock<IFilteringParams>();
            var pageMock = new Mock<IPagingParams>();
            var pagedListMock = new Mock<IPagedList<IVehicleModel>>();
            pagedListMock.Setup(s => s.Count).Returns(10);
            pagedListMock.Setup(s => s.PageNumber).Returns(10);
            pagedListMock.Setup(s => s.PageSize).Returns(10);
            pagedListMock.Setup(s => s.TotalItemCount).Returns(10);


            repoMock.Setup(s => s.FindModelAsync(It.IsAny<IFilteringParams>(),
                                                It.IsAny<ISortingParams>(),
                                                It.IsAny<IPagingParams>()))
                    .Returns(Task.FromResult(pagedListMock.Object));

            uowMock.SetupGet(s => s.Models).Returns(repoMock.Object);



            var modelService = new VehicleModelService(uowMock.Object);
            var result = await modelService.FindModelAsync(filterMock.Object, sortMock.Object, pageMock.Object);

            //Assert  
            Assert.Equal(10, result.TotalItemCount);
            Assert.Equal(10, result.Count);
            Assert.Equal(10, result.PageSize);
            Assert.Equal(10, result.PageNumber);
        }
    }
}
