using RmsSensorReader;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var datastore = new DataStore("");
            await datastore.StoreReading(10.1m, 0.22m);
        }
    }
}