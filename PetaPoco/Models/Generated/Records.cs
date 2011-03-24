
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace sqlserver
{
	public partial class sqlserverDB : Database
	{
		public sqlserverDB() 
			: base("sqlserver")
		{
			CommonConstruct();
		}

		public sqlserverDB(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			sqlserverDB GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static sqlserverDB GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new sqlserverDB();
        }

		[ThreadStatic] static sqlserverDB _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        
		public class Record<T> where T:new()
		{
			public static sqlserverDB repo { get { return sqlserverDB.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public void Save() { repo.Save(this); }
			public object Insert() { return repo.Insert(this); }
			public int Update() { return repo.Update(this); }
			public int Delete() { return repo.Delete(this); }
			public static int Update(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static PagedFetch<T> FetchPage(long page, long itemsPerPage, string sql, params object[] args) { return repo.FetchPage<T>(page, itemsPerPage, sql, args); }
			public static PagedFetch<T> FetchPage(long page, long itemsPerPage, Sql sql) { return repo.FetchPage<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }
		}
	}
	

    
	[TableName("articles")]
	[ExplicitColumns]
    public partial class article : sqlserverDB.Record<article>  
    {
        [Column] public long id { get; set; }
        [Column] public string title { get; set; }
        [Column] public bool draft { get; set; }
        [Column] public DateTime date_created { get; set; }
        [Column] public string content { get; set; }
	}

}


