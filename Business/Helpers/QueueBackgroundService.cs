using System.Text;
using Business.Manager;
using Microsoft.Extensions.Hosting;
using Model.Classes;
using Model.Enums;
using Newtonsoft.Json;

namespace Business.Helpers;

public class QueueBackgroundService : BackgroundService
{
    private IReport _report;
    private IContact _contact;

    public QueueBackgroundService(IReport report, IContact contact)
    {
        _report = report;
        _contact = contact;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(3000, stoppingToken);
            var channels = MessageHelper._channels;
            if (channels.Count == 0) continue;

            var receive = MessageHelper.ReceiveMessage(channels.Values.First());
            receive.Received += (model, e) =>
            {
                var body = Encoding.UTF8.GetString(e.Body.ToArray());
                if (string.IsNullOrEmpty(body)) return;

                var report = JsonConvert.DeserializeObject<Report>(body);
                UpdateReport(report);
            };

        }
        await Task.CompletedTask;
    }

    private void UpdateReport(Report? report)
    {
        if (report == null) return;

        report.IsReady = true;
        report.ReportSituation = "Tamamlandı.";

        var contact = _contact.GetAll();
        if (!contact.Success) return;

        var dto = contact.Data.Where(w =>
            {
                var location = JsonConvert.DeserializeObject<List<Location>>(w.LocationDetail);
                return location?.Count > 0 &&
                       location.Count(f => f.InformationType == (byte)LocationType.Location) > 0;
            })
            .Select(s =>
            {
                var detail = JsonConvert.DeserializeObject<List<Location>>(s.LocationDetail);
                if (detail == null)
                    return new
                    {
                        s.Id,
                        s.Company,
                        s.Name,
                        s.Surname,
                        Location = new List<Location>().Select(d =>
                        {
                            d.Contact = s;
                            return d;
                        }),
                    };


                return new
                {
                    s.Id,
                    s.Company,
                    s.Name,
                    s.Surname,
                    Location = detail.Select(d =>
                    {
                        d.Contact = s;
                        return d;
                    }),
                };
            }).ToList();

        var groupedUsers = dto
            .SelectMany(u => u.Location.Where(d => d.InformationType == (byte)LocationType.Location))
            .GroupBy(d => d.Information)
            .Where(g => g.Count() > 1);

        var list = (from @group in groupedUsers
                    let location = @group.Key
                    let phoneCount = @group.SelectMany(d => d.Contact.Locations!)
                        .Count(d => d.InformationType == (byte)LocationType.Phone)
                    let usersInTargetLocation = dto.Where(u => u.Location.Any(d => d.InformationType == (byte)LocationType.Location && d.Information == location))
                    select new ReportDetail { Location = location, ContactInLocationByPhoneCount = phoneCount, ContactInLocationCount = usersInTargetLocation.Count(), }).ToList();

        report.Detail = JsonConvert.SerializeObject(list);


        _report.Update(report);
    }
}
