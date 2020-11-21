(function (angular) {
    'use strict';
    angular.module('datatablesSampleApp', ['datatables', 'datatables.buttons']).
        controller('sampleCtrl', function ($scope, $http, $q, DTOptionsBuilder, DTColumnBuilder, DTColumnDefBuilder) {

            $scope.dtOptions = DTOptionsBuilder
                .fromFnPromise(function () {
                    var defer = $q.defer();
                    $http.get('/CiclosAutoClaveAgua/List').then(function (result) {
                        defer.resolve(result.data);
                    });
                    return defer.promise;
                })

           
                .withButtons([
                    {
                        
                        extend: 'excel',
                        text: '<i class="fa fa-file-text-o"></i> Excel',
                        titleAttr: 'Excel'
                    },
                    {
                        extend: 'print',
                        text: '<i class="fa fa-print" aria-hidden="true"></i> Print',
                        titleAttr: 'Print'
                    },
                    {
                        extend: 'copy',
                        text: '<i class="fa fa-files-o"></i> Copy',
                        titleAttr: 'Copy'
                        
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
                DTColumnBuilder.newColumn('idSeccion').withTitle('N.Carro'),
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
                DTColumnBuilder.newColumn('tif6').withTitle('P.FINAL F6').renderWith(function (data, type, full, meta) {
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
             
            ]

            $scope.dtColumnDefs = [
                DTColumnDefBuilder.newColumnDef(0),
                DTColumnDefBuilder.newColumnDef(1),
                DTColumnDefBuilder.newColumnDef(2),
                DTColumnDefBuilder.newColumnDef(3),
                DTColumnDefBuilder.newColumnDef(4)
            ];


        })

})(angular);

//var app = angular.module('myApp', [/*'datatables'*/]);
//app.controller('myCtrl', function ($scope) { });
//app.controller('homeCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',
//    function ($scope, $http, DTOptionsBuilder, DTColumnBuilder) {

//        $scope.dtColumns = [
//            DTColumnBuilder.newColumn("idAutoclave", "idAutoclave"),
        
//            //DTColumnBuilder.newColumn("CompanyName", "Company Name"),
//            //DTColumnBuilder.newColumn("ContactName", "Contact Name"),
//            //DTColumnBuilder.newColumn("Phone", "Phone"),
//            //DTColumnBuilder.newColumn("City", "City")
//        ]

//        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
//            url: "/CiclosAutoClaveAgua/List",
//            type: "GET"
//        })
//            .withPaginationType('full_numbers')
//            .withDisplayLength(10);
//    }])

//var app = angular.module('myApp', []);
//app.controller('myCtrl', function ($scope) {
//    $scope.firstName = "John";
//    $scope.lastName = "Doe";
//});