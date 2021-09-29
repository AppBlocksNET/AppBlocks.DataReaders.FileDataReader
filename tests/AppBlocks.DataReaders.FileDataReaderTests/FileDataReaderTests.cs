using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppBlocks.DataReaders.FileDataReaderTests
{
    [TestClass]
    public class FileDataReaderTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var path = ".\\data\\ItemChildrenDeserializationTest.json";
            var reader = new FileDataReader(path);
            Assert.IsNotNull(reader);
        }
    }
}
