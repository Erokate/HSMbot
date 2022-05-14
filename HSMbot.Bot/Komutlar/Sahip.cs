using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMbot.Komutlar
{
    public class Sahip : BaseCommandModule
    {
        [Command("sudo"), Description("Komudu başka birisi olarak yürütür"), Hidden, RequireOwner]
        public async Task SudoAsync(CommandContext ctx, [Description("Yürütülecek Kişi.")] DiscordMember kisi, [RemainingText, Description("Yürütülecek Komut.")] string komut)
        {
            await ctx.TriggerTypingAsync();
            var cmds = ctx.CommandsNext;
            var cmd = cmds.FindCommand(komut, out var customArgs);
            var fakeContext = cmds.CreateFakeContext(kisi, ctx.Channel, komut, ctx.Prefix, cmd, customArgs);
            await cmds.ExecuteCommandAsync(fakeContext);
        }

        //[RequireOwner]
        //[Hidden]
        //[Command("activity")]
        //[Aliases("setactivity")]
        //[Description("Durumunu günceller.")]
        //public async Task SetBotActivity(CommandContext ctx,
        //    [Description("Durum adı.")] [RemainingText]
        //    string activity)
        //{
        //    if (string.IsNullOrWhiteSpace(activity))
        //    {
        //        await ctx.Client.UpdateStatusAsync().ConfigureAwait(false);
        //        return;
        //    }

        //    var game = new DiscordActivity(activity);
        //    await ctx.Client.UpdateStatusAsync(game).ConfigureAwait(false);
        //    await ctx.RespondAsync($"{ctx.Client.CurrentUser} durumu, {game.Name} olarak değiştirildi")
        //        .ConfigureAwait(false);
        //}

        [Command("Sunucular"), Description("Botun olduğu sunucuları görüntüler..."), Hidden, RequireOwner]
        public async Task SunucuListAsync(CommandContext ctx)
        {
            IEnumerable<DiscordGuild> sunucular = ctx.Client.Guilds.Values;
            var embed = new DiscordEmbedBuilder()
                .WithTitle("Şuanki Sunucular")
                .WithColor(new DiscordColor(3, 184, 255))
                .WithAuthor(ctx.Client.CurrentUser.Username);
            foreach (DiscordGuild sunucu in sunucular)
            {
                int kanalSayisi = ( sunucu.Channels.ToString()).Count();
                int uyeSayisi = ( sunucu.MemberCount.ToString()).Count();
                string sunucuBilgisi = $"{kanalSayisi} Kanal, {uyeSayisi} Üye, Sunucu Sahibi {sunucu.Owner.Username}, {sunucu.CreationTimestamp} Tarihinde Kuruldu";
                //if (sunucu.Description.Length !< 1)
                //{
                //    sunucuBilgisi += $"\n Description: {sunucu.Description}";
                //}
                embed.AddField(sunucu.Name, sunucuBilgisi);
            }
            await ctx.RespondAsync(embed: embed.Build());
        }

        [Command("NotAl")]
        [Description("Not alır."), RequireOwner, Hidden]
        public async Task SoyletAsync(CommandContext ctx, [RemainingText] string yazi)
        {
            var interaktif = ctx.Client.GetInteractivity();
            if (yazi == null)
            {
                var SoyletEmbedHata = new DiscordEmbedBuilder()
                    .WithColor(new DiscordColor(207, 0, 0))
                    .WithTitle("Hata!!")
                    .WithAuthor("HSM")
                    .WithDescription("Komudu yürütme başarılı olamadı...");
                await ctx.Message.Channel.SendMessageAsync(SoyletEmbedHata.Build()).ConfigureAwait(false);

            }
            else
            {
                var maviRenk = new DiscordColor(3, 184, 255);
                var soyletEmbed = new DiscordEmbedBuilder()
                    .WithColor(maviRenk)
                    .WithTitle("NOT")
                    .WithDescription(yazi);
                soyletEmbed.Build();
                var notlarKanal = await ctx.Client.GetChannelAsync(942434821521674302);
                await ctx.Client.SendMessageAsync(notlarKanal, soyletEmbed);
                await ctx.Message.DeleteAsync(yazi);
                
            }
        }

        [Command("DenemeReact"), Hidden]
        public async Task DenemeReactAsync(CommandContext ctx)
        {
            var interaktif = ctx.Client.GetInteractivity();
            var mesaj = await interaktif.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);
            await ctx.Message.Channel.SendMessageAsync("Deneme Başarılı!");
        }

        [Command("denemeEmbed"), Hidden]
        public async Task DenemeEmbedAsync(CommandContext ctx)
        {
            var SoyletEmbed = new DiscordEmbedBuilder()
                    .WithColor(new DiscordColor(207, 0, 0))
                    .WithTitle("Hata!!")
                    .WithAuthor("HSM")
                    .WithDescription($"");
            await ctx.Message.Channel.SendMessageAsync(SoyletEmbed.Build()).ConfigureAwait(false);
        }   

        
    }
}
