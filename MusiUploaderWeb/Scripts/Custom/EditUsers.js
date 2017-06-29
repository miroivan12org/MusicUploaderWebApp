$(function () {    
    var initDialog = function (type) {    
        var title = type;    
        $("#divEdit").dialog({    
            autoOpen: false,    
            modal: true,    
            title: type + ' User',    
            width: 360,    
            buttons: {    
                Save: function () {    
                    var id = $("#hidID").val();    
                    var role = $("#ddlRoles").val();    
                    var loginName = $("#txtLoginName").val();    
                    var loginPass = $("#txtPassword").val();    
                    var fName = $("#txtFirstName").val();    
                    var lName = $("#txtLastName").val();    
                    var gender = $("#ddlGender").val();    
    
                    UpdateUser(id, loginName, loginPass, fName, lName, gender, role);    
                    $(this).dialog("destroy");    
                },    
                Cancel: function () { $(this).dialog("destroy"); }    
            }    
        });    
    }    
    
    $("a.lnkEdit").on("click", function () {    
        initDialog("Edit");    
        $(".alert-success").empty();    
        var row = $(this).closest('tr');    
    
        $("#hidID").val(row.find("td:eq(0)").html().trim());    
        $("#txtLoginName").val(row.find("td:eq(1)").html().trim())    
        $("#txtPassword").val(row.find("td:eq(2)").html().trim())    
        $("#txtFirstName").val(row.find("td:eq(3)").html().trim())    
        $("#txtLastName").val(row.find("td:eq(4)").html().trim())    
        $("#ddlGender").val(row.find("td:eq(5)").html().trim())    
        $("#ddlRoles").val(row.find("td:eq(7) > input").val().trim());    
    
        $("#divEdit").dialog("open");    
        return false;    
    });

    $("a.lnkDelete").on("click", function () {
        var row = $(this).closest('tr');
        var id = row.find("td:eq(0)").html().trim();
        var answer = confirm("You are about to delete this user with ID " + id + " . Continue?");
        if (answer) DeleteUser(id);
        return false;
    });
});