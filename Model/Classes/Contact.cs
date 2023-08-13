using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Model.Classes;

public class Contact
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Company { get; set; }
    public string LocationDetail { get; set; }

    [NotMapped] public List<Location>? Locations => JsonConvert.DeserializeObject<List<Location>>(LocationDetail);
}

public class Location
{
    public Guid Id { get; set; }
    public byte InformationType { get; set; }
    public string Information { get; set; }

    [JsonIgnore] public Contact? Contact { get; set; }
}

public class InformationByLocation
{
    public long Latitude { get; set; }
    public long Longitude { get; set; }
}