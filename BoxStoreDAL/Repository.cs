using BoxStoreModels;
using System.Collections.Generic;

namespace BoxStoreDAL
{
    public class Repository : IRepository
    {
        public void AddBox(Box box) => data.Boxes.Add(box);
        public Box GetBoxById(int id) => data.Boxes.Find(id);
        public IEnumerable<Box> GetBoxes() => data.Boxes;
        public void Save() => data.SaveChanges();
        public void Close() => data.Dispose();
        public Box RemoveBoxById(int id) => data.Boxes.Remove(GetBoxById(id));
        
        readonly DataContext data = new DataContext();
    }
}
