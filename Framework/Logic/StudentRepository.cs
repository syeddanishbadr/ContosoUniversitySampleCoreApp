using Framework.Core;
using Framework.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Framework.Logic
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(DataBaseContext context) : base(context)
        {
        }

        public override async Task<Student> GetByIdAsnyc(int id, bool noTracking = false)
        {
            return
                noTracking
                ?
                await
                _context.Students
                    .Include(i => i.Enrollments)
                        .ThenInclude(i => i.Course)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(i => i.ID == id)
                :
                await
                _context.Students
                    .Include(i => i.Enrollments)
                        .ThenInclude(i => i.Course)
                    .SingleOrDefaultAsync(i => i.ID == id);
        }
    }
}
