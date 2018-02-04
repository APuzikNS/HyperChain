using DatabaseHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DatabaseHelperTest
{
    
    
    /// <summary>
    ///This is a test class for WordItemTest and is intended
    ///to contain all WordItemTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WordItemTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for WordItem Constructor
        ///</summary>
        [TestMethod()]
        public void WordItemConstructorTest()
        {
            WordItem target = new WordItem();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CreateWordItem
        ///</summary>
        [TestMethod()]
        public void CreateWordItemTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            string word = string.Empty; // TODO: Initialize to an appropriate value
            WordItem expected = null; // TODO: Initialize to an appropriate value
            WordItem actual;
            actual = WordItem.CreateWordItem(id, word, (short)WordStatus.None, false);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Id
        ///</summary>
        [TestMethod()]
        public void IdTest()
        {
            WordItem target = new WordItem(); // TODO: Initialize to an appropriate value
            int expected = 3; // TODO: Initialize to an appropriate value
            int actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InMainDB
        ///</summary>
        [TestMethod()]
        public void InMainDBTest()
        {
            WordItem target = new WordItem(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.InMainDB = expected;
            actual = target.InMainDB;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LastTimeUpdate
        ///</summary>
        [TestMethod()]
        public void LastTimeUpdateTest()
        {
            WordItem target = new WordItem(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> expected = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> actual;
            target.StatusLastTimeUpdate = expected;
            actual = target.StatusLastTimeUpdate;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Status
        ///</summary>
        [TestMethod()]
        public void StatusTest()
        {
            WordItem target = new WordItem(); // TODO: Initialize to an appropriate value
            short expected = 0; // TODO: Initialize to an appropriate value
            short actual;
            target.Status = expected;
            actual = target.Status;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Word
        ///</summary>
        [TestMethod()]
        public void WordTest()
        {
            WordItem target = new WordItem(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Word = expected;
            actual = target.Word;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
