
function DeleteUser(userId) {
    $.post("DeleteUser", { userId: userId }, function (result) {
        if (result == "OK") {
            location.reload();
        }
    })
}

function UpdateUser(userId) {
    window.parent.backURL.push(window.location.href);
    window.location.href = "UserEdit?userId=" + userId;
}

function UpdateUserRole(userId) {
    window.parent.backURL.push(window.location.href);
    window.location.href = "RoleListByUserId?userId=" + userId;
}

function Add() {
    window.parent.backURL.push(window.location.href);
}

function ChangeUserStatus(userId) {
    $.post("ChangeUserStatus", { userId: userId }, function (result) {
        if (result == "OK") {
            location.reload();
        }
    })
}