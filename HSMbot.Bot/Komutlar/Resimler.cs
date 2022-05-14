using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using HSMbot.Entities;
using HSMbot.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using System.Net.Http;
using System.Threading.Channels;
using Newtonsoft.Json;
using System.IO;
using DSharpPlus.Entities;
using static HSMbot.Entities.NekoApi;
using DSharpPlus;

namespace HSMbot.Komutlar
{
    //[SlashCommandGroup("Resimler", "Resim Komutları")]
    public class Resimler : BaseCommandModule
    {
        
        private readonly NekoApi _nekoApi;
        public Resimler(NekoApi nekoApi)
        {
            this._nekoApi = nekoApi;
        }

        //[Command("denemefoto")]
        //public async Task NekoDeneme(CommandContext ctx, [RemainingText] string mesaj)
        //{

        //    var client = new HttpClient();
        //    var e = JsonConvert.DeserializeObject<NekoApiImage>(await client.GetStringAsync($"https://nekobot.xyz/api/imagegen?type=clyde&text={mesaj}"));
        //    Stream img = new MemoryStream(await client.GetByteArrayAsync(e.Message));
        //    DiscordMessageBuilder builder = new DiscordMessageBuilder();
        //    builder.WithFile($"clyde.png", img);
        //    await ctx.RespondAsync(builder);
        //    //var json = await client.GetStringAsync($"https://nekobot.xyz/api/imagegen?type=clyde&text{mesaj}");
        //    //await ctx.Message.RespondAsync(json);

        //}

        //[Command("deneme31")]
        //public async Task NekoImageAsync(InteractionContext ctx)
        //{
        //    var img = await _nekoApi.GetNekoApiImageAsync(ImageType.Neko);
        //    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent(img.Message));
        //}

        //[SlashCommand("deneme", "deneme")]
        //public async Task NekoDeneme2()
        //{

        //}
    }
}
