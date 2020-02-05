using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Project.Model.Common;
using Project.Service.Common;
using Project.WebAPI.Controllers;
using Project.WebAPI.Dtos;
using Project.WebAPI.Tests.Mappings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project.WebAPI.Tests
{
    public class VehicleModelControllerTest
    {
        VehicleModelController controller;
        Mock<IVehicleModelService> service;
        IMapper mapper;

        public VehicleModelControllerTest()
        {
            AutoMapperMap.ConfigureMapping();
            mapper = AutoMapperMap.GetMapper();
            service = new Mock<IVehicleModelService>();
            controller = new VehicleModelController(service.Object, mapper);

        }

        #region GetModel

        [Fact]
        public async void GetModel_Returns_Ok_With_Resource()
        {
            Guid g = Guid.NewGuid();
            Mock<IVehicleModel> vm = new Mock<IVehicleModel>();
            vm.Setup(s => s.Id).Returns(g);

            service.Setup(s => s.GetModelAsync(It.IsAny<Guid>())).Returns(Task.FromResult(vm.Object));

            var result = await controller.GetModel(Guid.NewGuid());

            var okObjectResult = result as OkObjectResult;
            var model = okObjectResult.Value as VehicleModelDto;

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.Equal(g, model.Id);
        }

        [Fact]
        public async void GetModel_Returns_BadRequest()
        {
            var result = await controller.GetModel(Guid.Empty);

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void GetModel_Returns_NotFound()
        {
            service.Setup(s => s.GetModelAsync(It.IsAny<Guid>())).Returns(Task.FromResult<IVehicleModel>(null));

            var result = await controller.GetModel(Guid.NewGuid());

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        #endregion GetModel

        #region Delete

        [Fact]
        public async void Delete_Returns_Ok()
        {
            service.Setup(s => s.RemoveModelAsync(It.IsAny<Guid>())).Returns(Task.FromResult(1));
            service.Setup(s => s.GetModelAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Mock<IVehicleModel>().Object));

            var result = await controller.Delete(Guid.NewGuid());

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkResult>(result);

        }

        [Fact]
        public async void Delete_Returns_BadRequest()
        {
            service.Setup(s => s.RemoveModelAsync(It.IsAny<Guid>())).Returns(Task.FromResult(0));
            service.Setup(s => s.GetModelAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Mock<IVehicleModel>().Object));

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
            service.Setup(s => s.GetModelAsync(It.IsAny<Guid>())).Returns(Task.FromResult<IVehicleModel>(null));

            var result = await controller.Delete(Guid.NewGuid());

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        #endregion Delete

        #region Create

        [Fact]
        public async void Create_Returns_CreatedAtRoute_With_Value()
        {
            VehicleModelDto createVehicleModelDto = new VehicleModelDto() { Name = "test" };

            var createdModel = mapper.Map<IVehicleModel>(createVehicleModelDto);
            createdModel.Id = Guid.NewGuid();


            service.Setup(s => s.AddModelAsync(It.IsAny<IVehicleModel>())).Returns(Task.FromResult(createdModel));

            var result = await controller.Create(createVehicleModelDto);

            var CreatedAtRouteResult = result as CreatedAtRouteResult;
            var modelId = CreatedAtRouteResult.RouteValues.Values as IList<object>;


            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<CreatedAtRouteResult>(result);
            Assert.Equal(createdModel.Id.ToString(), modelId[0].ToString());
            Assert.Equal("GetModel", CreatedAtRouteResult.RouteName);

        }

        #endregion Create

        #region Put

        [Fact]
        public async void Put_Returns_NoContent()
        {
            Guid g = Guid.NewGuid();
            Mock<IVehicleModel> vm = new Mock<IVehicleModel>();
            vm.Setup(s => s.Id).Returns(g);

            Mock<VehicleModelDto> vmDto = new Mock<VehicleModelDto>();
            service.Setup(s => s.UpdateModelAsync(It.IsAny<IVehicleModel>())).Returns(Task.FromResult(1));
            service.Setup(s => s.GetModelAsync(It.IsAny<Guid>())).Returns(Task.FromResult(vm.Object));

            var result = await controller.Put(g, vmDto.Object);

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NoContentResult>(result);

        }

        [Fact]
        public async void Put_Returns_BadRequest()
        {
            Mock<VehicleModelDto> vmDto = new Mock<VehicleModelDto>();

            var result = await controller.Put(Guid.Empty, vmDto.Object);

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Put_Returns_NotFound()
        {
            Mock<IVehicleModel> vm = new Mock<IVehicleModel>();
            Mock<VehicleModelDto> vmDto = new Mock<VehicleModelDto>();

            service.Setup(s => s.GetModelAsync(It.IsAny<Guid>())).Returns(Task.FromResult<IVehicleModel>(null));

            var result = await controller.Put(Guid.NewGuid(), vmDto.Object);

            //Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async void Put_Returns_Exception()
        {
            Guid g = Guid.NewGuid();
            Mock<IVehicleModel> vm = new Mock<IVehicleModel>();
            vm.Setup(s => s.Id).Returns(g);

            Mock<VehicleModelDto> vmDto = new Mock<VehicleModelDto>();
            service.Setup(s => s.UpdateModelAsync(It.IsAny<IVehicleModel>())).Returns(Task.FromResult(0));
            service.Setup(s => s.GetModelAsync(It.IsAny<Guid>())).Returns(Task.FromResult(vm.Object));

            try
            {
                var result = await controller.Put(g, vmDto.Object);
            }
            catch (Exception e)
            {
                Assert.Equal($"Updating model {g} failed on save", e.Message);
            }

        }

        #endregion Put
    }
}
