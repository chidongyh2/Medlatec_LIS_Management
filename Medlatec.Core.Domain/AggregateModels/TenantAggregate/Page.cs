using Medlatec.Infrastructure.Extensions;
using Medlatec.Infrastructure.SeedWorks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Medlatec.Core.Domain.AggregateModels.TenantAggregate
{
    public class Page : ModifierTrackingEntity
    {
        [Key]
        public int Id { get; private set; }
        /// <summary>
        /// Trạng thái kích hoạt của trang. Trường hợp không kích hoạt sẽ không được hiển thị ngoài menu của người dùng.
        /// </summary>
        public bool IsActive { get; private set; }
        /// <summary>
        ///  Tên trang
        /// </summary>
        [Required, MaxLength(250)]
        public string Name { get; private set; }
        public string UnsignName { get; private set; }
        /// <summary>
        ///  Mô tả
        /// </summary>
        [Required, MaxLength(500)]
        public string Description { get; private set; }

        /// <summary>
        /// Đường dẫn Id của trang.
        /// </summary>
        [MaxLength(250)]
        public string IdPath { get; private set; }

        /// <summary>
        /// Icon của trang.
        /// </summary>
        [MaxLength(250)]
        public string Icon { get; private set; }

        /// <summary>
        /// Mầy nền của trang.
        /// </summary>
        [MaxLength(500)]
        public string BgColor { get; private set; }

        /// <summary>
        /// Thứ tự hiển thị của trang.
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// Thứ tự sắp xếp theo thứ tự hiển thị.
        /// </summary>
        [MaxLength(250)]
        public string OrderPath { get; private set; }

        /// <summary>
        /// Mã trang cha.
        /// </summary>
        public int? ParentId { get; private set; }

        /// <summary>
        /// Trạng thái đánh dấu xóa trang.
        /// </summary>
        public bool IsDelete { get; private set; }

        /// <summary>
        /// Số lượng trang con.
        /// </summary>
        public int ChildCount { get; private set; }

        [MaxLength(500)]
        public string Url { get; private set; }

        [Required]
        public PageType Type { get; private set; }

        public virtual ISet<TenantPage> TenantPages { get; private set; }

        public Page()
        {
            IsActive = true;
            IsDelete = false;
            Order = 0;
        }

        public Page(int id, string name, string description, string icon, string bgColor, int order, int? parentId, string url, bool isActive, PageType type = PageType.Sub)
        {
            Id = id;
            IsActive = isActive;
            Icon = icon;
            BgColor = bgColor;
            Order = order;
            OrderPath = $"{id}.{order}";
            ParentId = parentId;
            Url = url;
            IdPath = id.ToString();
            Name = name;
            Description = description;
            Type = type;
        }

        public void UpdateInfo(string name, string description, string icon, string bgColor, int order, string url, bool isActive)
        {
            IsActive = isActive;
            Icon = icon;
            BgColor = bgColor;
            Order = order;
            Url = url;
            Name = name;
            Description = description;
        }

        public void SetParent(int? parentId)
        {
            ParentId = parentId ?? null;
            IdPath = $"{(parentId.HasValue ? parentId.ToString() : "")}.{Id.ToString()}";
        }

        public void SetTenantsPage(IList<Guid> tenantIds)
        {
            tenantIds ??= new List<Guid>();
            TenantPages ??= new HashSet<TenantPage>();
            var toBeAddedIds = tenantIds.Where(tenantId => TenantPages.All(t => t.TenantId != tenantId));
            TenantPages.AddRange(toBeAddedIds.Select(tenantId => new TenantPage(tenantId, Id, false)));
        }

        public void RemoveAll()
        {
            TenantPages = null;
        }
        public void UpdateIdPath(string idPath)
        {
            IdPath = idPath;
        }
        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }
        public void SetChildCount(int childCount)
        {
            ChildCount = childCount;
        }

    }
}
