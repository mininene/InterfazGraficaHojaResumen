$(document).ready(function () {
    loadData();
});
function loadData() {
    $.ajax({
        url: "/Home/ListHome",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            setTimeout(function () {
                sendRequest(); //this will send request again and again;
            }, 2000);
            var html = '';
            $.each(result, function (key, item) {

                html += '<tr>';
                html += '<td>' + item.id + '</td>';
                html += '<td>' + item.ultimoCiclo + '</td>';
                html += '<td>' + item.estado + '</td>';
                html += '<td>' + item.nombre + '</td>';
               
                //html += '<td><a href="#" onclick="return GetbyId(' + item.Id + ')">Edit</a> | <a href="#" onclick="Delete(' + item.Id + ')">Delete</a></td>';
                html += "<td><button type='button' id='btnEdit' class='btn btn-primary' onclick='return GetbyId(" + item.id + ")'>Edit</button> </td>"

                html += '</tr>';
            });
            $('.tbody').html(html);
        }, 
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function GetbyId(id) {
    $('#ID').css('border-color', 'lightgrey');
    $('#CICLO').css('border-color', 'lightgrey');
    $('#ESTADO').css('border-color', 'lightgrey');
    $('#NOMBRE').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Home/GetbyId/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#ID').val(result.id);
            $('#CICLO').val(result.ultimoCiclo);
            $('#ESTADO').val(result.estado);
            $('#NOMBRE').val(result.nombre);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function Edit() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        id: $('#ID').val(),
        ultimoCiclo: $('#CICLO').val(),
        estado: $('#ESTADO').val(),
        nombre: $('#NOMBRE').val(),
        
    };
    $.ajax({
        url: "/Home/Edit",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ID').val("");
            $('#CICLO').val("");
            $('#ESTADO').val("");
            $('#NOMBRE').val("");
          
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function validate() {
    var isValid = true;
    if ($('#CICLO').val().trim() == "") {
        $('#CICLO').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CICLO').css('border-color', 'lightgrey');
    }
    if ($('#ESTADO').val().trim() == "") {
        $('#ESTADO').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ESTADO').css('border-color', 'lightgrey');
    }
    if ($('#NOMBRE').val().trim() == "") {
        $('#NOMBRE').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#NOMBRE').css('border-color', 'lightgrey');
    }
    //if ($('#Country').val().trim() == "") {
    //    $('#Country').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#Country').css('border-color', 'lightgrey');
    //}
    return isValid;
}