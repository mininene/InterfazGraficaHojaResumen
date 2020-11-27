var app = angular.module('datatablesHomeApp', []);
app.controller('homeCtrl', function ($scope, $http, $interval,$timeout) {
    $scope.loading = true;
    $http.get('/Home/ListHome')
        .then(function (response) {
            $scope.loading = false;
            $scope.myWelcome = response.data;

        });
    $scope.LoadData = function () {
       

       
        $http.get('/Home/ListHome')
            .then(function (response) {
                $scope.loading = false;
                $scope.myWelcome = response.data;
                
            });
    }; 
    $interval($scope.LoadData, 5000);
});













//(function (angular) {
//    'use strict';
//    angular.module('datatablesHomeApp', [/*'datatables', 'datatables.buttons'*/]).
//        controller('homeCtrl', function ($scope, $http, $q/*, DTOptionsBuilder, DTColumnBuilder, DTColumnDefBuilder, DTDefaultOptions*/) {
//            //DTDefaultOptions.setLoadingTemplate('<div class="spinner-border text-primary" role="status"></div >' + '  ' + '<span class="sr - only">Cargando...</span>') //spinner carga

//            //$scope.dtOptions = DTOptionsBuilder
//                .fromFnPromise(function () {
//                    var defer = $q.defer();
//                    $http.get('/Home/ListHome').then(function (result) {
//                        defer.resolve(result.data);
//                        $scope.myWelcome =result.data /*parseInt(result.data[1].ultimoCiclo) - 1*/;


                        

                //    });
                   
                //    return defer.promise;
                   

                  
                //})
                //.withLanguage({
                //    "sEmptyTable": "Ningún dato disponible en esta tabla",
                //    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                //    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                //    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                //    "sInfoPostFix": "",

                //    "sInfoThousands": ",",
                //    "sLengthMenu": "Mostrar   _MENU_   registros",
                //    "sloadingRecords": '<div class="spinner-border text-primary" role="status">< span class= "sr-only" > Loading...</span></div >',
                //    "sProcessing": "procesando...",
                //    "sSearch": "Buscar:",
                //    "sZeroRecords": "No hay registros encontrados",
                //    "oPaginate": {
                //        "sFirst": "Primero",
                //        "sLast": "Último",
                //        "sNext": "Siguiente",
                //        "sPrevious": "Anterior"
                //    },
                //    "oAria": {
                //        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                //        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                //    },
                //    "oLanguage": {
                //        "sProcessing": "loading data...",
                //    }
                //})



                
          

            //$scope.dtColumns = [
            //    DTColumnBuilder.newColumn(null).withTitle('#').renderWith(function (data, type, full, meta) {
            //        //console.log(x);
            //        return meta.row + 1

            //    }),


            //    DTColumnBuilder.newColumn('nombre').withTitle('Nombre'),
            //    DTColumnBuilder.newColumn('ultimoCiclo').withTitle('Ciclo').renderWith(function (data, type, full, meta) {
            //        var x = (parseInt(data) - 1);
            //        //var y = x.padStart(2);
            //        return x
            //    }),

            //    DTColumnBuilder.newColumn('estado').withTitle('Estado'),
               
               


            //]




//        })

//})(angular);