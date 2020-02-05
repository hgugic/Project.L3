using Moq;
using Project.Common;
using Project.Model.Common;
using Project.Repository;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;
using Xunit;

namespace Project.Service.Tests
{
    public class VehicleMakeServiceTest
    {
        readonly Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>();
        Mock<IVehicleMakeRepository> repoMock = new Mock<IVehicleMakeRepository>();



        [Fact]
        public async void GetMakeAsync_Returns_IVehicleMake()
        {
            repoMock.Setup(s => s.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Mock<IVehicleMake>().Object));
            uowMock.SetupGet(s => s.Makers).Returns(repoMock.Object);

            var makeService = new VehicleMakeService(uowMock.Object);
            var make = await makeService.GetMakeAsync(Guid.NewGuid());

            //Assert  
            Assert.NotNull(make);
            Assert.IsAssignableFrom<IVehicleMake>(make);
        }

        [Fact]
        public void GetAllAsync_Returns_Enumerable_IVehicleMakeBase()
        {
            var list = new Mock<IEnumerable<IVehicleMakeBase>>();
            repoMock.Setup(s => s.GetMakerBaseAsync()).Returns(new Task<IEnumerable<IVehicleMakeBase>>(() => list.Object));
            uowMock.SetupGet(s => s.Makers).Returns(repoMock.Object);


            var makeService = new VehicleMakeService(uowMock.Object);
            var make = makeService.GetAllMakeAsync();


            //Assert  
            Assert.IsAssignableFrom<Task<IEnumerable<IVehicleMakeBase>>>(make);
        }

        [Fact]
        public async void AddMakeAsync_Returns_Maker()
        {
            Guid guid = Guid.NewGuid();
            var maker = new Mock<IVehicleMake>();
            maker.Setup(s => s.Id).Returns(guid);
            repoMock.Setup(s => s.AddAsync(It.IsAny<IVehicleMake>())).Returns(Task.FromResult(maker.Object));
            uowMock.SetupGet(s => s.Makers).Returns(repoMock.Object);

            var makeService = new VehicleMakeService(uowMock.Object);
            var make = await makeService.AddMakeAsync(maker.Object);

            //Assert  
            Assert.NotNull(make);
            Assert.Equal(guid, make.Id);
        }

        [Fact]
        public async void UpdateMakeAsync_Returns_1()
        {

            repoMock.Setup(s => s.UpdateAsync(It.IsAny<IVehicleMake>()));
            uowMock.SetupGet(s => s.Makers).Returns(repoMock.Object);
            uowMock.Setup(s => s.CommitAsync()).Returns(Task.FromResult(1));


            var makeService = new VehicleMakeService(uowMock.Object);
            var result = await makeService.UpdateMakeAsync(new Mock<IVehicleMake>().Object);

            //Assert  
            Assert.Equal(1, result);
        }

        [Fact]
        public async void RemoveMakeAsync_Returns_1()
        {
            repoMock.Setup(s => s.RemoveAsync(It.IsAny<Guid>())).Returns(Task.FromResult(1));
            uowMock.SetupGet(s => s.Makers).Returns(repoMock.Object);
            uowMock.Setup(s => s.CommitAsync()).Returns(Task.FromResult(1));


            var makeService = new VehicleMakeService(uowMock.Object);
            var result = await makeService.RemoveMakeAsync(Guid.NewGuid());

            //Assert  
            Assert.Equal(1, result);
        }

        [Fact]
        public async void FindMakeAsync_Returns_PagedList_Makers()
        {
            var sortMock = new Mock<ISortingParams>();
            var filterMock = new Mock<IFilteringParams>();
            var pageMock = new Mock<IPagingParams>();
            var pagedListMock = new Mock<IPagedList<IVehicleMake>>();
            pagedListMock.Setup(s => s.Count).Returns(10);
            pagedListMock.Setup(s => s.PageNumber).Returns(10);
            pagedListMock.Setup(s => s.PageSize).Returns(10);
            pagedListMock.Setup(s => s.TotalItemCount).Returns(10);


            repoMock.Setup(s => s.FindMakeAsync(It.IsAny<IFilteringParams>(), 
                                                It.IsAny<ISortingParams>(), 
                                                It.IsAny<IPagingParams>()))
                    .Returns(Task.FromResult(pagedListMock.Object));

            uowMock.SetupGet(s => s.Makers).Returns(repoMock.Object);



            var makeService = new VehicleMakeService(uowMock.Object);
            var result = await makeService.FindMakeAsync(filterMock.Object, sortMock.Object, pageMock.Object);

            //Assert  
            Assert.Equal(10, result.TotalItemCount);
            Assert.Equal(10, result.Count);
            Assert.Equal(10, result.PageSize);
            Assert.Equal(10, result.PageNumber);
        }
    }
}
