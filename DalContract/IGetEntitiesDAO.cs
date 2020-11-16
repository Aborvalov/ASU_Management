using System.Collections.Generic;

namespace DalContract
{
    public interface IGetEntitiesDAO<TEntities>
    {
        List<TEntities> GeatAll();
        TEntities GetById(int id);
    }
}
