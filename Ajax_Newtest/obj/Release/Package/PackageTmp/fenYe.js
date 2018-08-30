

var options = {
    bootstrapMajorVersion: 3,
    alignment: 'center',
    currentPage: 1,
    totalPages: totalPages,
    pageUrl: function (type, page, current) {
        return "action.ashx?page=" + page;
    },
    onPageClicked: function (event, originalEvent, type, page) {
        originalEvent.preventDefault();
        originalEvent.stopPropagation();
        $.ajax({
            url: originalEvent.target.href,
            type: "GET",
            dataType: "json",
            success: function (data) {
                $("#content li").remove();
                $(data).each(function () {
                    $("#content").append("<li>" + this.name + "</li>");
                });

            },
            error: function (error) {
                alert("error");
            }
        });
    }
};
$(document).ready(function () {
    $("#pagination").bootstrapPaginator(options);
    $("#pagination li:first a").trigger("click");
});
