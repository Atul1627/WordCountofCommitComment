using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WordCount.Models.CommentWordCount
{
    public class CommentWordCountModelEngine
    {
        TempDataDictionary tempData;
        public CommentWordCountModelEngine()
        {
            this.tempData = new TempDataDictionary();
        }

        public VMCommentWordCount GetWordCount()
        {
            VMCommentWordCount VMCommentWordCount = new VMCommentWordCount();
            try
            {
                List<string> gitCommits = GetGITCommits();
                foreach (string comment in gitCommits)
                {
                    var regex = new Regex(@"\b[\s,\.-:;]*"); //This works even if you have ".,; tabs and new lines" between your words.             
                    List<string> words = regex.Split(comment).Where(x => !string.IsNullOrEmpty(x)).ToList();
                    AVLTree<string> commentAVL = new AVLTree<string>();
                    //Storing the words in AVL Tree and the unsorted counts of words.
                    foreach (string word in words)
                    {
                        //duplicates are not allowed in AVL Tree
                        if (!commentAVL.Search(word))
                        {
                            commentAVL.Add(word);
                        }
                        //Adding each word and their occurence count
                        bool keyExists = VMCommentWordCount.WordkeyValuePairs.ContainsKey(word);
                        if (keyExists)
                        {
                            VMCommentWordCount.WordkeyValuePairs[word] += 1;
                        }
                        else
                        {
                            VMCommentWordCount.WordkeyValuePairs.Add(word, 1);
                        }

                    }
                    VMCommentWordCount.AllAVLTrees.Add(commentAVL);
                }

                //Sort the word count according to the ASCII codes.
                List<KeyValuePair<string, int>> wordCountList = VMCommentWordCount.WordkeyValuePairs.ToList();
                List<string> sortedkeyList = BubbleSortKeys(wordCountList.Select(X => X.Key).ToList());
                foreach (string key in sortedkeyList)
                {
                    VMCommentWordCount.SortedWordCount.Add(new KeyValuePair<string, int>(key, Convert.ToInt32(VMCommentWordCount.WordkeyValuePairs[key])));
                }

                //foreach (var AVL in VMCommentWordCount.AllAVLTrees)
                //{
                //    Action<string> action = GetNode;
                //    //AVL.PreOrderTraversal(action);
                //    int i = 0;
                //    //if (tempData["NodeValue"] != null)
                //    //{
                //    //    string AVLMsg = "Node " + Convert.ToString(i) + " " + tempData["NodeValue"].ToString();
                //    //    VMCommentWordCount.AVLMessages.Add(AVLMsg);
                //    //}
                //    //AVL.PostOrderTraversal(action);
                //    //if (tempData["NodeValue"] != null)
                //    //{
                //    //    string AVLMsg = "Node " + Convert.ToString(i) + " " +  tempData["NodeValue"].ToString();
                //    //    VMCommentWordCount.AVLMessages.Add(AVLMsg);
                //    //}
                //    AVL.InOrderTraversal(action);
                //    if (tempData["NodeValue"] != null)
                //    {
                //        string AVLMsg = "Node " + Convert.ToString(i) + " " + tempData["NodeValue"].ToString();
                //        VMCommentWordCount.AVLMessages.Add(AVLMsg);
                //    }
                //    i = i + 1;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return VMCommentWordCount;
        }

        public void GetNode(string value)
        {

            tempData["NodeValue"] = value;
        }

        private List<string> GetGITCommits()
        {
            List<string> GitCommits = new List<string>();
            string commit = string.Empty;
            try
            {
                commit = "Creating a project for testing. Initail commit testing";
                GitCommits.Add(commit);
                commit = "Adding commit for testing. Second commit testing";
                GitCommits.Add(commit);
                commit = "Adding commit for testing. Third commit testing";
                GitCommits.Add(commit);
                commit = "Adding commit for testing. Fourth commit testing";
                GitCommits.Add(commit);
                commit = "Adding commit for testing. Fifth commit testing";
                GitCommits.Add(commit);
                commit = "Testing AVL Tree Insertion. Sixth Commit testing";
                GitCommits.Add(commit);
                commit = "Testing bubble sort for testing. Seventh Commit testing";
                GitCommits.Add(commit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GitCommits;
        }
        private List<string> BubbleSortKeys(List<string> keyLists)
        {
            string temp;
            for (int i = 0; i < keyLists.Count(); i++)
            {
                for (int j = 0; j < keyLists.Count() - 1; j++)
                {
                    if ((int)keyLists[j].ToCharArray().Take(1).Single() > (int)keyLists[j + 1].ToCharArray().Take(1).Single())
                    {
                        temp = keyLists[j];
                        keyLists[j] = keyLists[j + 1];
                        keyLists[j + 1] = temp;
                    }
                }
            }
            return keyLists;
        }
    }
}