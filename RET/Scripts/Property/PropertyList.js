$(function () {
    if ($("#isAdmin").val() == "1") {
        AddApprovedAction("Property");
    }
})

function Delete(id) {
    $.post("DeleteProperty", { propertyId: id }, function (result) {
        if (result == "OK") {
            location.reload();
        }
    })
}

function Update(id) {
    window.parent.backURL.push(window.location.href);
    window.location.href = "PropertyEdit?propertyId=" + id;
}

function ShowBrand(id) {
    window.parent.backURL.push(window.location.href);
    window.location.href = $("#url").val() + "BrandProperty/GetBrandByPropertyId?propertyId=" + id + "&page=1";
}

function Add() {
    window.parent.backURL.push(window.location.href);
}