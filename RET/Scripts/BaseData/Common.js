function SaveData() {
    $.post($("#form").attr("action"), $("#form").serialize(), function (result) {
        alert(result);
    });
}