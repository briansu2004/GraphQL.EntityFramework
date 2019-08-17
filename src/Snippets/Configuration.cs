﻿using GraphQL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

class Configuration
{
    void RegisterInContainerServiceCollectionUsage(IServiceCollection serviceCollection)
    {
        #region RegisterInContainerServiceCollectionUsage

        var builder = new DbContextOptionsBuilder();
        builder.UseSqlServer("fake");
        using (var context = new MyDbContext(builder.Options))
        {
            EfGraphQLConventions.RegisterInContainer(
                serviceCollection,
                dbContext: context,
                dbContextFromUserContext: userContext => (MyDbContext) userContext);
        }

        #endregion
    }

    void RegisterInContainerViaServiceProviderUsage(IServiceCollection serviceCollection)
    {
        #region RegisterInContainerViaServiceProviderUsage
        EfGraphQLConventions.RegisterInContainer(
            serviceCollection,
            userContext => (MyDbContext)userContext);
        #endregion
    }

    public class MyDbContext :
        DbContext
    {
        public MyDbContext(DbContextOptions options):
            base(options)
        {
        }
    }
}