﻿using System;
using NitroxModel.DataStructures.Util;
using NitroxModel.Logger;
using NitroxServer.ConsoleCommands.Abstract;
using NitroxModel.DataStructures.GameLogic;
using NitroxServer.ConfigParser;

namespace NitroxServer.ConsoleCommands
{
    internal class ChangeAdminPasswordCommand : Command
    {
        private readonly ServerConfig serverConfig;

        public ChangeAdminPasswordCommand(ServerConfig serverConfig) : base("changeadminpassword", Perms.ADMIN, "{password}", "Changes admin password")
        {
            this.serverConfig = serverConfig;
        }

        public override void RunCommand(string[] args, Optional<Player> sender)
        {
            try
            {
                string playerName = sender.HasValue ? sender.Value.Name : "SERVER";
                serverConfig.ChangeAdminPassword(args[0]);

                Log.Info($"Admin password changed to \"{args[0]}\" by {playerName}");
                SendMessageToPlayer(sender, "Admin password changed");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error attempting to change admin password to \"{args[0]}\"");
            }
        }

        public override bool VerifyArgs(string[] args)
        {
            return args.Length >= 1;
        }

        private void ChangeAdminPassword(string password, string name)
        {
            serverConfig.ChangeAdminPassword(password);
            Log.InfoSensitive("Admin password changed to {0} by {1}", password, name);
        }
    }
}
