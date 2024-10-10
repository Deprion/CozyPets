using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public static class MessageManager
{
    public static TelegramBotClient Bot;

    private static Dictionary<string, MessageHandler> Handlers = new Dictionary<string, MessageHandler>()
    {
        { "Introduction", new IntroductionHandler() { IsUnique = false }},
        { "Menu", new MenuHandler() { IsUnique = false }},
        { "PetsHandler", new PetsHandler() },
        { "Adventure", new AdventureHandler() },
        { "Minigames", new MinigamesHandler() },
        { "Boss", new BossHandler() { IsUnique = false } }
    };

    public static MessageHandler GetHandler(string handler)
    { 
        return Handlers[handler].GetInstance();
    }

    public static async Task OnMessage(Message msg, UpdateType type)
    {
        /*await using Stream stream = System.IO.File.OpenRead("D:/Repos/CozyPets/Sprites/Collar.jpg");
        var message = await Bot.SendPhotoAsync(msg.Chat.Id, photo: InputFile.FromStream(stream, "Collar.jpg"));

        Console.WriteLine(message.Photo.Last().FileId);*/

        if (msg.Text is not { } text)
            Console.WriteLine($"Received a message of type {msg.Type}");
        else
        {
            var user = DataManager.TryCreateUser(msg.Chat.Id, GetName(msg.Chat.FirstName));
            await user.MsgHandler.GetMessage(msg);
        }
    }

    public static async Task OnUpdate(Update update)
    {
        var chat = update.CallbackQuery.Message.Chat;
        var user = DataManager.TryCreateUser(chat.Id, GetName(chat.FirstName));

        switch (update)
        {
            case { CallbackQuery: { } clbk }:
                await user.MsgHandler.GetCallback(clbk);
                break;
            case { PollAnswer: { } pollAnswer }: 
                await user.MsgHandler.GetPoll(pollAnswer);
                break;
            default: 
                Console.WriteLine($"Received unhandled update {update.Type}");
                break;
        };
    }

    static MessageManager()
    {
        Bot = BotConnection.GetClient();
    }

    private static string GetName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return "Anon";
        return name;
    }
}
