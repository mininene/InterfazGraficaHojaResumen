(function (angular) {
    'use strict';
    angular.module('datatablesVaporApp', ['datatables', 'datatables.buttons', 'datatables.colvis']).
        controller('vaporCtrl', function ($scope, $http, $q, DTOptionsBuilder, DTColumnBuilder, DTColumnDefBuilder, DTDefaultOptions) {
            DTDefaultOptions.setLoadingTemplate('<div class="spinner-border text-primary" role="status"></div >' +'  ' + '<span class="sr - only">Cargando...</span>') //spinner carga

            $scope.dtOptions = DTOptionsBuilder
                .fromFnPromise(function () {
                    var defer = $q.defer();
                    $http.get('/CiclosAutoClaveVapor/ListVapor').then(function (result) {
                        defer.resolve(result.data);
                        
                    });
                   
                    return defer.promise;

                   
                })

                // Active ColVis plugin
                .withColVis()
                // Add a state change function
                .withColVisStateChange(stateChange)


                .withLanguage({
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    
                    "sInfoThousands": ",",
                    "sLengthMenu": "Mostrar   _MENU_   registros",
                    "sloadingRecords": '<div class="spinner-border text-primary" role="status">< span class= "sr-only" > Loading...</span></div >',
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
                    },
                     "oLanguage": {
                        "sProcessing": "loading data...",
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

                    }
                ]
                )
                .withOption('scrollX', 'true')
                .withOption('scrollY', '380px')
                .withOption('lengthMenu', [[10, 50, 100, -1], [10, 50, 100, 'All']]);

            $scope.dtColumns = [
                DTColumnBuilder.newColumn(null).withTitle('#').renderWith(function (data, type, full, meta) {
                    //console.log(x);
                    return meta.row + 1

                }),


                DTColumnBuilder.newColumn('idAutoclave').withTitle('idAutoclave'),
                DTColumnBuilder.newColumn('notas').withTitle('N.Carro'),
                DTColumnBuilder.newColumn('numeroCiclo').withTitle('N.Progresivo'),
                DTColumnBuilder.newColumn('programa').withTitle('N.Programa'),
                DTColumnBuilder.newColumn('codigoProducto').withTitle('Cod.Producto'),
                DTColumnBuilder.newColumn('lote').withTitle('Lote'),
                DTColumnBuilder.newColumn('horaInicio').withTitle('Hora Inicio'),
                DTColumnBuilder.newColumn('horaFin').withTitle('Hora Fin'),
                DTColumnBuilder.newColumn('duracionTotalF1').withTitle('Duración F1'),
                DTColumnBuilder.newColumn('duracionTotalF2').withTitle('Duración F2'),
                DTColumnBuilder.newColumn('duracionTotalF3').withTitle('Duración F3'),

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

                DTColumnBuilder.newColumn('duracionTotalF4').withTitle('Duración F4'),
                DTColumnBuilder.newColumn('duracionTotalF5').withTitle('Duración F5'),
                DTColumnBuilder.newColumn('duracionTotalF6').withTitle('Duración F6'),
                DTColumnBuilder.newColumn('duracionTotalF7a').withTitle('Duración F7A'),
                DTColumnBuilder.newColumn('duracionTotalF8a').withTitle('Duración F8A'),
                DTColumnBuilder.newColumn('duracionTotalF7b').withTitle('Duración F7B'),
                DTColumnBuilder.newColumn('duracionTotalF8b').withTitle('Duración F8B'),
                DTColumnBuilder.newColumn('duracionTotalF9').withTitle('Duración F9'),
                DTColumnBuilder.newColumn('duracionTotalF10').withTitle('Duración F10'),
                DTColumnBuilder.newColumn('duracionTotalF11').withTitle('Duración F11'),
               
                DTColumnBuilder.newColumn('tif12').withTitle('T.TOTAL').renderWith(function (data, type, full, meta) {
                    var x = data.substring(0, 7)
                    return x
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
            ]
            function stateChange(iColumn, bVisible) {
                console.log('The column', iColumn, ' has changed its status to', bVisible);
            }



        })

})(angular);