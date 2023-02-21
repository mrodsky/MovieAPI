using Microsoft.Extensions.Configuration;

public static class Utilites
{
    public static string GetConfigurationValue(string key)
    {

        var appSettingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        var config = new ConfigurationBuilder()
            .AddJsonFile(appSettingsFilePath)
            .Build();

        return config[key];
    }

    public static void ConsoleLog(string msg)
    {
        Console.WriteLine("{0} \t {1}", Utilites.CentralTime(), msg);
    }


    public static DateTimeOffset CentralTime()
    {
        TimeZoneInfo chicagoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Chicago");

        // Get the current time in Chicago
        DateTimeOffset chicagoTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, chicagoTimeZone);
        return chicagoTime;
    }
}
