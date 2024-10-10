using Telegram.Bot;
using Telegram.Bot.Types;

public abstract class MessageHandler
{
    public bool IsUnique = true;

    public abstract Task Transfer(Message msg);
    public async virtual Task GetMessage(Message msg)
    {
        await MessageManager.Bot.SendTextMessageAsync(msg.Chat.Id, "Something went wrong");
    }
    public async virtual Task GetCallback(CallbackQuery clbk)
    {
        await MessageManager.Bot.AnswerCallbackQueryAsync(clbk.Id, "Something went wrong", true);
    }
    public async virtual Task GetPoll(PollAnswer pollAnswer)
    {
        await MessageManager.Bot.SendTextMessageAsync(pollAnswer.VoterChat.Id, "Something went wrong");
    }

    public virtual MessageHandler GetInstance()
    {
        if (IsUnique)
            return (MessageHandler)Activator.CreateInstance(GetType());

        return this;
    }
}
