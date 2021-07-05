﻿using Discord;
using Inkluzitron.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inkluzitron.Modules.Vote
{
    public class VoteMessageEventHandler : IMessageEventHandler
    {
        private VoteService VoteService { get; }

        public VoteMessageEventHandler(VoteService voteService)
        {
            VoteService = voteService;
        }

        public async Task<bool> HandleMessageUpdatedAsync(IMessageChannel channel, IMessage updatedMessage, Lazy<Task<IMessage>> freshMessageFactory)
        {
            var newMessage = await freshMessageFactory.Value;

            var (success, _, commandArgs) = await VoteService.TryMatchVoteCommand(newMessage);

            if (success)
            {
                // newMessage has Reactions.Count == 0, always
                var freshMessage = await channel.GetMessageAsync(newMessage.Id);
                if (freshMessage is IUserMessage freshUserMessage)
                    await VoteService.ProcessVoteCommandAsync(freshUserMessage, commandArgs);

                return true;
            }
            else
            {
                await VoteService.DeleteAssociatedVoteReplyIfExistsAsync(channel, newMessage.Id);
            }

            return false;
        }

        public async Task<bool> HandleMessageDeletedAsync(IMessageChannel channel, ulong messageId)
        {
            var wasVoteCommand = await VoteService.DeleteAssociatedVoteReplyIfExistsAsync(channel, messageId);
            if (!wasVoteCommand)
                await VoteService.DeleteVoteReplyRecordIfExistsAsync(channel, messageId);

            return false;
        }

        public async Task<bool> HandleMessagesBulkDeletedAsync(IMessageChannel channel, IReadOnlyCollection<ulong> messageIds)
        {
            foreach (var messageId in messageIds)
            {
                try
                {
                    await HandleMessageDeletedAsync(channel, messageId);
                }
                catch
                {
                    // we tried
                }
            }

            return false;
        }
    }
}
