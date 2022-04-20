﻿using Microsoft.AspNetCore.Components;
using SharpSplash.Blog.UI.Models;
using SharpSplash.Blog.UI.Services;

namespace SharpSplash.Blog.UI.Pages
{
    public partial class Index
    {
        [Inject] public ICosmicService CosmicService { get; set; }

        [Parameter] public int Page { get; set; }

        private AllPosts _allPosts = new();

        private bool _loading;
        private bool _noMorePosts;
        private const int AmountOfPostPerPage = 5;

        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            
            _allPosts = await CosmicService.GetPosts(AmountOfPostPerPage, Page);

            _loading = false;
            
            StateHasChanged();
        }

        private async Task OlderClick()
        {
            Page += 1;

            await GetPosts();
            
            StateHasChanged();
        }
        
        private async Task RecentClick()
        {
            if(Page == 0)
                return;

            Page -= 1;

            await GetPosts();

            StateHasChanged();
        }

        private async Task GetPosts()
        {
            _loading = true;

            _allPosts = await CosmicService.GetPosts(AmountOfPostPerPage, Page);

            _noMorePosts = (Page + 1) * AmountOfPostPerPage >= _allPosts.Total;

            _loading = false;

            StateHasChanged();
        }
    }
}