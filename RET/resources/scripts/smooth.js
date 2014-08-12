/* path to the stylesheets for the color picker */
var style_path = "resources/css/colors";

$(document).ready(function () {
    /* messages fade away when dismiss is clicked */
    $(".message > .dismiss > a").live("click", function (event) {
        var value = $(this).attr("href");
        var id = value.substring(value.indexOf('#') + 1);

        $("#" + id).fadeOut('slow', function () { });

        return false;
    });

    /* color picker */
    $("#colors-switcher > a").click(function () {
        var style = $("#color");

        style.attr("href", "" + style_path + "/" + $(this).attr("title").toLowerCase() + ".css");

        return false;
    });

    $("#menu h6 a").click(function () {
        var link = $(this);
        var value = link.attr("href");
        var id = value.substring(value.indexOf('#') + 1);

        var heading = $("#h-menu-" + id);
        var list = $("#menu-" + id);


        $("#menu ul[id != menu-" + id + "]").slideUp();
        $("#menu h6[id != h-menu-" + id + "]").attr("class", "");
        $("#menu ul[id != menu-" + id + "]").attr("class", "closed");
        $("#menu ul[id != menu-" + id + "] li[class ~=collapsible] a[class=minus]").attr("class", "plus");

        //list.slideToggle();
        if (list.attr("class") == "closed") {
            heading.attr("class", "selected");
            list.slideDown();
            list.attr("class", "");
        } else {
            heading.attr("class", "");
            list.slideUp();
            list.attr("class", "closed");
            $("#menu ul[id = menu-" + id + "] li[class ~=collapsible] a[class=minus]").attr("class", "plus");
        }
    });

    $("#menu li[class~=collapsible] a:first-child").click(function () {

        var hrefAttr = $(this).attr("href");
        var parentElement = $(this).parent("#menu li[class~=collapsible]").parent("#menu ul");
        parentElement.children("li[class~=collapsible]").each(function () {
            if ($(this).children("a:first-child").attr("href") != hrefAttr) {
                $(this).children("a:first-child").attr("class", "plus");
                $(this).children("ul").slideUp();
            }
        });

        var element = $(this).parent("#menu li[class~=collapsible]");

        element.children("a:first-child").each(function () {
            var child = $(this);

            if (child.attr("class") == "plus") {
                child.attr("class", "minus");
            } else {
                child.attr("class", "plus");
            }
        });

        element.children("ul").each(function () {
            var child = $(this);
            child.slideToggle();
        });
    });
});