$(function() {
    $.ajaxSetup({ cache: false });
    $(".taskItem").click(function(e) {

        e.preventDefault();
        $.get(this.href, function(data) {
            $('#dialogContent').html(data);
            $('#modDialog').modal('show');
        });
    });
});



        function afterloadsetting() {
            $.ajaxSetup({ cache: false });
            $('body').on('click', '.taskItem', function(e) {
                e.preventDefault();
                $.get(this.href, function(data) {
                    $('#dialogContent').html(data);
                    $('#modDialog').modal('show');
                });
            });
        }


function toresolved(i) {
    $.post("/Home/SetResolved", { id: i }, resolvedSuccess(i));
            
}

function resolvedSuccess(i) {
    var cdiv;
    cdiv= $('#childdiv>#task' + i);
    if (cdiv.length != 0) {
        $("#resolveddiv").append(cdiv);
        var chdiv = $("#chul" + i);
        var childdiv = $("#childdiv" + i);
        childdiv.hide();
        $("#resolveddiv").append(chdiv);
        cdiv.find("#lnks").append('<input id="clsbtn" value="Закрыть задачу" class="btn btn-danger pull-right" onclick="toclosed('+i+')">');
    } else {
        cdiv = $('#task' + i);
    }
    var sp = cdiv.find("#sp1");
    sp.text("Решенная задача");
    sp.attr("class","label label-default");
    cdiv.find("#addl").attr("style","display: none");
    cdiv.find("#edl").attr("style","display: none");
    cdiv.find("#addl").attr("style","display: none");
    cdiv.find("#chldsha"+i).attr("style","display: none");
    cdiv.find("#chldshlnk"+i).attr("style","display: none");
    cdiv.find("#chkbx" + i).attr("onclick", "toactive(" + i + ");");
}

function toactive(i) {
    $.post("/Home/FromResolvedToActive", { id: i }, activeSuccess(i));
            

}
function activeSuccess(i) {
    var cdiv;
    cdiv= $('#resolveddiv>#task' + i);
    if (cdiv.length != 0) {
        $("#childdiv").append(cdiv);
        var chdiv = $("#chul" + i);
        var childdiv = $("#childdiv" + i);
        childdiv.show();
        $("#childdiv").append(chdiv);
        cdiv.find("#clsbtn").remove();
    } else {
        cdiv = $('#task' + i);
    }
    var sp = cdiv.find("#sp1");
    sp.text("Активная задача");
    sp.attr("class","label label-primary");
    cdiv.find("#addl").attr("style","");
    cdiv.find("#edl").attr("style","");
    cdiv.find("#addl").attr("style","");
    cdiv.find("#chldshlnk"+i).attr("style","");
    cdiv.find("#chkbx" + i).attr("onclick", "toresolved(" + i + ");");

}


function toclosed(i) {
    $.post("/Home/FromResolvedToClosed", { id: i }, closedSuccess(i));
}

function closedSuccess(i) {
    var cdiv;
    cdiv= $('#resolveddiv>#task' + i);
    if (cdiv.length != 0) {
        $("#closeddiv").append(cdiv);
        var chdiv = $("#chul" + i);
        chdiv.remove();
        cdiv.find("#clsbtn").remove();
    } else {
        cdiv = $('#task' + i);
    }
    var sp = cdiv.find("#sp1");
    sp.text("Закрытая задача");
    sp.attr("class","label label-default");
    sp.attr("class","label label-default");
    cdiv.find("#addl").remove();
    cdiv.find("#edl").remove();
    cdiv.find("#addl").remove();
    cdiv.find("#chldsha"+i).remove();
    cdiv.find("#chldshlnk"+i).remove();
    cdiv.find("#chkbx" + i).remove();


}