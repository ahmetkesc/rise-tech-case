namespace Model.Classes.Dto;

public class DtoContactInformation
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Company { get; set; }

    public Guid InformationId { get; set; }
    public byte InformationType { get; set; }
    public string Information { get; set; }
}