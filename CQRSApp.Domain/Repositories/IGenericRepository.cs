using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRSApp.Repositories.Domain
{
    public interface IGenericRepository<Tentity> where Tentity : class
    {
        Tentity Add(Tentity entity);
        void Update(Tentity entity);
        void Delete(Tentity entity);

    }
}
