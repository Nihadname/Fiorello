using Microsoft.AspNetCore.Mvc;
using WebApplication11.Data;
using WebApplication11.Models;


public class BlogViewComponent: ViewComponent 
    {
    private readonly FiorelloDbContext fiorelloDbContext;
    public BlogViewComponent(FiorelloDbContext context)
    {
        fiorelloDbContext = context;
    }
    public async Task<IViewComponentResult> InvokeAsync(int take=3)
    {
        ICollection<Blog> blogs=fiorelloDbContext.blogs.Take(take).ToList();
        return  View( await Task.FromResult(blogs));

    }

    }

