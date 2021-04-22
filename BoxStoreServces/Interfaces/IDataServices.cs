using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxStoreServices
{
    public interface IDataServices
    {
        void AddBox(BoxStoreModels.Box box);
        Task AddBoxAsync(BoxStoreModels.Box box);
        BoxStoreModels.Box RemoveBoxById(int id);
        Task<BoxStoreModels.Box> RemoveBoxByIdAsync(int id);
        BoxStoreModels.Box GetBoxById(int id);
        IEnumerable<BoxStoreModels.Box> GetBoxes();
        Task<BoxStoreModels.Box> GetBoxByIdAsync(int id);
        Task<IEnumerable<BoxStoreModels.Box>> GetBoxesAsync();
        void Save();
        void Close();
    }
}
