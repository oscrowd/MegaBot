using MegaBot.Extensions;
using MegaBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MegaBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage; // Добавим это
        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage; // и это
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            string userLanguageCode = _memoryStorage.GetSession(message.Chat.Id).LanguageCode;
            if (string.IsNullOrEmpty(userLanguageCode) || userLanguageCode == "" || message.Text == "/start")
            { 
                switch (message.Text)
                {
                    case "/start":

                        // Объект, представляющий кноки
                        var buttons = new List<InlineKeyboardButton[]>();
                        buttons.Add(new[]
                        {
                            InlineKeyboardButton.WithCallbackData($" Длина строки" , $"le"),
                            InlineKeyboardButton.WithCallbackData($" Сумма чисел" , $"su")
                        });

                        // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Бот вычисляет длину строки или суммирует числа.  </b> {Environment.NewLine}" +
                            $"{Environment.NewLine}Выберите необходимый пункт меню:{Environment.NewLine}", cancellationToken: ct, replyMarkup: new InlineKeyboardMarkup(buttons));

                        break;
                    
                    default:
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Выберите одну из двух команд из стартового меню для сложения цифр или подсчета символов в строке", cancellationToken: ct);
                        break;
                }
            }
            else if (userLanguageCode == "le") 
            {
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Длина строки - " + message.Text.Length + " символов", cancellationToken: ct);
            }
            else if (userLanguageCode == "su")
            {
                SumExtension sumExtension = new SumExtension();
                string resultMessage = sumExtension.Summ(message.Text);
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Сумма чисел - " + resultMessage + " символов", cancellationToken: ct);
            }
        }
    }
}
