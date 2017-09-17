# Log4Telegram
Log4net appender that allows you to send log messages using Telegram

## What does it do?

Quite simple: it takes your logging events, and forwards them to the Telegram Bot API. 

## How do I use it?

1. First of all, you need to create a Telegram Bot account. You can follow [this guide](https://core.telegram.org/bots#3-how-do-i-create-a-bot) to do it.
2. Install the NuGet package (or compile from source if you'd like).
3. Configure the appender. It behaves like a regular appender in the way that you can use a layout for it. Aside from that, it requires two additional parameters: 
**token** - The token that identifies your Telegram Bot
**recipient** - The ID of the recipient, this can be a regular account or a group
4. Just use Log4net like you normally would. You can check out the Example project in the source code if you need to.

## Will you continue development on this? 

Probably not. I made this because I needed it myself and I wanted to share it with you. It's not exactly rocket science so you are free to fork it, or send pull requests, or whatever. Maybe if there's a big enough demand, I might work on it more.

## Important notes ##

* Telegram Bots can not send messages 'out of the blue' so to speak. You, as the recipient, need to initiate a conversation with your bot before it can send messages to your. You can also add the bot to a group conversation, and this will also enable it to send messages to the group.
* Telegram's API allows the use of Markdown in their messages. You can use Markdown in your Log4net layout in order to use this in your log messages.
