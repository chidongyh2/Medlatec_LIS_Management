using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LIS.Core.Domain.AggregateModels.TenantAggregate;
using LIS.Core.Domain.IRepository;
using LIS.Infrastructure.Oracle;

namespace LIS.Core.Infrastructure.Repository
{
    public class PageRepository : RepositoryBase, IPageRepository
    {
        private readonly IRepository<Page> _pageRepository;
        public PageRepository(IDbContext context) : base(context)
        {
            _pageRepository = Context.GetRepository<Page>();
        }

        public async Task<bool> CheckExists(int id)
        {
            return await _pageRepository.ExistAsync(x => x.Id == id);
        }

        public async Task<int> Insert(Page page)
        {
            _pageRepository.Create(page);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> Update(Page page)
        {
            return await Context.SaveChangesAsync();
        }

        public async Task<int> UpdateIdPath(int id, string idPath)
        {
            var info = await GetInfo(id);
            if (info == null)
                return -1;

            var oldIdPath = info.IdPath;
            info.UpdateIdPath(idPath);
            var result = await Context.SaveChangesAsync();
            if (result > 0 && info.ChildCount > 0)
            {
                await UpdateChildrenIdPath(oldIdPath, info.IdPath);
            }
            return result;
        }

        public async Task<int> UpdateIdPath(string oldIdPath, string newIdPath)
        {
            var pages = await _pageRepository.GetsAsync(false, x => x.IdPath.StartsWith(oldIdPath + "."));
            if (pages == null || !pages.Any())
                return -1;

            foreach (var page in pages)
            {
                page.UpdateIdPath(page.IdPath.Replace(oldIdPath, newIdPath));
            }
            return await Context.SaveChangesAsync();
        }

        public async Task<int> UpdateChildrenIdPath(string oldParentIdPath, string newParentIdPath)
        {
            var childrens = await GetListChildrenByParentIdPath(oldParentIdPath);
            if (!childrens.Any())
                return -1;

            foreach (var children in childrens)
            {
                var oldIdPath = children.IdPath;
                children.UpdateIdPath($"{newParentIdPath}.{children.Id}");
                await UpdateChildrenIdPath(oldIdPath, $"{children.IdPath}");
            }
            return await Context.SaveChangesAsync();
        }

        public async Task<List<Page>> GetListChildrenByParentIdPath(string parentIdPath, bool isReadOnly = false)
        {
            return await _pageRepository.GetsAsync(isReadOnly, x => x.IdPath.StartsWith($"{parentIdPath}."));
        }

        public async Task<List<Page>> GetListParentByChildrenIdPath(string childrenIdPath)
        {
            return await _pageRepository.GetsAsync(true, x => childrenIdPath.Contains(x.IdPath + "."));
        }

        public async Task<int> UpdateChildCount(int id)
        {
            var info = await GetInfo(id);
            if (info == null)
                return -1;

            var childCount = await _pageRepository.CountAsync(x => x.ParentId.HasValue && x.ParentId.Value == id);
            info.SetChildCount(childCount);
            var result = await Context.SaveChangesAsync();
            return result;
        }

        public async Task<int> Delete(int id)
        {
            var info = await GetInfo(id);
            if (info == null)
                return -1;
            info.RemoveAll();
            _pageRepository.Delete(info);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> ForceDelete(int id)
        {
            var info = await GetInfo(id);
            if (info == null)
                return -1;

            _pageRepository.Delete(info);
            return await Context.SaveChangesAsync();
        }
        public async Task<Page> GetInfo(int id, bool isReadOnly = false)
        {
            return await _pageRepository.GetAsync(isReadOnly, x => x.Id == id);
        }
        public async Task<int> UpdateActive(int id, bool isActive)
        {
            var info = await GetInfo(id);
            if (info == null)
                return -1;

            info.SetActive(isActive);
            return await Context.SaveChangesAsync();
        }

        public async Task<Page> GetInfo(int id, bool? isReadOnly)
        {
            return await _pageRepository.GetAsync(isReadOnly ?? false, x => x.Id == id);
        }
    }
}
