using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 模块
    /// </summary>
    public class Mod : TbBase
    {
        private int index = 0;

        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 菜单显示信息
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// 菜单类型，0为菜单项，1有子菜单
        /// </summary>
        public virtual int Type { get; set; }

        /// <summary>
        /// 菜单链接地址,点击菜单跳转的页面地址
        /// </summary>
        public virtual string Href { get; set; }

        /// <summary>
        /// 菜单显示的图标CSS样式
        /// </summary>
        public virtual string Icon { get; set; }

        /// <summary>
        /// 上一级模块ID
        /// </summary>
        public virtual Guid PId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Index
        {
            get { return index; }
            set { index = value; }
        }

        public virtual ModFunc ModFunc { get; set; }

        /// <summary>
        /// 子模块列表，这个字段不存数据库，而是存放该模块下的所有子模块
        /// </summary>
        public virtual IList<Mod> Mods { get; set; }
    }
}
