//GridTree
var gridTree;
function showGridTree(resourceArray, alreadbyResourceArray) {
    //init
    gridTree = new TableTree4J("gridTree", "../resources/");
    gridTree.tableDesc = "<table border=\"1\" class=\"GridView\" width=\"100%\" id=\"systemParameterTable\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse\">";
    var headerDataList = new Array("<img src=\"../resources/images/resx.gif\" alt=\"\"/>Page Management");
    var widthList = new Array("40px");
    //参数: arrayHeader,id,headerWidthList,booleanOpen,classStyle,hrefTip,hrefStatusText,icon,iconOpen
    gridTree.setHeader(headerDataList, "0", widthList, true, "Head", "", "", "", "");
    //设置列样式
    gridTree.gridHeaderColStyleArray = new Array("");
    gridTree.gridDataCloStyleArray = new Array("");
    //add data 

    for (i in resourceArray) {
        if (resourceArray[i].ResourceType == "Page") {
            dataList = new Array("<img src=\"../resources/images/fbico.gif\" alt=\"\"/><input type=\"checkbox\" id=\"" + resourceArray[i].Id + "\" name=\"checkboxPermission\" onclick=\"checkboxFunctionClick(" + resourceArray[i].Id + ")\" value=\"" + resourceArray[i].Id + "\"/>" + resourceArray[i].Name);
        }
        else {
            dataList = new Array("<img src=\"../resources/images/aspx.gif\" alt=\"\"/><input type=\"checkbox\" id=\"" + resourceArray[i].Id + "\" name=\"checkboxPermission\" onclick=\"checkboxClick(" + resourceArray[i].Id + ")\" value=\"" + resourceArray[i].Id + "\"/>" + resourceArray[i].Name);
        }
        gridTree.addGirdNode(dataList, resourceArray[i].Id, resourceArray[i].Parent, null, resourceArray[i].Id);
    }

    //print	
    gridTree.printTableTreeToElement("treeDiv");

    for (i in alreadbyResourceArray) {
        var permissionId = alreadbyResourceArray[i];
        $("input[type=checkbox][id =" + permissionId + "]").attr("checked", true);
        checkboxFunctionClick(permissionId);
    }
}

function checkboxModuleClick(permissionId) {
    var permissionIdString = "" + permissionId;
    var checkbox = $("#" + permissionId);
    permissionIdString = permissionIdString.substring(0, permissionIdString.length - 1);
    if (checkbox.is(":checked")) {
        $("input[type=checkbox][id ^=" + permissionIdString + "]").attr("checked", true);
    }
    else {
        $("input[type=checkbox][id ^=" + permissionIdString + "]").attr("checked", false);
    }
}

function checkboxClick(permissionId) {
    var checkbox = $("#" + permissionId);
    var permissionIdString = "" + permissionId;
    if (checkbox.is(":checked")) {
        $("input[type=checkbox][id ^=" + permissionId + "]").attr("checked", true);
        for (var i = 1; i < permissionIdString.length; i++) {
            if (i == 1) {
                $("input[type=checkbox][id =" + permissionIdString.substring(0, i) + "0]").attr("checked", true);
            }
            else {
                $("input[type=checkbox][id =" + permissionIdString.substring(0, i) + "]").attr("checked", true);
            }
        }
    }
    else {
        $("input[type=checkbox][id ^=" + permissionId + "]").attr("checked", false);
    }
}

function checkboxFunctionClick(permissionId) {
    var permissionIdString = "" + permissionId;
    var checkbox = $("#" + permissionId);
    if (checkbox.is(":checked")) {
        for (var i = 1; i < permissionIdString.length; i++) {
            $("input[type=checkbox][id =" + permissionIdString.substring(0, i) + "]").attr("checked", true);
        }
    }
}

function Save() {
    var resourceIds = getAllCheckedId();

    $.post("UpdatePermission", { roleId: $("#roleId").val(), resourceIds: resourceIds }, function (result) {

    })
}