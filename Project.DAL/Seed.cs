using Project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DAL
{
    public class Seed
    {
        public static void SeedDatabase(DataContext context)
        {
            if (!context.Makers.Any())
            {
                var makers = new List<VehicleMake>()
                {
                    new VehicleMake { Id = Guid.NewGuid(), Name = "Audi", Abrv = "A", Country = "Germany",
                        Founded = new DateTime(1910), City = "" },
                    new VehicleMake { Id = Guid.NewGuid(), Name = "Dacia", Abrv = "D", Country = "Romanian",
                        Founded = new DateTime(1910), City = "" },
                    new VehicleMake { Id = Guid.NewGuid(), Name = "BMW", Abrv = "B", Country = "Germany",
                        Founded = new DateTime(1916), City = "" },
                    new VehicleMake { Id = Guid.NewGuid(), Name = "Ford", Abrv = "F", Country = "Germany",
                        Founded = new DateTime(1903), City = "" },
                    new VehicleMake { Id = Guid.NewGuid(), Name = "Ford", Abrv = "F", Country = "USA",
                        Founded = new DateTime(1903), City = "" },
                    new VehicleMake { Id = Guid.NewGuid(), Name = "Peugeot", Abrv = "P", Country = "France",
                        Founded = new DateTime(1810), City = "" },
                    new VehicleMake { Id = Guid.NewGuid(), Name = "Citroen", Abrv = "C", Country = "France",
                        Founded = new DateTime(1919), City = ""},
                    new VehicleMake { Id = Guid.NewGuid(), Name = "Nissan", Abrv = "P", Country = "Japan",
                        Founded = new DateTime(1933), City = "" },
                    new VehicleMake { Id = Guid.NewGuid(), Name = "Toyota", Abrv = "P", Country = "Japan",
                        Founded = new DateTime(1937), City = "" },
                };

                foreach (var maker in makers)
                {
                    context.Makers.Add(maker);
                }
                context.SaveChanges();

                var models = new List<VehicleModel>()
                {

                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "S4",
                        Abrv = "S",
                        MakeId = makers.FirstOrDefault(s => s.Name == "Audi").Id
                    },
                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "S6",
                        Abrv = "S",
                        MakeId = makers.FirstOrDefault(s => s.Name == "Audi").Id
                    },
                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "A4",
                        Abrv = "S",
                        MakeId = makers.FirstOrDefault(s => s.Name == "Audi").Id
                    },
                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "A6",
                        Abrv = "S",
                        MakeId = makers.FirstOrDefault(s => s.Name == "Audi").Id
                    },
                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "A8",
                        Abrv = "S",
                        MakeId = makers.FirstOrDefault(s => s.Name == "Audi").Id
                    },

                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sandero",
                        Abrv = "S",
                        MakeId = makers.FirstOrDefault(s => s.Name == "Dacia").Id
                    },
                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Duster",
                        Abrv = "D",
                        MakeId = makers.FirstOrDefault(s => s.Name == "Dacia").Id
                    },

                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "X5",
                        Abrv = "S",
                        MakeId = makers.FirstOrDefault(s => s.Name == "BMW").Id
                    },
                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "X6",
                        Abrv = "D",
                        MakeId = makers.FirstOrDefault(s => s.Name == "BMW").Id
                    },

                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Fiesta",
                        Abrv = "F",
                        MakeId = makers.FirstOrDefault(s => s.Name == "Ford").Id
                    },
                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Mondeo",
                        Abrv = "M",
                        MakeId = makers.FirstOrDefault(s => s.Name == "Ford").Id
                    },

                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "106",
                        Abrv = "1",
                        MakeId = makers.FirstOrDefault(s => s.Name == "Peugeot").Id
                    },
                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "207",
                        Abrv = "2",
                        MakeId = makers.FirstOrDefault(s => s.Name == "Peugeot").Id
                    },
                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "306",
                        Abrv = "3",
                        MakeId = makers.FirstOrDefault(s => s.Name == "Peugeot").Id
                    },
                    new VehicleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "407",
                        Abrv = "4",
                        MakeId = makers.FirstOrDefault(s => s.Name == "Peugeot").Id
                    },
                };

                foreach (var model in models)
                {
                    context.Models.Add(model);
                }
                context.SaveChanges();
            }

        }
    }
}
