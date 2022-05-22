using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using HSMbot.Komutlar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HSMbot
{
    public class Bot
    {
        public readonly EventId BotEventId = new EventId(31, "HSMBot");

        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Command { get; private set; }
        public IConfiguration Configuration { get; private set; }
        //public SlashCommandsExtension SlashCommand { get; private set; }

        public async Task RunAsync()
        {

            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);
            Console.WriteLine(configJson.Token);
            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug
                //LogTimestampFormat = ("dd/mm/yyyy")

            };

            var services = new ServiceCollection()
                .AddSingleton<Random>()
                //.AddSingleton<NekoApi>()
                .BuildServiceProvider();

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;
            Client.GuildCreated += YeniSunucu;
            Client.ClientErrored += BotHata;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromSeconds(60)
            });



            //var elSallamaEmoji = DiscordEmoji.FromName(ctx.Client, ":wave:");

            Client.MessageCreated += async (s, e) =>
            {
                if (e.Author.IsBot)
                {
                    return;
                }
                else if (e.Message.Content.Contains("31") && e.Message.Content.Length <= 2)
                    await e.Message.RespondAsync("31 mi? SJ!");

                if (e.Message.Content.Length <= 2)
                {
                    if (e.Message.Content.StartsWith("sa"))
                        await e.Message.RespondAsync("Aleyküm Selam! Hoş Geldin.");
                    //await e.Message.CreateReactionAsync(elSallamaEmoji); 
                }
                if (e.Message.Content.Length <= 7)
                {
                    if (e.Message.Content.StartsWith("Merhaba"))
                        await e.Message.RespondAsync($"Merhaba {e.Message.Author.Mention} !");
                    //await e.Message.CreateReactionAsync(elSallamaEmoji); 
                }

                if (e.Message.Author.IsBot)
                {
                    return;
                }
                else if (e.Message.Content.Contains("hüsam".ToLower()) || e.Message.Content.Contains("husam".ToLower()))
                    await e.Message.RespondAsync("Evet? Beni mi çağırmıştınız?");
            };

            //var slash = Client.UseSlashCommands();

            //var slashCmdConfig = new SlashCommandsConfiguration
            //{
            //    Services = services,
            //};

            var cmdConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix }, //Botun Prefixi (config.json)
                EnableMentionPrefix = true, //Botu etiketleyerek komut kullanma
                EnableDms = false, //Özel mesajlarda komut kullanma
                DmHelp = false, //Help komutu girildiğinde komutları özelden atma
                Services = services,
            };

            Command = Client.UseCommandsNext(cmdConfig);

            Command.RegisterCommands<Genel>();
            Command.RegisterCommands<Eglence>();
            Command.RegisterCommands<Moderasyon>();
            Command.RegisterCommands<Sahip>();
            //Command.SetHelpFormatter<Yardim>();
            //Command.RegisterCommands<Resimler>();

            
            await Client.ConnectAsync();

            await Task.Delay(-1);
        }

        private Task YeniSunucu(DiscordClient sender, GuildCreateEventArgs e)
        {
            sender.Logger.LogInformation(BotEventId, $"Yeni bir sunucu: {e.Guild.Name}");
            return Task.CompletedTask;
        }

        private Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
        {
            sender.Logger.LogInformation(BotEventId, "HSM Bot Erokate#8457");
            return Task.CompletedTask;
        }

        private Task BotHata(DiscordClient sender, ClientErrorEventArgs e)
        {
            sender.Logger.LogError(BotEventId, e.Exception, "Hata oluştu!");
            return Task.CompletedTask;
        }

        private Task Komut_KomutKullanildi(DiscordClient sender, CommandExecutionEventArgs e)
        {
            sender.Logger.LogInformation(BotEventId, $"{e.Context.User.Username} başarıyla {e.Command.QualifiedName} komudunu kullandı.");
            return Task.CompletedTask;
        }

        private async Task Komut_KomutHata(DiscordClient sender, CommandErrorEventArgs e)
        {
            e.Context.Client.Logger.LogError(BotEventId, $"{e.Context.User.Username}, '{e.Command?.QualifiedName ?? "<Bilinmeyen Komut>"}' komudunu kullanmaya çalıştı fakat hata verdi: {e.Exception.GetType()}: {e.Exception.Message ?? "<Mesaj Yok>"}", DateTime.Now);
            if (e.Exception is ChecksFailedException ex)
            {
                var emoji = DiscordEmoji.FromName(e.Context.Client, ":no_entry:");
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Erişim Reddedildi",
                    Description = $"{emoji} Bu komudu çalıştırmaya yetkin yok.",
                    Color = new DiscordColor(0xFF0000) // Kırmızı Renk
                };
                await e.Context.RespondAsync(embed);
            }
        }
    }
}
