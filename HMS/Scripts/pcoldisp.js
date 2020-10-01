$(function () {

    var webcallp = $("#webcallp").val();
    var viewflag = $("#viewflag").val();

    //if (viewflag== "0")
    //{
    //    showreport();
    //    var pTable = table_viewpnp();
    //}
    //else
    //{
        var mywin = window.open(webcallp);
    //}

});

    function showreport() {
        var items = "";
        var URL = $('#loader1').data('request-daily');
        URL = $.trim(URL);
        $.ajax({
            type: "Post",
            url: URL,
            async: false,
            error: function (xhr, status, error) {
                alert("Error: " + xhr.status + " - " + error + " - " + URL);
            },
            success: function (data) {
                $.each(data, function (i, state) {
                    items += state.Text;
                });
            }
        });
        $('#report1').html(items);
    }


