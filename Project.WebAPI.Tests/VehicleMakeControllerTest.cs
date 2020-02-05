using Moq;
using Project.Model.Common;
using Project.Service.Common;
using Project.WebAPI.Controllers;
using Project.WebAPI.Dtos;
using Project.WebAPI.Tests.Mappings;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using Project.Common;
using X.PagedList;
using Project.WebAPI.Helpers;
using Project.Model;
using Microsoft.AspNetCore.Http;

namespace Project.WebAPI.Tests
{
    public class VehicleMakeControllerTest
    {
        VehicleMakeController controller;
        Mock<IVehicleMakeService> service;
        IMapper mapper;

        public VehicleMakeControllerTest()
        {
            AutoMapperMap.ConfigureMapping();
            mapper = AutoMapperMap.GetMapper();
            service = new Mock<IVehicleMakeService>();
            controller = new VehicleMakeController(service.Object, mapper);

        }

        #region GetMake

        [Fact]
        public async void GetMaker_Returns_Ok_With_Resource()
        {
            Guid g = Guid.NewGuid();
            Mock<IVehicleMake> vm = new Mock<IVehicleMake>();
            vm.Setup(s => s.Id).Returns(g);

            service.Setup(s => s.GetMakeAsync(It.IsAny<Guid>())).Returns(Task.FromResult(vm.Object));

            var result = await controller.GetMaker(Guid.NewGuid());

            var okObjectResult = result as OkObjectResult;
            var maker = okObjectResult.Value as VehicleMakeDto;

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.Equal(g, maker.Id);
        }

        [Fact]
        public async void GetMaker_Returns_BadRequest()
        {
            var result = await controller.GetMaker(Guid.Empty);

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void GetMaker_Returns_NotFound()
        {
            service.Setup(s => s.GetMakeAsync(It.IsAny<Guid>())).Returns(Task.FromResult<IVehicleMake>(null));

            var result = await controller.GetMaker(Guid.NewGuid());
            
            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        #endregion GetMake

        #region Delete

        [Fact]
        public async void Delete_Returns_Ok()
        {
            service.Setup(s => s.RemoveMakeAsync(It.IsAny<Guid>())).Returns(Task.FromResult(1));
            service.Setup(s => s.GetMakeAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Mock<IVehicleMake>().Object));

            var result = await controller.Delete(Guid.NewGuid());

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkResult>(result);

        }

        [Fact]
        public async void Delete_Returns_BadRequest()
        {
            service.Setup(s => s.RemoveMakeAsync(It.IsAny<Guid>())).Returns(Task.FromResult(0));
            service.Setup(s => s.GetMakeAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Mock<IVehicleMake>().Object));

            var resultEmptyGuid = await controller.Delete(Guid.Empty);
            var resultDeleteFailed = await controller.Delete(Guid.Empty);

            //Assert  
            Assert.NotNull(resultEmptyGuid);
            Assert.IsAssignableFrom<BadRequestObjectResult>(resultEmptyGuid);
            Assert.NotNull(resultDeleteFailed);
            Assert.IsAssignableFrom<BadRequestObjectResult>(resultDeleteFailed);
        }

        [Fact]
        public async void Delete_Returns_NotFound()
        {
            service.Setup(s => s.GetMakeAsync(It.IsAny<Guid>())).Returns(Task.FromResult<IVehicleMake>(null));

            var result = await controller.Delete(Guid.NewGuid());

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        #endregion Delete

        #region GetMakers

        [Fact]
        public async void GetMakers_Returns_Ok()
        {
            service.Setup(s => s.GetAllMakeAsync()).Returns(Task.FromResult<IEnumerable<IVehicleMakeBase>>(null));

            var result = await controller.GetMakers();

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result);

        }

