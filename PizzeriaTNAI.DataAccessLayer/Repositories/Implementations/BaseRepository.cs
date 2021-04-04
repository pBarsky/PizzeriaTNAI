using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaTNAI.Entities;

namespace PizzeriaTNAI.DataAccessLayer.Repositories.Implementations
{
    public class BaseRepository
    {
        protected readonly AppDbContext Context;

        internal BaseRepository()
        {
            Context = AppDbContext.Create();
        }
    }
}