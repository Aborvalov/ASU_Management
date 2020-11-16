namespace DalContract
{
    public interface IContractEntitiesDAO<TEntities> : IGetEntitiesDAO<TEntities>
    {
        int Add(TEntities entity);
        bool Remove(int id);
        bool Update(TEntities entity);
    }
}
