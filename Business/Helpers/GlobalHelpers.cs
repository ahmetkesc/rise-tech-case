using Model.Classes;
using Model.Enums;
using Newtonsoft.Json;

namespace Business.Helpers;

public static class GlobalHelpers
{
    public static string Information(Location location)
    {
        switch ((LocationType)location.InformationType)
        {
            case LocationType.Phone:
            case LocationType.Mail:
                return location.Information;
            case LocationType.Location:
            {
                var info = JsonConvert.DeserializeObject<InformationByLocation>(location.Information);
                return info == null
                    ? location.Information
                    : $"Latitude: {info.Latitude}, Longitude: {info.Longitude}";
            }
            default:
                return location.Information;
        }
    }
}