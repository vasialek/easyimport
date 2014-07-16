using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyImportTest
{
    [TestClass]
    public class MiscTest
    {
        [TestMethod]
        public void Test_Price_To_Minor()
        {
            decimal p = 12;
            string expected = "1200";
            var import = new DataImportTst();

            string r = import.TestGetPriceAsMinor(p);
            Assert.AreEqual(expected, r, "Expected: " + expected + ", got: " + r);

            p = 12.34m;
            expected = "1234";
            r = import.TestGetPriceAsMinor(p);
            Assert.AreEqual(expected, r, "Expected: " + expected + ", got: " + r);
        }

        [TestMethod]
        public void Test_Zero_Price_To_Minor()
        {
            decimal p = 0;
            string expected = "0";
            var import = new DataImportTst();

            string r = import.TestGetPriceAsMinor(p);
            Assert.AreEqual(expected, r, "Expected: " + expected + ", got: " + r);
        }
    }

}
