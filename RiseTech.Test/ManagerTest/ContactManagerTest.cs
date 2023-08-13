using Business.Helpers;
using Business.Manager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Classes;
using Newtonsoft.Json;
using Xunit;

namespace RiseTech.Test.ManagerTest;

public class ContactManagerTest
{
    private readonly IConfiguration _configuration;

    public ContactManagerTest(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [Fact]
    public void ForGetAll()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        var contactService = new ContactManager(dbContext);

        // Act
        var result = contactService.GetAll();

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Records not found!", result.Message);
        Assert.Empty(result.Data);
    }

    [Fact]
    public void ForGetAllWithAdding()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        dbContext.Contact.Add(new Contact { Id = Guid.NewGuid(), Name = "Test Name", Surname = "Test Surname" });
        dbContext.Contact.Add(new Contact { Id = Guid.NewGuid(), Name = "Test Name 2", Surname = "Test Surname 2" });
        dbContext.SaveChanges();

        var contactService = new ContactManager(dbContext);

        // Act
        var result = contactService.GetAll();

        // Assert
        Assert.True(result.Success);
        Assert.Equal(2, result.Data.Count);
    }

    [Fact]
    public void ForGetByLocationWithEmpty()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        var contactService = new ContactManager(dbContext);

        // Act
        var result = contactService.GetByLocation(Guid.NewGuid());

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Record not found!", result.Message);
        Assert.Null(result.Data);
    }

    [Fact]
    public void ForGetByLocationWithNullLocation()
    {
        // Arrange
        var contacts = new Dictionary<Guid, Contact>
        {
            { Guid.NewGuid(), new Contact { Id = Guid.NewGuid(), LocationDetail = null } }
        };
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        var contactService = new ContactManager(dbContext);

        // Act
        var result = contactService.GetByLocation(contacts.Keys.First());

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Record location not found!", result.Message);
        Assert.Null(result.Data);
    }

    [Fact]
    public void ForGetByLocationWithDummy()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var contacts = new Dictionary<Guid, Contact>
        {
            {
                contactId, new Contact
                {
                    Id = contactId,
                    LocationDetail = "[{ \"Id\": \"" + Guid.NewGuid() + "\", \"InformationType\": 1 }]"
                }
            }
        };
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        var contactService = new ContactManager(dbContext);

        // Act
        var result = contactService.GetByLocation(contactId);

        // Assert
        Assert.True(result.Success);
        Assert.Empty(result.Message);
        Assert.NotNull(result.Data);
        Assert.Single(result.Data);
        Assert.Equal(contactId, result.Data[0].Id);
        Assert.Equal("Some information", result.Data[0].Information);
    }

    [Fact]
    public void ForAddSuccess()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        var contactService = new ContactManager(dbContext);

        var contact = new Contact();

        // Act
        var result = contactService.Add(contact);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(true, result.Data);
        Assert.Empty(result.Message);
    }

    [Fact]
    public void ForAddFailed()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        var contactService = new ContactManager(dbContext);

        var contact = new Contact();

        // Act
        var result = contactService.Add(contact);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Contact not added!", result.Message);
        Assert.Equal(false, result.Data);
    }

    [Fact]
    public void ForDeleteWithNotExistData()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        var contactService = new ContactManager(dbContext);

        // Act
        var result = contactService.Delete(Guid.NewGuid());

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Contact already deleted.", result.Message);
        Assert.Equal(true, result.Data);
    }

    [Fact]
    public void ForAddLocationWithNotExistContactData()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        var contactService = new ContactManager(dbContext);


        // Act
        var result = contactService.AddLocation(Guid.NewGuid(), new Location());

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Contact not found.", result.Message);
        Assert.Equal(true, result.Data);
    }


    [Fact]
    public void ForDeleteLocationWithDummyData()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        var contactService = new ContactManager(dbContext);
        var contact = new Contact
        {
            Id = Guid.NewGuid(),
            LocationDetail = JsonConvert.SerializeObject(new[] { new Location { Id = Guid.NewGuid() } })
        };

        dbContext.Contact.Add(contact);
        dbContext.SaveChanges();

        // Act
        var result = contactService.DeleteLocation(contact.Id, contact.Locations.FirstOrDefault()?.Id ?? Guid.Empty);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void ForDeleteLocationWithNotContact()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        var contactService = new ContactManager(dbContext);

        // Act
        var result = contactService.DeleteLocation(Guid.NewGuid(), Guid.NewGuid());

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Contact not found.", result.Message);
    }
}