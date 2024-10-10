using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

public class AdventureHandler : MessageHandler
{
    private static ReplyKeyboardMarkup backMarkup = new ReplyKeyboardMarkup(true)
        .AddButton("Назад");

    private Entity ent;

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
        ent = new Entity(MobHolder.Entities[Global.random.Next
            (0, MobHolder.Entities.Length)], true);

        var pl = DataManager.GetPlayer(msg.Chat.Id);

        BattleManager.Battle(pl.Pets[pl.ChosedPet], ent);

        string addInfo = "";

        if (ent.HP <= 0)
        {
            pl.Pets[pl.ChosedPet].AddExp(Global.MultiFloat(ent.MaxHP, 0.1f));

            addInfo += "Вы победили";
        }
        else addInfo += "Вы проиграли";

        await MessageManager.Bot.SendTextMessageAsync(msg.Chat, $"{ent.GetInfo()}\n\n" +
            $"{pl.Pets[pl.ChosedPet].GetInfo()}\n\n{addInfo}", replyMarkup : backMarkup);
    }
}