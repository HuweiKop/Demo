//搜索框输入内容时,联想词汇
var IsFocus = false;
var Index = 0;
var gDropdowndivId;
$(function () {
    //去掉前后空格、制表符和 换页符
    String.prototype.trim = function () {
        return this.replace(/^\s+/g, "").replace(/\s+$/g, "");
    }

    ////去掉前后空格、制表符和 换页符
    //String.prototype.trim = function (value) {
    //    return this.replace(/^\S/g, "").replace(/\S$/g, "");
    //}

    //兼容ie，使用js控制th hover样式
    $("th[class=sortTh]").mouseover(function () {
        $(this).css("background", "#336699 url('../resources/images/colors/greyblue/menu_border.png') repeat");
    }).mouseout(function () {
        $(this).css("background", "#eeeeee");
    });

    //联想搜索中监听键盘事件
    $(document).keydown(function (keydownevent) {
        if ($("#" + gDropdowndivId).is(":hidden"))
            return;

        if (keydownevent.keyCode == 40) {
            if (!IsFocus) {
                $("#" + gDropdowndivId + " tr").first().focus();
                $("#" + gDropdowndivId).scrollTop(0);
            }
            else if ($("#" + gDropdowndivId + " tr").length == 1) { $("#" + gDropdowndivId + " tr").first().focus(); }
            else {
                if (Index == $("#" + gDropdowndivId + " tr").last().index()) {
                    $("#" + gDropdowndivId + " tr").last().focus();
                    return;
                }
                $("#" + gDropdowndivId + " tr").eq(Index + 1).focus();

                $("#" + gDropdowndivId + " tr").eq(Index - 1).blur();
                
                var position = $("#" + gDropdowndivId).scrollTop();
                var scrollHeight = $("#" + gDropdowndivId + " tr").eq(Index+1).height();
                $("#" + gDropdowndivId).scrollTop(position + scrollHeight);
            }
        }
        if (keydownevent.keyCode == 38) {
            if (!IsFocus) {
                $("#" + gDropdowndivId + " tr").first().focus();
                $("#" + gDropdowndivId).scrollTop(0);
            }
            else if ($("#" + gDropdowndivId + " tr").length == 1) { $("#" + gDropdowndivId + " tr").first().focus(); }
            else {
                if (Index == 0) {
                    $("#" + gDropdowndivId + " tr").first().focus();
                    return;
                }
                $("#" + gDropdowndivId + " tr").eq(Index - 1).focus();

                $("#" + gDropdowndivId + " tr").eq(Index + 1).blur();
                var position = $("#" + gDropdowndivId).scrollTop();
                var scrollHeight = $("#" + gDropdowndivId + " tr").eq(Index-1).height();
                $("#" + gDropdowndivId).scrollTop(position - scrollHeight);
            }
        }
        if (keydownevent.keyCode == 13) {
            if (Index != -1) {
                var text = $("#" + gDropdowndivId + " tr").eq(Index).text();
                $("#" + gDropdowndivId).prev().val(text);
            }
            $("#" + gDropdowndivId).hide();

        }
    });

    $(document).click(function () {
        $("#" + gDropdowndivId).hide();
        IsFocus = false;
    });

});


//联想搜索时，点击输入框的事件
function InputClick(actionname, value, personalProfileId, dropdowndivId, otherinfo)
{
    Index = -1;
    IsFocus = false;
    gDropdowndivId = dropdowndivId;
    $("#" + gDropdowndivId).scrollTop(0);
    if (value == "") {
        $.post(actionname, { value: value, otherinfo: otherinfo }, function (data, status) {
            if (data[0].names.length == 0) {
                $("#" + dropdowndivId).hide();
                return;
            }
            $("#" + dropdowndivId + " tr").remove();
            for (var i in data[0].names) {
                $("#" + dropdowndivId + " table").append("<tr><td>" + data[0].names[i].name + "</td></tr>");
                $("#" + dropdowndivId).css("background-color", "white");
            }

            $("#" + dropdowndivId + " tr").click(function () {
                var suggesttext = $(this).text();
                $("#" + dropdowndivId).prev().val(suggesttext);
                $("#" + dropdowndivId).hide();
            }).hover(function () {
                $(this).css("background", "#EFF3FF");
            }, function () {
                $(this).css("background", "white");
            }).focus(function () {
                $(this).css("background", "#EFF3FF");
                IsFocus = true;
                Index = $(this).index();
            }).blur(function () {
                $(this).css("background", "white");

            });


            if (data[0].names.length == 1 && $("#" + dropdowndivId + " td").eq(0).text() == $("#" + dropdowndivId).prev().val()) {
                $("#" + dropdowndivId).hide();
            }
            else {
                $("#" + dropdowndivId).show();
            }

        });
    }
}

