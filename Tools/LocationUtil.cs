using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tools
{
    /// <summary>
    /// 用经纬度计算两点间的距离
    /// </summary>
    public class LocationUtil
    {
        private const double EARTH_RadIUS = 6378.137;//地球半径
        private const string BaiduMapGeocoderUrl = "http://api.map.baidu.com/geocoder";

        public class LocationStatus
        {
            public string status { get; set; }
            public LocationReset result { get; set; }
        }

        public class LocationReset
        {
            public Location location { get; set; }
            public long precise { get; set; }
            public long confidence { get; set; }
            public string level { get; set; }
        }

        public class Location{

            public double lng{get;set;}
            public double lat{get;set;}
        }

        private static double Rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        /// <summary>
        /// 计算两点之间的距离KM
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lng1"></param>
        /// <param name="lat2"></param>
        /// <param name="lng2"></param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double RadLat1 = Rad(lat1);
            double RadLat2 = Rad(lat2);
            double a = RadLat1 - RadLat2;
            double b = Rad(lng1) - Rad(lng2);

            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(RadLat1) * Math.Cos(RadLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RadIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }

        /// <summary>
        /// 根据地址获取坐标
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public static Location GetLocation(string addr)
        {
            LocationStatus ls = null;
            string getStr = HttpUtil.HttpGet(BaiduMapGeocoderUrl, "output=json&address=" + addr);
            ls = JsonUtil.parse<LocationStatus>(getStr);
            if (ls != null && ls.result != null && ls.result.location!=null)
            {
                return ls.result.location;
            }else{
                return null;
            }
        }
    }
}