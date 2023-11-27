using Core.Persistance.Repositories;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract;

public interface ICategoryRepository : IEntityRepository<Category,int>
{

}
