using DSharpPlus;
using DSharpPlus.Entities;
using HSMbot.Handlers.Diyalog.Steps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HSMbot.Handlers.Diyalog
{
    public class DiyalogHandler
    {
        private readonly DiscordUser _user;
        private readonly DiscordClient _client;
        private readonly DiscordChannel _channel;
        private IDialogueStep _currentStep;
        public DiyalogHandler(
            DiscordClient client,
            DiscordChannel channel,
            DiscordUser user,
            IDialogueStep startingStep)
        {
            _client = client;
            _channel = channel;
            _user = user;
            _currentStep = startingStep;
        }

        private readonly List<DiscordMessage> messages = new List<DiscordMessage>();

        public async Task<bool> ProcessDialogue()
        {
            while (_currentStep != null)
            {
                _currentStep.OnMessageAdded += (message) => messages.Add(message);

                bool iptalEdildi = await _currentStep.ProcessStep(_client, _channel, _user).ConfigureAwait(false);

                if (iptalEdildi)
                {
                    await DeleteMessages().ConfigureAwait(false);

                    var iptalEmbed = new DiscordEmbedBuilder
                    {
                        Title = "Diyalog Başarıyla İptal Edildi",
                        Description = _user.Mention,
                        Color = new DiscordColor(3, 184, 255)
                    };

                    await _channel.SendMessageAsync(embed: iptalEmbed).ConfigureAwait(false);

                    return false;
                }

                _currentStep = _currentStep.NextStep;
            }

            await DeleteMessages().ConfigureAwait(false);

            return true;
        }

        private async Task DeleteMessages()
        {
            if (_channel.IsPrivate) { return; }

            foreach (var message in messages)
            {
                await message.DeleteAsync().ConfigureAwait(false);
            }
        }
    }
}
