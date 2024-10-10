using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;

class Program
{
    static async Task Main(string[] args)
    {
        DataManager.Load();
        
        await RunBot();

        Thread.Sleep(Timeout.Infinite);
    }

    private static async Task RunBot()
    {
        //await Task.Delay(40000);

        BotConnection.GetClient().OnMessage += MessageManager.OnMessage;
        BotConnection.GetClient().OnUpdate += MessageManager.OnUpdate;
        BotConnection.GetClient().OnError += ErrorAsync;

        var me = await BotConnection.GetClient().GetMeAsync();

        Console.WriteLine($"Start listening for @{me.Username}");
    }

    private static Task ErrorAsync(Exception exception, HandleErrorSource source)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]" +
                "\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}
