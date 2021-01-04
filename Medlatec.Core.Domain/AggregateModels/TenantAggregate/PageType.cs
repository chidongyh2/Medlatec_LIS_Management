﻿namespace Medlatec.Core.Domain.AggregateModels.TenantAggregate
{
    public enum PageType
    {
        Sub, // 0 => for parent menu
        Tab, // 1 => for Tab
        Url, // 2 => Open new Url
        Modal, // 3 => for open modal
    }
}
