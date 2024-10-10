using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

public class BossHandler : MessageHandler
{
    public static Boss? Boss;

    private static ReplyKeyboardMarkup backMarkup = new ReplyKeyboardMarkup(true)
        .AddButton("Назад");

    public override async Task GetMessage(Message msg)
    {
        var pl = DataManager.GetPlayer(msg.Chat.Id);

        switch (msg.Text)
        {
            case "Назад":
                pl.MsgHandler = MessageManager.GetHandler("Menu");
                await pl.MsgHandler.Transfer(msg);
                break;
        }
    }

    public override async Task Transfer(Message msg)
    {
        if (Boss == null)
        {
            Boss = new Boss(MobHolder.Entities
                [Global.random.Next(0, MobHolder.Entities.Length)]);
        }

        var pl = DataManager.GetPlayer(msg.Chat.Id);

        BattleManager.Battle(pl.Pets[pl.ChosedPet], Boss);

        string addInfo = "";

        if (Boss.HP <= 0)
        {
            pl.Pets[pl.ChosedPet].AddExp(Global.MultiFloat(Boss.MaxHP, 0.1f));

            addInfo += "Вы победили";
        }
        else addInfo += "Вы проиграли";

        await MessageManager.Bot.SendTextMessageAsync(msg.Chat, $"{Boss.GetInfo()}\n\n" +
            $"{pl.Pets[pl.ChosedPet].GetInfo()}\n\n{addInfo}", replyMarkup: backMarkup);
    }
}