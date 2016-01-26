using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Tools
{
    public class StreamUtil
    {
        public static string StreamToStr(Stream stream)
        {
            return StreamToStr(stream, Encoding.UTF8);
        }
        public static string StreamToStr(Stream stream, Encoding encoding)
        {
            string retVal = encoding.GetString(StreamToByte(stream));

            return retVal;
        }

        public static byte[] StreamToByte(Stream stream)
        {
            byte[] requestByte = new byte[stream.Length];
            stream.Read(requestByte, 0, (int)stream.Length);

            return requestByte;
        }
    }
}