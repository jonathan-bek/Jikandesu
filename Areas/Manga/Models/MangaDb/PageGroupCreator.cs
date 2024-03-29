﻿using System;
using System.Data;
using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Manga.Models.MangaDb.DataObjects;
using Jikandesu.Services;

namespace Jikandesu.Areas.Manga.Models.MangaDb
{
    public class PageGroupCreator
    {
        private readonly IJdCrud _crud;

        public PageGroupCreator(IJdCrud crud)
        {
            _crud = crud;
        }

        public async Task CreateDefaultGroup(MangaPage page, User user)
        {
            var newGroup = new PageGroup { PageGroupId = Guid.NewGuid() };
            SetGroupDefaultInfo(page, newGroup);
            var userGroupLink = CreateLinkFromUserToGroup(user, newGroup);
            var groupPageLink = CreateLinkFromGroupToPage(page, newGroup);
            await InsertAll(newGroup, userGroupLink, groupPageLink);
        }

        private async Task InsertAll(PageGroup group,
            LinkUserPageGroup userGroupLink,
            PageGroupMember groupPageLink)
        {
            using (var con = _crud.GetOpenConnection())
            using (var trn = con.BeginTransaction())
            {
                try
                {
                    await _crud.InsertAsync(group, trn);
                    await _crud.InsertAsync(groupPageLink, trn);
                    await _crud.InsertAsync(userGroupLink, trn);
                    trn.Commit();
                }
                catch
                {
                    if (con.State == ConnectionState.Open)
                    {
                        trn.Rollback();
                    }
                }
            }
        }

        private void SetGroupDefaultInfo(MangaPage page, PageGroup group)
        {
            group.Name = page.Title;
            group.ThumbnailImageUrl = page.ImageUrl;
        }

        private LinkUserPageGroup CreateLinkFromUserToGroup(User user, PageGroup group)
        {
            return new LinkUserPageGroup
            {
                UserId = user.UserId,
                PageGroupId = group.PageGroupId
            };
        }

        private PageGroupMember CreateLinkFromGroupToPage(MangaPage page, PageGroup group)
        {
            return new PageGroupMember
            {
                PageGroupId = group.PageGroupId,
                PageId = page.Id
            };
        }
    }
}