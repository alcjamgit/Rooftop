using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RealEstateApp.ExtensionMethods
{
  public static class HtmlHelpers
  {
    public static MvcHtmlString Paginator(this HtmlHelper htmlHelper, int PageSize = 20, int resultCount = 1)
    {

      int pageCount = (int)(Math.Ceiling(resultCount / Convert.ToDouble(PageSize)) * PageSize) / PageSize;
      var sb = new StringBuilder("<ul class='pagination margin-less'>");
      sb.Append(string.Format("<li><a href='{0}'>&laquo;</a></li>", "#"));
      for (int i = 1; i <= pageCount; i++)
      {
        sb.Append(string.Format("<li value={1} class='pagination-item'><a href='{0}'>{1}<span class='sr-only'>{2}</span></a></li>", '#', i, "(current)"));
      }

      sb.Append(string.Format("<li><a href='{0}'>&raquo;</a></li>", "#"));
      sb.Append("</ul>");

      return new MvcHtmlString(sb.ToString());
    }
    public static MvcHtmlString EnumToDropDownOptions(this HtmlHelper htmlHelper, RealEstateApp.ViewModels.SortOrder sortOrderEnum)
    {
      StringBuilder sb = new StringBuilder();
      int valInt;
      string activeAttribute;
      foreach (RealEstateApp.ViewModels.SortOrder val in Enum.GetValues(typeof(RealEstateApp.ViewModels.SortOrder)))
      {
        if (sortOrderEnum == val)
        {
          activeAttribute = "selected='selected'";
        }
        else
        {
          activeAttribute = "";
        }
        valInt = (int)val;
        sb.Append(string.Format("<option value={0} {2} >{1}</option>", valInt, val, activeAttribute));
      }
      return new MvcHtmlString(sb.ToString());
    }


    public static MvcHtmlString RenderAdImages(this HtmlHelper htmlHelper, string userId, IEnumerable<RealEstateApp.Models.RealtyAdImage> realtyAdImages)
    {
      StringBuilder sb = new StringBuilder();
      string temp;
      foreach (var imageObj in realtyAdImages)
      {
        //VirtualPathUtility is same as Url.Content in MVC
        temp = VirtualPathUtility.ToAbsolute(string.Format("~/Content/images/{0}/{1}", userId, imageObj.FileName));
        temp = string.Format("<img src = '{0}' />", temp);
        sb.Append(temp);
      }
      //sb.Append("<img src='http://placehold.it/300x400' />");
      return new MvcHtmlString(sb.ToString());
    }
  }
}