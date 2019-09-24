using System;

namespace ClientGUI.Conversion
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
                    this.Page10Received?.Invoke(new PageArgs(data));
                    break;
                case "19":
                    this.Page19Received?.Invoke(new PageArgs(data));
                    break;
                case "50":
                    this.Page50Received?.Invoke(new PageArgs(data));
                    break;
            }
        }
    }

    public class PageArgs : EventArgs
    {
        public byte[] Data { get; private set; }

        public PageArgs(byte[] Data)
        {
            this.Data = Data;
        }
    }
}