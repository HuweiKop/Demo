$(function () {
    $("#CityId").change(function () {
        //alert($(this).val());
        $.post("GetBusinessDistrictByCityId", { cityId: $(this).val() }, function (result) {
            $("#BusinessDistrictId").html(result);
        });
        $.post("GetDistrictByCityId", { cityId: $(this).val() }, function (result) {
            $("#DistrictId").html(result);
        });
    })
})

function SaveData() {
    //var regNum = /^\d{1,2}$/;
    //if (!regNum.test($("#overground").val())) {
    //    alert("地上楼层数需为两位数以内数字");
    //    return;
    //}
    //if (!regNum.test($("#underground").val())) {
    //    alert("地下楼层数需为两位数以内数字");
    //    return;
    //}
    $.post($("#form").attr("action"), $("#form").serialize(), function (result) {
        alert(result);
    });
}