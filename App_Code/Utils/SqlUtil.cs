using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

/// <summary>
/// 操作数据库的类
/// </summary>
public class SqlUtil
{
    /// <summary>
    /// 字段开始字符
    /// </summary>
    public string QuotePrefix { get; set; }
    /// <summary>
    /// 字段结尾字符
    /// </summary>
    public string QuoteSuffix { get; set; }
    /// <summary>
    /// 架构分隔符
    /// </summary>
    public string SchemaSeparator { get; set; }
    /// <summary>
    /// 参数前缀
    /// </summary>
    public string ParameterPrefix { get; set; }
    IDbConnection dbConnection;
    IDbTransaction dbTransaction;
    /// <summary>
    /// 链接字符串
    /// </summary>
    public string ConStr { get; private set; }
    /// <summary>
    /// 共有者
    /// </summary>
    public string ProviderName { get; private set; }

    /// <summary>
    /// 构造函数，默认读取数据库连接defaultDb的节点配置
    /// </summary>
    public SqlUtil()
    {
        ParameterPrefix = "@";
        QuotePrefix = "[";
        QuoteSuffix = "]";
        SchemaSeparator = ".";
        //取数据库链接
        ConStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        //取ProviderName
        ProviderName = ConfigurationManager.ConnectionStrings["conStr"].ProviderName;
        //修改初始化数据
        ChangeInitData(ProviderName);
    }
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configName">配置数据库连接的节点名</param>
    public SqlUtil(string configName)
    {
        ParameterPrefix = "@";
        QuotePrefix = "[";
        QuoteSuffix = "]";
        SchemaSeparator = ".";
        //取数据库链接
        ConStr = ConfigurationManager.ConnectionStrings[configName].ConnectionString;
        //取ProviderName
        ProviderName = ConfigurationManager.ConnectionStrings[configName].ProviderName;
        //修改初始化数据
        ChangeInitData(ProviderName);
    }

    void ChangeInitData(string providerName)
    {
        if (ProviderName.ToUpper().Contains("MYSQL"))
        {
            ParameterPrefix = "@";
            QuotePrefix = "`";
            QuoteSuffix = "`";
            SchemaSeparator = ".";
        }
        if (ProviderName.ToUpper().Contains("ORACLE"))
        {
            ParameterPrefix = ":";
            QuotePrefix = "";
            QuoteSuffix = "";
            SchemaSeparator = ".";
        }
    }

    /// <summary>
    /// 开始事务
    /// </summary>
    public void BeginTransaction()
    {
        dbConnection = CreateConnection();
        dbTransaction = dbConnection.BeginTransaction();
    }
    /// <summary>
    /// 提交事务
    /// </summary>
    public void Commit()
    {
        try
        {
            dbTransaction.Commit();
        }
        finally
        {
            dbConnection.Close();
            dbConnection = null;
            dbTransaction = null;
        }
    }
    /// <summary>
    /// 回滚事务
    /// </summary>
    public void Rollback()
    {
        try
        {
            dbTransaction.Rollback();
        }
        finally
        {
            dbConnection.Close();
            dbConnection = null;
            dbTransaction = null;
        }
    }

    /// <summary>
    /// 创建Connection
    /// </summary>
    /// <returns></returns>
    public IDbConnection CreateConnection()
    {
        if (dbConnection == null)
        {
            var con = System.Data.Common.DbProviderFactories.GetFactory(ProviderName).CreateConnection();
            con.ConnectionString = ConStr;
            con.Open();
            return con;
        }
        else
        {
            return dbConnection;
        }
    }
    /// <summary>
    /// 创建DataAdapter
    /// </summary>
    /// <returns></returns>
    public IDbDataAdapter CreateDataAdapter()
    {
        var dapt = System.Data.Common.DbProviderFactories.GetFactory(ProviderName).CreateDataAdapter();
        return dapt;
    }
    /// <summary>
    /// 创建Command
    /// </summary>
    /// <returns></returns>
    public IDbCommand CreateCommand()
    {
        var cmd = System.Data.Common.DbProviderFactories.GetFactory(ProviderName).CreateCommand();
        return cmd;
    }
    /// <summary>
    /// 创建CommandBuilder
    /// </summary>
    /// <returns></returns>
    public System.Data.Common.DbCommandBuilder CreateCommandBuilder()
    {
        var builder = System.Data.Common.DbProviderFactories.GetFactory(ProviderName).CreateCommandBuilder();
        builder.QuotePrefix = QuotePrefix;
        builder.QuoteSuffix = QuoteSuffix;
        builder.SchemaSeparator = SchemaSeparator;
        return builder;
    }

