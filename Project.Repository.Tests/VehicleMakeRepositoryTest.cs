using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Project.DAL;
using Project.DAL.Entities;
using Project.Model.Common;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Project.Repository.Tests.Mappings;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Collections.Generic;
using Project.Common;
using X.PagedList;

namespace Project.Repository.Tests
{
    public class VehicleMakeRepositoryTest
    {
        private readonly IMapper mapper;


        public VehicleMakeRepositoryTest()
        {

            AutoMapperMap.ConfigureMapping();
            mapper = AutoMapperMap.GetMapper();
        }

        [Fact]
        public async void RemoveAsync_Returns_1()
        {

            //Setup DbContext and DbSet mock  
            var dbContextMock = new Mock<DataContext>();
            var dbSetMock = new Mock<DbSet<VehicleMake>>();
            dbSetMock.Setup(s => s.AddAsync(It.IsAny<VehicleMake>(), It.IsAny<System.Threading.CancellationToken>()));
            dbContextMock.Setup(s => s.Set<VehicleMake>().FindAsync(It.IsAny<Guid>())).Returns(new ValueTask<VehicleMake>(new VehicleMake()));

            var repository = new VehicleMakeRepository(dbContextMock.Object, mapper);
            var result = await repository.RemoveAsync(Guid.NewGuid());

            //Assert  
            Assert.Equal(1, result);

        }

        [Fact]
        public async void AddAsync_Returns_IVehicleMake()
        {

            //Setup DbContext and DbSet mock  
            var dbContextMock = new Mock<DataContext>();
            var dbSetMock = new Mock<DbSet<VehicleMake>>();
            dbSetMock.Setup(s => s.AddAsync(It.IsAny<VehicleMake>(), It.IsAny<System.Threading.CancellationToken>()));
            dbContextMock.Setup(s => s.Set<VehicleMake>()).Returns(dbSetMock.Object);
            dbContextMock.Setup(s => s.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>())).Returns(Task.FromResult(1));


            var repository = new VehicleMakeRepository(dbContextMock.Object, mapper);
            var make = await repository.AddAsync(new Mock<IVehicleMake>().Object);

            //Assert  
            Assert.NotNull(make);
            Assert.IsAssignableFrom<IVehicleMake>(make);

        }

        [Fact]
        public void GetAsync_Returns_IVehicleMake()
        {
 
            var dbContextMock = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<VehicleMake>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<Guid>())).Returns(new ValueTask<VehicleMake>(new VehicleMake()));
            dbContextMock.Setup(s => s.Set<VehicleMake>()).Returns(dbSetMock.Object);

 
            var repository = new VehicleMakeRepository(dbContextMock.Object, mapper) ;
            var make = repository.GetAsync(Guid.NewGuid());

            //Assert  
            Assert.NotNull(make);
            Assert.IsAssignableFrom<Task<IVehicleMake>>(make);

        }

        [Fact]
        public async void FindMakeAsync_Returns_PagedList_IVehicleMake()
        {
            var sortMock = new Mock<ISortingParams>();
            sortMock.Setup(s => s.SortDirection).Returns("asc");

            var filterMock = new Mock<IFilteringParams>();
            filterMock.Setup(s => s.Search).Returns("a");
            filterMock.Setup(s => s.MakeId).Returns(Guid.NewGuid());

            var pageMock = new Mock<IPagingParams>();
            pageMock.Setup(s => s.CurrentPage).Returns(2);
            pageMock.Setup(s => s.PageSize).Returns(3);




            var list = new List<VehicleMake>()
            {
                new VehicleMake { Id = Guid.NewGuid(), Name = "Audi", Abrv = "A" },
                new VehicleMake { Id = Guid.NewGuid(), Name = "Dacia", Abrv = "D" },
                new VehicleMake { Id = Guid.NewGuid(), Name = "Audi", Abrv = "A" },
                new VehicleMake { Id = Guid.NewGuid(), Name = "Dacia", Abrv = "D" },
                new VehicleMake { Id = Guid.NewGuid(), Name = "Audi", Abrv = "A" },
                new VehicleMake { Id = Guid.NewGuid(), Name = "Dacia", Abrv = "D" },

            }.AsQueryable();


            //Setup DbContext and DbSet mock  
            var dbContextMock = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<VehicleMake>>();
            dbSetMock.As<IQueryable<VehicleMake>>().Setup(m => m.Provider).Returns(list.Provider);
            dbSetMock.As<IQueryable<VehicleMake>>().Setup(m => m.Expression).Returns(list.Expression);
            dbSetMock.As<IQueryable<VehicleMake>>().Setup(m => m.ElementType).Returns(list.ElementType);
            dbSetMock.As<IQueryable<VehicleMake>>().Setup(m => m.GetEnumerator()).Returns(() => list.GetEnumerator());


            dbContextMock.Setup(s => s.Makers).Returns(dbSetMock.Object);


            var repository = new VehicleMakeRepository(dbContextMock.Object, mapper);
            var make = await repository.FindMakeAsync(filterMock.Object, sortMock.Object, pageMock.Object);

            //Assert  
            Assert.NotNull(make);
            Assert.IsAssignableFrom<IPagedList<IVehicleMake>>(make);
            Assert.Equal(2, make.PageNumber);
            Assert.Equal(3, make.PageSize);

        }

        [Fact]
        public async void GetMakerBaseAsync_Returns_IVehicleMakeBase()
        {
            var list = new List<VehicleMake>()
            {
                new VehicleMake { Id = Guid.NewGuid(), Name = "Audi", Abrv = "A" },
                new VehicleMake { Id = Guid.NewGuid(), Name = "Dacia", Abrv = "D" },

            }.AsQueryable();

            var dbContextMock = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<VehicleMake>>();
            dbSetMock.As<IQueryable<VehicleMake>>().Setup(m => m.Provider).Returns(list.Provider);
            dbSetMock.As<IQueryable<VehicleMake>>().Setup(m => m.Expression).Returns(list.Expression);
            dbSetMock.As<IQueryable<VehicleMake>>().Setup(m => m.ElementType).Returns(list.ElementType);
            dbSetMock.As<IQueryable<VehicleMake>>().Setup(m => m.GetEnumerator()).Returns(() => list.GetEnumerator());


            dbContextMock.Setup(s => s.Makers).Returns(dbSetMock.Object);


            var repository = new VehicleMakeRepository(dbContextMock.Object, mapper);
            var make = await repository.GetMakerBaseAsync();

            //Assert  
            Assert.NotNull(make);
            Assert.IsAssignableFrom<IEnumerable<IVehicleMakeBase>>(make);

        }

    }
}
