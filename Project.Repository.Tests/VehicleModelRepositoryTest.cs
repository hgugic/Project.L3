using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Project.Common;
using Project.DAL.Entities;
using Project.Model.Common;
using Project.Repository.Tests.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;
using Xunit;

namespace Project.Repository.Tests
{
    public class VehicleModelRepositoryTest
    {
        private readonly IMapper mapper;
        public VehicleModelRepositoryTest()
        {
            AutoMapperMap.ConfigureMapping();
            mapper = AutoMapperMap.GetMapper();
        }

        [Fact]
        public async void FindMakeAsync_Returns_PagedList_IVehicleModel()
        {
            // problem sa include

            var sortMock = new Mock<ISortingParams>();
            sortMock.Setup(s => s.SortDirection).Returns("asc");

            var filterMock = new Mock<IFilteringParams>();
            filterMock.Setup(s => s.Search).Returns("a");
            filterMock.Setup(s => s.MakeId).Returns(Guid.NewGuid());

            var pageMock = new Mock<IPagingParams>();
            pageMock.Setup(s => s.CurrentPage).Returns(2);
            pageMock.Setup(s => s.PageSize).Returns(3);

            Guid g = Guid.NewGuid();

            var makeList = new List<VehicleMake>()
            {
                new VehicleMake { Id = g, Name = "Audi", Abrv = "A" },

            }.AsQueryable();

            var modelList = new List<VehicleModel>()
            {
                new VehicleModel { Id = Guid.NewGuid(), Name = "A1", Abrv = "A", MakeId = g },
                new VehicleModel { Id = Guid.NewGuid(), Name = "A2", Abrv = "A", MakeId = g  },
                new VehicleModel { Id = Guid.NewGuid(), Name = "A3", Abrv = "A", MakeId = g  },
                new VehicleModel { Id = Guid.NewGuid(), Name = "A4", Abrv = "A", MakeId = g  },
                new VehicleModel { Id = Guid.NewGuid(), Name = "A5", Abrv = "A", MakeId = g },
                new VehicleModel { Id = Guid.NewGuid(), Name = "A6", Abrv = "A", MakeId = g  },

            }.AsQueryable();


  
            var dbContextMock = new Mock<DatabaseContext>();

            var dbSetMakersMock = new Mock<DbSet<VehicleMake>>();
            dbSetMakersMock.As<IQueryable<VehicleMake>>().Setup(m => m.Provider).Returns(makeList.Provider);
            dbSetMakersMock.As<IQueryable<VehicleMake>>().Setup(m => m.Expression).Returns(makeList.Expression);
            dbSetMakersMock.As<IQueryable<VehicleMake>>().Setup(m => m.ElementType).Returns(makeList.ElementType);
            dbSetMakersMock.As<IQueryable<VehicleMake>>().Setup(m => m.GetEnumerator()).Returns(() => makeList.GetEnumerator());

            var dbSetModelsMock = new Mock<DbSet<VehicleModel>>();
            dbSetModelsMock.As<IQueryable<VehicleModel>>().Setup(m => m.Provider).Returns(modelList.Provider);
            dbSetModelsMock.As<IQueryable<VehicleModel>>().Setup(m => m.Expression).Returns(modelList.Expression);
            dbSetModelsMock.As<IQueryable<VehicleModel>>().Setup(m => m.ElementType).Returns(modelList.ElementType);
            dbSetModelsMock.As<IQueryable<VehicleModel>>().Setup(m => m.GetEnumerator()).Returns(() => modelList.GetEnumerator());


            dbSetModelsMock.As<IQueryable<VehicleModel>>().Setup(s => s.Include(It.IsAny<string>()));
            //dbSetModelsMock.Setup(s => s.Include(It.IsAny<string>())).Returns(() => modelList);

            dbContextMock.Setup(s => s.Models).Returns(dbSetModelsMock.Object);
            dbContextMock.Setup(s => s.Makers).Returns(dbSetMakersMock.Object);




            var repository = new VehicleModelRepository(dbContextMock.Object, mapper);
            var model = await repository.FindModelAsync(filterMock.Object, sortMock.Object, pageMock.Object);

            //Assert  
            Assert.NotNull(model);
            Assert.IsAssignableFrom<IPagedList<IVehicleModel>>(model);
            Assert.Equal(2, model.PageNumber);
            Assert.Equal(3, model.PageSize);

        }
    }
}
