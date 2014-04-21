using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateApp.Helpers
{
  public static class HtmlHelperExtensions
  {
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