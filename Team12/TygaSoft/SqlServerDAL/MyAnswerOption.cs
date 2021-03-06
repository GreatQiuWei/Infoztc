﻿using System;
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
    public partial class AnswerOption
    {
        public int InsertOW(AnswerOptionInfo model)
        {
            int sort = 0;
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select max(Sort) as MaxSort from AnswerOption where QuestionSubjectId = @QuestionSubjectId");
            SqlParameter[] cmdParms = {
                                       new SqlParameter("@QuestionSubjectId",SqlDbType.UniqueIdentifier)
                                   };
            cmdParms[0].Value = model.QuestionSubjectId;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.HnztcTeamDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        sort = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    }
                }
            }
            model.Sort = sort + 1;
            sb.Clear();

            sb.Append(@"insert into AnswerOption (QuestionSubjectId,OptionContent,Sort,IsTrue,Remark,IsDisable,LastUpdatedDate)
			            values
						(@QuestionSubjectId,@OptionContent,@Sort,@IsTrue,@Remark,@IsDisable,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@QuestionSubjectId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OptionContent",SqlDbType.NVarChar,512),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@IsTrue",SqlDbType.Bit),
new SqlParameter("@Remark",SqlDbType.NVarChar,300),
new SqlParameter("@IsDisable",SqlDbType.Bit),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.QuestionSubjectId;
            parms[1].Value = model.OptionContent;
            parms[2].Value = model.Sort;
            parms[3].Value = model.IsTrue;
            parms[4].Value = model.Remark;
            parms[5].Value = model.IsDisable;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.HnztcTeamDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int DeleteForQS(object QuestionSubjectId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from AnswerOption where QuestionSubjectId = @QuestionSubjectId");
            SqlParameter parm = new SqlParameter("@QuestionSubjectId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(QuestionSubjectId.ToString());

            return SqlHelper.ExecuteNonQuery(SqlHelper.HnztcTeamDbConnString, CommandType.Text, sb.ToString(), parm);
        }

        public bool DeleteBatchForQS(IList<object> list)
        {
            if (list == null || list.Count == 0) return false;

            bool result = false;
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from AnswerOption where QuestionSubjectId = @QuestionSubjectId" + n + " ;");
                SqlParameter parm = new SqlParameter("@QuestionSubjectId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.HnztcTeamDbConnString))
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

        public IList<AnswerOptionInfo> GetListOW(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from AnswerOption ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.HnztcTeamDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<AnswerOptionInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by AO.Sort asc) as RowNumber,
			          AO.Id,AO.QuestionSubjectId,AO.OptionContent,AO.Sort,AO.IsTrue,AO.Remark,AO.IsDisable,AO.LastUpdatedDate,QS.QuestionContent
					  from AnswerOption AO, QuestionSubject QS ");
            sb.AppendFormat(" where AO.QuestionSubjectId = QS.Id {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<AnswerOptionInfo> list = new List<AnswerOptionInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.HnztcTeamDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AnswerOptionInfo model = new AnswerOptionInfo();
                        model.Id = reader.GetGuid(1);
                        model.QuestionSubjectId = reader.GetGuid(2);
                        model.OptionContent = reader.GetString(3);
                        model.Sort = reader.GetInt32(4);
                        model.IsTrue = reader.GetBoolean(5);
                        model.Remark = reader.GetString(6);
                        model.IsDisable = reader.GetBoolean(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);
                        model.RowNumber = reader.GetInt64(0);
                        model.QuestionContent = reader.GetString(9);
                        model.IsTrueContent = model.IsTrue ? "是" : "否";
                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AnswerOptionInfo> GetListOW(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by AO.Sort asc) as RowNumber,
			           AO.Id,AO.QuestionSubjectId,AO.OptionContent,AO.Sort,AO.IsTrue,AO.Remark,AO.IsDisable,AO.LastUpdatedDate,QS.QuestionContent
					  from AnswerOption AO, QuestionSubject QS ");
            sb.AppendFormat(" where AO.QuestionSubjectId = QS.Id {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<AnswerOptionInfo> list = new List<AnswerOptionInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.HnztcTeamDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AnswerOptionInfo model = new AnswerOptionInfo();
                        model.Id = reader.GetGuid(1);
                        model.QuestionSubjectId = reader.GetGuid(2);
                        model.OptionContent = reader.GetString(3);
                        model.Sort = reader.GetInt32(4);
                        model.IsTrue = reader.GetBoolean(5);
                        model.Remark = reader.GetString(6);
                        model.IsDisable = reader.GetBoolean(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }
    }
}
