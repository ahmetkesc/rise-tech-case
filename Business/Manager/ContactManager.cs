using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Model.Classes;
using Model.Classes.Dto;
using Model.Classes.Result;
using Newtonsoft.Json;
using Business.Helpers;


namespace Business.Manager;

public class ContactManager : IContact
{
    private RContext _db;

    public ContactManager(RContext db)
    {
        this._db = db;
    }
    public IResult<List<Contact>> GetAll()
    {
        try
        {
            var result = _db.Contact.ToList();

            return result.Count == 0
                ? Result.Message<List<Contact>>("Records not found!")
                : Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Message<List<Contact>>(e.Message);
        }
    }
    public IResult<List<DtoContactInformation>> GetByLocation(Guid contactId)
    {
        try
        {
            var result = _db.Contact.FirstOrDefault(f => f.Id == contactId);

            if (result == null) return Result.Message<List<DtoContactInformation>>("Record not found!");

            var location = JsonConvert.DeserializeObject<List<Location>>(result.LocationDetail);

            if (location == null || location.Count == 0) return Result.Message<List<DtoContactInformation>>("Record location not found!");

            var model = location.Select(s => new DtoContactInformation
            {
                Id = result.Id,
                Company = result.Company,
                Name = result.Name,
                Surname = result.Name,
                InformationId = s.Id,
                Information = GlobalHelpers.Information(s),
                InformationType = s.InformationType
            }).ToList();

            return Result.Ok(model);

        }
        catch (Exception e)
        {
            return Result.Message<List<DtoContactInformation>>(e.Message);
        }
    }
    public IResult<bool> Add(Contact contact)
    {
        IDbContextTransaction? transaction = null;
        try
        {
            transaction = _db.Database.BeginTransaction();

            contact.Id = Guid.NewGuid();

            var result = _db.Add(contact);

            if (result.State != EntityState.Added) return Result.Message<bool>("Contact not added!");

            _db.SaveChanges();

            transaction.Commit();

            return Result.Ok(true);

        }
        catch (Exception e)
        {
            transaction?.Rollback();

            return Result.Message<bool>(e.Message);
        }
    }
    public IResult<bool> Delete(Guid contactId)
    {
        IDbContextTransaction? transaction = null;
        try
        {
            var contact = _db.Contact.FirstOrDefault(f => f.Id == contactId);
            if (contact == null) return Result.Ok(true, "Contact already deleted.");

            transaction = _db.Database.BeginTransaction();

            var result = _db.Remove(contact);

            if (result.State != EntityState.Added) return Result.Message<bool>("Contact not deleted!");

            _db.SaveChanges();

            transaction.Commit();

            return Result.Ok(true);

        }
        catch (Exception e)
        {
            transaction?.Rollback(); ;
            return Result.Message<bool>(e.Message);
        }
    }
    public IResult<bool> AddLocation(Guid contactId, Location location)
    {
        IDbContextTransaction? transaction = null;
        try
        {
            var contact = _db.Contact.FirstOrDefault(f => f.Id == contactId);
            if (contact == null) return Result.Ok(true, "Contact not found.");

            transaction = _db.Database.BeginTransaction();

            var locations = JsonConvert.DeserializeObject<List<Location>>(contact.LocationDetail);

            if (locations == null || locations.Count == 0) locations = new List<Location>();

            locations.Add(location);

            contact.LocationDetail = JsonConvert.SerializeObject(locations);

            var result = _db.Update(contact);

            if (result.State != EntityState.Modified) return Result.Message<bool>("Contact not deleted!");

            _db.SaveChanges();

            transaction.Commit();

            return Result.Ok(true);

        }
        catch (Exception e)
        {
            transaction?.Rollback();
            return Result.Message<bool>(e.Message);
        }
    }

    public IResult<bool> DeleteLocation(Guid contactId, Guid locationId)
    {
        IDbContextTransaction? transaction = null;
        try
        {
            var contact = _db.Contact.FirstOrDefault(f => f.Id == contactId);
            if (contact == null) return Result.Ok(true, "Contact not found.");

            transaction = _db.Database.BeginTransaction();

            var locations = JsonConvert.DeserializeObject<List<Location>>(contact.LocationDetail);

            if (locations == null || locations.Count == 0) return Result.Ok(true);

            var location = locations.FirstOrDefault(f => f.Id == locationId);

            if (location == null) return Result.Ok(true);

            locations.Remove(location);

            contact.LocationDetail = JsonConvert.SerializeObject(locations);

            var result = _db.Update(contact);

            if (result.State != EntityState.Modified) return Result.Message<bool>("Contact not deleted!");

            _db.SaveChanges();

            transaction.Commit();

            return Result.Ok(true);

        }
        catch (Exception e)
        {
            transaction?.Rollback();
            return Result.Message<bool>(e.Message);
        }
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