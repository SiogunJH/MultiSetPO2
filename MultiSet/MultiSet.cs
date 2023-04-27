using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMultiSet;

namespace MultiSet
{
    public class MultiSet<T> : IMultiSet<T> where T : notnull
    {
        #region Class Variables

        public Dictionary<T, int> dict;

        #endregion

        #region ICollection Implementation
        public int Count
        {
            get
            {
                int total = 0;

                foreach (var item in dict)
                    total += item.Value;

                return total;
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return false; //TODO
            }
        }
        public void Add(T item)
        {
            if (IsReadOnly) throw new NotSupportedException("This MultiSet is of readonly type");
            if (Contains(item)) dict[item]++;
            else dict.Add(item, 1);
        }
        public bool Remove(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();
            if (Contains(item))
            {
                if (dict[item]==1) dict.Remove(item);
                else dict[item]--;
                return true;
            }
            else return false;
        }
        public bool Contains(T item) => dict.ContainsKey(item);
        public void Clear()
        {
            if (IsReadOnly) throw new NotSupportedException();
            dict.Clear();
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            T[] tempArray = ToArray();
            int i = 0;

            while (arrayIndex < array.Length && i < Count && i < array.Length)
            {
                array[arrayIndex+i] = tempArray[i];
                i++;
            }
        }
        #endregion

        #region IEnumerable Implementation
        public IEnumerator<T> GetEnumerator() 
        {
            foreach (var item in dict)
                for (int i = 0; i < item.Value; i++)
                    yield return item.Key;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion

        #region MultiSet Specific
        public MultiSet<T> Add(T item, int numberOfTimes = 1)
        {
            if (IsReadOnly) throw new NotSupportedException("This MultiSet is of readonly type");
            if (Contains(item)) dict[item]+=numberOfTimes;
            else dict.Add(item, numberOfTimes);
            return this;
        }
        public MultiSet<T> Remove(T item, int numberOfTimes = 1)
        {
            if (IsReadOnly) throw new NotSupportedException();
            if (Contains(item))
            {
                if (dict[item] <= numberOfTimes) dict.Remove(item);
                else dict[item] -= numberOfTimes;
            }
            return this;
        }
        public MultiSet<T> RemoveAll(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();
            if (Contains(item)) dict.Remove(item);
            return this;
        }
        public MultiSet<T> UnionWith(IEnumerable<T> other)
        {
            if (IsReadOnly) throw new NotSupportedException();
            if (other is null) throw new ArgumentNullException();
            foreach (var item in other) Add(item);
            return this;
        }
        public MultiSet<T> IntersectWith(IEnumerable<T> other)
        {
            if (IsReadOnly) throw new NotSupportedException();
            if (other is null) throw new ArgumentNullException();
            var tempMultiSet = new MultiSet<T>();
            foreach (var item in other)
            {
                if (Contains(item))
                {
                    tempMultiSet.Add(item);
                    Remove(item);
                }
            }
            dict = tempMultiSet.dict;
            return this;
        }
        public MultiSet<T> ExceptWith(IEnumerable<T> other)
        {
            if (IsReadOnly) throw new NotSupportedException();
            if (other is null) throw new ArgumentNullException();
            foreach (var item in other) 
                if (Contains(item)) Remove(item);
            return this;
        }
        public MultiSet<T> SymmetricExceptWith(IEnumerable<T> other)
        {
            if (IsReadOnly) throw new NotSupportedException();
            if ((other is null)) throw new ArgumentNullException();
            var tempMultiSet = new MultiSet<T>();
            foreach(var item in other)
            {
                if (Contains(item)) Remove(item);
                else Add(item);
            }
            dict = tempMultiSet.dict;
            return this;
        }
        
        #endregion

        #region ToOtherType
        public T[] ToArray()
        {
            T[] result = new T[Count];
            int i = 0;

            foreach (var item in dict)
            {
                for (int ii=0; ii<item.Value; ii++)
                {
                    result[i] = item.Key;
                    i++;
                }
            }

            return result;
        }
        #endregion

        #region Static Methods
        public static MultiSet<T> Empty
        {
            get => new();
        }
        #endregion

        #region Constructors
        public MultiSet()
        {
            dict = new();
        }
        #endregion
    }
}
