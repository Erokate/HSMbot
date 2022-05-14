//using DSharpPlus.CommandsNext.Converters;
//using DSharpPlus.CommandsNext;
//using DSharpPlus.CommandsNext.Entities;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using DSharpPlus.Entities;

//namespace HSMbot.Komutlar
//{
//    public class Yardim : BaseHelpFormatter
//    {
//        protected DiscordEmbedBuilder _embed;
//        protected StringBuilder _strBuilder;

//        public Yardim(CommandContext ctx) : base(ctx)
//        {
//            _embed = new DiscordEmbedBuilder();
//            _strBuilder = new StringBuilder();
//        }
//        public override BaseHelpFormatter WithCommand(Command command)
//        {
//            //_embed.AddField("`" + command.Name + "`", command.Description);
//            //_embed.Color = DiscordColor.Aquamarine;
//            // _strBuilder.AppendLine($"{command.Name} - {command.Description}");

//            return this;
//        }
//        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> cmds)
//        {
//            foreach (var cmd in cmds)
//            {
//                //_embed.AddField(cmd.Name, cmd.Description);
//                //_embed.Color = DiscordColor.Aquamarine;
//                //_embed.WithTitle("**Komutlar:**");
//                //_embed.WithFooter("HSM Bot 2022\nby Erokate#8457");
//                // _strBuilder.AppendLine($"{cmd.Name} - {cmd.Description}");
//            }

//            return this;
//        }
//        public override CommandHelpMessage Build()
//        {
//            return new CommandHelpMessage(embed: _embed);
//            // return new CommandHelpMessage(content: _strBuilder.ToString());
//        }
//    }
//}
