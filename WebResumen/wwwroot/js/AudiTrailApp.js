(function (angular) {
    'use strict';
    angular.module('datatablesAudiTrailApp', ['datatables', 'datatables.buttons', 'datatables.colvis']).
        controller('audiTrailCtrl', function ($scope, $http, $q, DTOptionsBuilder, DTColumnBuilder, DTColumnDefBuilder, DTDefaultOptions, $filter) {
            DTDefaultOptions.setLoadingTemplate('<div class="spinner-border text-primary" role="status"></div >' + '  ' + '<span class="sr - only">Cargando...</span>') //spinner carga

            

            $scope.dtOptions = DTOptionsBuilder
                .fromFnPromise(function () {
                    var defer = $q.defer();
                    $http.get('/AudiTrail/ListAudiTrail').then(function (result) {
                        defer.resolve(result.data);
                        $scope.searchid = result.data
                        
                        //console.log(result.data)

                           
                    });

                   
                    return defer.promise;



                })
                //.withOption('searching', false)

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
                    //    extend: 'pdf',
                    //    text: '<i class="fa fa-print" aria-hidden="true"></i> Pdf',
                    //    titleAttr: 'PDF'

                    //},
                     //{
                        //extend: 'colvis',
                        //text: '<i class="fa fa-print" aria-hidden="true"></i> Columnas',
                        //titleAttr: 'Columnas'

                    //},
                 
                     
                ]
            )
               
                .withOption('scrollX', 'true')
                .withOption('scrollY', '380px')
               
                .withOption('lengthMenu', [[10, 50, 100, -1], [10, 50, 100, 'All']]);
      
           
            $scope.formatDate = function (date) {
                var dateOut = new Date(date);
                dateOut.setMonth(dateOut.getMonth() - 1);
                return dateOut;
            };

             

            $scope.dtColumns = [
                DTColumnBuilder.newColumn(null).withTitle('#').renderWith(function (data, type, full, meta) {
               
                    return meta.row + 1
                   
                }),
                
                //DTColumnBuilder.newColumn('id').withTitle('id'),             
                DTColumnBuilder.newColumn('usuario').withTitle('Usuario'),
                DTColumnBuilder.newColumn('tabla').withTitle('Tabla'),

                DTColumnBuilder.newColumn('fechaHora').withTitle('FechaHora').renderWith(function (data, type, full, meta) {
                    var fechaHora = data;
                    var date = new Date(fechaHora);
                    var dateStr =
                        
                        ("00" + date.getDate()).slice(-2)+ "/" +
                        ("00" + (date.getMonth() + 1)).slice(-2)+ "/" +
                        date.getFullYear() + " " +
                        ("00" + date.getHours()).slice(-2) + ":" +
                        ("00" + date.getMinutes()).slice(-2) + ":" +
                        ("00" + date.getSeconds()).slice(-2);
                    return (dateStr)
                }),

            
                DTColumnBuilder.newColumn('evento').withTitle('Evento'),

                DTColumnBuilder.newColumn('campo').withTitle('Campo'),
                DTColumnBuilder.newColumn('valor').withTitle('ValorAntiguo'),
                DTColumnBuilder.newColumn('valorActualizado').withTitle('ValorActualizado'),
                DTColumnBuilder.newColumn('comentario').withTitle('Motivo'),
               
            ]
            function stateChange(iColumn, bVisible) {
               // console.log('The column', iColumn, ' has changed its status to', bVisible);
            }
         
        }) 
 

})(angular);

