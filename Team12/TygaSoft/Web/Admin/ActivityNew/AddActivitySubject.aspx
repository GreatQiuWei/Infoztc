﻿<%@ Page Title="新建活动" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="AddActivitySubject.aspx.cs" Inherits="TygaSoft.Web.Admin.ActivityNew.AddActivitySubject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Scripts/plugins/kindeditor/themes/default/default.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Scripts/plugins/kindeditor/plugins/code/prettify.css" rel="stylesheet"
        type="text/css" />
    <script src="../../Scripts/JeasyuiExtend.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/plugins/kindeditor/kindeditor.js"></script>
    <script type="text/javascript" src="../../Scripts/plugins/kindeditor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../../Scripts/plugins/kindeditor/plugins/code/prettify.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="row mt10">
        <span class="rl"><b class="cr">*</b>标题：</span>
        <div class="fl">
            <input type="text" id="txtTitle" runat="server" clientidmode="Static" class="easyui-textbox mtxt"
                data-options="required:true,missingMessage:'必填项'" style="width: 400px" />
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="fl rl">图片：</span>
        <div class="fl ml10">
            <img id="imgSinglePicture" runat="server" clientidmode="Static" src="../../Images/nopic.gif"
                alt="图片" width="120" height="120" onclick="DlgSelectPicture.DlgSingle('ActivityPhotoPicture')" /><br />
            <input id="hImgSinglePictureId" runat="server" clientidmode="Static" type="hidden" />
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl"><b class="cr">*</b>内容：</span>
        <div class="fl">
            <textarea id="txtContent" runat="server" clientidmode="Static" cols="120" rows="25"></textarea>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl"><b class="cr">*</b>有效期：</span>
        <div class="fl">
            <input class="easyui-datebox mtxt" id="startDate" runat="server" clientidmode="Static"
                data-options="required:true,missingMessage:'必填项'" style="width: 150px" />-
            <input class="easyui-datebox mtxt" id="endDate" runat="server" clientidmode="Static"
                data-options="required:true,missingMessage:'必填项'" style="width: 150px" />
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl"><b class="cr">*</b>排序：</span>
        <div class="fl">
            <input type="text" id="txtSort" value="0" runat="server" clientidmode="Static" class="easyui-numberbox mtxt"
                data-options="required:true,missingMessage:'必填项'" style="width: 60px" />
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl"><b class="cr">*</b>最大投票数：</span>
        <div class="fl">
            <input type="text" id="txtMaxVoteCount" value="10000" runat="server" clientidmode="Static"
                data-options="required:true,missingMessage:'必填项'" class="easyui-numberbox mtxt"
                style="width: 60px" />
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl"><b class="cr">*</b>最大报名数：</span>
        <div class="fl">
            <input type="text" id="txtMaxSignUpCount" value="5000" runat="server" clientidmode="Static"
                data-options="required:true,missingMessage:'必填项'" class="easyui-numberbox mtxt"
                style="width: 60px" />
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl">实际报名数：</span>
        <div class="fl">
            <input type="text" id="txtActualSignUpCount" value="0" runat="server" clientidmode="Static"
                readonly="readonly" class="easyui-numberbox mtxt" style="width: 60px" />
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl">虚拟报名数：</span>
        <div class="fl">
            <input type="text" id="txtUpdateSignUpCount" value="0" runat="server" clientidmode="Static"
                class="easyui-numberbox mtxt" style="width: 60px" />
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl"><b class="cr">*</b>报名规则：</span>
        <div class="fl">
            <textarea id="txtSignUpRule" runat="server" clientidmode="Static" cols="120" rows="15"></textarea>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl">访问量：</span>
        <div class="fl">
            <input type="text" id="txtViewCount" value="0" runat="server" clientidmode="Static"
                class="easyui-numberbox mtxt" style="width: 60px" />
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl">选手隐藏项：</span>
        <div class="fl">
            <input type="checkbox" id="Professional" runat="server" name="ckHideAttr" value="false"
                clientidmode="Static" /><label>专业</label>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl">是否刮刮奖：</span>
        <div class="fl">
            <input type="radio" id="rdPrizeFalse" runat="server" name="rdIsPrize" value="false" clientidmode="Static" checked="true" /><label>否</label>
            <input type="radio" id="rdPrizeTrue" runat="server" name="rdIsPrize" value="true" clientidmode="Static" /><label>是</label>
        </div>
        <span class="clr"></span>
    </div>
    <div id="divPrize" style="display:none;" runat="server" clientidmode="Static">
        <div class="row mt10">
            <span class="rl">中奖几率：</span>
            <div class="fl">
                <input type="text" id="txtPrizeProbability" value="0" runat="server" clientidmode="Static" class="easyui-numberbox mtxt"
                    data-options="max:1000,precision:0" style="width: 60px" />‰
            </div>
            <span class="clr"></span>
        </div>
        <div class="row mt10">
            <span class="rl">刮刮奖规则：</span>
            <div class="fl">
                <textarea id="txtPrizeRule" runat="server" clientidmode="Static" style="width:859px;height:220px;"></textarea>
            </div>
            <span class="clr"></span>
        </div>
    </div>
    <div class="row mt10">
        <span class="rl">是否禁用：</span>
        <div class="fl">
            <input type="radio" id="rdFalse" runat="server" name="rdIsDisable" value="false"
                clientidmode="Static" checked="true" /><label>否</label>
            <input type="radio" id="rdTrue" runat="server" name="rdIsDisable" value="true" clientidmode="Static" /><label>是</label>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl">是否推送：</span>
        <div class="fl">
            <input type="radio" id="rdPushFalse" runat="server" name="rdIsPush" value="false" clientidmode="Static" checked="true" /><label>否</label>
            <input type="radio" id="rdPushTrue" runat="server" name="rdIsPush" value="true" clientidmode="Static" /><label>是</label>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl">&nbsp;</span>
        <div class="fl">
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save'"
                onclick="AddActivitySubject.OnSave()">提交</a>
        </div>
        <span class="clr"></span>
    </div>
    <input type="hidden" id="hId" runat="server" clientidmode="Static" />
    <div id="dlgUploadPicture" style="padding: 10px;">
    </div>
    <div id="dlgSingleSelectPicture" class="easyui-dialog" title="选择图片（单选）" data-options="closed:true,modal:true,href:'/t/yt.html?dlgId=dlgSingleSelectPicture&funName=ActivityPhotoPicture',width:810,height:$(window).height()*0.8,
    buttons: [{
    id:'btnSelectPicture',text:'确定',iconCls:'icon-ok',
        handler:function(){
            DlgSelectPicture.SetSinglePicture('imgSinglePicture');
            $('#dlgSingleSelectPicture').dialog('close');
        }
    },{
    id:'btnCancelSelectPicture', text:'取消',iconCls:'icon-cancel',
        handler:function(){
	        $('#dlgSingleSelectPicture').dialog('close');
        }
    }],
    toolbar:[{
        id:'dlgSingleSelectPictureToolbarUpload',text:'上传',iconCls:'icon-add',
		handler:function(){
            DlgSelectPicture.DlgUpload();
        }
	}]" style="padding: 10px;">
    </div>
    <script type="text/javascript" src="../../Scripts/Admin/DlgSelectPicture.js"></script>
    <script type="text/javascript" src="../../Scripts/Admin/ActivityNew/AddActivitySubject.js"></script>
    <script type="text/javascript">
        var editor_content;
        var editor_signUpRule;
        var editor_PrizeRule;
        KindEditor.ready(function (K) {
            editor_content = K.create('#txtContent', {
                uploadJson: '../../Handlers/Admin/HandlerKindeditor.ashx',
                fileManagerJson: '../../Handlers/Admin/HandlerKindeditor.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                    });
                }
            });

            editor_signUpRule = K.create('#txtSignUpRule', {
                uploadJson: '../../Handlers/Admin/HandlerKindeditor.ashx',
                fileManagerJson: '../../Handlers/Admin/HandlerKindeditor.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                    });
                }
            });

            editor_PrizeRule = K.create('#txtPrizeRule', {
                uploadJson: '../../Handlers/Admin/HandlerKindeditor.ashx',
                fileManagerJson: '../../Handlers/Admin/HandlerKindeditor.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                    });
                }
            });
            prettyPrint();

        });

        $(function () {
            try {
                AddActivitySubject.Init();
            }
            catch (e) {
                $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
            }
        });
    </script>
</asp:Content>