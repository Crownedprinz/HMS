$(document).ready(function () {
    $(".table").on('click', '.deleteitem', function () {
        var ispan = $(this).closest("td");
        var keyg = ispan.children("[data-keyg]").data('keyg');
        bootbox.confirm("Are you sure you want to delete?", function (a) {
            if (a) {

                delete_ajax_rtn(keyg);
                location.reload();
            }
            else {
                $("#id_xhrt").val("X");
            }
        });
    });
    function delete_ajax_rtn(keyg) {

        var URL = $('#loader2').data('request-ajax2');
        if (isNaN(keyg)) {
            keyg = keyg.replace(/&/g, "[]");
        }
        var URLext = keyg;
        var Acct = " "
        URL = $.trim(URL);
        URLext = $.trim(URLext);
        URL = URL + "/" + URLext;
        $.ajax({
            type: "Post",
            async: false,
            url: URL,
            success: function (data) {
                var items = '';
                //$.each(data, function (i, state) {
                //    items = state.Text;
                //    //if (state.Value == "1") {
                //    //    alert("Transaction Code is in Use");
                //    //}
                //});
            }

        });
        // location.reload();
    }
    $(".table").DataTable({
        dom: '<"clear">l<"centered"i><"dtwrapper"frtip>',
        iDisplayLength: 25,
        order: [],
        scrollX: false,
        fixedHeader: true,
        colReorder: true,
        stateSave: true,
        "columnDefs": [{ "targets": [0], "orderable": false }]

    });
    $("table.iTable").DataTable({
        dom: '<"clear">l<"centered"i><"dtwrapper"frtip>',
                "columnDefs": [
                {
                    "targets": [2],
                    "visible": false,
                }
                ]
            });

    $("#id_delete").click(function () {
        var $form = $(this).closest('form');
        bootbox.confirm("<p><h3><font color=red>Do you want to delete the Record?</font></h3></p>", function (a) {
            if (a) {
                $("#id_xhrt").val("D");
                $form.trigger("submit");
            }
            else {
                $("#id_xhrt").val("X");
            }
        });
    });
    $(function ($) {
        $(".numformat").autoNumeric('init', { mDec: 2 });
        $(".table.numformat").autoNumeric('init', { mDec: 2 });
    });

    $(".table").on('click', '.deleteitemlist', function () {
        var ispan = $(this).closest("td");
        var keyg = ispan.children("[data-keyg]").data('keyg');
        bootbox.confirm("Are you sure you want to delete?", function (a) {
            if (a) {

                delete_ajax_rtnl(keyg);
                location.reload();
            }
            else {
                $("#id_xhrt").val("X");
            }
        });
    });

    function delete_ajax_rtnl(keyg) {

        var URL = $('#loader5').data('request-ajax5');
        if (isNaN(keyg)) {
            keyg = keyg.replace(/&/g, "[]");
        }
        var URLext = keyg;
        var Acct = " "
        URL = $.trim(URL);
        URLext = $.trim(URLext);
        URL = URL + "/" + URLext;

        $.ajax({
            type: "Post",
            async: false,
            url: URL,
            success: function (data) {
                var items = '';
                //$.each(data, function (i, state) {
                //    items = state.Text;
                //    if (state.Value == "1") {
                //        alert("Transaction Code is in Use");
                //    }
                //});
            }

        });
        // location.reload();
    }


    $(".datet").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy"
    });

    $(function () {
        $("#dialog").dialog("open");
    });
    $('#dialog').dialog({
        autoOpen: false,
        modal: true,
        bgiframe: true,
        autoOpen: false,
        width: 600,
        buttons: {
            "Ok": function () { $(this).dialog("close"); },
            "Print": function () {
                window.open('Checkin/print_receipt');
                $(this).dialog("close");
            }
        }
    });
    var baseloc = $("#locat").val();
    $.sessionTimeout({
        title: 'Expiring : Your Current Session ',
        keepAliveButton: ' Continue ',
        keepAliveUrl: baseloc + 'home/KeepAlive',
        logoutUrl: baseloc + 'home/signout',
        redirUrl: baseloc + 'home/signout',
         warnAfter: 900000,
         redirAfter: 1200000,
        countdownMessage: 'Redirecting in {timer} seconds.'
    });


});

//$.validator.setDefaults({
//    ignore: []
//});