function OnParticipantSuccess(response) {
    debugger;
    if (response.Success === true) {
        alert('Participant Inserted Successfully');
        GetAllParticipants();
        ClearParticipantForm();
    }
    else {
        alert(response.Message);
    }
}

function OnParticipantFailure(response) {
    alert(response.Message);
}

function ClearParticipantForm() {
    $(".frm").val('');
}

function GetAllParticipants() {
    $.ajax({
        url: "/Home/GetAllParticipants",
        type: "GET",
        dataType: "html",
        success: function (response) {
            $('#MainTable').html(response);
        },
        error: function (response) {
            alert(response.Message);
        }
    })
}


