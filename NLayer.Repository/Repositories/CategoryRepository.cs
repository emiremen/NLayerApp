using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> GetSingleCategoryWithProductsAsync(int CategoryId)
        {
            return await _dbContext.Categories.Include(x => x.Products).Where(x => x.Id == CategoryId).SingleOrDefaultAsync();
        }
    }
}
