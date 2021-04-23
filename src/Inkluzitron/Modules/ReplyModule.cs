﻿using Discord.WebSocket;
using Inkluzitron.Handlers;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Inkluzitron.Modules
{
    public class ReplyModule : ModuleBase
    {
        private DiscordSocketClient DiscordClient { get; }
        private Random Random { get; }

        public ReplyModule(DiscordSocketClient discordClient, Random random)
        {
            DiscordClient = discordClient;
            Random = random;

            DiscordClient.MessageReceived += OnMessageReceivedAsync;
        }

        private static bool ContainsPhrase(string message, string regex, bool matchOnlyWord=true)
        {
            if (matchOnlyWord) regex = $"(?<!\\w){regex}(?!\\w)";

            return Regex.IsMatch(message, regex, RegexOptions.IgnoreCase);
        }

        private async Task OnMessageReceivedAsync(SocketMessage message)
        {
            if (!MessagesHandler.TryParseMessageAndCheck(message, out SocketUserMessage _)) return;

            if (ContainsPhrase(message.Content, "uh ?oh"))
            {
                await message.Channel.SendMessageAsync("uh oh");
            }
            else if(ContainsPhrase(message.Content, "oh ?no"))
            {
                await message.Channel.SendMessageAsync("oh no");
            }
            else if (ContainsPhrase(message.Content, "m[aá]m pravdu.*\\?", false))
            {
                //await ReplyAsync(Random.Next(0, 2) == 1 ? "Ano, máš pravdu." : "Ne, nemáš pravdu.");
                // TODO Temp fix, než se fixne kontext ReplyAsync
                await message.Channel.SendMessageAsync(Random.Next(0, 2) == 1 ? "Ano, máš pravdu." : "Ne, nemáš pravdu.");
            }
            else if (ContainsPhrase(message.Content, "^je [cč]erstv[aá]"))
            {
                await message.Channel.SendMessageAsync("Není čerstvá!");
            }
            else if (ContainsPhrase(message.Content, "^nen[ií] [cč]erstv[aá]"))
            {
                await message.Channel.SendMessageAsync("Je čerstvá!");
            }
            else if (ContainsPhrase(message.Content, "^PR$"))
            {
                await message.Channel.SendMessageAsync("https://github.com/Misha12/Inkluzitron");
            }
        }
    }
}
