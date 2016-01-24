using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;
using ORM.Hibernate;

namespace EasyJob.Pojo.Pojo.Bases
{
    public class TbBaseOper<T> where T:TbBase
    {
        public enum QueryDel
        {
            All,
            Del,
            UnDel
        }
        public delegate void OnCriteria(object sender, ICriteria criteria);
        public delegate void OnSession(object sender, ISession session);

        private IHibernateOper hibernateOper = null;
        private Type type = null;

        public TbBaseOper(IHibernateOper hibernateOper,Type type)
        {
            this.hibernateOper = hibernateOper;
            this.type = type;
        }

        private T Get(ISession s,T t)
        {
            T retVal = null;
            retVal = s.Get<T>(t.Id);
            return retVal;
        }

        public string Add(T t)
        {
            return Add(t,null);
        }

        public string Add(T t,OnSession onSession)
        {
            string retVal = "";
            ISession s = null;
            ITransaction trans = null;
            try
            {
                s = hibernateOper.GetCurrentSession();
                trans = s.BeginTransaction();

                if (onSession != null)
                {
                    onSession(this,s);
                }

                s.Save(t);

                trans.Commit();

                retVal = t.Id.ToString();
            }
            catch (Exception e)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw e;
            }
            finally
            {
                //                hibernateOper.Close(s);
            }

            return retVal;
        }

        public bool Update(T t)
        {
            return Update(t, null);
        }

        public bool Update(T t, OnSession onSession)
        {
            bool retVal = false;
            ISession s = null;
            ITransaction trans = null;
            try
            {
                s = hibernateOper.GetCurrentSession();
                trans = s.BeginTransaction();

                if (onSession != null)
                {
                    onSession(this, s);
                }

                T temp = Get(s,t);
                t.ModDate = DateTime.Now;
                t.UploadDate = temp.UploadDate;
                t.TrashFlag = temp.TrashFlag;
                t.ExHost = temp.ExHost;
                t.Del = false;

                s.Merge(t);

                trans.Commit();

                retVal = true;
            }
            catch (Exception e)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw e;
            }
            finally
            {
//                hibernateOper.Close(s);
            }

            return retVal;
        }

        public bool Del(T t)
        {
            bool retVal = false;
            ISession s = null;
            ITransaction trans = null;
            try
            {
                s = hibernateOper.GetCurrentSession();
                trans = s.BeginTransaction();

                t = Get(s, t);
                t.ModDate = DateTime.Now;
                t.Del = true;

                s.Update(t);

                trans.Commit();

                retVal = true;
            }
            catch (Exception e)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw e;
            }
            finally
            {
                //                hibernateOper.Close(s);
            }

            return retVal;
        }

        public T Get(object id)
        {
            T retVal = null;
            ISession s = null;
            ITransaction trans = null;
            try
            {
                s = hibernateOper.GetCurrentSession();
                trans = s.BeginTransaction();

                retVal = s.Get<T>(id);

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
            finally
            {
                //                hibernateOper.Close(s);
            }

            return retVal;
        }

        public IList<T> Get()
        {
            return Get(null);
        }

        public IList<T> Get(OnCriteria onCriteria)
        {
            return Get(onCriteria, QueryDel.UnDel,0,0);
        }

        public IList<T> Get(OnCriteria onCriteria, int pageSize, int pageNum)
        {
            return Get(onCriteria, QueryDel.UnDel,pageSize,pageNum);
        }

        public IList<T> Get(OnCriteria onCriteria, QueryDel qd,int pageSize,int pageNum)
        {
            IList<T> retVal = null;
            ISession s = null;
            ITransaction trans = null;
            try
            {
                s = hibernateOper.GetCurrentSession();
                trans = s.BeginTransaction();

                ICriteria criteria = s.CreateCriteria(type);

                if (qd == QueryDel.Del)
                {
                    ICriterion criterion = Restrictions.Eq("Del", true);
                    criteria.Add(criterion);
                }
                else if (qd == QueryDel.UnDel)
                {
                    ICriterion criterion = Restrictions.Eq("Del", false);
                    criteria.Add(criterion);
                }

                //如果要触发事件
                if (onCriteria != null)
                {
                    onCriteria(this,criteria);
                }

//                criteria.SetResultTransformer(new AliasToBeanResultTransformer(type));

                if (pageNum>0 && pageSize > 0)
                {
                    criteria.SetFirstResult((pageNum - 1) * pageSize);
                    criteria.SetMaxResults(pageSize);
                }
                criteria.SetCacheable(true);

                retVal = criteria.List<T>();

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
            finally
            {
                //                hibernateOper.Close(s);
            }

            return retVal;
        }

        public int GetPageCount()
        {
            return GetPageCount(null);
        }

        public int GetPageCount(OnCriteria onCriteria)
        {
            return GetPageCount(onCriteria, QueryDel.UnDel);
        }

        public int GetPageCount(OnCriteria onCriteria, QueryDel qd)
        {
            return GetPageCount(onCriteria, qd, 0);
        }

        public int GetPageCount(OnCriteria onCriteria, int pageSize)
        {
            return GetPageCount(onCriteria, QueryDel.UnDel, pageSize);
        }

        public int GetPageCount(OnCriteria onCriteria, QueryDel qd, int pageSize)
        {
            int retVal = 0;
            ISession s = null;
            ITransaction trans = null;
            try
            {
                s = hibernateOper.GetCurrentSession();
                trans = s.BeginTransaction();

                ICriteria criteria = s.CreateCriteria(type);

                if (qd == QueryDel.Del)
                {
                    ICriterion criterion = Restrictions.Eq("Del", true);
                    criteria.Add(criterion);
                }
                else if (qd == QueryDel.UnDel)
                {
                    ICriterion criterion = Restrictions.Eq("Del", false);
                    criteria.Add(criterion);
                }

                //如果要触发事件
                if (onCriteria != null)
                {
                    onCriteria(this, criteria);
                }

                //统计
                criteria.SetProjection(
                    Projections.ProjectionList()
                    .Add(Projections.Count("Id"))
                    );

                criteria.SetCacheable(true);

                int count = (int)criteria.UniqueResult();
                if (pageSize > 0)
                {
                    retVal = count/pageSize;
                    if (count%pageSize > 0)
                    {
                        retVal++;
                    }
                }
                else
                {
                    retVal = 1;
                }

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
            finally
            {
                //                hibernateOper.Close(s);
            }

            return retVal;
        }
    }
}