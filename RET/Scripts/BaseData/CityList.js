
function Delete(id) {
    $.post("DeleteCity", { cityId: id }, function (result) {
        if (result == "OK") {
            location.reload();
        }
    })
}

function Update(id) {
    window.parent.backURL.push(window.location.href);
    window.location.href = "CityEdit?cityId=" + id;
}

function Add() {
    window.parent.backURL.push(window.location.href);
}