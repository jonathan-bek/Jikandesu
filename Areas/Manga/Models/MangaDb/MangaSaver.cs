﻿using System;
using System.Threading.Tasks;
using Jikandesu.Services;

namespace Jikandesu.Areas.Manga.Models.MangaDb
{
    public class MangaSaver : IMangaSaver
    {
        private readonly IJdCrud _crud;

        public MangaSaver(IJdCrud crud)
        {
            _crud = crud;
        }

        public async Task<int> SaveMangaPage(MangaPage page)
        {
            using (_crud.GetOpenConnection())
            {
                var id = await _crud.InsertAsync(page);
                return id ?? throw new Exception("Failed to insert manga page.");
            }
        }
    }
}