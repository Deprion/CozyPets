using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

public class PetsHandler : MessageHandler
{
    private static InlineKeyboardMarkup inlineMulti = new InlineKeyboardMarkup()
        .AddButtons("<--", "-->")
        .AddNewRow()
        .AddButton("Выбрать", "Choose")
        .AddNewRow()
        .AddButton("Назад", "Back");

    private static InlineKeyboardMarkup inlineSolo = new InlineKeyboardMarkup()
        .AddButton("Выбрать", "Choose")
        .AddNewRow()
        .AddButton("Назад", "Back");

    private InlineKeyboardMarkup inlineCur = inlineSolo;

    private int index;

    public override async Task GetCallback(CallbackQuery clbk)
    {
        var pl = DataManager.GetPlayer(clbk.Message.Chat.Id);

        switch (clbk.Data)
        {
            case "<--":
                index = index - 1 < 0 ? index = pl.Pets.Count - 1 : index - 1;
                await SendPetInfo(clbk.Message, true);
                break;
            case "-->":
                index = index + 1 >= pl.Pets.Count ? 0 : index + 1;
                await SendPetInfo(clbk.Message, true);
                break;
            case "Choose":
                if (pl.ChosedPet == index)
                {
                    await MessageManager.Bot.AnswerCallbackQueryAsync
                        (clbk.Id, "Уже выбран");
                }
                else await SendPetInfo(clbk.Message, true);
                break;
            case "Back":
                //await MessageManager.Bot.AnswerCallbackQueryAsync();
                pl.MsgHandler = MessageManager.GetHandler("Menu");
                await pl.MsgHandler.Transfer(clbk.Message);
                break;
            default:
                await base.GetCallback(clbk);
                return;
        }
    }

    private async Task SendPetInfo(Message msg, bool isEdit, bool chosed = false)
    {
        var pl = DataManager.GetPlayer(msg.Chat.Id);
        var pet = pl.Pets[index];

        if (pl.Pets.Count > 1)
            inlineCur = inlineMulti;
        else
            inlineCur = inlineSolo;

        string cap;

        if (pl.ChosedPet == index)
            cap = "Выбран ✅\n" + pet.GetInfo();
        else
            cap = pet.GetInfo();

        if (isEdit)
        {
            InputMedia media = new InputMediaPhoto(pet.Photo);
            media.Caption = cap;

            await MessageManager.Bot.EditMessageMediaAsync
            (msg.Chat, msg.MessageId, media, replyMarkup: inlineCur);
        }
        else
        {
            await MessageManager.Bot.SendPhotoAsync
                (msg.Chat.Id, pet.Photo, caption: cap, replyMarkup: inlineCur);
        }
    }

    public override async Task GetMessage(Message msg)
    {
        await SendPetInfo(msg, false);
    }

    public override async Task Transfer(Message msg)
    {
        await GetMessage(msg);
    }
}