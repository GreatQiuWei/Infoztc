﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddServiceContent.aspx.cs" Inherits="TygaSoft.Web.Templates.Admin.AddServiceContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="dlgFmServiceContent" runat="server">
    <div>
        <div class="row">
            <span class="fl rl">所属服务分类：</span>
            <div class="fl ml10">
                <span></span>
                <input type="hidden" name="ServiceItemId" />
            </div>
            <span class="clr"></span>
        </div>
        <div class="row mt10">
            <span class="fl rl"><b class="cr">*</b>名称：</span>
            <div class="fl ml10">
                <input type="text" id="txtNamed_ServiceContent" runat="server" clientidmode="Static" tabindex="1" maxlength="256" class="easyui-validatebox txt" data-options="required:true" />
            </div>
            <span class="clr"></span>
        </div>
        <div class="row mt10">
            <span class="fl rl">图片：</span>
            <div class="fl ml10">
                <img id="imgPicture_ServiceContent" runat="server" clientidmode="Static" src="../../Images/nopic.gif" alt="图片" width="150" height="140" onclick="ListServiceContent.OnPictureClick()" />
                <input type="hidden" id="hPictureId_ServiceContent" runat="server" clientidmode="Static" />
            </div>
            <span class="clr"></span>
        </div>
        <div class="row mt10">
            <span class="fl rl">排序：</span>
            <div class="fl ml10">
                <input type="text" name="Sort" tabindex="4" maxlength="9" class="easyui-validatebox txt" data-options="validType:'int'" />
            </div>
            <span class="clr"></span>
        </div>
        <div class="row mt10">
            <span class="rl">有效时间：</span>
            <div class="fl ml10">
                <input id="txtEnableStartTime_ServiceContent" runat="server" clientidmode="Static" class="easyui-datetimebox" style="width:160px;" />
            </div>
            <div class="fl mlr10"> 至 </div>
            <div class="fl">
                <input id="txtEnableEndTime_ServiceContent" runat="server" clientidmode="Static" class="easyui-datetimebox" style="width:160px;" />
            </div>
            <span class="clr"></span>
        </div>
        <div class="row mt10">
            <span class="rl">是否禁用：</span>
            <div class="fl ml10">
                <asp:RadioButtonList ID="rbtnList_ServiceContent" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:RadioButtonList>
            </div>
            <span class="clr"></span>
        </div>
        <div class="row mt10">
            <span class="fl rl">简介：</span>
            <div class="fl ml10">
                <textarea id="txtaDescr_ServiceContent" runat="server" clientidmode="Static" rows="3" cols="100" style="width:100%; height:70px;"></textarea>
            </div>
            <span class="clr"></span>
        </div>
        <div class="row mt10">
            <span class="fl rl">内容：</span>
            <div class="fl ml10">
                <textarea id="txtaContent_ServiceContent" name="txtaContent_ServiceContent" runat="server" clientidmode="Static" cols="100" rows="8"></textarea>
            </div>
            <span class="clr"></span>
        </div>

        <input type="hidden" id="hServiceContentId" runat="server" clientidmode="Static" />
    </div>

    <asp:Literal ID="ltrMyData" runat="server"></asp:Literal>

    </form>

    <div id="dlgSingleSelectServiceContentPicture" class="easyui-dialog" title="选择图片（单选）" data-options="closed:true,modal:true,href:'/t/tpicture.html?dlgId=dlgSingleSelectServiceContentPicture&funName=PictureServiceContent',width:810,height:$(window).height()*0.8,
        buttons: [{
            id:'btnSingleSelectServiceContentPicture',text:'确定',iconCls:'icon-ok',
            handler:function(){
                ListServiceContent.SetSinglePicture('imgPicture_ServiceContent');
                $('#dlgSingleSelectServiceContentPicture').dialog('close');
            }
        },{
            id:'btnCancelSingleSelectServiceContentPicture', text:'取消',iconCls:'icon-cancel',
            handler:function(){
                $('#dlgSingleSelectServiceContentPicture').dialog('close');
            }
        }],
        toolbar:[{
            id:'dlgSingleSelectServiceContentPictureToolbarUpload',text:'上传',iconCls:'icon-add',
		    handler:function(){
                DlgPictureSelect.DlgUpload();
            }
	    }]" style="padding:10px;"></div>

    <script type="text/javascript">
        var editor_content;
        editor_content = KindEditor.create('textarea[name=txtaContent_ServiceContent]', {
            cssPath: '/Scripts/plugins/kindeditor/plugins/code/prettify.css',
            width: '1000px',
            height: '800px',
            uploadJson: '../../Handlers/Admin/HandlerKindeditor.ashx',
            fileManagerJson: '../../Handlers/Admin/HandlerKindeditor.ashx',
            allowFileManager: true,
            afterCreate: function () {
                var self = this;
                KindEditor.ctrl(document, 13, function () {
                });
                KindEditor.ctrl(self.edit.doc, 13, function () {
                });
            }
        });

        var ServiceContent = {
            Init: function () {
                this.InitModel();
                this.InitParent();
            },
            InitParent: function () {
                var node = $("#treeCt").tree('getSelected');
                if (node) {
                    if ($("#hServiceContentId").val() == "") {
                        var hParentId = $("#dlgFmServiceContent").find("[name=ServiceItemId]");
                        hParentId.val(node.id);
                        hParentId.prev().text(node.text);
                    }
                }
            },
            InitModel: function () {
                if ($.trim($("#hServiceContentId").val()) != "") {
                    var fmId = $("#dlgFmServiceContent");
                    fmId.find("[id=myDataAppend_ServiceContent]").children().each(function () {
                        if ($(this).attr("code") == "myDataForModel") {
                            var jsonModel = eval("(" + $(this).html() + ")");
                            $.map(jsonModel, function (item) {
                                fmId.find("[name=ServiceItemId]").val(item.ServiceItemId);
                                fmId.find("[name=ServiceItemId]").prev().text(item.ServiceItemName);
                                fmId.find("[name=Sort]").val(item.Sort);

                            })
                        }
                    })
                }
            }
        }

        $(function () {
            ServiceContent.Init();
        })
    </script>
</body>
</html>