        [Fact]
        public async void GetMakers_Returns_NotFound()
        {
            Mock<IEnumerable<IVehicleMakeBase>> vm = new Mock<IEnumerable<IVehicleMakeBase>>();

            service.Setup(s => s.GetAllMakeAsync()).Returns(Task.FromResult(vm.Object));

            var result = await controller.GetMakers();

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        #endregion GetMakers

        #region Create

        [Fact]
        public async void Create_Returns_CreatedAtRoute_With_Value ()
        {
            VehicleMakeDto createVehicleMakeDto = new VehicleMakeDto() { Name = "test" };

            var createdMaker = mapper.Map<IVehicleMake>(createVehicleMakeDto);
            createdMaker.Id = Guid.NewGuid();


            service.Setup(s => s.AddMakeAsync(It.IsAny<IVehicleMake>())).Returns(Task.FromResult(createdMaker));

            var result = await controller.Create(createVehicleMakeDto);

            var CreatedAtRouteResult = result as CreatedAtRouteResult;
            var makerId = CreatedAtRouteResult.RouteValues.Values as IList<object>;


            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<CreatedAtRouteResult>(result);
            Assert.Equal(createdMaker.Id.ToString(), makerId[0].ToString());
            Assert.Equal("GetMaker", CreatedAtRouteResult.RouteName);

        }

        #endregion Create

        #region Put

        [Fact]
        public async void Put_Returns_NoContent()
        {
            Guid g = Guid.NewGuid();
            Mock<IVehicleMake> vm = new Mock<IVehicleMake>();
            vm.Setup(s => s.Id).Returns(g);

            Mock<VehicleMakeDto> vmDto = new Mock<VehicleMakeDto>();
            service.Setup(s => s.UpdateMakeAsync(It.IsAny<IVehicleMake>())).Returns(Task.FromResult(1));
            service.Setup(s => s.GetMakeAsync(It.IsAny<Guid>())).Returns(Task.FromResult(vm.Object));

            var result = await controller.Put(g, vmDto.Object);

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NoContentResult>(result);

        }

        [Fact]
        public async void Put_Returns_BadRequest()
        {
            Mock<VehicleMakeDto> vmDto = new Mock<VehicleMakeDto>();

            var result = await controller.Put(Guid.Empty, vmDto.Object);

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Put_Returns_NotFound()
        {
            Mock<IVehicleMake> vm = new Mock<IVehicleMake>();
            Mock<VehicleMakeDto> vmDto = new Mock<VehicleMakeDto>();

            service.Setup(s => s.GetMakeAsync(It.IsAny<Guid>())).Returns(Task.FromResult<IVehicleMake>(null));

            var result = await controller.Put(Guid.NewGuid(), vmDto.Object);

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async void Put_Returns_Exception()
        {
            Guid g = Guid.NewGuid();
            Mock<IVehicleMake> vm = new Mock<IVehicleMake>();
            vm.Setup(s => s.Id).Returns(g);

            Mock<VehicleMakeDto> vmDto = new Mock<VehicleMakeDto>();
            service.Setup(s => s.UpdateMakeAsync(It.IsAny<IVehicleMake>())).Returns(Task.FromResult(0));
            service.Setup(s => s.GetMakeAsync(It.IsAny<Guid>())).Returns(Task.FromResult(vm.Object));

            try
            {
                var result = await controller.Put(g, vmDto.Object);
            }
            catch (Exception e)
            {
                Assert.Equal($"Updating maker {g} failed on save", e.Message);
            }

        }

        #endregion Put

        [Fact]
        public async void GetMakers_Returns_Ok_With_Headers()
        {
            VehicleParams vehicleMakeParams = new VehicleParams()
            {
                PageNumber = 1,
                PageSize= 2
            };


            var pagedList = new List<IVehicleMake>
            {
                new Mock<IVehicleMake>().Object,
                new Mock<IVehicleMake>().Object

            }.ToPagedList(1,2);

            service.Setup(s => s.FindMakeAsync(It.IsAny<IFilteringParams>(), 
                                               It.IsAny<ISortingParams>(), 
                                               It.IsAny<IPagingParams>()))
                   .Returns(Task.FromResult(pagedList));

            


            var result = await controller.GetMakers(vehicleMakeParams);
            var okObjectResult = result as OkObjectResult;

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result);

        }
    }
}
