using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyJob.Pojo.Pojo;
using ORM.Hibernate;

namespace EasyJob.Controllers.Api
{
    public class PowerController : BaseController
    {
        public Employee MySelf { get; set; }
        public IList<EmpModFunc> MyModFunc { get; set; }
    }
}