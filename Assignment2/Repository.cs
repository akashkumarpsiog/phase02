using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    public class Repository<T> where T: class, new()
    { //T means the class accepts a type where T must be a reference type and new() means the T must have a public parameterless constructor.

        private readonly List<T> _items = new(); //where repository stores objects.

        public void Add(T item)
        {
            _items.Add(item);
        }

        public List<T> GetAll()
        {
            return new List<T>(_items);
        }

        public bool Update(int index, T updatedItem)
        {
            if (index < 0 || index >= _items.Count)
            {
                return false;
            }
            _items[index] = updatedItem;
            return true;
        }

        public bool Delete(int index)
        {
            if (index < 0 || index >= _items.Count)
            {
                return false;
            }
            _items.RemoveAt(index);
            return true;
        }

        public T CreateNew()
        {
            //This creates an empty object of whichever type T represents.
            return new T();
            //if we didnt have the new() above, then if the T here is a class with a constructor it wont work. thats why we have new().
        }
    }
}
