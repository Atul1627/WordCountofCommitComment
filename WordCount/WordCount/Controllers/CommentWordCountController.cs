using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WordCount.Models.CommentWordCount;

namespace WordCount.Controllers
{
    public class CommentWordCountController : Controller
    {
        CommentWordCountModelEngine _modelEngine;
        
        public CommentWordCountController()
        {
            _modelEngine = new CommentWordCountModelEngine();            
        }
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCommentWordData(string userName,string accessToken, string gheRepoURL)
        {            
            VMCommentWordCount data = new VMCommentWordCount();
            try
            {
                // Prepare Ajax JSON Data Result.  
                data = _modelEngine.GetWordCount(userName, accessToken, gheRepoURL);
                TempData["SortedData"] = Newtonsoft.Json.JsonConvert.SerializeObject(data.SortedWordCount);
            }
            catch (Exception ex)
            {
                data.ErrorMessage = ex.Message;
            }
            // Return info.  
            return this.Json(JsonConvert.SerializeObject(data), JsonRequestBehavior.AllowGet);

        }

        public FileResult Export(string userName, string accessToken, string gheRepoURL)
        {
            List<KeyValuePair<string, int>> exportData = new List<KeyValuePair<string, int>>();
            StringBuilder sb = new StringBuilder();
            try
            {
                if (TempData["SortedData"].ToString() != null)
                {
                    exportData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(TempData["SortedData"].ToString());
                    //exportData = (<List<KeyValuePair<string, int>>>)TempData["SortedData"];
                }
                else
                {
                    VMCommentWordCount data = new VMCommentWordCount();
                    data = _modelEngine.GetWordCount(userName, accessToken, gheRepoURL);
                    exportData = data.SortedWordCount;
                }                
                sb.Append("Word,Occurrence Count");
                sb.Append("\r\n");
                foreach (var expData in exportData)
                {
                    //Append data with separator.
                    sb.Append(expData.Key + "," + expData.Value);

                    //Append new line character.
                    sb.Append("\r\n");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "WordCount.csv");
        }
    }
}