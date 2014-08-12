
function Delete(id) {
    $.post("DeleteDistrict", { districtId: id }, function (result) {
        if (result == "OK") {
            location.reload();
        }
    })
}

function Update(id) {
    window.parent.backURL.push(window.location.href);
    window.location.href = "DistrictEdit?districtId=" + id;
}

function Add() {
    window.parent.backURL.push(window.location.href);
}