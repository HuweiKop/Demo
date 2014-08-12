

$(function () {
    

    //添加用户角色
    $("#AddRole-dialog-form").dialog({
        autoOpen: false,
        height: 550,
        width: 600,
        modal: true,
        position: [200, 100],
        buttons: {
            'Save': function () {
                var roleIds = "";
                $("#iframeShowRole").contents().find("input:checkbox:checked").each(function () {
                    roleIds += $(this).val() + ",";
                })
                if (roleIds.length > 0) {
                    roleIds = roleIds.substring(0, roleIds.length - 1);
                }
                $.post("AddUserRole", { userIds: $("#userId").val(), roleIds: roleIds }, function (result) {
                    if (result == "OK") {
                        location.reload();
                    }
                })
                $(this).dialog('close');
            },
            Cancel: function () {
                $(this).dialog('close');
            }
        },
        close: function (event, ui) {
        }
    });

    //给角色添加用户
    $("#AddUser-dialog-form").dialog({
        autoOpen: false,
        height: 550,
        width: 600,
        modal: true,
        position: [200, 100],
        buttons: {
            'Save': function () {
                var userIds = "";
                $("#iframeShowUser").contents().find("input:checkbox:checked").each(function () {
                    userIds += $(this).val() + ",";
                })
                if (userIds.length > 0) {
                    userIds = userIds.substring(0, userIds.length - 1);
                }
                $.post("AddUserRole", { roleIds: $("#roleId").val(), userIds: userIds }, function (result) {
                    if (result == "OK") {
                        location.reload();
                    }
                })
                $(this).dialog('close');
            },
            Cancel: function () {
                $(this).dialog('close');
            }
        },
        close: function (event, ui) {
        }
    });
})

function AddUserRoleByUser(userId) {
    $("#iframeShowRole").attr("src", "RoleListDialog?userId=" + userId);
    OpenUserRoleDialog("AddRole-dialog-form");
}

function AddUserRoleByRole(roleId) {
    $("#iframeShowUser").attr("src", "UserListDialog?roleId=" + roleId);
    OpenUserRoleDialog("AddUser-dialog-form");
}

function DeleteUserRole(userRoleId) {
    $.post("DeleteUserRole", { userRoleId: userRoleId }, function (result) {
        if (result == "OK") {
            location.reload();
        }
    })
}

function OpenUserRoleDialog(name) {
    $("#" + name).dialog('option', 'width', 500);
    $("#" + name).dialog('option', 'height', 600);
    center($("#" + name).dialog("option", "width"), $("#" + name).dialog("option", "height"), "AddRole-dialog-form");
    $("#" + name).dialog("open");
}