    /// <summary>
    /// 创建参数
    /// </summary>
    /// <param name="pName"></param>
    /// <param name="pVal"></param>
    /// <returns></returns>
    public IDbDataParameter CreateParameter(string pName, object pVal)
    {
        //实例化参数
        var pp = System.Data.Common.DbProviderFactories.GetFactory(ProviderName).CreateParameter();
        //设置参数名
        if (pName.Contains(ParameterPrefix))
        {
            pp.ParameterName = pName;
        }
        else
        {
            pp.ParameterName = ParameterPrefix + pName;
        }
        //设置参数值
        pp.Value = pVal;
        //返回参数对象
        return pp;
    }

    /// <summary>
    /// 执行SQL语句
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <returns></returns>

    public int ExecuteSql(string sql)
    {
        return ExecuteSql(sql, null);
    }
    /// <summary>
    /// 执行SQL语句
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    public int ExecuteSql(string sql, IDbDataParameter[] parameters)
    {
        return ExecuteSql(sql, CommandType.Text, parameters);
    }
    /// <summary>
    /// 执行SQL语句
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="commandType">命令类别</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    public int ExecuteSql(string sql, CommandType commandType, IDbDataParameter[] parameters)
    {
        LogSql(sql, parameters);
        //创建数据库链接
        var con = CreateConnection();
        try
        {
            {
                //创建SQL命令
                var cmd = con.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = commandType;
                if (dbTransaction != null)
                    cmd.Transaction = dbTransaction;
                //设置参数
                if (parameters != null && parameters.Length > 0)
                {
                    foreach (var p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                //执行SQL语句并返回影响的行数
                return cmd.ExecuteNonQuery();
            }
        }
        finally
        {
            if (dbTransaction == null)
                con.Close();
        }
    }
    /// <summary>
    /// 记录日志
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    private void LogSql(string sql, IDbDataParameter[] parameters)
    {
        try
        {
            StringBuilder sLog = new StringBuilder();
            sLog.AppendLine(sql);
            if (parameters != null)
            {
                foreach (var pp in parameters)
                {
                    sLog.AppendLine(string.Format("{0}={1}", pp.ParameterName, pp.Value));
                }
            }
            //LogUtil.Info(sLog.ToString());
        }
        catch
        {

        }
    }

    /// <summary>
    /// 执行查询并返回第一行第一列
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <returns></returns>

    public object ExecuteScalar(string sql)
    {
        return ExecuteScalar(sql, null);
    }
    /// <summary>
    /// 执行查询并返回第一行第一列
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    public object ExecuteScalar(string sql, IDbDataParameter[] parameters)
    {
        return ExecuteScalar(sql, CommandType.Text, parameters);
    }
    /// <summary>
    /// 执行查询并返回第一行第一列
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="commandType">命令类别</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    public object ExecuteScalar(string sql, CommandType commandType, IDbDataParameter[] parameters)
    {
        LogSql(sql, parameters);
        //创建数据库链接
        var con = CreateConnection();
        try
        {
            {
                //创建命令
                var cmd = con.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = commandType;
                if (dbTransaction != null)
                    cmd.Transaction = dbTransaction;
                //设置参数
                if (parameters != null && parameters.Length > 0)
                {
                    foreach (var p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                //返回第一行第一列的值
                return cmd.ExecuteScalar();
            }
        }
        finally
        {
            if (dbTransaction == null)
                con.Close();
        }
    }

    /// <summary>
    /// 执行查询并返回数据集
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <returns></returns>

    public DataSet QueryDataSet(string sql)
    {
        return QueryDataSet(sql, null);
    }
    /// <summary>
    /// 执行查询并返回数据集
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    public DataSet QueryDataSet(string sql, IDbDataParameter[] parameters)
    {
        return QueryDataSet(sql, CommandType.Text, parameters);
    }
    /// <summary>
    /// 执行查询并返回数据集
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="commandType">命令类别</param>
    /// <param name="parameters">参数</param>
    /// <returns></returns>
    public DataSet QueryDataSet(string sql, CommandType commandType, IDbDataParameter[] parameters)
    {
        LogSql(sql, parameters);
        //创建数据库链接
        var con = CreateConnection();
        try
        {
            {
                //创建SQL命令
                var cmd = con.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = commandType;
                if (dbTransaction != null)
                    cmd.Transaction = dbTransaction;
                //设置参数
                if (parameters != null && parameters.Length > 0)
                {
                    foreach (var p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                //查询数据库并将数据返回到数据集
                var cb = CreateCommandBuilder();
                var dapt = CreateDataAdapter();
                cb.DataAdapter = (System.Data.Common.DbDataAdapter)dapt;
                dapt.SelectCommand = cmd;
                DataSet ds = new DataSet();
                dapt.Fill(ds);
                //返回数据集
                return ds;
            }
        }
        finally
        {
            if (dbTransaction == null)
                con.Close();
        }
    }

    /// <summary>
    /// 获取表名
    /// </summary>
    /// <returns></returns>
    public string GetTableName<T>() where T : class, new()
    {
        var name = typeof(T).FullName;
        var c = typeof(T).Assembly.CreateInstance(name) as T;
        return GetTableName(c);
    }
    /// <summary>
    /// 获取表名
    /// </summary>
    /// <returns></returns>
    public string GetTableName<T>(T t)
    {
        string tblName = t.GetType().Name;

        var tblAttr = typeof(T).GetCustomAttributes(false).Where(a => a is TableNameAttribute).FirstOrDefault();
        if (tblAttr != null)
        {
            tblName = (tblAttr as TableNameAttribute).Name;
        }

        return this.QuotePrefix + tblName + this.QuoteSuffix;
    }
    /// <summary>
    /// 查询数据库
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="whereCluase"></param>
    /// <param name="parms"></param>
    /// <returns></returns>
    public List<T> Query<T>(string whereCluase) where T : class, new()
    {
        return Query<T>(whereCluase, null);
    }
    /// <summary>
    /// 查询总数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="whereCluase"></param>
    /// <returns></returns>
    public int QueryCount<T>(string whereCluase) where T : class, new()
    {
        return QueryCount<T>(whereCluase, null);
    }
    /// <summary>
    /// 查询总数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="whereCluase"></param>
    /// <param name="parms"></param>
    /// <returns></returns>
    public int QueryCount<T>(string whereCluase, System.Data.IDbDataParameter[] parms) where T : class, new()
    {
        string tblName = GetTableName<T>();
        string sql = "Select count(*) from " + tblName + " where 1=1";
        if (!string.IsNullOrEmpty(whereCluase))
        {
            sql += " AND " + whereCluase;
        }

        return ExecuteScalar(sql,parms).AsInt();
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="whereCluase"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public List<T> Query<T>(string whereCluase, int page, int limit) where T : class, new()
    {
        return Query<T>(whereCluase, null, page, limit);
    }
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="whereCluase"></param>
    /// <param name="parms"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public List<T> Query<T>(string whereCluase, System.Data.IDbDataParameter[] parms, int page, int limit) where T : class, new()
    {
        StringBuilder sOrder = new StringBuilder();

        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
        {
            //如果是主键则跳过
            int count = propertyInfo.GetCustomAttributes(false).Where(a => a is KeyAttribute).Count();
            if (count <= 0)
            {
                continue;
            }
            if (sOrder.Length > 0) sOrder.Append(",");
            sOrder.AppendFormat("{0} ASC", propertyInfo.Name);
            break;
        }

        if (sOrder.Length <= 0)
        {
            throw new Exception("实体未设置KeyAttribute属性");
        }


        /*
         SELECT TOP 大小 * FROM(
        SELECT ROW_NUMBER() OVER(ORDER BY 字段 ASC) AS row_num,*
          FROM 表) tmp
          WHERE tmp.row_num>(页数-1)*大小

         */


        string tblName = GetTableName<T>();
        StringBuilder sSql = new StringBuilder();
        sSql.AppendFormat("SELECT * FROM ",limit);
        sSql.AppendFormat("(SELECT rownum as rowno,{0}.* FROM {0} ", tblName);
        if (!string.IsNullOrEmpty(whereCluase))
        {
            sSql.AppendFormat("WHERE {0} ", whereCluase);
        }
        else
        {
            sSql.Append("WHERE 1=1 ");
        }
        sSql.AppendFormat("AND rownum<={0}*{1}) TMP ", page,limit);
        sSql.AppendFormat("WHERE TMP.ROWNO>({0}-1)*{1}", page, limit);

        string sql = sSql.ToString();
        //查询数据
        DataTable table = QueryDataSet(sql, parms).Tables[0];
        //如果没有数据则返回空记录
        if (table == null || table.Rows.Count <= 0)
        {
            return new List<T>();
        }
        //定义变量集合
        var dataList = new List<T>();
        //获取属性
        PropertyInfo[] propertyInfos = typeof(T).GetProperties();
        foreach (DataRow row in table.Rows)
        {
            T t = new T();
            //循环属性
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //过滤Ignore属性
                int count = propertyInfo.GetCustomAttributes(false).Where(a => a is IgnoreAttribute).Count();
                if (count > 0)
                {
                    continue;
                }
                //数据库为空时
                if (row[propertyInfo.Name] == DBNull.Value)
                {
                    propertyInfo.SetValue(t, null, null);
                }
                else
                {
                    //否则直接赋值
                    propertyInfo.SetValue(t, row[propertyInfo.Name], null);
                }
            }
            //添加数据到集合
            dataList.Add(t);
        }
        //返回列表
        return dataList;
    }

    public List<T> Query<T>(string whereCluase, System.Data.IDbDataParameter[] parms) where T : class, new()
    {

        string tblName = GetTableName<T>();
        string sql = "Select * from " + tblName + " where 1=1";
        if (!string.IsNullOrEmpty(whereCluase))
        {
            sql += " AND " + whereCluase;
        }
        //查询数据
        DataTable table = QueryDataSet(sql, parms).Tables[0];
        //如果没有数据则返回空记录
        if (table == null || table.Rows.Count <= 0)
        {
            return new List<T>();
        }
        //定义变量集合
        var dataList = new List<T>();
        //获取属性
        PropertyInfo[] propertyInfos = typeof(T).GetProperties();
        foreach (DataRow row in table.Rows)
        {
            T t = new T();
            //循环属性
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //过滤Ignore属性
                int count = propertyInfo.GetCustomAttributes(false).Where(a => a is IgnoreAttribute).Count();
                if (count > 0)
                {
                    continue;
                }
                //数据库为空时
                if (row[propertyInfo.Name] == DBNull.Value)
                {
                    propertyInfo.SetValue(t, null, null);
                }
                else
                {
                    //否则直接赋值
                    propertyInfo.SetValue(t, row[propertyInfo.Name], null);
                }
            }
            //添加数据到集合
            dataList.Add(t);
        }
        //返回列表
        return dataList;
    }
    /// <summary>
    /// 插入数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="con"></param>
    /// <param name="t"></param>
    /// <param name="trans"></param>
    /// <returns></returns>
    public int Insert<T>(T t) where T : class, new()
    {
        //创建数据库链接
        var con = CreateConnection();
        try
        {
            //获取属性集合
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            //获取表名
            string tblName = GetTableName<T>();
            //判断实体类中是否设置主键
            int keyCount = 0;
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                int count = propertyInfo.GetCustomAttributes(false).Where(a => a is KeyAttribute).Count();
                if (count <= 0)
                {
                    continue;
                }
                keyCount++;
            }
            //如果未设置主键，则异常
            if (keyCount <= 0)
            {
                throw new Exception("实体未设置KeyAttribute属性");
            }
            //查询数据库
            DataSet ds = new DataSet();
            var cmd = con.CreateCommand();
            cmd.CommandText = "select * from " + tblName + " where 1<>1";
            cmd.CommandType = CommandType.Text;
            if (dbTransaction != null)
                cmd.Transaction = dbTransaction;
            var dapt = CreateDataAdapter();
            dapt.SelectCommand = cmd;
            dapt.Fill(ds);
            //在查询的数据集中新增记录
            DataRow row = ds.Tables[0].NewRow();
            //设置新增行的值
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //过滤Ignore属性
                int count = propertyInfo.GetCustomAttributes(false).Where(a => a is IgnoreAttribute).Count();
                if (count > 0)
                {
                    continue;
                }
                //主键如果是Identity则不允许主动插入数据
                object pkAttr = propertyInfo.GetCustomAttributes(false).Where(a => a is KeyAttribute).FirstOrDefault();
                if (pkAttr != null)
                {
                    if ((pkAttr as KeyAttribute).Identity)
                    {
                        continue;
                    }
                }
                //获取值
                object obj = propertyInfo.GetValue(t, null);
                //赋值
                if (obj == null)
                {
                    row[propertyInfo.Name] = DBNull.Value;
                }
                else
                {
                    row[propertyInfo.Name] = obj;
                }
            }
            //将新增行加入数据集
            ds.Tables[0].Rows.Add(row);
            //更新数据到数据库
            var scb = CreateCommandBuilder();
            scb.DataAdapter = (System.Data.Common.DbDataAdapter)dapt;
            return dapt.Update(ds);
        }
        finally
        {
            if (dbTransaction == null)
                con.Close();
        }
    }

    /// <summary>
    /// 插入数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="con"></param>
    /// <param name="t"></param>
    /// <param name="trans"></param>
    /// <returns></returns>
    public int Insert<T>(List<T> tList) where T : class, new()
    {
        //创建数据库链接
        var con = CreateConnection();
        try
        {
            //获取属性集合
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            //获取表名
            string tblName = GetTableName<T>();
            //判断实体类中是否设置主键
            int keyCount = 0;
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                int count = propertyInfo.GetCustomAttributes(false).Where(a => a is KeyAttribute).Count();
                if (count <= 0)
                {
                    continue;
                }
                keyCount++;
            }
            //如果未设置主键，则异常
            if (keyCount <= 0)
            {
                throw new Exception("实体未设置KeyAttribute属性");
            }
            //查询数据库
            DataSet ds = new DataSet();
            var cmd = con.CreateCommand();
            cmd.CommandText = "select * from " + tblName + " where 1<>1";
            cmd.CommandType = CommandType.Text;
            if (dbTransaction != null)
                cmd.Transaction = dbTransaction;
            var dapt = CreateDataAdapter();
            dapt.SelectCommand = cmd;
            dapt.Fill(ds);
            foreach (var t in tList)
            {
                //在查询的数据集中新增记录
                DataRow row = ds.Tables[0].NewRow();
                //设置新增行的值
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    //过滤Ignore属性
                    int count = propertyInfo.GetCustomAttributes(false).Where(a => a is IgnoreAttribute).Count();
                    if (count > 0)
                    {
                        continue;
                    }
                    //主键如果是Identity则不允许主动插入数据
                    object pkAttr = propertyInfo.GetCustomAttributes(false).Where(a => a is KeyAttribute).FirstOrDefault();
                    if (pkAttr != null)
                    {
                        if ((pkAttr as KeyAttribute).Identity)
                        {
                            continue;
                        }
                    }
                    //获取值
                    object obj = propertyInfo.GetValue(t, null);
                    //赋值
                    if (obj == null)
                    {
                        row[propertyInfo.Name] = DBNull.Value;
                    }
                    else
                    {
                        row[propertyInfo.Name] = obj;
                    }
                }
                //将新增行加入数据集
                ds.Tables[0].Rows.Add(row);
            }
            //更新数据到数据库
            var scb = CreateCommandBuilder();
            scb.DataAdapter = (System.Data.Common.DbDataAdapter)dapt;
            return dapt.Update(ds);
        }
        finally
        {
            if (dbTransaction == null)
                con.Close();
        }
    }

    /// <summary>
    /// 修改数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="con"></param>
    /// <param name="t"></param>
    /// <param name="trans"></param>
    /// <returns></returns>
    public int Edit<T>(T t) where T : class, new()
    {
        //创建数据库链接
        var con = CreateConnection();
        try
        {
            //定义变量
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            StringBuilder s_where = new StringBuilder();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //如果是主键则跳过
                int count = propertyInfo.GetCustomAttributes(false).Where(a => a is KeyAttribute).Count();
                if (count <= 0)
                {
                    continue;
                }
                //获取实体属性的值
                object obj = propertyInfo.GetValue(t, null);
                //创建参数
                IDbDataParameter parameter = null;
                if (obj == null)
                {
                    parameter = CreateParameter(propertyInfo.Name, DBNull.Value);
                }
                else
                {
                    parameter = CreateParameter(propertyInfo.Name, obj);
                }
                //设置参数类型
                parameter.DbType = SetDbType(obj);
                //如果是字符串参数则设置值大小
                if (obj.GetType() == typeof(string))
                {
                    var tmp_sla = propertyInfo.GetCustomAttributes(false).Where(a => a is StringLengthAttribute).FirstOrDefault();
                    if (tmp_sla != null)
                    {
                        parameter.Size = (tmp_sla as StringLengthAttribute).Length;
                    }
                    var tmp_tsa = propertyInfo.GetCustomAttributes(false).Where(a => a is TextStringAttribute).FirstOrDefault();
                    if (tmp_tsa != null)
                    {

                    }
                }
                //加入参数集合
                parameters.Add(parameter);
                //设置sql语句
                s_where.AppendAnd(QuotePrefix + propertyInfo.Name + QuoteSuffix + "=" + ParameterPrefix + propertyInfo.Name);
            }
            //如果实体未设置主键，则异常
            if (parameters.Count <= 0)
            {
                throw new Exception("实体未设置KeyAttribute属性");
            }
            //获取表名
            string tblName = GetTableName<T>();
            //定义数据集
            DataSet ds = new DataSet();
            //创建sql命令查询数据库
            var cmd = con.CreateCommand();
            cmd.CommandText = "select * from " + tblName + " where " + s_where.ToString();
            cmd.CommandType = CommandType.Text;
            //设置sql参数
            foreach (var pp in parameters)
            {
                cmd.Parameters.Add(pp);
            }
            //设置事务
            if (dbTransaction != null)
                cmd.Transaction = dbTransaction;
            //查询数据
            var dapt = CreateDataAdapter();
            dapt.SelectCommand = cmd;
            dapt.Fill(ds);
            //如果数据没记录，则异常提示
            if (ds.Tables[0].Rows.Count <= 0)
            {
                throw new Exception("未找到符合条件的记录");
            }
            //设置行修改状态
            DataRow row = ds.Tables[0].Rows[0];
            row.BeginEdit();
            //修改行的值
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //过滤Ignore和Key属性
                int count = propertyInfo.GetCustomAttributes(false).Where(a => a is IgnoreAttribute || a is KeyAttribute).Count();
                if (count > 0)
                {
                    continue;
                }
                //获取属性值
                object obj = propertyInfo.GetValue(t, null);
                //赋值
                if (obj == null)
                {
                    row[propertyInfo.Name] = DBNull.Value;
                }
                else
                {
                    row[propertyInfo.Name] = obj;
                }
            }
            row.EndEdit();
            //更新数据到数据库
            var scb = CreateCommandBuilder();
            scb.DataAdapter = (System.Data.Common.DbDataAdapter)dapt;
            return dapt.Update(ds);
        }
        finally
        {
            if (dbTransaction == null)
                con.Close();
        }
    }
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="con"></param>
    /// <param name="t"></param>
    /// <param name="trans"></param>
    /// <returns></returns>
    public int Delete<T>(T t) where T : class, new()
    {
        //创建数据库链接
        var con = CreateConnection();
        try
        {
            //定义变量
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            StringBuilder s_where = new StringBuilder();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //如果是主键则跳过
                int count = propertyInfo.GetCustomAttributes(false).Where(a => a is KeyAttribute).Count();
                if (count <= 0)
                {
                    continue;
                }
                //获取实体类的值
                object obj = propertyInfo.GetValue(t, null);
                //创建参数
                IDbDataParameter parameter = null;
                if (obj == null)
                {
                    parameter = CreateParameter(propertyInfo.Name, DBNull.Value);
                }
                else
                {
                    parameter = CreateParameter(propertyInfo.Name, obj);
                }
                //设置参数类型
                parameter.DbType = SetDbType(obj);
                //如果时字符串的参数，设置其大小
                if (obj.GetType() == typeof(string))
                {
                    parameter.DbType = DbType.String;
                    var tmp_sla = propertyInfo.GetCustomAttributes(false).Where(a => a is StringLengthAttribute).FirstOrDefault();
                    if (tmp_sla != null)
                    {
                        parameter.Size = (tmp_sla as StringLengthAttribute).Length;
                    }

                    var tmp_tsa = propertyInfo.GetCustomAttributes(false).Where(a => a is TextStringAttribute).FirstOrDefault();
                    if (tmp_tsa != null)
                    {

                    }

                }
                //设置参数值
                parameters.Add(parameter);

                //设置sql语句
                s_where.AppendAnd(QuotePrefix + propertyInfo.Name + QuoteSuffix + "=" + ParameterPrefix + propertyInfo.Name);
            }
            //校验实体是否设置主键
            if (parameters.Count <= 0)
            {
                throw new Exception("实体未设置KeyAttribute属性");
            }
            //查找表名
            string tblName = GetTableName<T>();
            //定义数据集变量
            DataSet ds = new DataSet();
            //创建命令对象用于查询数据库
            var cmd = con.CreateCommand();
            cmd.CommandText = "select * from " + tblName + " where " + s_where.ToString();
            cmd.CommandType = CommandType.Text;
            //设置参数
            foreach (var pp in parameters)
            {
                cmd.Parameters.Add(pp);
            }
            //设置事务
            if (dbTransaction != null)
                cmd.Transaction = dbTransaction;
            //填充记录
            var dapt = CreateDataAdapter();
            dapt.SelectCommand = cmd;
            dapt.Fill(ds);
            //校验是否有记录
            if (ds.Tables[0].Rows.Count <= 0)
            {
                throw new Exception("未找到符合条件的记录");
            }
            //删除数据
            DataRow row = ds.Tables[0].Rows[0];
            row.Delete();
            //更新删除操作到数据库
            var scb = CreateCommandBuilder();
            scb.DataAdapter = (System.Data.Common.DbDataAdapter)dapt;
            return dapt.Update(ds);
        }
        finally
        {
            if (dbTransaction == null)
                con.Close();
        }
    }
    /// <summary>
    /// 设置类型
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    DbType SetDbType(object obj)
    {
        if (obj.GetType() == typeof(double))
            return DbType.Double;
        else if (obj.GetType() == typeof(float))
            return DbType.Double;
        else if (obj.GetType() == typeof(decimal))
            return DbType.Decimal;
        else if (obj.GetType() == typeof(DateTime))
            return DbType.DateTime;
        else if (obj.GetType() == typeof(int))
            return DbType.Int32;
        else if (obj.GetType() == typeof(long))
            return DbType.Int64;
        else
            return DbType.String;
    }

    public int CreateTable<T>() where T : class, new()
    {
        var name = typeof(T).FullName;
        var c = typeof(T).Assembly.CreateInstance(name) as T;
        return CreateTable(c);
    }
    public virtual int CreateTable<T>(T t)
    {
        string sql = GetTableSql(t);
        if (string.IsNullOrEmpty(sql)) return -1;
        int count = 0;
        try
        {
            count = QueryDataSet("select top 1 * from " + GetTableName(t)).Tables[0].Rows.Count;
        }
        catch
        {
            count = -1;
        }
        if (count < 0)
        {
            ExecuteSql(sql);
        }

        return count;
    }
    /// <summary>
    /// 初始化表数据
    /// </summary>
    protected virtual void InitTableData()
    {

    }
    /// <summary>
    /// 获取创建表的语句
    /// </summary>
    /// <returns></returns>
    public string GetTableSql<T>() where T : class, new()
    {
        var name = typeof(T).FullName;
        var c = typeof(T).Assembly.CreateInstance(name) as T;
        return GetTableSql(c);
    }







    /// <summary>
    /// 获取创建表的语句
    /// </summary>
    /// <returns></returns>
    public virtual string GetTableSql<T>(T t)
    {
        PropertyInfo[] propertyInfos = t.GetType().GetProperties();
        StringBuilder s_pk = new StringBuilder();
        StringBuilder s_col = new StringBuilder();
        //循环属性
        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            //过滤Ignore属性
            int count = propertyInfo.GetCustomAttributes(false).Where(a => a is IgnoreAttribute).Count();
            if (count > 0)
            {
                continue;
            }
            //判断主键
            bool isNull = false;
            var attr_key = propertyInfo.GetCustomAttributes(false).Where(a => a is KeyAttribute).FirstOrDefault();
            if (attr_key != null)
            {
                isNull = false;
                if (s_pk.Length > 0) s_pk.Append(",");
                s_pk.Append(QuotePrefix + propertyInfo.Name + QuoteSuffix);
                bool identity = (attr_key as KeyAttribute).Identity;
                //如果识别到自增长主键，则执行玩之后循环下一条
                if (identity)
                {
                    if (s_col.Length > 0) s_col.AppendLine(",");
                    s_col.AppendFormat("{0} int identity(1,1) not null", QuotePrefix + propertyInfo.Name + QuoteSuffix);
                    continue;
                }
            }

            string dbType = "";
            if (propertyInfo.PropertyType == typeof(double))
            {
                dbType = "numeric(12,2)";
            }
            else if (propertyInfo.PropertyType == typeof(float))
            {
                dbType = "numeric(12,2)";
            }
            else if (propertyInfo.PropertyType == typeof(decimal))
            {
                dbType = "numeric(12,2)";
            }
            else if (propertyInfo.PropertyType == typeof(DateTime))
            {
                dbType = "datetime";
            }
            else if (propertyInfo.PropertyType == typeof(int))
            {
                dbType = "int";
            }
            else if (propertyInfo.PropertyType == typeof(long))
            {
                dbType = "long";
            }
            else if (propertyInfo.PropertyType == typeof(double?))
            {
                isNull = true;
                dbType = "numeric(12,2)";
            }
            else if (propertyInfo.PropertyType == typeof(float?))
            {
                isNull = true;
                dbType = "numeric(12,2)";
            }
            else if (propertyInfo.PropertyType == typeof(decimal?))
            {
                isNull = true;
                dbType = "numeric(12,2)";
            }
            else if (propertyInfo.PropertyType == typeof(DateTime?))
            {
                isNull = true;
                dbType = "datetime";
            }
            else if (propertyInfo.PropertyType == typeof(int?))
            {
                isNull = true;
                dbType = "int";
            }
            else if (propertyInfo.PropertyType == typeof(long?))
            {
                isNull = true;
                dbType = "long";
            }
            else
            {
                isNull = true;
                dbType = "nvarchar(max)";
                var attr_strLen = propertyInfo.GetCustomAttributes(false).Where(a => a is StringLengthAttribute).FirstOrDefault();
                if (attr_strLen != null)
                {
                    int len = (attr_strLen as StringLengthAttribute).Length;
                    dbType = "nvarchar(" + len.ToString() + ")";
                }
            }
            //主键不需要判断是否为空的属性
            var attr_req = propertyInfo.GetCustomAttributes(false).Where(a => a is RequiredAttribute).FirstOrDefault();
            //非主键的可以校验
            if (attr_req != null && attr_key == null)
            {
                isNull = false;
            }
            //主键强制设置为非空
            if (attr_key != null)
            {
                isNull = false;
            }

            if (s_col.Length > 0) s_col.AppendLine(",");
            s_col.AppendFormat("{0} {1} {2}", QuotePrefix + propertyInfo.Name + QuoteSuffix, dbType, isNull ? "null" : "not null");
        }

        StringBuilder s_tbl = new StringBuilder();
        s_tbl.AppendLine("CREATE TABLE " + GetTableName(t) + "(");
        s_tbl.AppendLine(s_col.ToString()+",");
        s_tbl.AppendLine("constraint pk_" + GetTableName(t).Replace(QuotePrefix, "").Replace(QuoteSuffix, "") + " primary key(" + s_pk.ToString() + ")");
        s_tbl.AppendLine(")");


        return s_tbl.ToString();
    }
}

