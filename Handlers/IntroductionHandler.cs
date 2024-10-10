using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

public class IntroductionHandler : MessageHandler
{
    private static string[] pets = { "Cat", "Dog", "Parrot", "Hamster" };

    private static InlineKeyboardMarkup[] inlines = new InlineKeyboardMarkup[]
        {
            new InlineKeyboardMarkup()
                .AddButton("<--", "3")
                .AddButton("-->", "1")
                .AddNewRow()
                .AddButton("Выбрать", pets[0]),
            new InlineKeyboardMarkup()
                .AddButton("<--", "0")
                .AddButton("-->", "2")
                .AddNewRow()
                .AddButton("Выбрать", pets[1]),
            new InlineKeyboardMarkup()
                .AddButton("<--", "1")
                .AddButton("-->", "3")
                .AddNewRow()
                .AddButton("Выбрать", pets[2]),
            new InlineKeyboardMarkup()
                .AddButton("<--", "2")
                .AddButton("-->", "0")
                .AddNewRow()
                .AddButton("Выбрать", pets[3]),
};

    private static InputMedia[] media = new InputMediaPhoto[]
    { 
            new InputMediaPhoto(Global.Photos[pets[0]])
            { Caption = PetHolder.Pets[pets[0]].Description },
            new InputMediaPhoto(Global.Photos[pets[1]])
            { Caption = PetHolder.Pets[pets[1]].Description },
            new InputMediaPhoto(Global.Photos[pets[2]])
            { Caption = PetHolder.Pets[pets[2]].Description },
            new InputMediaPhoto(Global.Photos[pets[3]])
            { Caption = PetHolder.Pets[pets[3]].Description },
            new InputMediaPhoto (Global.Photos["Collar"])
            { Caption = "Введите имя питомца"}
        };

    private static InlineKeyboardMarkup nameInline = new InlineKeyboardMarkup()
        .AddButton("Назад", "Back");

    private string petChoice = "";

    public override async Task GetCallback(CallbackQuery clbk)
    {
        string txt = clbk.Data;

        int id = clbk.Message.MessageId;

        int num = 0;

        if (int.TryParse(txt, out num) && num >= 0 && num <= 3)
        {
            await MessageManager.Bot.EditMessageMediaAsync
                (clbk.Message.Chat, id, media[num], replyMarkup: inlines[num]);
        }
        else if (pets.Contains(txt))
        {
            petChoice = txt;

            await MessageManager.Bot.EditMessageMediaAsync
                (clbk.Message.Chat, id, media[4],  replyMarkup: nameInline);
        }
        else if (txt == "Back")
        {
            await MessageManager.Bot.EditMessageMediaAsync
                (clbk.Message.Chat, id, media[num], replyMarkup: inlines[num]);
        }
        else
        {
            await MessageManager.Bot.AnswerCallbackQueryAsync
                (clbk.Id, "Что-то пошло не так", true);
        }
    }

    public override async Task GetMessage(Message msg)
    {
        if (msg.Text == "/start")
        {
            await Transfer(msg);
        }
        else if (!string.IsNullOrEmpty(petChoice))
        {
            Player pl = DataManager.GetPlayer(msg.Chat.Id);
            pl.AddPet(new Pet(PetHolder.Pets[petChoice]));
            pl.Pets[0].Name = msg.Text;

            pl.MsgHandler = MessageManager.GetHandler("Menu");
            await pl.MsgHandler.Transfer(msg);
        }
    }

    public override async Task Transfer(Message msg)
    {
        await MessageManager.Bot.SendPhotoAsync
            (msg.Chat.Id, Global.Photos["Cat"],
            caption: PetHolder.Pets[pets[0]].Description,
            replyMarkup: inlines[0]);
    }
}