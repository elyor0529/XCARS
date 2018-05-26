using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XCars.Helpers
{
    public static class MyHelpers
    {
        public static MvcHtmlString Breadcrumbs(this HtmlHelper html, Dictionary<string, string> breadcrumbs)
        {
            if (breadcrumbs == null)
                return new MvcHtmlString("");

            //TagBuilder divMain = new TagBuilder("div");
            //divMain.Attributes["id"] = "breadcrumb";

            //TagBuilder divContainer = new TagBuilder("div");
            //divContainer.AddCssClass("container");

            //TagBuilder divRow = new TagBuilder("div");
            //divRow.AddCssClass("row");

            //TagBuilder divCol = new TagBuilder("div");
            //divCol.AddCssClass("col-md-12");

            //return new MvcHtmlString(div.ToString());



            TagBuilder br = new TagBuilder("br");
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("breadcrumb");
            foreach (KeyValuePair<string, string> item in breadcrumbs)
            {
                TagBuilder li = new TagBuilder("li");
                if (item.Key != "#")
                {
                    TagBuilder a = new TagBuilder("a");
                    a.Attributes["href"] = item.Key;
                    a.SetInnerText(item.Value);
                    li.InnerHtml += a.ToString();
                }
                else
                    li.SetInnerText(item.Value);
                
                ul.InnerHtml += li.ToString();
            }
            return new MvcHtmlString(br.ToString() +  ul.ToString());
        }
    }
}