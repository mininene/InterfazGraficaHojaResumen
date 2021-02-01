(function (angular) {

    'use strict';
    angular.module('datatablesVaporApp', ['datatables', 'datatables.buttons', 'datatables.colvis']).
        controller('vaporCtrl', function ($scope, $http, $q, DTOptionsBuilder, DTColumnBuilder, DTColumnDefBuilder, DTDefaultOptions, $filter, $interval) {
            DTDefaultOptions.setLoadingTemplate('<div class="spinner-border text-primary" role="status"></div >' + '  ' + '<span class="sr - only">Cargando...</span>') //spinner carga


            $scope.dtOptions = DTOptionsBuilder.newOptions()
                .withOption('serverSide', false)
                .withOption('processing', false)
                .withOption('ajax', function (data, callback, settings) {


                    $http({
                        method: 'GET',
                        url: '/CiclosAutoClaveVapor/ListVapor',
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }).then(function (result) {
                        $scope.searchid = result.data;
                        console.log(result.data)
                        callback({

                            infox: result.data

                        });
                    });
                })

                .withDataProp('infox')

                //$scope.dtOptions = DTOptionsBuilder
                //    .fromFnPromise(function () {
                //        var defer = $q.defer();
                //      $http.get('/CiclosAutoClaveAgua/ListAgua').then(function (result) {

                //            $scope.searchid = result.data

                //          defer.resolve(result.data);
                //            console.log(result.data)
                //            console.log(DTOptionsBuilder)

                //        });


                //        return defer.promise;
                //    })




                // .withOption('searching', false)

                // Active ColVis plugin
                .withColVis()
                // Add a state change function
                .withColVisStateChange(stateChange)

                .withOption('initComplete', function () {

                    // $(document).ready(function () { var table = $('#example').DataTable(); $("#example tfoot th").each(function (i) { var select = $('<select><option value=""></option></select>').appendTo($(this).empty()).on('change', function () { var val = $(this).val(); table.column(i).search(val ? '^' + $(this).val() + '$' : val, true, false).draw(); }); table.column(i).data().unique().sort().each(function (d, j) { select.append('<option value="' + d + '">' + d + '</option>') }); }); });
                    // $(document).ready(function () {  $('#example tfoot th').each( function () { var title = $('#example thead th').eq( $(this).index() ).text(); $(this).html( '<input type="text" placeholder="Search '+title+'" />' ); } );   var table = $('#example').DataTable();  table.columns().eq( 0 ).each( function ( colIdx ) { $( 'input', table.column( colIdx ).footer() ).on( 'keyup change', function () { table .column( colIdx ) .search( this.value ) .draw(); } ); } ); } )

                    //$(document).ready(function () {
                    //    // Setup - add a text input to each footer cell
                    //    $('#example thead tr').clone(true).appendTo('#example thead');
                    //    $('#example thead tr:eq(1) th').each(function (i) {
                    //        var title = $(this).text();
                    //        $(this).html('<input type="text" placeholder="Search ' + title + '" />');

                    //        $('input', this).on('keyup change', function () {
                    //            if (table.column(i).search() !== this.value) {
                    //                table
                    //                    .column(i)
                    //                    .search(this.value)
                    //                    .draw();
                    //            }
                    //        });
                    //    });

                    //    var table = $('#example').DataTable({
                    //        orderCellsTop: true,
                    //        fixedHeader: true
                    //    });
                    //})  

                })





                .withLanguage({
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sInfoThousands": ",",
                    "sLengthMenu": "Mostrar   _MENU_   registros",
                    "sLoadingRecords": "Cargando...",
                    "sProcessing": "procesando...",
                    "sSearch": "Buscar:",
                    "sZeroRecords": "No hay registros encontrados",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                })


                .withButtons([
                    {

                        extend: 'excel',
                        text: '<i class="fa fa-file-text-o"></i> Excel',
                        titleAttr: 'Excel',
                        exportOptions: {
                            columns: ':visible'
                        },
                    },
                    {
                        extend: 'print',
                        text: '<i class="fa fa-print" aria-hidden="true"></i> Print',
                        titleAttr: 'Print',
                        exportOptions: {
                            columns: ':visible'
                        },
                    },
                    {
                        extend: 'copy',
                        text: '<i class="fa fa-files-o"></i> Copy',
                        titleAttr: 'Copy',
                        exportOptions: {
                            columns: ':visible'
                        },

                    },
                    //{
                    //    extend: 'columnsToggle',
                    //    text: '<i class="fa fa-files-o"></i> Copy',
                    //    titleAttr: 'Copy',
                    //    exportOptions: {
                    //        columns: ':visible'
                    //    },

                    //},

                ]
                )

                .withOption('scrollX', 'true')
                .withOption('scrollY', '380px')
                .withOption('lengthMenu', [[10, 50, 100, -1], [10, 50, 100, 'All']]);




            $scope.dtColumns = [
                DTColumnBuilder.newColumn(null).withTitle('#').renderWith(function (data, type, full, meta) {

                    return meta.row + 1

                }),

                //DTColumnBuilder.newColumn('id').withTitle('id'),             
                DTColumnBuilder.newColumn('idAutoclave').withTitle('idAutoclave'),
                DTColumnBuilder.newColumn('notas').withTitle('N.Carro'),
                DTColumnBuilder.newColumn('numeroCiclo').withTitle('N.Progresivo'),
                DTColumnBuilder.newColumn('programa').withTitle('N.Programa'),
                DTColumnBuilder.newColumn('codigoProducto').withTitle('Cod.Producto'),
                DTColumnBuilder.newColumn('lote').withTitle('Lote'),
                DTColumnBuilder.newColumn('horaInicio').withTitle('Hora Inicio'),
                DTColumnBuilder.newColumn('horaFin').withTitle('Hora Fin'),
               // DTColumnBuilder.newColumn('duracionTotalF1').withTitle('Duración F1'),
                DTColumnBuilder.newColumn('duracionTotalF1').withTitle('Duración F1').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    console.log(x)
                    var min = parseInt(x[0].trim());
                    console.log(min +"min")
                    var sec = (parseInt(x[1].trim())/60);
                    console.log(sec+"sec")
                    var s = (min + sec).toFixed(2);
                    return s
                }),
              //  DTColumnBuilder.newColumn('duracionTotalF2').withTitle('Duración F2'),
                DTColumnBuilder.newColumn('duracionTotalF2').withTitle('Duración F2').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
          
               // DTColumnBuilder.newColumn('duracionTotalF3').withTitle('Duración F3'),
                DTColumnBuilder.newColumn('duracionTotalF3').withTitle('Duración F3').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),

                DTColumnBuilder.newColumn('tif3').withTitle('P.INICIAL F3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(5, 14)
                    return x
                }),

                DTColumnBuilder.newColumn('tif3').withTitle('TE2 IF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(14, 21)
                    return x
                }),
                DTColumnBuilder.newColumn('tif3').withTitle('TE3 IF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(21, 28)
                    return x
                }),
                DTColumnBuilder.newColumn('tif3').withTitle('TE4 IF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(28, 35)
                    return x
                }),

                DTColumnBuilder.newColumn('tff3').withTitle('P.FINAL F3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(6, 14)
                    return x
                }),
                DTColumnBuilder.newColumn('tff3').withTitle('TE2 FF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(14, 21)
                    return x
                }),
                DTColumnBuilder.newColumn('tff3').withTitle('TE3 FF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(21, 28)
                    return x
                }),
                DTColumnBuilder.newColumn('tff3').withTitle('TE4 FF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(28, 35)
                    return x
                }),

                DTColumnBuilder.newColumn('tfsubF3').withTitle('FoTE2 FF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(2, 9)
                    return x
                }),
                DTColumnBuilder.newColumn('tfsubF3').withTitle('FoTE3 FF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(9, 18)
                    return x
                }),
                DTColumnBuilder.newColumn('tfsubF3').withTitle('FoTE4 FF3').renderWith(function (data, type, full, meta) {
                    var x = data.substring(18, 27)
                    return x
                }),
                DTColumnBuilder.newColumn('tminima').withTitle('TMinina Estr.'),
                DTColumnBuilder.newColumn('tmaxima').withTitle('TMaximaa Estr.'),

               // DTColumnBuilder.newColumn('duracionTotalF4').withTitle('Duración F4'),
                DTColumnBuilder.newColumn('duracionTotalF4').withTitle('Duración F4').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF5').withTitle('Duración F5'),
                DTColumnBuilder.newColumn('duracionTotalF5').withTitle('Duración F5').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF6').withTitle('Duración F6'),
                DTColumnBuilder.newColumn('duracionTotalF6').withTitle('Duración F6').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF7a').withTitle('Duración F7A'),
                DTColumnBuilder.newColumn('duracionTotalF7a').withTitle('Duración F7A').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF8a').withTitle('Duración F8A'),
                DTColumnBuilder.newColumn('duracionTotalF8a').withTitle('Duración F8A').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF7b').withTitle('Duración F7B'),
                DTColumnBuilder.newColumn('duracionTotalF7b').withTitle('Duración F7B').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF8b').withTitle('Duración F8B'),
                DTColumnBuilder.newColumn('duracionTotalF8b').withTitle('Duración F8B').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF9').withTitle('Duración F9'),
                DTColumnBuilder.newColumn('duracionTotalF9').withTitle('Duración F9').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF10').withTitle('Duración F10'),
                DTColumnBuilder.newColumn('duracionTotalF10').withTitle('Duración F10').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),
               // DTColumnBuilder.newColumn('duracionTotalF11').withTitle('Duración F11'),
                DTColumnBuilder.newColumn('duracionTotalF11').withTitle('Duración F11').renderWith(function (data, type, full, meta) {
                    var x = data.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),

                //DTColumnBuilder.newColumn('tif12').withTitle('T.TOTAL').renderWith(function (data, type, full, meta) {
                //    var x = data.substring(0, 7)
                //    return x
                //}),

                DTColumnBuilder.newColumn('tif12').withTitle('T.TOTAL').renderWith(function (data, type, full, meta) {
                    var Y = data.substring(0, 7)
                    var x = Y.split(':');
                    var min = parseInt(x[0].trim());
                    var sec = (parseInt(x[1].trim()) / 60);
                    var s = (min + sec).toFixed(2);
                    return s
                }),

                DTColumnBuilder.newColumn('tisubF12').withTitle('FoTE2 FF12').renderWith(function (data, type, full, meta) {
                    var x = data.substring(2, 9)
                    return x
                }),
                DTColumnBuilder.newColumn('tisubF12').withTitle('FoTE3 FF12').renderWith(function (data, type, full, meta) {
                    var x = data.substring(9, 18)
                    return x
                }),
                DTColumnBuilder.newColumn('tisubF12').withTitle('FoTE4 FF12').renderWith(function (data, type, full, meta) {
                    var x = data.substring(18, 27)
                    return x
                }),
                DTColumnBuilder.newColumn('difMaxMin').withTitle('FoMax-FoMin'),
            ];





            function stateChange(iColumn, bVisible) {
                console.log('The column', iColumn, ' has changed its status to', bVisible);
            }

            function filterGlobal() {
                $('#example').DataTable().search(
                    $('#global_filter').val(),
                    $('#global_regex').prop('checked'),
                    $('#global_smart').prop('checked')
                ).draw();
            }

            function filterColumn(i) {
                $('#example').DataTable().column(i).search(
                    $('#col' + i + '_filter').val(),
                    $('#col' + i + '_regex').prop('checked'),
                    $('#col' + i + '_smart').prop('checked')
                ).draw();
            }

            $(document).ready(function () {
                $('#example').DataTable();

                $('input.global_filter').on('keyup click', function () {
                    filterGlobal();
                });

                $('input.column_filter').on('keyup click', function () {
                    filterColumn($(this).parents('tr').attr('data-column'));
                });
            });








        })







})(angular);


