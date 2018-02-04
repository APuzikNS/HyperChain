using SemanticLinkHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SemanticLinkHelper.ExplDictService;
using SemanticLinkHelper.WordFormsService;
using System.Collections.Generic;
using System.ServiceModel.Description;
using ExplEntities;
using SemanticLinkHelper.CommonDictService;

namespace SemanticLinkHelperTest
{
    
    
    /// <summary>
    ///This is a test class for SemanticHelperTest and is intended
    ///to contain all SemanticHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SemanticHelperTest
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
        ///A test for SemanticHelper Constructor
        ///</summary>
        [TestMethod()]
        public void SemanticHelperConstructorTest()
        {
            SemanticHelper target = new SemanticHelper();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CheckCredentials
        ///</summary>
        [TestMethod()]
        public void CheckCredentialsTest()
        {
            SemanticHelper target = new SemanticHelper(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CheckCredentials();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateChannel
        ///</summary>
        [TestMethod()]
        public void CreateChannelTest()
        {
            SemanticHelper target = new SemanticHelper(); // TODO: Initialize to an appropriate value
            ClientCredentials cred =  new ClientCredentials();
            cred.UserName.UserName = string.Empty;
            cred.UserName.Password = string.Empty;
            target.SetCredentials(cred);
            IExplDic actual;
            actual = target.CreateChannel();
            Assert.IsNotNull(actual);

            int n1 = actual.GetRowCount("КОС", true, new int[] { 1, 1000 }, false, 1);// ExplGetVolumes(1);
            int nQaunt = 0;
            int n2 = actual.Search(out nQaunt, "КОРЧОМ", true, string.Empty, true, new int[] { 1, 1000 }, false, 1);// ExplGetVolumes(1);
            int n = actual.ExplCheckReestr("КО#СА", 0, 1);
            n = actual.ExplCheckReestr("КО#СА", 1, 1);
            n = actual.ExplCheckReestr("КОСА", 0, 1);
            n = actual.ExplCheckReestr("КОСА", 1, 1);
            n = actual.ExplCheckReestr("КОСА#", 0, 1);
            n = actual.ExplCheckReestr("КОСА#", 1, 1);
            SemanticLinkHelper.ExplDictService.elList[] page1 = null;
            SemanticLinkHelper.ExplDictService.elList[] page2 = null;
            SemanticLinkHelper.ExplDictService.elList[] page3 = null;
            if (n != -1)
            {
                
                page1 = actual.SupplyPageOfData(n, 10, true, string.Empty, true, new int[] { 1 }, false, 1);
                page2 = actual.SupplyPageOfData(n, 10, true, string.Empty, false, new int[] { 1, 1000 }, false, 1);
                page3 = actual.SupplyPageOfData(1000, 10, true, string.Empty, false, new int[] { 1000, 5000 }, false, 1);
                page3 = actual.SupplyPageOfData(n2, 10, true, string.Empty, false, new int[] { 1000, 5000 }, false, 1);
                int a = 1;
            }
            n = actual.ExplCheckReestr("КОСА#", 2, 1);
            n = actual.ExplCheckReestr("КОСА#", 3, 1);
            n = actual.ExplCheckReestr("КОСА#", 4, 1);
            n = actual.ExplCheckReestr("КОСА#", 5, 1); 
            
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateWordFormsChannel
        ///</summary>
        [TestMethod()]
        public void CreateWordFormsChannelTest()
        {
            SemanticHelper target = new SemanticHelper(); // TODO: Initialize to an appropriate value
            Ilib5 expected = null; // TODO: Initialize to an appropriate value
            Ilib5 actual;
            actual = target.CreateWordFormsChannel();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetChildren
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SemanticLinkHelper.dll")]
        public void GetChildrenTest()
        {
            //SemanticHelper_Accessor target = new SemanticHelper_Accessor(); // TODO: Initialize to an appropriate value
            //string sParent = string.Empty; // TODO: Initialize to an appropriate value
            //string sAnyFormWord = string.Empty; // TODO: Initialize to an appropriate value
            //List<WordLink> expected = null; // TODO: Initialize to an appropriate value
            //List<WordLink> actual;
            //actual = target.GetChildren(sParent, sAnyFormWord, target.CreateChannel(), 0);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSemanticLink
        ///</summary>
        [TestMethod()]
        public void GetSemanticLinkTest()
        {
            SemanticHelper target = new SemanticHelper(); // TODO: Initialize to an appropriate value
            string sWord = string.Empty; // TODO: Initialize to an appropriate value
            Options SearchOption = null; // TODO: Initialize to an appropriate value
            string sParent = string.Empty; // TODO: Initialize to an appropriate value
            WordSemanticBranch expected = null; // TODO: Initialize to an appropriate value
            WordSemanticBranch actual;
            actual = target.GetSemanticLink(sWord, SearchOption);//, sParent);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSemanticLinkFromDB
        ///</summary>
        [TestMethod()]
        public void GetSemanticLinkFromDBTest()
        {
            SemanticHelper target = new SemanticHelper(); // TODO: Initialize to an appropriate value
            string sWord = string.Empty; // TODO: Initialize to an appropriate value
            WordSemanticBranch expected = null; // TODO: Initialize to an appropriate value
            WordSemanticBranch actual;
            actual = target.GetSemanticLinkFromDB(sWord);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsNoun
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SemanticLinkHelper.dll")]
        public void IsNounTest()
        {
            //SemanticHelper_Accessor target = new SemanticHelper_Accessor(); // TODO: Initialize to an appropriate value
            //string sLangDesc = string.Empty; // TODO: Initialize to an appropriate value
            //bool expected = false; // TODO: Initialize to an appropriate value
            //bool actual;
            //actual = target.IsNoun(sLangDesc);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsNoun
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SemanticLinkHelper.dll")]
        public void IsNounTest1()
        {
            //SemanticHelper_Accessor target = new SemanticHelper_Accessor(); // TODO: Initialize to an appropriate value
            byte btLangPartId = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            //actual = target.IsNoun(btLangPartId);
            //Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RemoveAccents
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SemanticLinkHelper.dll")]
        public void RemoveAccentsTest()
        {
            //SemanticHelper_Accessor target = new SemanticHelper_Accessor(); // TODO: Initialize to an appropriate value
            //string sOrig = string.Empty; // TODO: Initialize to an appropriate value
            //string expected = string.Empty; // TODO: Initialize to an appropriate value
            //string actual;
            //actual = target.RemoveAccents(sOrig);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SetCredentials
        ///</summary>
        [TestMethod()]
        public void SetCredentialsTest()
        {
            SemanticHelper target = new SemanticHelper(); // TODO: Initialize to an appropriate value
            ClientCredentials credentials = null; // TODO: Initialize to an appropriate value
            target.SetCredentials(credentials);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UpdateWordStatus
        ///</summary>
        [TestMethod()]
        public void UpdateWordStatusTest()
        {
            SemanticHelper target = new SemanticHelper(); // TODO: Initialize to an appropriate value
            RegistryWord aWord = null; // TODO: Initialize to an appropriate value
            WordStatus eStatus = new WordStatus(); // TODO: Initialize to an appropriate value
            RegistryWord expected = null; // TODO: Initialize to an appropriate value
            RegistryWord actual;
            actual = target.UpdateWordStatus(aWord, eStatus);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateCommonChannel
        ///</summary>
        [TestMethod()]
        public void CreateCommonChannelTest()
        {
            SemanticHelper target = new SemanticHelper(); // TODO: Initialize to an appropriate value

            ClientCredentials cred = new ClientCredentials();
            cred.UserName.UserName = "khnure";
            cred.UserName.Password = "415oy22w2z";
            //target.SetCredentials(cred);
 
            //IExplDic actual1;
            //actual1 = target.CreateChannel();
            //Assert.IsNotNull(actual1);
            //int n = actual1.ExplCheckReestr("КОСА#", 1, 1);


            try
            {
                target.SetCredentials2(cred);
                ICommonDic actual;
                actual = target.CreateCommonChannel();
                Assert.IsNotNull(actual);
                //int l = actual.LogUser(1, true);
                string[] s = actual.CheckMessages();
                //int[] lq = actual.GetUserIDsForLS(1, 0);
               // byte l = actual.GetUserRightsForLS(1);// LogUser(1, false);
                //Dictionary<byte, string> rights = actual.GetUserRights();
                //users_short[] users = actual.GetUsers();
            }
            catch (Exception ex)
            {
                int j = 0;
            }
            
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
