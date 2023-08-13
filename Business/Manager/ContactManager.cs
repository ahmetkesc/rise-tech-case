using Model.Classes;
using Model.Classes.Dto;
using Model.Classes.Result;

namespace Business.Manager;

public class ContactManager : IContact
{
    public IResult<List<Contact>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IResult<List<DtoContactInformation>> GetByLocation(Guid contactId)
    {
        throw new NotImplementedException();
    }

    public IResult<bool> Add(Contact contact)
    {
        throw new NotImplementedException();
    }

    public IResult<bool> Delete(Guid contactId)
    {
        throw new NotImplementedException();
    }

    public IResult<bool> AddLocation(Guid contactId, Location location)
    {
        throw new NotImplementedException();
    }

    public IResult<bool> DeleteLocation(Guid contactId, Guid locationId)
    {
        throw new NotImplementedException();
    }
}

public interface IContact
{
    IResult<List<Contact>> GetAll();
    IResult<List<DtoContactInformation>> GetByLocation(Guid contactId);
    IResult<bool> Add(Contact contact);
    IResult<bool> Delete(Guid contactId);
    IResult<bool> AddLocation(Guid contactId,Location location);
    IResult<bool> DeleteLocation(Guid contactId,Guid locationId);
}