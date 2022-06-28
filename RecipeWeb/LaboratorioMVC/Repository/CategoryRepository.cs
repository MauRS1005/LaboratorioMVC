using LaboratorioMVC.Data;
using LaboratorioMVC.Models;
using LaboratorioMVC.Repository.IRepository;

namespace LaboratorioMVC.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            _db.Update(category);
        }
    }
}
