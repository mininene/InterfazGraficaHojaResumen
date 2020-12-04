(function (angular) {
    'use strict';
    angular.module('datatablesAguaApp', ['datatables', 'datatables.buttons']).
        controller('aguaCtrl', function ($scope, $http, $q, DTOptionsBuilder, DTColumnBuilder, DTColumnDefBuilder, DTDefaultOptions, $filter) {
            DTDefaultOptions.setLoadingTemplate('<div class="spinner-border text-primary" role="status"></div >' + '  ' + '<span class="sr - only">Cargando...</span>') //spinner carga



            $scope.dtOptions = DTOptionsBuilder
                .fromFnPromise(function () {
                    var defer = $q.defer();
                    $http.get('/CiclosAutoClaveAgua/ListAgua').then(function (result) {
                        defer.resolve(result.data);
                        $scope.searchid = result.data
                        
                       // console.log(result.data)

                           
                    });

                   
                    return defer.promise;



                })
                //.withOption('searching', false)
    
              
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
                        titleAttr: 'Excel'
                    },
                    //{
                    //    extend: 'print',
                    //    text: '<i class="fa fa-print" aria-hidden="true"></i> Print',
                    //    titleAttr: 'Print'
                    //},
                    {
                        extend: 'copy',
                        text: '<i class="fa fa-files-o"></i> Copy',
                        titleAttr: 'Copy'
                        
                    },
                     
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
                DTColumnBuilder.newColumn('duracionTotalF1').withTitle('Duración F1'),
                DTColumnBuilder.newColumn('duracionTotalF2').withTitle('Duración F2'),
                DTColumnBuilder.newColumn('duracionTotalF3').withTitle('Duración F3'),
                DTColumnBuilder.newColumn('duracionTotalF4').withTitle('Duración F4'),
                DTColumnBuilder.newColumn('duracionTotalF5').withTitle('Duración F5'),
                DTColumnBuilder.newColumn('tff5').withTitle('P.FINAL F5').renderWith(function (data, type, full, meta) {
                    var x = data.substring(5, 14)
                    return x
                }),
              
                DTColumnBuilder.newColumn('duracionTotalF6').withTitle('Duración F6'),
                DTColumnBuilder.newColumn('tif6').withTitle('P.INICIAL F6').renderWith(function (data, type, full, meta) {
                    var x = data.substring(5, 14)
                    return x
                }),
                DTColumnBuilder.newColumn('tif6').withTitle('TE2 IF6').renderWith(function (data, type, full, meta) {
                    var x = data.substring(14, 21)
                    return x
                }),
                DTColumnBuilder.newColumn('tif6').withTitle('TE3 IF6').renderWith(function (data, type, full, meta) {
                    var x = data.substring(21, 28)
                    return x
                }),
                DTColumnBuilder.newColumn('tif6').withTitle('TE4 IF6').renderWith(function (data, type, full, meta) {
                    var x = data.substring(28, 35)
                    return x
                }),

                DTColumnBuilder.newColumn('tff6').withTitle('P.FINAL F6').renderWith(function (data, type, full, meta) {
                    var x = data.substring(6, 14)
                    return x
                }),
                DTColumnBuilder.newColumn('tff6').withTitle('TE2 FF6').renderWith(function (data, type, full, meta) {
                    var x = data.substring(14, 21)
                    return x
                }),
                DTColumnBuilder.newColumn('tff6').withTitle('TE3 FF6').renderWith(function (data, type, full, meta) {
                    var x = data.substring(21, 28)
                    return x
                }),
                DTColumnBuilder.newColumn('tff6').withTitle('TE4 FF6').renderWith(function (data, type, full, meta) {
                    var x = data.substring(28, 35)
                    return x
                }),

                DTColumnBuilder.newColumn('tfsubF6').withTitle('FoTE2 FF6').renderWith(function (data, type, full, meta) {
                    var x = data.substring(2, 9)
                    return x
                }),
                DTColumnBuilder.newColumn('tfsubF6').withTitle('FoTE3 FF6').renderWith(function (data, type, full, meta) {
                    var x = data.substring(9, 18)
                    return x
                }),
                DTColumnBuilder.newColumn('tfsubF6').withTitle('FoTE4 FF6').renderWith(function (data, type, full, meta) {
                    var x = data.substring(18, 27)
                    return x
                }),
                DTColumnBuilder.newColumn('tminima').withTitle('TMinina Estr.'),
                DTColumnBuilder.newColumn('tmaxima').withTitle('TMaximaa Estr.'),
                DTColumnBuilder.newColumn('duracionTotalF7').withTitle('Duración F7'),
                DTColumnBuilder.newColumn('tif7').withTitle('P.FINAL F7').renderWith(function (data, type, full, meta) {
                    var x = data.substring(6, 14)
                    return x
                }),
                DTColumnBuilder.newColumn('duracionTotalF8').withTitle('Duración F8'),
                DTColumnBuilder.newColumn('duracionTotalF9').withTitle('Duración F9'),
                DTColumnBuilder.newColumn('duracionTotalF10').withTitle('Duración F10'),
                DTColumnBuilder.newColumn('duracionTotalF11').withTitle('Duración F11'),
                DTColumnBuilder.newColumn('duracionTotalF12').withTitle('Duración F12'),
                DTColumnBuilder.newColumn('tff13').withTitle('T.TOTAL').renderWith(function (data, type, full, meta) {
                    var x = data.substring(0, 7)
                    return x
                }),
                DTColumnBuilder.newColumn('tiempoCiclo').withTitle('T.CALCULADO'),
                DTColumnBuilder.newColumn('tfsubF13').withTitle('FoTE2 FF13').renderWith(function (data, type, full, meta) {
                    var x = data.substring(2, 9)
                    return x
                }),
                DTColumnBuilder.newColumn('tfsubF13').withTitle('FoTE3 FF13').renderWith(function (data, type, full, meta) {
                    var x = data.substring(9, 18)
                    return x
                }),
                DTColumnBuilder.newColumn('tfsubF13').withTitle('FoTE4 FF13').renderWith(function (data, type, full, meta) {
                    var x = data.substring(18, 27)
                    return x
                }),
                DTColumnBuilder.newColumn('difMaxMin').withTitle('FoMax-FoMin'),
            ]

            $scope.dtInstance = {};
            $scope.dtInstance1 = {};
         


           
            $scope.searchTable = function () {
                console.log($scope.dtInstance);
                var query = $scope.searchText;
               // console.log(query);
               
                //console.log($scope.dtInstance.DataTable);
              var resultado2=$scope.dtInstance.DataTable.search(query)
              var resultado = $scope.dtInstance.DataTable.search(query).draw();
               console.log(resultado2);

               
            };


            $scope.searchTable2 = function () {

                var query = $scope.searchText;
                // console.log(query);

                //console.log($scope.dtInstance.DataTable);
                var resultado2 = $scope.dtInstance.DataTable.search(query)
                var resultado = $scope.dtInstance.DataTable.search(query).draw();
                console.log(resultado2);


            };

             $scope.$on('event:dataTableLoaded', function(event, loadedDT) {
      // Setup - add a text input to each footer cell
      var id = '#' + loadedDT.id;
      $(id + ' tfoot th').each(function() {
        var title = $(id + ' thead th').eq($(this).index()).text();
          $(this).html('<input type="text" placeholder="Search ' + title + '" />');
          console.log("hola")
      });

      var table = loadedDT.DataTable;
      // Apply the search
      table.columns().eq(0).each(function(colIdx) {
        $('input', table.column(colIdx).footer()).on('keyup change', function() {
          table
            .column(colIdx)
            .search(this.value)
                .draw();
            console.log("asas")
        });
      });
    });

           
            //$scope.dtInstance = {};

            //$scope.dtIntanceCallback = function (instance) {
            //    $scope.dtInstance = instance;
            //}

            // You should be able to get the table instance
           

            //$scope.dtInstance = {};
            //$scope.dtIntanceCallback = function (instance) {
            //    $scope.dtInstance = instance;
            //    $scope.dtInstance.DataTable.search(query).draw()
            
            //}
            //console.log($scope.dtInstance);
            //$scope.dtRebind = function () {
            //    $scope.dtInstance.DataTable.draw()
            //}

            //$scope.dtInstance = {};
            //$scope.search = search;

            //function search(query, dtInstance) {
            //    $scope.dtInstance.DataTable.search(query).draw()
            //}
         


        })

    


  
 

})(angular);

