using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FietsDemo.Core.Conversion
{
    class PageConversion
    {
        public byte Page { get; private set; }

        public event PageHandler Page10Received;
        public event PageHandler Page19Received;
        public event PageHandler Page50Received;
        public delegate void PageHandler(PageArgs args);

        public void RegisterData(byte[] data)
        {
            string value = data[0].ToString("X");
            switch (value)
            {
                case "10":
                    this.Page10Received?.Invoke(new PageArgs(value));
                    break;
                case "19":
                    this.Page19Received?.Invoke(new PageArgs(value));
                    break;
                case "50":
                    this.Page50Received?.Invoke(new PageArgs(value));
                    break;
            }
        }
    }

    public class PageArgs : EventArgs
    {
        public string Page { get; private set; }

        public PageArgs(string page)
        {
            this.Page = page;
        }
    }
}