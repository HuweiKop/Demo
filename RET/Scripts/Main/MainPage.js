///创建人：
///修改人：
///最后修改日期：
///功能介绍：

/// <summary>
/// 全局变量
/// </summary>
//用于rightpage子页面中返回的url
var backURL = new Array();
//用于记录PersonalProfileSelectTree页面中选择的员工
var gPersonalProfileId = "";
//记录当前滚动条的位置
var gScrollY;
var gScrollX;

$(function () {
    //显示头像
    var attachments = $("#attachments").val();
    var avtar = "resources/images/portrait.jpg";
    if (attachments != "") {
        avtar = attachments;
    }
    $("#avatar").attr("src", $("#avatar").attr("src") + avtar);

    style_path = "resources/css/colors";
    $("#box-tabs, #box-left-tabs").tabs();

    //根据用户权限显示右侧菜单项
    var perminssionString = $("#permission").val();
    perminssionString = perminssionString.replace(/&quot;/g, '"');
    var permissionArray = $.parseJSON(perminssionString);
    for (i in permissionArray) {
        var permissionName = permissionArray[i] + "";
        permissionName = permissionName.replace(/\s/g, "");
        permissionName = permissionName.replace(/\//g, "");
        $("#" + permissionName).css("display", "block");
        $("#" + permissionName).parent().css("display", "block");
        $("#" + permissionName).parent().parent('.slide').css("display", "block");
    }

    //作为测试暂时显示
    $("#h-menu-rpts a").css("display", "block");
    $("#menu-rpts a").css("display", "block");
    $("#menu-rpts li").css("display", "block");

    $("#dialog-message-success").dialog({
        autoOpen: false,
        height: 170,
        modal: true,
        buttons: {
            Ok: function () {
                $(this).dialog('close');
            }
        }
    });

    $("#dialog-message-failure").dialog({
        autoOpen: false,
        height: 170,
        modal: true,
        buttons: {
            Ok: function () {
                $(this).dialog('close');
            }
        }
    });

    $("#dialog-message").dialog({
        autoOpen: false,
        height: 710,
        width: 800,
        modal: true,
        buttons: {
            Ok: function () {
                $(this).dialog('close');
            }
        }
    });

    //导入excel
    $("#import-dialog").dialog({
        autoOpen: false,
        height: 500,
        width: 550,
        modal: true,
        position: [200, 100],
        buttons: {
            'Save': function () {
                window.frames['iframeImportIIEvalue'].SubmitForm();
            },
            Cancel: function () {
                $(this).dialog('close');
                $("#iframeImportIIEvalue").attr("src", "");
            }
        },
        close: function (event, ui) { $("#iframeImportIIEvalue").attr("src", ""); }
    });

    //更改浏览器模式提示
    $("#dialog-message-warning").dialog({
        autoOpen: false,
        resizable: false,
        height: 170,
        modal: true,
        position: [200, 100],
        buttons: {
            'OK': function () {
                $(this).dialog('close');
            }
        }
    });

    //更改浏览器模式提示
    var userAgent = navigator.userAgent;
    //当为兼容模式时弹出提示框
    if (userAgent.indexOf("MSIE 7.0") > 0 && userAgent.indexOf("Trident") > 0) {
        $("#iframeViewSetting").attr("src", "ViewSettingDialog");
        center($("#dialog-message").dialog("option", "width"), $("#dialog-message").dialog("option", "height"), "dialog-message");
        $("#dialog-message").dialog("open");
    }
});

//$(function () {
//    $("#dialog-message-warning").dialog({
//        autoOpen: false,
//        resizable: false,
//        height: 170,
//        modal: true,
//        position: [200, 100],
//        buttons: {
//            'OK': function () {
//                $(this).dialog('close');
//            }
//        }
//    });
//});

//iframe自适应
function dyniframesize(down) {
    var pTar = null;
    if (document.getElementById) {
        pTar = document.getElementById(down);
        pTar.height = 1000;
    }
    else {
        eval('pTar = ' + down + ';');
    }
    if (pTar && !window.opera) {
        //begin resizing iframe 
        pTar.style.display = "block"
        if (pTar.contentDocument && pTar.contentDocument.body.offsetHeight) {
            //ns6 syntax 
            pTar.height = pTar.contentDocument.body.offsetHeight + 20;
            pTar.width = pTar.contentDocument.body.scrollWidth + 20;
        }
        else if (pTar.Document && pTar.Document.body.scrollHeight) {
            //ie5+ syntax 
            pTar.height = pTar.Document.body.scrollHeight;
            pTar.width = pTar.Document.body.scrollWidth;
        }
    }

    pTar.height -= 20;
}

//返回页面顶部
function pageScroll() {
    //window.scrollBy(0, -10);
    //scrolldelay = setTimeout('pageScroll()', 100);
    //if (document.documentElement.scrollTop == 0) clearTimeout(scrolldelay);
    scroll(0, 0);
}

//设置滚动条的位置
function setPageScroll() {
    scroll(0, gScrollY);
}

//获取滚动条的位置
function GetPageScroll() {
    var x, y;
    if (window.pageYOffset) {
        // all except IE
        y = window.pageYOffset;
        x = window.pageXOffset;
    } else if (document.documentElement
      && document.documentElement.scrollTop) {
        // IE 6 Strict
        y = document.documentElement.scrollTop;
        x = document.documentElement.scrollLeft;
    } else if (document.body) {
        // all other IE
        y = document.body.scrollTop;
        x = document.body.scrollLeft;
    }

    gScrollX = x;
    gScrollY = y;
}

function OperateJob(operate) {
    $("#warningNotification").text(operate);
    OpenDialog("dialog-message-warning");
    return false;
}

//设置弹出框位置居中
function center(width, height, id) {
    var top = ($(window.parent).height() - height) / 2;
    var left = ($(window).width() - width) / 2;

    $("#" + id).dialog('option', 'position', [left, top]);
}