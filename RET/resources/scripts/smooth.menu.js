/* 
Kriesi (http://themeforest.net/user/Kriesi)
http://www.kriesi.at/archives/create-a-multilevel-dropdown-menu-with-css-and-improve-it-via-jquery 
*/

function quick() {
    $("#quick ul ").css({ display: "none" });
    $("#quick li").hover(function () {
        $(this).find('ul:first').css({ visibility: "visible", display: "none" }).show(400);
    }, function () {
        $(this).find('ul:first').css({ visibility: "hidden" });
    });

    //当一个页面中有两个动态弹出菜单的时候，第二个菜单的代码
    $("#quick2 ul ").css({ display: "none" });
    $("#quick2 li").hover(function () {
        $(this).find('ul:first').css({ visibility: "visible", display: "none" }).show(400);
    }, function () {
        $(this).find('ul:first').css({ visibility: "hidden" });
    });
}

$(document).ready(function () {
    quick();
});