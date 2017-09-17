namespace Log4Telegram
{
    public class BotSendMessageRequest
    {
#pragma warning disable IDE1006 // Naming Styles
        public string chat_id { get; set; }

        public string text { get; set; }

        public string parse_mode { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
