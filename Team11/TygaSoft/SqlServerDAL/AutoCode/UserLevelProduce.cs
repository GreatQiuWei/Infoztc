using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class UserLevelProduce : IUserLevelProduce
    {
        #region IUserLevelProduce Member

        public int Insert(UserLevelProduceInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into UserLevelProduce (UserId,FunCode,EnumSource,TotalGold,TotalSilver,TotalIntegral,LastUpdatedDate)
			            values
						(@UserId,@FunCode,@EnumSource,@TotalGold,@TotalSilver,@TotalIntegral,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@FunCode",SqlDbType.Int),
                                        new SqlParameter("@EnumSource",SqlDbType.Int),
                                        new SqlParameter("@TotalGold",SqlDbType.Int),
                                        new SqlParameter("@TotalSilver",SqlDbType.Int),
                                        new SqlParameter("@TotalIntegral",SqlDbType.Int),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.FunCode;
            parms[2].Value = model.EnumSource;
            parms[3].Value = model.TotalGold;
            parms[4].Value = model.TotalSilver;
            parms[5].Value = model.TotalIntegral;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(UserLevelProduceInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update UserLevelProduce set UserId = @UserId,FunCode = @FunCode,EnumSource = @EnumSource,TotalGold = @TotalGold,TotalSilver = @TotalSilver,TotalIntegral = @TotalIntegral,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@FunCode",SqlDbType.Int),
new SqlParameter("@EnumSource",SqlDbType.Int),
new SqlParameter("@TotalGold",SqlDbType.Int),
new SqlParameter("@TotalSilver",SqlDbType.Int),
new SqlParameter("@TotalIntegral",SqlDbType.Int),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.FunCode;
            parms[3].Value = model.EnumSource;
            parms[4].Value = model.TotalGold;
            parms[5].Value = model.TotalSilver;
            parms[6].Value = model.TotalIntegral;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(object Id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from UserLevelProduce where Id = @Id");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, sb.ToString(), parm);
        }

        public bool DeleteBatch(IList<object> list)
        {
            bool result = false;
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from UserLevelProduce where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.SqlProviderConnString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int effect = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null);
                        tran.Commit();
                        if (effect > 0) result = true;
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                }
            }
            return result;
        }

        public UserLevelProduceInfo GetModel(object Id)
        {
            UserLevelProduceInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,FunCode,EnumSource,TotalGold,TotalSilver,TotalIntegral,LastUpdatedDate 
			            from UserLevelProduce
						where Id = @Id ");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new UserLevelProduceInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.FunCode = reader.GetInt32(2);
                        model.EnumSource = reader.GetInt32(3);
                        model.TotalGold = reader.GetInt32(4);
                        model.TotalSilver = reader.GetInt32(5);
                        model.TotalIntegral = reader.GetInt32(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);
                    }
                }
            }

            return model;
        }

        public IList<UserLevelProduceInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from UserLevelProduce ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<UserLevelProduceInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,FunCode,EnumSource,TotalGold,TotalSilver,TotalIntegral,LastUpdatedDate
					  from UserLevelProduce ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<UserLevelProduceInfo> list = new List<UserLevelProduceInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserLevelProduceInfo model = new UserLevelProduceInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.FunCode = reader.GetInt32(3);
                        model.EnumSource = reader.GetInt32(4);
                        model.TotalGold = reader.GetInt32(5);
                        model.TotalSilver = reader.GetInt32(6);
                        model.TotalIntegral = reader.GetInt32(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<UserLevelProduceInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,UserId,FunCode,EnumSource,TotalGold,TotalSilver,TotalIntegral,LastUpdatedDate
					   from UserLevelProduce ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<UserLevelProduceInfo> list = new List<UserLevelProduceInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserLevelProduceInfo model = new UserLevelProduceInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.FunCode = reader.GetInt32(3);
                        model.EnumSource = reader.GetInt32(4);
                        model.TotalGold = reader.GetInt32(5);
                        model.TotalSilver = reader.GetInt32(6);
                        model.TotalIntegral = reader.GetInt32(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<UserLevelProduceInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,UserId,FunCode,EnumSource,TotalGold,TotalSilver,TotalIntegral,LastUpdatedDate
                        from UserLevelProduce ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            IList<UserLevelProduceInfo> list = new List<UserLevelProduceInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserLevelProduceInfo model = new UserLevelProduceInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.FunCode = reader.GetInt32(2);
                        model.EnumSource = reader.GetInt32(3);
                        model.TotalGold = reader.GetInt32(4);
                        model.TotalSilver = reader.GetInt32(5);
                        model.TotalIntegral = reader.GetInt32(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<UserLevelProduceInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,UserId,FunCode,EnumSource,TotalGold,TotalSilver,TotalIntegral,LastUpdatedDate 
			            from UserLevelProduce
					    order by LastUpdatedDate desc ");

            IList<UserLevelProduceInfo> list = new List<UserLevelProduceInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserLevelProduceInfo model = new UserLevelProduceInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.FunCode = reader.GetInt32(2);
                        model.EnumSource = reader.GetInt32(3);
                        model.TotalGold = reader.GetInt32(4);
                        model.TotalSilver = reader.GetInt32(5);
                        model.TotalIntegral = reader.GetInt32(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
