using Microsoft.EntityFrameworkCore;
using Otus.Teaching.Pcf.Administration.DataAccess;
using Otus.Teaching.Pcf.Administration.DataAccess.Repositories;
using System;

namespace Otus.Teaching.Pcf.Administration.IntegrationTests
{
    public class TestDataContext
        : DataContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=PromocodeFactoryAdministrationDb.sqlite");
        }
    }
}