//联想搜索中，输入框值改变时的事件
function ValueChanged(actionname, value, personalProfileId, dropdowndivId, otherinfo) {
    Index = -1;
    IsFocus = false;
    gDropdowndivId = dropdowndivId;
    if (value == "") {
        $("#" + dropdowndivId).hide();
        return;
    }
    if (personalProfileId == "") {
        $.post(actionname, { value: value, otherinfo: otherinfo }, function (data, status) {
            if (data[0].names.length == 0) {
                $("#" + dropdowndivId).hide();
                return;
            }
            $("#" + dropdowndivId + " tr").remove();
            for (var i in data[0].names) {
                $("#" + dropdowndivId + " table").append("<tr><td>" + data[0].names[i].name + "</td></tr>");
                $("#" + dropdowndivId ).css("background-color", "white");
            }

            $("#" + dropdowndivId + " tr").click(function () {
                var suggesttext = $(this).text();
                $("#" + dropdowndivId).prev().val(suggesttext);
                $("#" + dropdowndivId).hide();
            }).hover(function () {
                $(this).css("background", "#EFF3FF");
            }, function () {
                $(this).css("background", "white");
            }).focus(function () {
                $(this).css("background", "#EFF3FF");
                IsFocus = true;
                Index = $(this).index();
            }).blur(function () {
                $(this).css("background", "white");

            });


            if (data[0].names.length == 1 && $("#" + dropdowndivId + " td").eq(0).text() == $("#" + dropdowndivId).prev().val()) {
                $("#" + dropdowndivId).hide();
            }
            else {
                $("#" + dropdowndivId).show();
            }
        });
    }
    else {
        $.post(actionname, { value: value, personalProfileId: personalProfileId, otherinfo: otherinfo }, function (data, status) {
            if (data[0].names.length == 0) {
                $("#" + dropdowndivId).hide();
                return;
            }
            $("#" + dropdowndivId + " tr").remove();
            for (var i in data[0].names) {
                $("#" + dropdowndivId + " table").append("<tr><td>" + data[0].names[i].name + "</td></tr>");
                $("#" + dropdowndivId).css("background-color", "white");
            }

            $("#" + dropdowndivId + " tr").click(function () {
                var suggesttext = $(this).text();
                $("#" + dropdowndivId).prev().val(suggesttext);
                $("#" + dropdowndivId).hide();
            }).hover(function () {
                $(this).css("background", "#EFF3FF");
            }, function () {
                $(this).css("background", "white");
            }).focus(function () {
                $(this).css("background", "#EFF3FF");
                IsFocus = true;
                Index = $(this).index();
            }).blur(function () {
                $(this).css("background", "white");

            });
            

            if (data[0].names.length == 1 && $("#" + dropdowndivId + " td").eq(0).text() == $("#" + dropdowndivId).prev().val()) {
                $("#" + dropdowndivId).hide();
            }
            else {
                $("#" + dropdowndivId).show();
            }
        });
    }
}


function ValueChangeCallBack(data, status, dropdowndivId) {
    if (data[0].names.length == 0) {
        $("#" + dropdowndivId).hide();
        return;
    }
    $("#" + dropdowndivId + " tr").remove();
    for (var i in data[0].names) {
        $("#" + dropdowndivId + " table").append("<tr><td>" + data[0].names[i].name + "</td></tr>");
        $("#" + dropdowndivId).css("background-color", "white");
    }

    $("#" + dropdowndivId + " tr").click(function () {
        var suggesttext = $(this).text();
        $("#" + dropdowndivId).prev().val(suggesttext);
        $("#" + dropdowndivId).hide();
    }).hover(function () {
        $(this).css("background", "#EFF3FF");
    }, function () {
        $(this).css("background", "white");
    }).focus(function () {
        $(this).css("background", "#EFF3FF");
        IsFocus = true;
        Index = $(this).index();
    }).blur(function () {
        $(this).css("background", "white");

    });

    //$("#dropdowndiv").prev().blur(function () {
    //    $("#dropdowndiv").hide();
    //});

    if ($("#" + dropdowndivId + " td").eq(0).text() == $("#" + dropdowndivId).prev().val()) {
        $("#" + dropdowndivId).hide();
    }
    else {
        $("#" + dropdowndivId).show();
    }
}

