﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Text;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.WebHelper;
using TygaSoft.DBUtility;

namespace TygaSoft.Web.Admin.AboutSite
{
    public partial class ListAdOther : System.Web.UI.Page
    {
        StringBuilder myDataAppend = new StringBuilder(1000);
        int pageIndex = WebCommon.PageIndex;
        int pageSize = WebCommon.PageSize10;
        string queryStr;
        ParamsHelper parms;
        string sqlWhere;
        string keyword;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                NameValueCollection nvc = Request.QueryString;
                int index = 0;
                foreach (string item in nvc.AllKeys)
                {
                    GetParms(item, nvc);

                    if (item != "pageIndex" && item != "pageSize")
                    {
                        index++;
                        if (index > 1) queryStr += "&";
                        queryStr += string.Format("{0}={1}", item, Server.HtmlEncode(nvc[item]));
                    }
                }

                Bind();
            }

            ltrMyData.Text = "<div id=\"myDataAppend\" style=\"display:none;\">" + myDataAppend.ToString() + "</div>";
        }

        private void Bind()
        {
            var adId = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["adId"]))
            {
                Guid.TryParse(Request.QueryString["adId"], out adId);
            }
            if (adId.Equals(Guid.Empty))
            {
                MessageBox.Messager(this.Page, "请先完成基本信息后再进行此操作", MessageContent.AlertTitle_Error, "error");
                return;
            }

            //查询条件
            GetSearchItem();

            int totalRecords = 0;
            AdItem bll = new AdItem();

            rpData.DataSource = bll.GetDsByJoinForAdmin(pageIndex, pageSize, out totalRecords, sqlWhere, parms == null ? null : parms.ToArray());
            rpData.DataBind();

            myDataAppend.Append("<div id=\"myDataForPage\" style=\"display:none;\">[{\"PageIndex\":\"" + pageIndex + "\",\"PageSize\":\"" + pageSize + "\",\"TotalRecord\":\"" + totalRecords + "\",\"QueryStr\":\"" + queryStr + "\"}]</div>");
        }

        private void GetSearchItem()
        {
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                //if (parms == null) parms = new ParamsHelper();

                //sqlWhere += "and SupplierName like @SupplierName ";
                //SqlParameter parm = new SqlParameter("@SupplierName", SqlDbType.NVarChar, 30);
                //parm.Value = "%" + keyword + "%";
                //parms.Add(parm);

            }
        }

        private void GetParms(string key, NameValueCollection nvc)
        {
            switch (key)
            {
                case "pageIndex":
                    Int32.TryParse(nvc[key], out pageIndex);
                    break;
                case "pageSize":
                    Int32.TryParse(nvc[key], out pageSize);
                    break;
                case "keyword":
                    txtKeyword.Value = nvc[key];
                    keyword = nvc[key]; 
                    break;
                default:
                    break;
            }
        }
    }
}