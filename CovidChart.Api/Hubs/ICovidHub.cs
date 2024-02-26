namespace CovidChart.Api.Hubs
{
    public interface ICovidHub
    {
        Task ReceiveCovidList(List<Models.CovidChart> covidCharts);
    }
}
