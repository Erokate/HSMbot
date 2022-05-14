using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Threading.Tasks;

namespace HSMbot.Handlers.Diyalog.Steps
{
    public class IntStep : DialogueStepBase
    {
        private IDialogueStep _nextStep;
        private readonly int? _minValue;
        private readonly int? _maxValue;

        public IntStep(
            string content,
            IDialogueStep nextStep,
            int? minValue = null,
            int? maxValue = null) : base(content)
        {
            _nextStep = nextStep;
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public Action<int> OnValidResult { get; set; } = delegate { };

        public override IDialogueStep NextStep => _nextStep;

        public void SetNextStep(IDialogueStep nextStep)
        {
            _nextStep = nextStep;
        }

        public override async Task<bool> ProcessStep(DiscordClient client, DiscordChannel channel, DiscordUser user)
        {
            var embedBuilder = new DiscordEmbedBuilder
            {
                Title = $"Lütfen Cevaplayın",
                Description = $"{user.Mention}, {_content}",
                Color = new DiscordColor(3, 184, 255)
            };

            embedBuilder.AddField("Diyaloğu Durdurmak İçin", "*iptal komudunu kullan");

            if (_minValue.HasValue)
            {
                embedBuilder.AddField("Minimum Değer:", $"{_minValue.Value}");
            }
            if (_maxValue.HasValue)
            {
                embedBuilder.AddField("Maximum Değer:", $"{_maxValue.Value}");
            }

            var interactivity = client.GetInteractivity();

            while (true)
            {
                var embed = await channel.SendMessageAsync(embed: embedBuilder).ConfigureAwait(false);

                OnMessageAdded(embed);

                var messageResult = await interactivity.WaitForMessageAsync(
                    x => x.ChannelId == channel.Id && x.Author.Id == user.Id).ConfigureAwait(false);

                OnMessageAdded(messageResult.Result);

                if (messageResult.Result.Content.Equals("*iptal", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (!int.TryParse(messageResult.Result.Content, out int inputValue))
                {
                    await TryAgain(channel, $"Girdiğiniz değer sayı değil").ConfigureAwait(false);
                    continue;
                }

                if (_minValue.HasValue)
                {
                    if (inputValue < _minValue.Value)
                    {
                        await TryAgain(channel, $"Girdiğiniz değer {inputValue} çok küçük. (Minimum değer {_minValue})").ConfigureAwait(false);
                        continue;
                    }
                }
                if (_maxValue.HasValue)
                {
                    if (inputValue > _maxValue.Value)
                    {
                        await TryAgain(channel, $"Girdiğiniz değer {inputValue} çok büyük. (Maximum değer {_maxValue})").ConfigureAwait(false);
                        continue;
                    }
                }

                OnValidResult(inputValue);

                return false;
            }
        }
    }
}
