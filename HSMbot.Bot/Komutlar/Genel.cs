using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HSMbot.Komutlar
{

    public class Genel : BaseCommandModule
    {
        [Command("Ping"), Description("PONG! Botun gecikmesini gösterir."), RequireGuild]
        public async Task Ping(CommandContext ctx)
        {
            var PingEmbed = new DiscordEmbedBuilder()
                .WithColor(new DiscordColor(3, 184, 255))
                .WithTitle("Ping")
                .WithFooter("HSM Bot")
                .WithTimestamp(DateTime.Now)
                .WithDescription($"**{ctx.Client.Ping}ms**");
            await ctx.RespondAsync(PingEmbed.Build()).ConfigureAwait(false);
        }


        //Klasik Bir Toplama Komudu...
        [Command("topla"), Description("Girilen sayıların toplamını verir"), Aliases("toplama")]
        public async Task Toplama(CommandContext ctx, [Description("Toplanacak sayılar")] params double[] sayilar /*[RemainingText] int sayi1*/)
        {

            var toplam = sayilar.Sum();
            await ctx.Message.Channel.SendMessageAsync(toplam.ToString());
        }

        [Command("kullanıcıbilgi"), Aliases("kullanicibilgi", "kb"), Description("Kullanıcı hakkında bilgi verir.")]
        public async Task KullaniciBilgi(CommandContext ctx, [Description("Kullanıcının Etiketi")] DiscordUser user)
        {

            DiscordMember kullanici = null;

            if (user != null)
            {
                try
                {
                    kullanici = await ctx.Guild.GetMemberAsync(user.Id).ConfigureAwait(false);
                }
                catch
                {
                    await ctx.RespondAsync("Sanırım bu kişi sunucuda yok. Doğru girdiğine emin misin?");
                    return;
                }
            }
            if (kullanici == null)
            {
                kullanici = ctx.Member;
            }

            string yetki = null;
            if (kullanici.Permissions.HasPermission(Permissions.BanMembers) && kullanici.Permissions.HasPermission(Permissions.KickMembers))
            {
                yetki = "Moderatör (İnsanları sunucudan atma ve uzaklaştırma)";
            }
            if (kullanici.Permissions.HasPermission(Permissions.Administrator))
            {
                yetki = "Yönetici";
            }
            if (kullanici.IsOwner)
            {
                yetki = "Sunucu Sahibi";
            }

            string roller = "Yok";
            if (kullanici.Roles.Any())
            {
                roller = string.Empty;
                foreach (DiscordRole rol in kullanici.Roles.OrderBy(roller => roller.Position).Reverse())
                {
                    roller += rol.Mention + " ";
                }
            }



            DiscordEmbedBuilder kBilgiEmbed = new DiscordEmbedBuilder()
                .WithColor(new DiscordColor($"{kullanici.Color}"))
                .WithFooter($"Kullanan kişi {ctx.Member.Username}#{ctx.Member.Discriminator}")
                .AddField("Kullanıcı Etiketi", kullanici.Mention)
                .AddField("Kullanıcı ID", $"{kullanici.Id}")
                .AddField("Hesap Kayıt Tarihi", $"{kullanici.CreationTimestamp.DateTime}")
                .AddField("Sunucuya Katılma Tarihi", $"{kullanici.JoinedAt.DateTime}")
                .AddField("Roller", roller)
                .WithThumbnail(kullanici.AvatarUrl)
                .WithTimestamp(DateTime.UtcNow);

            if (yetki != null)
            {
                kBilgiEmbed.AddField("Yetki", yetki);
            }
            if (kullanici.PremiumSince != null)
            {
                DateTime PremiumSince = kullanici.PremiumSince.Value.UtcDateTime;
                long unixZaman = ((DateTimeOffset)PremiumSince).ToUnixTimeSeconds();
                string BoostZamani = $"Şu Kadar Süredir Booster: {unixZaman}";

                kBilgiEmbed.AddField("Booster", BoostZamani);
            }
            if (kullanici.IsBot == kullanici.IsBot)
            {
                kBilgiEmbed.WithDescription("__**BU KULLANICI BİR BOT.**__");
            }

            await ctx.Message.Channel.SendMessageAsync(/*$"**{kullanici.Username}#{kullanici.Discriminator}** Hakkında Bilgi" + */kBilgiEmbed.Build());
        }

        [Command("avatar"), Aliases("pp", "profilFoto"), Description("Kullanıcının profil fotoğrafını verir")]
        public async Task Avatar(CommandContext ctx, [RemainingText] DiscordMember kullanici)
        {
            if (kullanici == null)
            {
                kullanici = ctx.Member;
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
                .WithAuthor($"Profil Fotoğrafı Sahibi\n{kullanici.Username}#{kullanici.Discriminator}")
                .WithImageUrl(kullanici.AvatarUrl);
            await ctx.RespondAsync(embed.Build());
        }
    }
}
