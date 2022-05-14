using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMbot.Komutlar
{
    public class Moderasyon : BaseCommandModule
    {
        [Command("rolal"), Aliases("rolalma", "rolver"), Description("Emojiye tıklayarak rol alma"),
            RequireUserPermissions(DSharpPlus.Permissions.ManageRoles)]
        public async Task RolAlAsync(CommandContext ctx)
        {
            var RolAlEmbed = new DiscordEmbedBuilder()
                .WithTitle("Rol Al")
                .WithDescription("Emojiye Tıklayarak Rolünü Alabilirsin.")
                .WithColor(new DiscordColor(3, 184, 255));
            RolAlEmbed.Build();

            var RolAlMesaj= await ctx.Channel.SendMessageAsync(RolAlEmbed).ConfigureAwait(false);
            var waveEmoji = DiscordEmoji.FromName(ctx.Client, ":wave:");
            await RolAlMesaj.CreateReactionAsync(waveEmoji).ConfigureAwait(false);

            var interaktif = ctx.Client.GetInteractivity();

            var sonuc = await interaktif.WaitForReactionAsync(
                x => x.Message == RolAlMesaj &&
                x.Emoji == waveEmoji).ConfigureAwait(false);

            if (sonuc.Result.Emoji == waveEmoji)
            {
                var rol = ctx.Guild.GetRole(942419788259536928);
                await ctx.Member.GrantRoleAsync(rol).ConfigureAwait(false);
            }
            else
            {
                await ctx.RespondAsync("Bir Sorun Oluştu!!");
            }
        }

        [Command("soylet")]
        [Description("Bota söylenen şeyi söyletir."), RequirePermissions(Permissions.ManageMessages)]
        public async Task SoyletAsync(CommandContext ctx, [RemainingText] string yazi)
        {
            var interaktif = ctx.Client.GetInteractivity();
            if (yazi == null)
            {
                var SoyletEmbedHata = new DiscordEmbedBuilder()
                    .WithColor(new DiscordColor(207, 0, 0))
                    .WithTitle("Hata!!")
                    .WithAuthor("HSM")
                    .WithDescription("Komudu gerçekleştirmek için yazı yazmalısınız.\nDoğru Kullanım: *soylet <mesaj>");
                await ctx.Message.Channel.SendMessageAsync(SoyletEmbedHata.Build()).ConfigureAwait(false);

            }
            else
            {
                await ctx.Message.Channel.SendMessageAsync(yazi);
                await ctx.Message.DeleteAsync(yazi);
            }
        }

        [Command("soyletEmbed"), Description("Bota söylenen şeyi embed ile söyletir."), RequirePermissions(Permissions.ManageMessages)]
        public async Task SoyletEmbed(CommandContext ctx, [RemainingText, Description("Söylenecek Mesaj")] string yazi)
        {
            if (yazi == null)
            {
                var SoyletEmbedHata = new DiscordEmbedBuilder()
                    .WithColor(new DiscordColor(207, 0, 0))
                    .WithTitle("Hata!!")
                    .WithAuthor("HSM")
                    .WithDescription("Komudu gerçekleştirmek için yazı yazmalısınız.\nDoğru Kullanım: *soylet <mesaj>");
                await ctx.Message.Channel.SendMessageAsync(SoyletEmbedHata.Build()).ConfigureAwait(false);

            }
            else
            {
                var SoyletEmbed = new DiscordEmbedBuilder()
                    .WithColor(new DiscordColor(3, 184, 255))
                    .WithDescription(yazi);
                await ctx.Message.Channel.SendMessageAsync(SoyletEmbed.Build());
                await ctx.Message.DeleteAsync(yazi);
            }
        }

        [Command("Anket"), Description("Anket oluşturur"), RequireGuild, RequirePermissions(Permissions.ManageMessages)]
        public async Task Anket(CommandContext ctx, TimeSpan duration, params DiscordEmoji[] emojiOptions)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var options = emojiOptions.Select(x => x.ToString());

            var pollEmbed = new DiscordEmbedBuilder()
                .WithTitle("Anket")
                .WithColor(new DiscordColor(3, 184, 255))
                .AddField("Süre", duration.ToString())
                .WithDescription(string.Join("  ", options));
            

            var pollMessage = await ctx.Channel.SendMessageAsync(embed: pollEmbed).ConfigureAwait(false);

            foreach (var option in emojiOptions)
            {
                await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);
            }

            var result = await interactivity.CollectReactionsAsync(pollMessage, duration).ConfigureAwait(false);
            var distinctResult = result.Distinct();

            //var emojiResults = distinctResult.Select(x => $"{x.Emoji}");
            //var toplamResult = distinctResult.Select(x => $"{x.Total}");
            var results = distinctResult.Select(x => $"{x.Emoji}: {x.Total}");

            var sonucEmbed = new DiscordEmbedBuilder()
                .WithTitle("Sonuçlar")
                .WithColor(new DiscordColor(3, 184, 255))
                //.AddField(emojiResults.ToString(), toplamResult.ToString())
                .WithDescription(string.Join("\n", results));

            await ctx.Channel.SendMessageAsync(sonucEmbed.Build()).ConfigureAwait(false);

        }
    }
}
