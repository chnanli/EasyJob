using EasyJob.Pojo.Pojo;
using EasyJob.Tools;
using NHibernate;
using ORM.Hibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tools;

namespace EasyJob
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public IHibernateOper HibernateOper { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            try
            {
                //获取数据库操作对象
                HibernateOper = HibernateFactory.GetInstance();
                updateVersion();//更新数据库
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void updateVersion(){
		    ISession session=null;
            ITransaction trans=null;
		    try{
			    session=HibernateOper.GetCurrentSession();
			    trans=session.BeginTransaction();
			
			    //获取版本
                ICriteria criteria = session.CreateCriteria<EasyJob.Pojo.Pojo.Version>();
                EasyJob.Pojo.Pojo.Version ver = (EasyJob.Pojo.Pojo.Version)criteria.UniqueResult();
			    if(ver==null){
				    ver=new EasyJob.Pojo.Pojo.Version();
			    }
			
			    //如果有更新则版本加1
			    while(checkVersion(session,ver.Ver)){
				    ver.Ver+=1;//版本加1
			    }
			
			    session.SaveOrUpdate(ver);
			
			    trans.Commit();
		    }catch(Exception e){
                if(trans!=null){
                    trans.Rollback();
                }
		    }finally{
		    }
	    }
	
	    public bool checkVersion(ISession session,int ver){
            bool retVal = true;

		    String strSql="";
		    switch(ver){
                case 0:
                    //初始化维修类型
                    InitWorkType(session);
                    break;
                case 1:
                    Department dept = new Department();
                    dept.Code = "00001";
                    dept.Name = "广州棠下店";
                    dept.AddrCode="440106011";
                    dept.Addr=PojoUtil.GetAddrForCode(session,dept.AddrCode);
                    dept.Location = "棠下小区";
                    LocationUtil.Location loc = LocationUtil.GetLocation(dept.Addr + dept.Location);
                    if (loc != null)
                    {
                        dept.Lat = loc.lat;
                        dept.Lng = loc.lng;
                    }
                    session.Save(dept);

                    dept = new Department();
                    dept.Code = "00002";
                    dept.Name = "广州太和店";
                    dept.AddrCode = "440111107";
                    dept.Addr=PojoUtil.GetAddrForCode(session,dept.AddrCode);
                    dept.Location = "太和镇";
                    loc = LocationUtil.GetLocation(dept.Addr + dept.Location);
                    if (loc != null)
                    {
                        dept.Lat = loc.lat;
                        dept.Lng = loc.lng;
                    }
                    session.Save(dept);
                    break;
		        default:
			        retVal=false;
                    break;
		    }
		    return retVal;
	    }

        /// <summary>
        /// 初始化维修类型
        /// </summary>
        /// <returns></returns>
        private void InitWorkType(ISession session)
        {
            WorkCodeType1 wct1 = new WorkCodeType1();
            wct1.Code = "01";
            wct1.Name = "灯饰";
            session.Save(wct1);

            wct1 = new WorkCodeType1();
            wct1.Code = "02";
            wct1.Name = "水暖";
            session.Save(wct1);

            wct1 = new WorkCodeType1();
            wct1.Code = "03";
            wct1.Name = "家具";
            session.Save(wct1);

            wct1 = new WorkCodeType1();
            wct1.Code = "04";
            wct1.Name = "门窗";
            session.Save(wct1);

            wct1 = new WorkCodeType1();
            wct1.Code = "05";
            wct1.Name = "家电";
            session.Save(wct1);

            wct1 = new WorkCodeType1();
            wct1.Code = "06";
            wct1.Name = "空调";
            session.Save(wct1);

            wct1 = new WorkCodeType1();
            wct1.Code = "07";
            wct1.Name = "木地板";
            session.Save(wct1);

            wct1 = new WorkCodeType1();
            wct1.Code = "08";
            wct1.Name = "墙地砖";
            session.Save(wct1);

            wct1 = new WorkCodeType1();
            wct1.Code = "09";
            wct1.Name = "墙纸";
            session.Save(wct1);

            wct1 = new WorkCodeType1();
            wct1.Code = "10";
            wct1.Name = "地钻";
            session.Save(wct1);

            wct1 = new WorkCodeType1();
            wct1.Code = "11";
            wct1.Name = "乳胶漆";
            session.Save(wct1);

            wct1 = new WorkCodeType1();
            wct1.Code = "12";
            wct1.Name = "疏通";
            session.Save(wct1);

            wct1 = new WorkCodeType1();
            wct1.Code = "13";
            wct1.Name = "搬运";
            session.Save(wct1);

            wct1 = new WorkCodeType1();
            wct1.Code = "14";
            wct1.Name = "其它";
            session.Save(wct1);

            //##############################################################
            WorkCodeType2 wct2 = new WorkCodeType2();
            wct2.Code = "01";
            wct2.Name = "安装";
            session.Save(wct2);

            wct2 = new WorkCodeType2();
            wct2.Code = "02";
            wct2.Name = "清洗";
            session.Save(wct2);

            wct2 = new WorkCodeType2();
            wct2.Code = "03";
            wct2.Name = "维修";
            session.Save(wct2);

            wct2 = new WorkCodeType2();
            wct2.Code = "04";
            wct2.Name = "翻新";
            session.Save(wct2);

            wct2 = new WorkCodeType2();
            wct2.Code = "05";
            wct2.Name = "张贴";
            session.Save(wct2);

            wct2 = new WorkCodeType2();
            wct2.Code = "06";
            wct2.Name = "其它";
            session.Save(wct2);

            //##############################################################
            ICriteria criteria=session.CreateCriteria(typeof(WorkCodeType1));
            IList<WorkCodeType1> wct1s= criteria.List<WorkCodeType1>();

            criteria = session.CreateCriteria(typeof(WorkCodeType2));
            IList<WorkCodeType2> wct2s = criteria.List<WorkCodeType2>();

            foreach (WorkCodeType1 t1 in wct1s)
            {
                foreach (WorkCodeType2 t2 in wct2s)
                {
                    WorkCodeType wct = new WorkCodeType();
                    wct.Type1 = t1;
                    wct.Type2 = t2;
                    session.Save(wct);
                }
            }
        }
    }
}