using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using HSMbot.Handlers.Diyalog;
using HSMbot.Handlers.Diyalog.Steps;
using System;
using System.Threading.Tasks;

namespace HSMbot.Komutlar
{
    public class Eglence : BaseCommandModule
    {
        public Random rastgele { private get; set; }

        [Command("RastgeleSayi")]
        [Aliases("random", "rastgele")]
        [Description("belirlediğiniz iki sayı arasında rastgele bir sayı verir")]
        public async Task RastgeleSayiAsync(CommandContext ctx, 
            [Description("Minimum girilecek sayı")] int min,
            [Description("Maximum girilecek sayı")] int max)
        {
            await ctx.RespondAsync($"Gelen sayı: {rastgele.Next(min, max)}");
        }

        [Command("31"),Aliases("sj")]
        [Description("31 SJ")]
        public async Task SJAsync(CommandContext ctx)
        {
            await ctx.Message.Channel.SendMessageAsync("SJ!");
        }

        [Command("KacCM"),Aliases("kaçcm")]
        [Description("Kaç cm bilmiyor musun? dene ve öğren...")]
        public async Task KacCMAsync(CommandContext ctx, [RemainingText] DiscordMember isim)
        {
            Random cm = new Random();
            int kaccm = cm.Next(0, 31);
            string TrollMesaj = $"Derinlik mi ölçmek istiyordun?";
            string MünirMesaj = $"{ctx.Message.Author.Mention} Maalesef senin için bu komudu kullanamıyorum...\nÇünkü baktığımızda bir 🍆 yok!";
            string KlasikMesaj = $"Seninki {kaccm}CM!";
            var KacCMembed = new DiscordEmbedBuilder()
                .WithColor(new DiscordColor(3, 184, 255));
            if (ctx.Message.Author.Id == 767645866991157249)
            {
                if (isim == null)
                {
                    KacCMembed.WithDescription(MünirMesaj);
                    await ctx.Message.Channel.SendMessageAsync(KacCMembed.Build());
                }
                else
                {
                    string MünirMesajEtiket = $"{isim.Mention}'nınkini bilmem ama senin bir 🍆'a sahip olmadığını çok iyi biliyorum...";
                    KacCMembed.WithDescription(MünirMesajEtiket);
                    await ctx.Message.Channel.SendMessageAsync(KacCMembed.Build());
                }
            }
            //else if (isim.Id == 888509269706670140 && isim != null)
            //{
            //    await ctx.Message.Channel.SendMessageAsync($"Hiç çıkarmayayım... Gölgesinde çay içmek isterseniz ayrı!!");
            //}
            else if (ctx.Message.Author.Id == 429986349039484938)
            {
                if (isim == null)
                {
                    KacCMembed.WithDescription(TrollMesaj);
                    await ctx.Message.Channel.SendMessageAsync(KacCMembed.Build());
                }
                else
                {
                    string TrollMesajEtiket = $"Kendin için {isim.Mention}'nınkinin büyüklüğünü merak ediyorsun değil mi?\nHadi tamam meraklanma {kaccm}CM!";
                    KacCMembed.WithDescription(TrollMesajEtiket);
                    await ctx.Message.Channel.SendMessageAsync(KacCMembed.Build());
                }
            }
            else if (ctx.Message.Author.Id == 459835373518848016)
            {
                if (isim == null)
                {
                    KacCMembed.WithDescription(TrollMesaj);
                    await ctx.Message.Channel.SendMessageAsync(KacCMembed.Build());
                }
                else
                {
                    string TrollMesajEtiket = $"Kendin için {isim.Mention}'nınkinin büyüklüğünü merak ediyorsun değil mi?\nHadi tamam meraklanma {kaccm}CM!";
                    KacCMembed.WithDescription(TrollMesajEtiket);
                    await ctx.Message.Channel.SendMessageAsync(KacCMembed.Build());
                }
            }
            //else if (ctx.Message.Author.Id == 476095405940277265 || isim.Id == 476095405940277265 && isim != null)
            //{
            //    await ctx.Message.Channel.SendMessageAsync($"Kralıma bulaşmayın!! Eğer ölçmek için çıkartmaya kalkarsam yerle bir oluruz!!");
            //}
            else
            {
                if (isim == null)
                {
                    KacCMembed.WithDescription(KlasikMesaj);
                    await ctx.Message.Channel.SendMessageAsync(KacCMembed.Build());
                }
                else
                {
                    string KlasikMesajEtiket = $"{isim.Mention}'nınki {kaccm}CM!";
                    KacCMembed.WithDescription(KlasikMesajEtiket);
                    await ctx.Message.Channel.SendMessageAsync(KacCMembed.Build());
                }
            }
        }
        
