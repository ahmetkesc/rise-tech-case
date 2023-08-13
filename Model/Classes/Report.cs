namespace Model.Classes;

public class Report
{
    public Guid Id { get; set; }
    public DateTime RequestDate { get; set; }
    public bool IsReady { get; set; }
    public string ReportSituation { get; set; }
    public string Detail { get; set; }
}

public class ReportDetail
{
    public string Location { get; set; }
    public int ContactInLocationCount{ get; set; }
    public int ContactInLocationByPhoneCount{ get; set; }
}