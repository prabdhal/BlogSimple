using BlogSimpleMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimpleMVC.BusinessManagers.Interfaces
{
    public interface IHomeBusinessManager
    {
        /// <summary>
        /// Returns the author details and all their published posts.
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        ActionResult<AuthorViewModel> GetAuthorViewModel(string authorId, string searchString, int? page);
    }
}
