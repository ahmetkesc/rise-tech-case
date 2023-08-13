using Business.Helpers;
using Business.Manager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Classes;
using Xunit;

namespace RiseTech.Test.ManagerTest;

public class ReportManagerTest
{
    private readonly IConfiguration _configuration;

    public ReportManagerTest(IConfiguration configuration)
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
        var reportService = new ReportManager(dbContext);

        // Act
        var result = reportService.GetAll();

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Records not found!", result.Message);
        Assert.Empty(result.Data);
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
        var reportService = new ReportManager(dbContext);

        var contact = new Report();

        // Act
        var result = reportService.Add(contact);

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
        var reportService = new ReportManager(dbContext);

        var contact = new Report();

        // Act
        var result = reportService.Add(contact);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Contact not added!", result.Message);
        Assert.Equal(false, result.Data);
    }

    [Fact]
    public void ForUpdateFailed()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        var reportService = new ReportManager(dbContext);


        // Act
        var result = reportService.Update(new Report { Id = reportId });

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Report not updated!", result.Message);
        Assert.Equal(false, result.Data);
    }


    [Fact]
    public void ForRequestReportAddFails()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        var reportService = new ReportManager(dbContext);

        // Act
        var result = reportService.RequestReport();

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Report not added!", result.Message);
        Assert.Null(result.Data);
    }

    [Fact]
    public void ForGetReportWihtNoData()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var options = new DbContextOptionsBuilder<RContext>()
            .UseNpgsql(_configuration.GetConnectionString(
                "Server=localhost;Database=microservice_test; User Id=postgres;Password=12345;Pooling=False"))
            .Options;

        using var dbContext = new RContext(options);
        var reportService = new ReportManager(dbContext);


        // Act
        var result = reportService.GetReport(reportId);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Record not found!", result.Message);
        Assert.Null(result.Data);
    }
}