        [Command("8ball"),Description("Evet mi? Hayır mı? Belki mi?")]
        public async Task EightBall(CommandContext ctx, [RemainingText, Description("Soru girilmeli")] string soru)
        {
            var cevaplar = new[]
            {
                "Kesinlikle!!", 
                "Şüphesiz!", 
                "Emin olabilirsin!", 
                "Gördüğüm kadarıyla... Evet", 
                "Büyük ihtimalle evet.", 
                "Evet!", 
                "Emin değilim... Tekrar dene.", 
                "Anlamadım? Bir daha söyle.", 
                "Şuan bunu bilmemen senin için daha iyi.",
                "Canım düşünmek istemiyor...",
                "Büyük ihtimalle hayır.",
                "Cevabım... Hayır.",
                "Edindiğim bilgiler hayır yönünde.",
                "Hiç iyi görünmüyor!",
                "Aşırı şüpheli!",
                "Hayır!",
                "Kesinlikle ve kesinlikle hayır!!",
                "Vazgeçmen senin için daha iyi!"
            };
            await ctx.TriggerTypingAsync();
            await Task.Delay(100);
            await ctx.RespondAsync(cevaplar[new Random().Next(0, cevaplar.Length + 1)]);
        }

        //[Command("korsandeneme")]
        //public async Task KorsanDili(CommandContext ctx, [RemainingText, Description("İngilizce bir yazı yazın.")] string mesaj)
        //{
        //    if (mesaj == null)
        //    {
        //        await ctx.Message.Channel.SendMessageAsync("Yarr! Mesaj nerde a...");
        //    }
        //    else
        //    {
        //        var c = new HttpClient();
        //        var e = JsonConvert.DeserializeObject(await c.GetStringAsync($"https://pirate.monkeyness.com/api/translate?english={mesaj}"));
        //        await ctx.Message.Channel.SendMessageAsync(e.ToString());
        //    }
        //}

        [Command("yazıtura"), Description("Yazı Tura atar."), Aliases("yazitura")]
        public async Task YaziTura(CommandContext ctx)
        {
            
            var yaziTura = new[]
            {
                "Yazı!!",
                "Tura!!"
            };
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
                .WithColor(new DiscordColor(3, 184, 255))
                .WithDescription($"**{yaziTura[new Random().Next(0, yaziTura.Length)]}**");
            await ctx.RespondAsync(embed.Build());
        }

        [Command("diyalogTest"), Aliases("dt"), Description("Diyalog Test ediyok bre susak"), RequireGuild]
        public async Task DiyalogTest(CommandContext ctx)
        {
           

            var funnyStep = new IntStep("AHAHAHHA\n31 AHAHAH", null, maxValue: 100);
            var inputStep = new TextStep("Yazı yaz?!", null, 2);

            string input = string.Empty;
            int value = 0;

            inputStep.OnValidResult += (result) =>
            {
                input = result;

                if (result == "Deneme3131")
                {
                    inputStep.SetNextStep(funnyStep);
                }
            };

            funnyStep.OnValidResult += (result) => value = result;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DiyalogHandler(
                ctx.Client,
                userChannel,
                ctx.User,
                inputStep
            );

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);
             
            if (!succeeded) { return; }

            await ctx.Channel.SendMessageAsync(input).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(value.ToString()).ConfigureAwait(false);
        }
    }
}
