using Medlatec.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
namespace Medlatec.Core.Application.Commands.Pages
{
    public class UpdatePageCommand : IRequest<ActionResultResponse>
    {
        public int Id { get; private set; }
        public Guid TenantId { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsShowSidebar { get; private set; }
        public string Icon { get; private set; }
        public int Order { get; private set; }
        public int? ParentId { get; private set; }
        public string Url { get; private set; }
        public List<string> TenantIds { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public UpdatePageCommand(int id, Guid tenantId, bool isActive, bool isShowSidebar, string icon, int order, int? parentId, string url, string name, string description)
        {
            Id = id;
            TenantId = tenantId;
            IsActive = isActive;
            IsShowSidebar = isShowSidebar;
            Icon = icon;
            Order = order;
            ParentId = parentId;
            Url = url;
            Name = name;
            Description = description;
        }
    }
}
