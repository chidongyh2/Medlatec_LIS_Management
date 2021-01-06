using Medlatec.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Medlatec.Core.Application.Commands.Pages
{
    public class InsertPageCommand : IRequest<ActionResultResponse>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid TenantId { get; private set; }
        public bool IsActive { get; private set; }
        public string Icon { get; private set; }
        public int Order { get; private set; }
        public int? ParentId { get; private set; }
        public bool IsShowSidebar { get; private set; }
        public string Url { get; private set; }
        public List<Guid> TenantIds { get; private set; }
        
        public InsertPageCommand(int id, string name, string description, Guid tenantId, bool isActive, string icon,
            int order, int? parentId, bool isShowSidebar, string url)
        {
            Id = id;
            TenantId = tenantId;
            IsActive = isActive;
            Icon = icon;
            Order = order;
            ParentId = parentId;
            IsShowSidebar = isShowSidebar;
            Url = url;
            Name = name;
            Description = description;
        }
    }
}
