using System;
using System.Collections.Generic;
using System.Text;

namespace LIS.Infrastructure.SeedWorks
{
    public interface IModifierTrackingEntity
    {
        string CreatedById { get; }
        string LastUpdatedById { get; }
        bool? IsDeleted { get; }
    }
    public abstract class ModifierTrackingEntity : DateTrackingEntity, IModifierTrackingEntity
    {
        public string CreatedBy { get; private set; }
        public string CreatedById { get; private set; }

        public string LastUpdatedBy { get; private set; }
        public string LastUpdatedById { get; private set; }

        public bool? IsDeleted { get; private set; }

        public void MarkCreated(string authorId, string authorName)
        {
            CreatedBy = authorName;
            LastUpdatedBy = authorName;
            CreatedById = authorId;
            LastUpdatedById = authorId;
        }

        public void MarkModified(string authorId, string authorName)
        {
            LastUpdatedBy = authorName;
            LastUpdatedById = authorId;
        }
    }
}
