using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordCount.Models.CommentWordCount
{
    public class VMCommentWordCount
    {
        public string ErrorMessage { get; set; }
        public bool ErrorFlag { get; set; }
        public List<string> AVLMessages { get; set; }
        public AVLTree<string> AllAVLTree { get; set; }
        public List<AVLTree<string>> AllAVLTrees { get; set; }
        public Dictionary<string, int> WordkeyValuePairs { get; set; }
        public List<KeyValuePair<string, int>> SortedWordCount { get; set; }
        public VMCommentWordCount()
        {
            this.AllAVLTree = new AVLTree<string>();
            this.AllAVLTrees = new List<AVLTree<string>>();
            this.AVLMessages = new List<string>();
            this.WordkeyValuePairs = new Dictionary<string, int>();
            this.SortedWordCount = new List<KeyValuePair<string, int>>();
        }
    }
}