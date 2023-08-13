using Business.Helpers;
using Model.Classes;
using Model.Enums;
using Newtonsoft.Json;
using Xunit;

namespace RiseTech.Test.HelperTest;

public class GlobalHelpersTest
{
    [Fact]
    public void ForPhone()
    {
        // Arrange
        var location = new Location
        {
            InformationType = (int)LocationType.Phone,
            Information = "123-456-7890"
        };

        // Act
        var result = GlobalHelpers.Information(location);

        // Assert
        Assert.Equal("123-456-7890", result);
    }

    [Fact]
    public void ForMail()
    {
        // Arrange
        var location = new Location
        {
            InformationType = (int)LocationType.Mail,
            Information = "test@example.com"
        };

        // Act
        var result = GlobalHelpers.Information(location);

        // Assert
        Assert.Equal("test@example.com", result);
    }

    [Fact]
    public void ForLocation()
    {
        // Arrange
        var infoByLocation = new InformationByLocation
        {
            Latitude = 1,
            Longitude = 1
        };
        var location = new Location
        {
            InformationType = (int)LocationType.Location,
            Information = JsonConvert.SerializeObject(infoByLocation)
        };

        // Act
        var result = GlobalHelpers.Information(location);

        // Assert
        Assert.Equal("Latitude: 1, Longitude: 1", result);
    }

    [Fact]
    public void ForUnknow()
    {
        // Arrange
        var location = new Location
        {
            InformationType = 4,
            Information = "Some unknown information"
        };

        // Act
        var result = GlobalHelpers.Information(location);

        // Assert
        Assert.Equal("Some unknown information", result);
    }

    [Fact]
    public void ForNull()
    {
        // Arrange
        Location location = null;

        // Act
        var result = GlobalHelpers.Information(location);

        // Assert
        Assert.Null(result);
    }
}