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
        public string Icon { get; private set; }
        public string BgColor { get; private set; }
        public int Order { get; private set; }
        public int? ParentId { get; private set; }
        public bool IsPublic { get; private set; }
        public string Url { get; private set; }
        public List<string> TenantIds { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public UpdatePageCommand(int id, Guid tenantId, bool isActive, string icon, string bgColor,
            int order, int? parentId, bool isPublic, string url, string name, string description, List<string> tenantIds)
        {
            Id = id;
            TenantId = tenantId;
            IsActive = isActive;
            Icon = icon;
            BgColor = bgColor;
            Order = order;
            ParentId = parentId;
            IsPublic = isPublic;
            Url = url;
            TenantId = tenantId;
            Name = name;
            Description = description;
        }
    }
}
