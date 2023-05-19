namespace MeihuaWintry.Pages;

partial class CaseCreatePage
{
    DateTime? WesternDate { get; set; }
    TimeSpan? WesternTime { get; set; }

    protected override void OnParametersSet()
    {
        var dateTime = DateTime.Now;
        this.WesternDate = dateTime.Date;
        this.WesternTime = dateTime.TimeOfDay;
    }
}
