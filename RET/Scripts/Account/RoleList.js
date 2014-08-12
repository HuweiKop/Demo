
function DeleteRole(roleId) {
    $.post("DeleteRole", { roleId: roleId }, function (result) {
        if (result == "OK") {
            location.reload();
        }
        else {
            alert(result);
        }
    })
}

function UpdateRole(roleId) {
    window.parent.backURL.push(window.location.href);
    window.location.href = "RoleEdit?roleId=" + roleId;
}

function UpdateUserRole(roleId) {
    window.parent.backURL.push(window.location.href);
    window.location.href = "UserListByRoleId?roleId=" + roleId;
}

function UpdateRoleResource(roleId) {
    window.parent.backURL.push(window.location.href);
    window.location.href = "ResourceList?roleId=" + roleId;
}

function Add() {
    window.parent.backURL.push(window.location.href);
}