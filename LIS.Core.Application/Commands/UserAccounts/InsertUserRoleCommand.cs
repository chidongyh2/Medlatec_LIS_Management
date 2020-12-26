﻿using LIS.Infrastructure.Models;
using MediatR;
using System;

namespace LIS.Core.Application.Commands.UserAccounts
{
    public class InsertUserRoleCommand : IRequest<ActionResultResponse>
    {
        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }
        public InsertUserRoleCommand(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
