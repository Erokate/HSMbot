using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //[Command("temizle"), Description("1-100 arası girilen değerde mesajı siler.."), RequireBotPermissions(Permissions.ManageMessages), RequireGuild]
        //public async Task TemizleAsync(CommandContext ctx, [RemainingText, Description("1 ile 100 arasında değer girilmeli")] int deger)
        //{
        //    if (deger == null)
        //    {
        //        await ctx.RespondAsync("1 ile 100 arasında sayı girin...");
        //    }
        //    else
        //    {


        //        await ctx.Channel.SendMessageAsync("**Başarılı!**");
        //    }
        //}


        //Klasik Bir Toplama Komudu...
        [Command("topla"), Description("Girilen sayıların toplamını verir"), Aliases("toplama")]
        public async Task Toplama(CommandContext ctx, [Description("Toplanacak sayılar")] params double[] sayilar /*[RemainingText] int sayi1*/)
        {
            
            var toplam = sayilar.Sum();
            await ctx.Message.Channel.SendMessageAsync(toplam.ToString());
        }
    }
}
