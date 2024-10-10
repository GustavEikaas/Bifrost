using Bifrost.Read;

namespace Bifrost.WebApp.Read
{
    public class MyQueryable : IReadModel
    {
        public string Something { get; set; }
    }
    public class MyQuery : IQueryFor<MyQueryable>
    {
        public IQueryable<MyQueryable> Query
        {
            get
            {
                return new List<MyQueryable>
                {
                    new MyQueryable { Something = "Hello" },
                    new MyQueryable { Something = "World" }
                }.AsQueryable();
            }
        }
    }
}

