$(document).ready(function () {
    loadData();
});
function loadData() {
    $.ajax({
        url: "/CiclosAutoClaveAgua/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
         
            var html = '';
            $.each(result, function (key, item) {
                             
                html += '<tr>';
                html += '<td>' + item.idAutoclave + '</td>';
                html += '<td>' + item.idSeccion + '</td>';
                html += '<td>' + item.numeroCiclo + '</td>';
                html += '<td>' + item.programa  + '</td>';
                html += '<td>' + item.codigoProducto + '</td>';
                html += '<td>' + item.lote + '</td>';
                html += '<td>' + item.horaInicio + '</td>';
                html += '<td>' + item.horaFin + '</td>';
                html += '<td>' + item.duracionTotalF1 + '</td>';
                html += '<td>' + item.duracionTotalF2 + '</td>';
                html += '<td>' + item.duracionTotalF3 + '</td>';
                html += '<td>' + item.duracionTotalF4 + '</td>';
                html += '<td>' + item.duracionTotalF5 + '</td>';
                html += '<td>' + item.tff5.substring(5, 14) + '</td>';
                html += '<td>' + item.duracionTotalF6 + '</td>';
                html += '<td>' + item.tif6.substring(5, 14) + '</td>';
                html += '<td>' + item.tif6.substring(14, 21) + '</td>';
                html += '<td>' + item.tif6.substring(21, 28) + '</td>';
                html += '<td>' + item.tif6.substring(28, 35) + '</td>';
                html += '<td>' + item.tff6.substring(5, 14) + '</td>';
                html += '<td>' + item.tff6.substring(14, 21) + '</td>';
                html += '<td>' + item.tff6.substring(21, 28) + '</td>';
                html += '<td>' + item.tff6.substring(28, 35) + '</td>';
                html += '<td>' + item.tfsubF6.substring(2, 9) + '</td>';
                html += '<td>' + item.tfsubF6.substring(9, 18) + '</td>';
                html += '<td>' + item.tfsubF6.substring(18, 27) + '</td>';
                html += '<td>' + item.tminima + '</td>';
                html += '<td>' + item.tmaxima + '</td>';
                html += '<td>' + item.duracionTotalF7 + '</td>';
                html += '<td>' + item.tif7.substring(5, 14) + '</td>';
                html += '<td>' + item.duracionTotalF8 + '</td>';
                html += '<td>' + item.duracionTotalF9 + '</td>';
                html += '<td>' + item.duracionTotalF10 + '</td>';
                html += '<td>' + item.duracionTotalF11 + '</td>';
                html += '<td>' + item.duracionTotalF12 + '</td>';
                html += '<td>' + item.tff13.substring(0, 7) + '</td>';
                html += '<td>' + item.tiempoCiclo + '</td>';
                html += '<td>' + item.tfsubF13.substring(2, 9) + '</td>';
                html += '<td>' + item.tfsubF13.substring(9, 18) + '</td>';
                html += '<td>' + item.tfsubF13.substring(18, 27) + '</td>';
                html += '<td>' + item.difMaxMin + '</td>';
                //html += '<td><a href="#" onclick="return GetbyId(' + item.Id + ')">Edit</a> | <a href="#" onclick="Delete(' + item.Id + ')">Delete</a></td>';
                html += "<td><button type='button' id='btnEdit' class='btn btn-primary' onclick='return GetbyId(" + item.Id + ")'>Imprimir</button> <button type='button' id='btnDelete' class='btn btn-danger' onclick='return Delete(" + item.Id + ")'>PDF</button></td>"

                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

