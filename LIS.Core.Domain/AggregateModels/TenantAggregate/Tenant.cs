using System;
using System.Collections.Generic;
using LIS.Infrastructure.Extensions;
using LIS.Infrastructure.SeedWorks;

namespace LIS.Core.Domain.AggregateModels.TenantAggregate
{
    public class Tenant : ModifierTrackingEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string UnsignName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public bool IsActive { get; private set; }
        public string Note { get; private set; }
        public string Logo { get; private set; }
        public virtual IList<TenantPage> TenantPages { get; private set; }

        public Tenant()
        {

        }
        public Tenant(Guid id, string name, string email, string phoneNumber, string address, bool isActive, string note, string logo)
        {
            Id = id;
            Name = name.Trim();
            Email = email?.Trim();
            PhoneNumber = phoneNumber?.Trim();
            Address = address?.Trim();
            IsActive = isActive;
            Note = note?.Trim();
            Logo = logo?.Trim();
            UnsignName = $"{Name.StripVietnameseChars().ToUpper()} {email?.StripVietnameseChars().ToUpper()} {phoneNumber?.StripVietnameseChars().ToUpper()}";
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
