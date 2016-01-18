using EasyJob.Pojo.Pojo;
using EasyJob.Pojo.Pojo.Bases;
using NHibernate;
using NHibernate.Criterion;
using ORM.Hibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyJob.Tools
{
    public class PojoUtil
    {
        public delegate void OnSession(ISession session);
        public static void Exec(IHibernateOper hibernateOper, OnSession onSession)
        {
            ISession s = null;
            ITransaction trans = null;
            try
            {
                s = hibernateOper.GetCurrentSession();
                trans = s.BeginTransaction();

                onSession(s);
                
                trans.Commit();
            }
            catch (Exception e)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw e;
            }
        }


        /// <summary>
        /// 由OpenId得到客户
        /// </summary>
        /// <param name="hibernateOper"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static Customer GetCust(IHibernateOper hibernateOper, string openId)
        {
            Customer retVal = null;

            Exec(hibernateOper,
                delegate(ISession s){
                    //查找客户openId
                retVal = GetCust(s, openId);
                }
                );

            return retVal;
        }

        /// <summary>
        /// 由OpenId得到客户
        /// </summary>
        /// <param name="session"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static Customer GetCust(ISession session, string openId)
        {
            Customer retVal = null;

            ICriteria criteria = session.CreateCriteria(typeof(Customer));
            //模糊查找当前代码
            ICriterion criterion = Restrictions.Eq("OpenId", openId);
            criteria.Add(criterion);

            criterion = Restrictions.Eq("Del", false);
            criteria.Add(criterion);

            criteria.SetFirstResult(0);
            criteria.SetMaxResults(1);
            criteria.SetCacheable(true);

            retVal = criteria.UniqueResult<Customer>();

            return retVal;
        }

        public static Employee GetEmployee(IHibernateOper hibernateOper, string code)
        {
            Employee retVal = null;

            Exec(hibernateOper,
                delegate(ISession s)
                {
                    //查找员工工号
                    retVal = GetEmployee(s, code);
                }
                );

            return retVal;
        }

        public static Employee GetEmployee(ISession session, string code)
        {
            Employee retVal = null;

            ICriteria criteria = session.CreateCriteria(typeof(Employee));
            //模糊查找当前代码
            ICriterion criterion = Restrictions.Eq("Code", code);
            criteria.Add(criterion);

            criterion = Restrictions.Eq("Del", false);
            criteria.Add(criterion);

            criteria.SetFirstResult(0);
            criteria.SetMaxResults(1);
            criteria.SetCacheable(true);

            retVal = criteria.UniqueResult<Employee>();

            return retVal;
        }


        /// <summary>
        /// 根据地址码得到AddrCode
        /// </summary>
        /// <param name="hibernateOper"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static AddrCode GetAddrCode(IHibernateOper hibernateOper, string code)
        {
            AddrCode retVal = null;

            Exec(hibernateOper,
                delegate(ISession s)
                {
                    //查找地址码
                    retVal = GetAddrCode(s, code);
                }
                );
            
            return retVal;
        }

        /// <summary>
        /// 根据地址码得到AddrCode
        /// </summary>
        /// <param name="session"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static AddrCode GetAddrCode(ISession session, string code)
        {
            AddrCode retVal = null;

            ICriteria criteria = session.CreateCriteria(typeof(AddrCode));
            //模糊查找当前代码
            ICriterion criterion = Restrictions.Eq("Code", code);
            criteria.Add(criterion);
            criterion = Restrictions.Eq("Del", false);
            criteria.Add(criterion);

            criteria.SetFirstResult(0);
            criteria.SetMaxResults(1);
            criteria.SetCacheable(true);

            retVal = criteria.UniqueResult<AddrCode>();

            return retVal;
        }


        /// <summary>
        /// 由地址码获取地址全称
        /// </summary>
        /// <param name="hibernateOper"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetAddrForCode(IHibernateOper hibernateOper, string code)
        {
            string retVal = null;

            Exec(hibernateOper,
                delegate(ISession s)
                {
                    //查找地址码
                    retVal = GetAddrForCode(s, code);
                }
                );

            return retVal;
        }

        /// <summary>
        /// 由地址码获取地址全称
        /// </summary>
        /// <param name="session"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetAddrForCode(ISession session, string code)
        {
            string retVal="";
            if (code == null)
            {
                code = "";
            }
		    int addrCodeLen=code.Length;
		
		    if(addrCodeLen==2 || addrCodeLen==4 || addrCodeLen==6 || addrCodeLen==9){
			    String province="";//省份
			    String city="";//城市
			    String area="";//区域
			    String road="";//街道
			
			    if(addrCodeLen>=2){
				    province=code.Substring(0, 2);//省份
			    }
			    if(addrCodeLen>=4){
				    city=code.Substring(0, 4);//城市
			    }
			    if(addrCodeLen>=6){
				    area=code.Substring(0,6);//区域
			    }
			    if(addrCodeLen>=9){
				    road=code.Substring(0,9);//街道
			    }

                ICriteria criteria = session.CreateCriteria(typeof(AddrCode));

                ICriterion criterion=null;
                criterion = Restrictions.Eq("Del", false);
                criteria.Add(criterion);

                ICriterion p = Restrictions.Eq("Code", province);
                ICriterion c = Restrictions.Eq("Code", city);
                ICriterion a = Restrictions.Eq("Code", area);
                ICriterion r = Restrictions.Eq("Code", road);
                //省市区街道4个条件或者关系
                criteria.Add(
                    Restrictions.Or(p,
                        Restrictions.Or(c,
                            Restrictions.Or(a,r)
                        )
                    )
                );

                criteria.SetCacheable(true);

                IList<AddrCode> list=criteria.List<AddrCode>();
                if(list!=null){
                    foreach(AddrCode ac in list){
                        retVal+=ac.Name;
                    }
                }
                else
                {
                    throw new Exceptions.AddrCodeIsNotFoundException();
                }
            }
            else
            {
                throw new Exceptions.AddrCodeErrorException();
            }
		    return retVal;
        }

        /// <summary>
        /// 获取最短距离的分店
        /// </summary>
        /// <param name="hibernateOper"></param>
        /// <param name="loc">地址</param>
        /// <returns></returns>
        public static Department GetMinDistDept(IHibernateOper hibernateOper, TbLocation loc)
        {
            return GetMinDistDept(hibernateOper,loc,double.MaxValue);
        }

        /// <summary>
        /// 获取最短距离的分店
        /// </summary>
        /// <param name="hibernateOper"></param>
        /// <param name="loc">地址</param>
        /// <param name="maxDist">最长距离</param>
        /// <returns></returns>
        public static Department GetMinDistDept(IHibernateOper hibernateOper, TbLocation loc,double maxDist)
        {
            Department retVal = null;

            Exec(hibernateOper,
                delegate(ISession s)
                {
                    //查找地址码
                    retVal = GetMinDistDept(s, loc, maxDist);
                }
                );

            return retVal;
        }

        /// <summary>
        /// 获取最短距离的分店
        /// </summary>
        /// <param name="session"></param>
        /// <param name="loc">地址</param>
        /// <returns></returns>
        public static Department GetMinDistDept(ISession session, TbLocation loc)
        {
            return GetMinDistDept(session,loc,double.MaxValue);
        }

        /// <summary>
        /// 获取最短距离的分店
        /// </summary>
        /// <param name="session"></param>
        /// <param name="loc">地址</param>
        /// <param name="maxDist">最长距离</param>
        /// <returns></returns>
        public static Department GetMinDistDept(ISession session, TbLocation loc, double maxDist)
        {
            Department retVal = null;
            double minDest = double.MaxValue;

            string addrCode = loc.AddrCode;
            string paddrCode = "";
            //如果达到街道级别
            if (addrCode.Length >= 9)
            {
                //获取上级地址码
                paddrCode = addrCode.Substring(0, 6);
            }
            else
            {
                //获取当级地址码
                paddrCode = addrCode;
            }

            ICriteria criteria=session.CreateCriteria(typeof(Department));
            //模糊查找当前代码
            ICriterion criterion = Restrictions.Or(
                Restrictions.Eq("AddrCode", addrCode),
                Restrictions.Like("AddrCode", paddrCode+"%")
                );
            criteria.Add(criterion);

            criterion = Restrictions.Eq("Del", false);
            criteria.Add(criterion);

            criteria.AddOrder(Order.Desc("AddrCode"));

            criteria.SetCacheable(true);

            IList<Department> depts = criteria.List<Department>();
            foreach (Department dept in depts)
            {
                //如果地址相同则返回该商店
                if (dept.Lat == loc.Lat && dept.Lng == loc.Lng)
                {
                    retVal = dept;
                    break;
                }
                else
                {
                    //计算距离
                    double dest = LocationUtil.GetDistance(dept.Lat, dept.Lng, loc.Lat, loc.Lng);
                    //取最短距离的分店
                    if (dest<=maxDist && dest < minDest)
                    {
                        minDest = dest;
                        retVal = dept;
                    }
                }
            }

            return retVal;
        }
    }
}