using System;
using System.Collections.Generic;
using System.Text;

namespace TestOrionTek.ApiSettings
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public class UrlAttribute : Attribute
    {
        public string Url { get; }

        public UrlAttribute(string url)
        {
            Url = url;
        }
    }
}
