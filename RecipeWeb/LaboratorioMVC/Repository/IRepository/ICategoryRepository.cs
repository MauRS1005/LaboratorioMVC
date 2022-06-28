using LaboratorioMVC.Models;

namespace LaboratorioMVC.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
