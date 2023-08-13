
using Business.Helpers;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Model.Classes.Result;
using Model.Classes;
using Newtonsoft.Json;

namespace Business.Manager;

public class ReportManager : IReport
{
     private RContext _db;
    public ReportManager(RContext db)
    {
        this._db = db;
    }
    public IResult<List<Report>> GetAll()
    {
        try
        {
            var result = _db.Report.ToList();

            return result.Count == 0
                ? Result.Message<List<Report>>("Records not found!")
                : Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Message<List<Report>>(e.Message);
        }
    }
    public IResult<bool> Add(Report report)
    {
        IDbContextTransaction? transaction = null;
        try
        {
            transaction = _db.Database.BeginTransaction();

            report.Id = Guid.NewGuid();

            var result = _db.Add(report);

            if (result.State != EntityState.Added) return Result.Message<bool>("Report not added!");

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
    public IResult<bool> Update(Report report)
    {
        IDbContextTransaction? transaction = null;
        try
        {
            transaction = _db.Database.BeginTransaction();

            var result = _db.Update(report);

            if (result.State != EntityState.Modified) return Result.Message<bool>("Report not updated!");

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
    public IResult<Report> RequestReport()
    {
        var id = Guid.NewGuid();
        var report = new Report
        {
            Detail = JsonConvert.SerializeObject(new ReportDetail()),
            Id = id,
            ReportSituation = "Hazırlanıyor",
            RequestDate = DateTime.Now
        };

        var result = Add(report);

        if (!result.Success) return Result.Message<Report>(result.Message);

        MessageHelper.CreateMessage("requestreport", report);

        return Result.Ok(report);
    }
    public IResult<Report> GetReport(Guid id)
    {
        try
        {
            var result = _db.Report.FirstOrDefault(f => f.Id == id);

            return result == null
                ? Result.Message<Report>("Record not found!")
                : Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Message<Report>(e.Message);
        }
    }
}

public interface IReport
{
    IResult<List<Report>> GetAll();
    IResult<bool> Add(Report report);
    IResult<bool> Update(Report report);
    IResult<Report> RequestReport();
    IResult<Report> GetReport(Guid id);
}