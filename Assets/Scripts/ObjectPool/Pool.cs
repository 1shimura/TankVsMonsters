using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Scripts
{
    public class Pool<T> : IEnumerable where T : IResettable
    {
        public List<T> Members = new List<T>();
        public HashSet<T> Unavailable = new HashSet<T>();
        
        private IFactory<T> _factory;

        public Pool(IFactory<T> factory, int poolSize)
        {
            _factory = factory;

            for (var i = 0; i < poolSize; i++)
            {
                Create();
            }
        }

        public T Allocate()
        {
            foreach (var item in Members.Where(item => !Unavailable.Contains(item)))
            {
                Unavailable.Add(item);
                item.PrewarmSetup();
                return item;
            }

            var newMember = Create();
            Unavailable.Add(newMember);
            return newMember;
        }

        public void Release(T member)
        {
            member.Reset();
            Unavailable.Remove(member);
        }

        private T Create()
        {
            var member = _factory.Create();
            Members.Add(member);
            return member;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Members.GetEnumerator();
        }
    }
}