using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using log4net.Appender;
using log4net.Core;
using Newtonsoft.Json;

namespace Log4Telegram
{
    /// <summary>
    /// Log4net appender that logs to the Telegram messaging service.
    /// </summary>
    public class TelegramBotAppender : AppenderSkeleton
    {
        private const string ApiUrlTemplate = "https://api.telegram.org/bot{0}/";
        private const string RequestMethod = "sendMessage";

        /// <summary>
        /// The Auth Token of the bot.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The ID of the recipient. Can be a group or a user.
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Allows the use of markdown in the log4net layout string.
        /// </summary>
        public bool EnableMarkdown { get; set; }

        protected override void Append(LoggingEvent loggingEvent)
        {
            // Use log4net's default rendering behavior, to allow the use of patterns.
            string message = RenderLoggingEvent(loggingEvent);

            //ITelegramClient client = Activator.CreateInstance(type) as ITelegramClient;
            SendMessage(Recipient, message);
        }

        private async void SendMessage(string recipient, string message)
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new ArgumentException("Token is required.");

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(string.Format(ApiUrlTemplate, Token));

                var request = new BotSendMessageRequest
                {
                    chat_id = recipient,
                    text = message,

                    // Note that HTML is also supported by Telegram's API, but Markdown is more lightweight.
                    // Feel free to send me a pull request if you need HTML support.
                    parse_mode = "Markdown"
                };

                var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, RequestMethod)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
                });

                if (!response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Trace.WriteLine("Error while sending request to Telegram API:");
                    Trace.WriteLine(responseContent);
                }
            }
        }
    }
}
