using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data
{
    public static class MvcExtension
    {
        public static ActionResult ToMvcJson(this object obj)
        {
            //return newtonsoft.Json.JsonConvert.SerializeObject(obj);
            ContentResult con = new ContentResult();
            con.Content = JsonConvert.SerializeObject(obj);
            return con;
        }
        public static ActionResult ToMvcJson(this object obj, Newtonsoft.Json.Formatting formatting, JsonSerializerSettings settings)
        {
            ContentResult con = new ContentResult();
            con.Content = JsonConvert.SerializeObject(obj, formatting, settings);
            return con;
        }
        public static ActionResult ToMvcJson(this object obj, string DateTimeFormats = "yyyy-MM-dd HH:mm:ss")
        {
            ContentResult con = new ContentResult();
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = DateTimeFormats };
            con.Content = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, timeConverter);
            return con;
        }
    }
}
