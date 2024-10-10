using Telegram.Bot;

public static class BotConnection
{
    private readonly static string API = "7516259470:AAENsF0OQ188aaB5M4thOzwmtSdEQX6r950";
    private static TelegramBotClient client;
    private static CancellationToken cts = new();

    public static TelegramBotClient GetClient()
    {
        return client;
    }

    public static CancellationToken GetCTS()
    { 
        return cts;
    }

    static BotConnection()
    {
        client = new TelegramBotClient(token:API, cancellationToken:cts);
    }
}
