using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

public class MinigamesHandler : MessageHandler
{
    private static InlineKeyboardMarkup menuChoice = new InlineKeyboardMarkup()
       .AddButton("Один кубик из трех", "One")
       .AddNewRow()
       .AddButton("Рулетка", "Two")
       .AddNewRow()
       .AddButton("Назад", "Back");

    private static ReplyKeyboardMarkup backMarkup = new ReplyKeyboardMarkup(true)
        .AddButton("Назад");

    private static string betInfo = "Напишите ставку от 1 до ";
    private static string chooseOne = "Напишите число от 1 до 6";
    private static string chooseTwo = "Напишите число 1 или 2 для черного или красного и 3 для зеленого";
    private static string incorrect = "Не правильный ввод, попробуйте снова";

    private int curState = 0;

    private int bet = 0;

    public override async Task GetCallback(CallbackQuery clbk)
    {
        switch (clbk.Data)
        {
            case "One":
                curState = 1;
                await MessageManager.Bot.SendTextMessageAsync(clbk.Message.Chat,
                    betInfo + DataManager.GetPlayer(clbk.Message.Chat.Id).Coins,
                    replyMarkup: backMarkup);
                break;
            case "Two":
                curState = 2;
                await MessageManager.Bot.SendTextMessageAsync(clbk.Message.Chat,
                    betInfo + DataManager.GetPlayer(clbk.Message.Chat.Id).Coins,
                    replyMarkup: backMarkup);
                break;
            case "Back":
                await ToMenu(clbk.Message);
                break;
        }
    }

    private async Task ToMenu(Message msg)
    {
        var pl = DataManager.GetPlayer(msg.Chat.Id);
        pl.MsgHandler = MessageManager.GetHandler("Menu");
        await pl.MsgHandler.Transfer(msg);
    }

    public override async Task GetMessage(Message msg)
    {
        var pl = DataManager.GetPlayer(msg.Chat.Id);

        int num = -1;
        int.TryParse(msg.Text, out num);

        if (msg.Text == "Назад")
        {
            await ToMenu(msg);
            return;
        }

        if (bet <= 0)
        {
            if (num <= 0 || num > pl.Coins)
            {
                await MessageManager.Bot.SendTextMessageAsync(msg.Chat, incorrect);
                return;
            }

            bet = num;
            num = -1;

            pl.Coins -= bet;

            if (curState == 1)
                await MessageManager.Bot.SendTextMessageAsync(msg.Chat, chooseOne);
            else
                await MessageManager.Bot.SendTextMessageAsync(msg.Chat, chooseTwo);
            return;
        }

        if (curState == 1)
        {
            if (num > 0 && num < 7)
            {
                bool isWon = false;

                int[] rolls = new int[3];

                for (int i = 0; i < rolls.Length; i++)
                {
                    rolls[i] = Global.random.Next(1, 7);

                    if (num == rolls[i]) isWon = true;
                }

                if (isWon)
                {
                    bet *= 2;
                    pl.Coins += bet;
                }
                else
                {
                    bet = 0;
                }

                await MessageManager.Bot.SendTextMessageAsync
                    (msg.Chat, $"Ваш баланс {pl.Coins}\nПолучили за ставку {bet}\n" +
                    $"Выпало {rolls[0]} - {rolls[1]} - {rolls[2]}", replyMarkup: menuChoice);
            }
            else
            {
                await MessageManager.Bot.SendTextMessageAsync(msg.Chat, incorrect);
            }
        }
        else if (curState == 2)
        {
            if (num > 0 && num < 4)
            {
                int roll = Global.random.Next(0, 21);
                int multi = 2;

                if (roll == 0) multi = 10;

                if ((roll == 0 && num == 0) || (roll % 2 == 0 && num == 1) || (roll % 2 == 1 && num == 2))
                {
                    bet *= multi;
                    pl.Coins += bet;
                }
                else
                {
                    bet = 0;
                }

                await MessageManager.Bot.SendTextMessageAsync
                    (msg.Chat, $"Ваш баланс {pl.Coins}\nПолучили за ставку {bet}\n" +
                    $"Выпало {roll}", replyMarkup:menuChoice);
            }
            else
            {
                await MessageManager.Bot.SendTextMessageAsync(msg.Chat, incorrect);
            }
        }

        bet = 0;
    }

    private static string introText = "\"Один кубик из трех\" - необходимо угадать" +
        " число, которое выпадет хотя бы на одном из трех кубиков. Выигрыш 2X\n" +
        "\"Рулетка\" - делаете ставку на черное (чет), красное (нечет) или зеленое. Выигрыш 2X. Для зеленого 10X";

    public override async Task Transfer(Message msg)
    {
        await MessageManager.Bot.SendTextMessageAsync(msg.Chat, introText, replyMarkup: menuChoice);
    }
}