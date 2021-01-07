using System;
using System.Collections.Generic;
using System.Text;

namespace Medlatec.Infrastructure.SeedWorks
{
    public interface IModifierTrackingEntity
    {
        string CreatedBy { get; }
        string LastUpdatedBy { get; }
        Guid CreatedById { get; }
        Guid LastUpdatedById { get; }
        bool? IsDeleted { get; }
    }
    public abstract class ModifierTrackingEntity : DateTrackingEntity, IModifierTrackingEntity
    {
        public string CreatedBy { get; private set; }
        public Guid CreatedById { get; private set; }

        public string LastUpdatedBy { get; private set; }
        public Guid LastUpdatedById { get; private set; }

        public bool? IsDeleted { get; private set; }

        public void MarkCreated(Guid authorId, string authorName)
        {
            CreatedBy = authorName;
            CreatedById = authorId;
        }

        public void MarkModified(Guid authorId, string authorName)
        {
            LastUpdatedBy = authorName;
            LastUpdatedById = authorId;
        }
    }
}
