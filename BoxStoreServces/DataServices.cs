using BoxStoreDAL;
using BoxStoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxStoreServices
{
    public class DataServices : IDataServices
    {
        public void AddBox(Box box) { data.AddBox(box); data.Save(); }
        public Task AddBoxAsync(Box box) => Task.Run(() => AddBox(box));

        public Box RemoveBoxById(int id) { Box ret = data.RemoveBoxById(id); data.Save(); return ret; }
        public Task<Box> RemoveBoxByIdAsync(int id) => Task.Run(() => RemoveBoxById(id));

        public Box GetBoxById(int id) => data.GetBoxById(id);
        public Task<Box> GetBoxByIdAsync(int id) => Task.Run(() => GetBoxById(id));

        public IEnumerable<Box> GetBoxes() => data.GetBoxes();
        public Task<IEnumerable<Box>> GetBoxesAsync() => Task.Run(GetBoxes);

        public void Close() { data.Save(); data.Close(); }
        public void Save() => data.Save();
        
        readonly IRepository data = new Repository();
    }
}

