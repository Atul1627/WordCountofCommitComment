using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WordCount.Models.CommentWordCount;

namespace UnitTestWordCount
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            string userName = "ATUL1627";
            string token = "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";
            string repoURL = "https://github.com/Atul1627/WordCountofCommitComment";
            CommentWordCountModelEngine _model = new CommentWordCountModelEngine();

            //Act
            var result = _model.GetWordCount(userName, token, repoURL);

            //Assert
            Assert.AreEqual(18, result.SortedWordCount.Count);
        }
        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            string userName = "ATUL1627";
            string token = "btrnrnyjtybyewtwreetew tet";
            string repoURL = "https://github.com/Atul1627/WordCountofCommitComment";
            CommentWordCountModelEngine _model = new CommentWordCountModelEngine();

            //Act
            var result = _model.GetWordCount(userName, token, repoURL);

            //Assert
            Assert.AreEqual(null, result.AllAVLTrees);
        }

    }
}
