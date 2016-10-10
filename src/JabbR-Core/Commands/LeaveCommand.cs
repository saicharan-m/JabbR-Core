﻿using System;
using JabbR_Core.Models;
using Microsoft.AspNetCore.SignalR;

namespace JabbR_Core.Commands
{
    [Command("leave", "Leave_CommandInfo", "room", "room")]
    public class LeaveCommand : UserCommand
    {
        public override void Execute(CommandContext context, CallerContext callerContext, ChatUser callingUser, string[] args)
        {
            string targetRoomName = args.Length > 0 ? args[0] : callerContext.RoomName;

            if (String.IsNullOrEmpty(targetRoomName))
            {
                throw new HubException(LanguageResources.Leave_RoomRequired);
            }

            ChatRoom room = context.Repository.VerifyRoom(targetRoomName, mustBeOpen: false);

            context.Service.LeaveRoom(callingUser, room);

            context.NotificationService.LeaveRoom(callingUser, room);

            context.Repository.CommitChanges();
        }
    }
}