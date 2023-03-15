using System.Collections;
using System.Collections.Generic;

namespace Architecture.Pools.ObjectsPool
{
    public class ObjectPool<T> : IEnumerator<T>, IEnumerable<T>
    {
        private List<T> values;

        public ObjectPool()
        {
            values = new List<T>();
        }

        public T this[int index]
        {
            get { return values[index]; }
        }

        public void AddObject(T _object)
        {
            if (!values.Contains(_object))
                values.Add(_object);
        }
        public void RemoveObject(T _object)
        {
            if (values.Contains(_object))
                values.Remove(_object);
        }

        private int position = -1;
        public T Current
        {
            get => values[position];
        }
        object IEnumerator.Current
        {
            get => values[position];
        }
        public void Dispose()
        {
            Reset();
        }
        public bool MoveNext()
        {
            if (position < values.Count - 1)
            {
                if (position == -1)
                    nullClean();

                position++;
                return true;
            }
            return false;
        }
        public void Reset() => position = -1;
        public IEnumerator<T> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => this;

        private void nullClean()
        {
            foreach (var item in values)
                if (item is null)
                    RemoveObject(item);
        }
    }
}
