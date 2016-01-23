using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyJob.Pojo.Pojo;
using ORM.Hibernate;
using EasyJob.Consts;

namespace EasyJob.Controllers.Api
{
    public class PowerController : BaseController
    {
        private Employee mySelf = null;
        public Employee MySelf {
            get {
                if (mySelf == null)
                {
                    mySelf = (Employee)Session[SessionConst.Employee];
                }
                return mySelf;
            }
            set {
                mySelf = value;
                Session[SessionConst.Employee] = value;
            }
        }
        public IList<EmpModFunc> MyModFunc { get; set; }
    }
}