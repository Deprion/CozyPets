using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

public class MenuHandler : MessageHandler
{
    private static ReplyKeyboardMarkup menuMarkup = new ReplyKeyboardMarkup(true)
        .AddButton("Приключение").AddButton("Босс")
        .AddNewRow()
        .AddButton("Питомцы")
        .AddNewRow()
        .AddButton("Мини-игры");

    public override async Task GetMessage(Message msg)
    {
        var pl = DataManager.GetPlayer(msg.Chat.Id);

        switch(msg.Text)
        {
            case "/start":
                await Transfer(msg);
                break;
            case "Приключение":
                pl.MsgHandler = MessageManager.GetHandler("Adventure");
                await pl.MsgHandler.Transfer(msg);
                break;
            case "Питомцы":
                pl.MsgHandler = MessageManager.GetHandler("PetsHandler");
                await pl.MsgHandler.Transfer(msg);
                break;
            case "Мини-игры":
                pl.MsgHandler = MessageManager.GetHandler("Minigames");
                await pl.MsgHandler.Transfer(msg);
                break;
            case "Босс":
                pl.MsgHandler = MessageManager.GetHandler("Boss");
                await pl.MsgHandler.Transfer(msg);
                break;
        }
    }

    public override async Task Transfer(Message msg)
    {
        var pl = DataManager.GetPlayer(msg.Chat.Id);
        string text = $"Привет, {pl.Name}!\nЭнергия: {pl.Energy}/{pl.MaxEnergy} ⚡️\n" +
            $"Монетки: {pl.Coins} 🌕";

        await MessageManager.Bot.SendTextMessageAsync
            (msg.Chat.Id, text, replyMarkup: menuMarkup);
    }
}