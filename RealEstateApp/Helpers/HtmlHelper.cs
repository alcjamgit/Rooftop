using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RealEstateApp.Helpers
{
  public static class HtmlHelperExtensions
  {
    //http://stackoverflow.com/questions/1799370/getting-attributes-of-enums-value
    //this will get enum attribute description
    public static Expected GetAttributeValue<T, Expected>(this Enum enumeration, Func<T, Expected> expression)
    where T : Attribute
    {
      T attribute = enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();

      if (attribute == null)
        return default(Expected);

      return expression(attribute);
    }


    public static IHtmlString EnumToDropDownOptions(this HtmlHelper htmlHelper, RealEstateApp.ViewModels.SortOrder sortOrderEnum)
    { 
        StringBuilder sb = new StringBuilder();
          int valInt;
          string activeAttribute;
          foreach (RealEstateApp.ViewModels.SortOrder val in Enum.GetValues(typeof(RealEstateApp.ViewModels.SortOrder)))
          {
            if (sortOrderEnum == val) { 
              activeAttribute = "selected='selected'";
            }
            else
            {
              activeAttribute = "";
            }
            valInt = (int)val;
            sb.Append(string.Format("<option value={0} {2} >{1}</option>", valInt, val, activeAttribute));
          }
      return htmlHelper.Raw(sb.ToString());
    }

    public static MvcHtmlString Paginator(this HtmlHelper htmlHelper, int PageSize, int resultCount)
    {
            //      <ul class="pagination margin-less">
            //    <li><a href="#">&laquo;</a></li>
            //    <li class="active"><a href="#">1<span class="sr-only">(current)</span></a></li>
            //    @for (var i = 2; i < 4; i++)
            //    {
            //        <li><a href="#">@i.ToString() <span class="sr-only">(current)</span></a></li>
            //    }
            //    <li><a href="#">&raquo;</a></li>
            //</ul>
      int pageCount = 4;
      var sb = new StringBuilder("<ul class='pagination margin-less'>");
      sb.Append(string.Format("<li><a href='{0}'>&laquo;</a></li>","#"));
      for (int i = 1; i <= pageCount; i++)
      {
        sb.Append(string.Format("<li value={1} class='pagination-item'><a href='{0}'>{1}<span class='sr-only'>{2}</span></a></li>", '#', i, "(current)"));
      }

      sb.Append(string.Format("<li><a href='{0}'>&raquo;</a></li>", "#"));
      sb.Append("</ul>");

      return new MvcHtmlString(sb.ToString());
    }

    public static MvcHtmlSurrounder BeginSurrounder(this HtmlHelper htmlHelper, string htmlTag)
    {
      //MvcDiv is a custom class
      return new MvcHtmlSurrounder(htmlHelper.ViewContext, htmlTag);
    }
    public static MvcHtmlSurrounder BeginSurrounder(this HtmlHelper htmlHelper, string htmlTag, string htmlClassName)
    {
      //MvcDiv is a custom class
      return new MvcHtmlSurrounder(htmlHelper.ViewContext, htmlTag, htmlClassName);
    }


  }

  public class MvcHtmlSurrounder : IDisposable
  {
    private bool _disposed;
    private readonly FormContext _originalFormContext;
    private readonly ViewContext _viewContext;
    private readonly TextWriter _writer;


    private string _htmlTag;
    private string _htmlClassName;
    private bool _writeStartTag = true;
    private bool _writeEndTag = true;

    public MvcHtmlSurrounder(ViewContext viewContext, string htmlTag)
    {
      if (viewContext == null)
      {
        throw new ArgumentNullException("viewContext");
      }

      _viewContext = viewContext;
      _writer = viewContext.Writer;
      _originalFormContext = viewContext.FormContext;
      viewContext.FormContext = new FormContext();
      _htmlTag = htmlTag;
      Begin();
    }

    public MvcHtmlSurrounder(ViewContext viewContext,string htmlTag, string htmlClassName)
      :this(viewContext,htmlTag)
    {
      _htmlClassName = htmlClassName;
    }

    public MvcHtmlSurrounder(ViewContext viewContext, string htmlTag, string htmlClassName, bool writeStartTag, bool writeEndTag)
      : this(viewContext, htmlTag, htmlClassName)
    {
      _writeStartTag = writeStartTag;
      _writeEndTag = writeEndTag;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    public void Begin()
    {
      if (_writeStartTag) 
        {_writer.Write("<{0}>", _htmlTag );}
    }

    private void End()
    {
      if (_writeEndTag)
      {
        _writer.Write("</{0}>", _htmlTag);
      }
      
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposed)
      {
        _disposed = true;
        End();

        if (_viewContext != null)
        {
          _viewContext.OutputClientValidation();
          _viewContext.FormContext = _originalFormContext;
        }
      }
    }

    public void EndForm()
    {
      Dispose(true);
    }
  }
}