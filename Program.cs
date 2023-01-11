using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp2
{
    internal class Program
    {
        private static string button_text;

        static void Main(string[] args)
        {
            var client = new TelegramBotClient("5613398552:AAFl6D-g12LaoToae8Q8I9mdZmFY7z2Uees");
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        { 
            throw new NotImplementedException();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            

            if (message != null && message.Text != null)
            {
                if (message.Text.ToLower() == "start")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat,
                        text: "Главное меню",
                        replyMarkup: InlineKeyboard);

                    return;
                }
                    Console.WriteLine($"Пришло сообщение с текстом: {message.Text}");

               
                switch (button_text)
                {
                    case "111":
                        await botClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "https://www.livelib.ru/books/" + message.Text,
                            replyToMessageId: message.MessageId,
                            replyMarkup: InlineKeyboardMainMenu);
                        

                        break;

                    case "222":
                        await botClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "https://www.livelib.ru/quotes/" + message.Text,
                            replyToMessageId: message.MessageId,
                            replyMarkup: InlineKeyboardMainMenu);
                        break;


                    default:
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите команду: ",
                            replyMarkup: InlineKeyboard);
                        break;
                }

            }

            if (update.Type == UpdateType.CallbackQuery)
            {
                Console.WriteLine($"i`m in callback");

                var callbackQuery = update.CallbackQuery;

                if (update.CallbackQuery != null)
                    switch (update.CallbackQuery.Data)
                    {
                        case "11":
                            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "введите название книги");
                            button_text = "111";
                            break;


                        case "12":
                            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "введите текст цитаты");
                            button_text = "222";
                            break;

                        case "13":
                            await botClient.SendTextMessageAsync(
                                chatId: callbackQuery.Message.Chat.Id,
                                text: "Главное меню",
                                replyMarkup: InlineKeyboard);
                            break;

                    }
            }



        }



        // using Telegram.Bot.Types.ReplyMarkups;

        public static bool CallbackQuery { get; set; }

        private static readonly InlineKeyboardMarkup InlineKeyboard = new(new[]
        {
            // first row
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "книга", callbackData: "11"),
                InlineKeyboardButton.WithCallbackData(text: "цитата", callbackData: "12"),
            },
            // second row
        });

        private static readonly InlineKeyboardMarkup InlineKeyboardMainMenu = new(new[]
        {
            // first row
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "главное меню", callbackData: "13")
            },
            // second row
        });





    }
}