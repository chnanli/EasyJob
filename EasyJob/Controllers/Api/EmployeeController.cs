using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyJob.Consts;
using EasyJob.Filters;
using EasyJob.Pojo.Pojo;
using EasyJob.Pojo.Pojo.Bases;
using NHibernate;
using NHibernate.Criterion;
using EasyJob.Tools;

namespace EasyJob.Controllers.Api
{
    public class EmployeeController : ApiPowerController
    {
        private TbBaseOper<Employee> employeeOper = null;
        public EmployeeController()
            : base()
        {
            employeeOper = new TbBaseOper<Employee>(HibernateOper, typeof(Employee));
        }

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(Employee employee)
        {
            bool retVal = false;
            IList<Employee> emps = employeeOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    ICriterion criterion = Restrictions.Eq("Code", employee.Code);
                    criteria.Add(criterion);
                    criterion = Restrictions.Eq("Pwd", employee.Pwd);
                    criteria.Add(criterion);
                }
                );
            if (emps != null && emps.Count > 0)
            {
                retVal = true;
                Session[SessionConst.Employee] = emps[0];
                emps.Clear();
            }
            else
            {
                throw new  Exceptions.UserNameOrPwdErrorException();//用户名或密码错误
            }

            return Json(retVal);
        }

        public ActionResult Logout()
        {
            bool retVal = false;

            Session[SessionConst.Employee] = null;
            retVal = true;

            return Json(retVal);
        }

        [LoginActionFilterAttribute]
        public ActionResult GetMySelf()
        {
            return Json(MySelf);
        }

        [LoginActionFilterAttribute]
        public ActionResult GetMyModFunc()
        {
            IList<EmpModFunc> retVal = new List<EmpModFunc>();

            foreach (EmpModFunc empModFunc in MyModFunc)
            {
                EmpModFunc emf = new EmpModFunc();
                emf.ModFunc = new ModFunc();
                emf.ModFunc.Cls = empModFunc.ModFunc.Cls;
                emf.FuncNames = empModFunc.FuncNames;
                retVal.Add(emf);
            }

            return Json(retVal);
        }

        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(Employee employee)
        {
            //根据地址码获取地址
            employee.Addr = PojoUtil.GetAddrForCode(HibernateOper, employee.AddrCode);
            LocationUtil.Location loc = LocationUtil.GetLocation(employee.Addr + employee.Location);
            if (loc != null)
            {
                employee.Lat = loc.lat;
                employee.Lng = loc.lng;
            }
            return Json(employeeOper.Add(employee));
        }

        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(Employee employee)
        {
            //根据地址码获取地址
            employee.Addr = PojoUtil.GetAddrForCode(HibernateOper, employee.AddrCode);
            LocationUtil.Location loc = LocationUtil.GetLocation(employee.Addr + employee.Location);
            if (loc != null)
            {
                employee.Lat = loc.lat;
                employee.Lng = loc.lng;
            }
            return Json(employeeOper.Update(employee));
        }

        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(Employee employee)
        {
            return Json(employeeOper.Del(employee));
        }

        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum)
        {
            return Json(employeeOper.Get(Get_OnCriteria, pageSize, pageNum));
        }

        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize)
        {
            return Json(employeeOper.GetPageCount(GetPageCount_OnCriteria, pageSize));
        }

        public void Get_OnCriteria(object sender, ICriteria criteria)
        {
            criteria.AddOrder(Order.Asc("Code"));
            //            ICriterion criterion = Restrictions.Eq("Name", "测试123");
            //            criteria.Add(criterion);
        }
        public void GetPageCount_OnCriteria(object sender, ICriteria criteria)
        {
            //            ICriterion criterion = Restrictions.Eq("Name", "测试123");
            //            criteria.Add(criterion);
        }
    }
}
