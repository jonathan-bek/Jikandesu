using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Manga.Models.MangaDb.DataObjects;
using Jikandesu.Services;

namespace Jikandesu.Areas.Manga.Models.MangaDb
{
    public class PageGroupProvider : IPageGroupProvider
    {
        private readonly IJdCrud _crud;

        public PageGroupProvider(IJdCrud crud)
        {
            _crud = crud;
        }

        public async Task<List<PageGroup>> GetPageGroups(User user)
        {
            var links = await GetUserPageGroupLinks(user);
            var pageGroups = await GetPageGroups(links);
            var pageGroupMembers = await GetPageGroupMembers(pageGroups);
            AssignMembersToGroups(pageGroups, pageGroupMembers);
            return pageGroups;
        }

        private async Task<List<LinkUserPageGroup>> GetUserPageGroupLinks(User user)
        {
            const string where = "WHERE userId = @userId";
            using (_crud.GetOpenConnection())
            {
                var result = await _crud.GetListAsync<LinkUserPageGroup>(
                    where, user.UserId);
                return result.ToList();
            }
        }

        private async Task<List<PageGroup>> GetPageGroups(List<LinkUserPageGroup> links)
        {
            var pageGroupIds = links.Select(x => x.PageGroupId);
            const string where = "WHERE PageGroupId IN @pageGroupIds";
            using (_crud.GetOpenConnection())
            {
                var result = await _crud.GetListAsync<PageGroup>(
                    where, pageGroupIds);
                return result.ToList();
            }
        }

        private async Task<List<PageGroupMember>> GetPageGroupMembers(List<PageGroup> groups)
        {
            var pageGroupIds = groups.Select(x => x.PageGroupId);
            const string where = "WHERE pageGroupId IN @pageGroupIds";
            using (_crud.GetOpenConnection())
            {
                var result = await _crud.GetListAsync<PageGroupMember>(
                    where, pageGroupIds);
                return result.ToList();
            }
        }

        private void AssignMembersToGroups(
            List<PageGroup> groups,
            List<PageGroupMember> members)
        {
            var groupedMembers = members
                .GroupBy(x => x.PageGroupId)
                .ToDictionary(x => x.Key, x => x.ToList());
            groups.ForEach(x =>
            {
                x.PageGroupMembers = groupedMembers[x.PageGroupId];
            });
        }
    }
}