function AddErrorMsg(div) {
    var path = $("#errorPath").val();
    $("#" + div + " input[type=text]").each(function () {
        var msg = "<span name='ErrorSpan' style='display: none;'><div style='display: inline-block;'><div class='messages'><div class='message message-error'><div class='image'><img src='" + path + "' alt='Error' height='20' /></div><div class='text'><span name='ErrorMassage'></span></div></div></div></div></span>";
        $(this).parent().append(msg);
    });
    $("#" + div + " textarea").each(function () {
        var msg = "<span name='ErrorSpan' style='display: none;'><div style='display: inline-block;'><div class='messages'><div class='message message-error'><div class='image'><img src='" + path + "' alt='Error' height='20' /></div><div class='text'><span name='ErrorMassage'></span></div></div></div></div></span>";
        $(this).parent().append(msg);
    });
}

function AddErrorEvent(div) {
    $("#" + div + " input[type=text]").change(function () {
        $(this).val($(this).val().trim());
        if ($(this).val().length <= 0) {
            $(this).parent().find("span[name='ErrorMassage']").text("This item can not be null.");
            $(this).parent().find("span[name='ErrorSpan']").css("display", "");
        }
        else if ($(this).val().length >= 30) {
            $(this).parent().find("span[name='ErrorMassage']").text("Enter less than 30 characters.");
            $(this).parent().find("span[name='ErrorSpan']").css("display", "");
        }
        else {
            $(this).parent().find("span[name='ErrorSpan']").css("display", "none");
        }
    });
    $("#" + div + " textarea").change(function () {
        if ($(this).val().length >= 500) {
            $(this).parent().find("span[name='ErrorMassage']").text("Enter less than 500 characters.");
            $(this).parent().find("span[name='ErrorSpan']").css("display", "");
        }
        else {
            $(this).parent().find("span[name='ErrorSpan']").css("display", "none");
        }
    });
}

//设置弹出框位置居中
function center(width, height, id) {
    var top = ($(window.parent).height() - height) / 2;
    var left = ($(window).width() - width) / 2;
    var scrollTop = $(window.parent.document).scrollTop();
    var scrollLeft = $(window.parent.document).scrollLeft();

    top = top + scrollTop - 50;
    left = left + scrollLeft;
    $("#" + id).dialog('option', 'position', [left, top]);
}

function CheckTextInfo(text, overlength) {
    var flag = true;
    $("#" + text).each(function () {
        flag = ShowErrorMessage(this, parseInt(overlength));
        if (!flag) {
            return false;
        }
    });
    return flag;
}

function OpenDialog(dialogId) {
    $("#" + dialogId).dialog('option', 'height', 170);
    center($("#" + dialogId).dialog("option", "width"), $("#" + dialogId).dialog("option", "height"), dialogId);
    $("#" + dialogId).dialog("open");
    return false;
}

//json日期转换
function ChangeDateFormat(cellval) {

    var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));

    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;

    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();

    var datetime = null;
    var reg = /\d{13}/ig;
    var results = cellval.match(reg);
    if(results && results.length >0 )
    {
        datetime = new Date(+results[0]);
    }
    var strTime = datetime + "";
    var time = strTime.split(' ');

    return date.getFullYear() + "-" + month + "-" + currentDate + " " + time[3];

}

//iframe自适应
function DynIframeSize(down) {
    var pTar = null;
    if (document.getElementById) {
        pTar = document.getElementById(down);
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
    //pTar.height -= 20;

    window.parent.dyniframesize('rightpage');
}

function getAllCheckedId() {
    var ids = "";
    $("input:checkbox:checked").each(function () {
        ids += $(this).attr("id") + ",";
    });
    if (ids.length > 0) {
        ids = ids.substring(0, ids.length - 1);
    }

    return ids;
}

function AddApprovedAction(tableName) {
    
    $("li").find("[name='action']").each(function () {
        if ($(this).attr("approved") == "0") {
            var html = "<a href='#' onclick=\"Approved('" + tableName + "','" + $(this).attr("objId") + "')\">批准</a>";
            $(this).append(html);
        }
    })
}

function Approved(tableName, id) {
    $.post("Approved", { tableName: tableName, id: id }, function () {
        location.reload();
    })
}

function GoBack() {
    location.href = window.parent.backURL.pop();
}