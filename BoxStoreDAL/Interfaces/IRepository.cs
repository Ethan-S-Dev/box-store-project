using System.Collections.Generic;

namespace BoxStoreDAL
{
    public interface IRepository
    {
        void AddBox(BoxStoreModels.Box box);
        BoxStoreModels.Box RemoveBoxById(int id);
        IEnumerable<BoxStoreModels.Box> GetBoxes();
        BoxStoreModels.Box GetBoxById(int id);
        void Save();
        void Close();
    }
}
