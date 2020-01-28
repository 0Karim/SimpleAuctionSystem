function OnItemsSuccess(response) {
    debugger;
    if (response.Success === true) {
        alert('Item Inserted Successfully');
        GetAllItems();
        ClearItemsForm();
    }
    else {
        alert(response.Message);
    }
}

function OnItemFailure(response) {
    alert(response.Message);
}

function ClearItemsForm() {
    $(".frm").val('');
}

function GetAllItems()
{
    $.ajax({
        url: "/Home/GetAllItems",
        type: "GET",
        dataType: "html",
        success: function (response)
        {
            $('#MainTable').html(response);
        },
        error: function (response) {
            alert(response.Message);
        }
    })
}

//function GetBiddersForm() {
//    debugger;
//    $('#MainForm').html('');
//    $('#MainTable').html('');

//    $.ajax({

//        url: "/Home/GetParticipantForm",
//        type: "GET",
//        dataType: "html",
//        success: function (response) {
//            $('#MainForm').html(response);
//            GetAllParticipants();
//        },
//        error: function (response) {
//            alert(response.Message);
//        }
//    });
//}

//$(document).ready(function () {
//    GetAllItems();
//})