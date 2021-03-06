﻿using System;
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
using Tools;
using EasyJob.ContractResolver;

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

        private void IsExistsCode(ISession session, Employee emp)
        {
            ICriteria criteria = session.CreateCriteria(typeof(Employee));

            ICriterion criterion = null;
            if (emp.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(emp.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("Code", emp.Code);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.EmpCodeIsExistsException();//部门Code已经存在
            }
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
            //由工号与密码查找员工
            Employee emp = GetEmpForCodeAndPwd(employee.Code, employee.PwdWeb);

            if (emp != null)
            {
                retVal = true;
                //保存到SESSION
                MySelf = emp;
            }
            else
            {
                throw new  Exceptions.UserNameOrPwdErrorException();//用户名或密码错误
            }

            return Json(retVal);
        }

        //由工号与密码获取用户
        private Employee GetEmpForCodeAndPwd(string code,string pwd)
        {
            Employee retVal = null; ;
            IList<Employee> emps = employeeOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    ICriterion criterion = Restrictions.Eq("Code", code);
                    criteria.Add(criterion);
                    criterion = Restrictions.Eq("PwdWeb", pwd);
                    criteria.Add(criterion);
                }
                );
            if (emps != null && emps.Count > 0)
            {
                retVal = emps[0];
                emps.Clear();
            }
            return retVal;
        }

        [LoginActionFilterAttribute]
        public ActionResult UpdatePwd(string oldPwd,string newPwd)
        {
            bool retVal = false;

            //由工号与密码查找员工
            Employee emp = GetEmpForCodeAndPwd(MySelf.Code, oldPwd);

            if (emp != null)
            {
                //修改密码
                emp.PwdWeb = newPwd;
                retVal=employeeOper.Update(emp);
                //保存到SESSION
                MySelf = emp;
            }
            else
            {
                throw new Exceptions.PwdErrorException();//密码错误
            }

            return Json(retVal);
        }

        public ActionResult Logout()
        {
            bool retVal = false;

            //保存到SESSION
            MySelf = null;
            retVal = true;

            return Json(retVal);
        }

        [LoginActionFilterAttribute]
        public ActionResult GetMySelf()
        {
            return Json(MySelf);
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
            return Json(employeeOper.Add(employee,
                delegate(object sender, ISession session)
                {
                    //判断是否存在部门Code
                    IsExistsCode(session, employee);
                }
                ));
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
            return Json(employeeOper.Update(employee,
                delegate(object sender, ISession session)
                {
                    //判断是否存在部门Code
                    IsExistsCode(session, employee);
                }
                ));
        }

        /// <summary>
        /// 修改我自己
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public ActionResult UpdateMySelf(Employee employee)
        {
            employee.Id = MySelf.Id;
            employee.Code = MySelf.Code;//自己不修改自己的工号，如果要修改必须要经过Update接口
            employee.IfPrincipal = MySelf.IfPrincipal;
            employee.IfSysUser = MySelf.IfSysUser;
            employee.IfWork = MySelf.IfWork;
            employee.State = MySelf.State;
            employee.PwdWeb = MySelf.PwdWeb;
            employee.Dept = MySelf.Dept;
            employee.Pos = MySelf.Pos;

            return Update(employee);
        }

        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(Employee employee)
        {
            return Json(employeeOper.Del(employee));
        }

        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum,string code,string empName,string nickName)
        {
            return Json(employeeOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(code))
                    {
                        ICriterion criterion = Restrictions.Eq("Code", code);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(empName))
                    {
                        ICriterion criterion = Restrictions.Like("EmpName", empName,MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(nickName))
                    {
                        ICriterion criterion = Restrictions.Like("NickName", nickName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("Code"));
                }
                , pageSize, pageNum)
                //,new LimitPropsContractResolver(new string[] { "Code", "Id" })
                );
        }

        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string code, string empName, string nickName)
        {
            return Json(employeeOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(code))
                    {
                        ICriterion criterion = Restrictions.Eq("Code", code);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(empName))
                    {
                        ICriterion criterion = Restrictions.Like("EmpName", empName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(nickName))
                    {
                        ICriterion criterion = Restrictions.Like("NickName", nickName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }
    }
}
