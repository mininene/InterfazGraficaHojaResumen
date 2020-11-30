var app = angular.module('datatablesHomeApp', []);
app.controller('homeCtrl', function ($scope, $http, $interval,$timeout) {
    $scope.loading = true;
    $http.get('/Home/ListHome')
        .then(function (response) {
            $scope.loading = false;
            $scope.myWelcome = response.data;

            $scope.a = parseInt(response.data[0].ultimoCiclo) - 1
            $scope.b = parseInt(response.data[1].ultimoCiclo) - 1
            $scope.c = parseInt(response.data[2].ultimoCiclo) - 1
            $scope.d = parseInt(response.data[3].ultimoCiclo) - 1
            $scope.e = parseInt(response.data[4].ultimoCiclo) - 1
            $scope.f = parseInt(response.data[5].ultimoCiclo) - 1
            $scope.g = parseInt(response.data[6].ultimoCiclo) - 1
            $scope.h = parseInt(response.data[7].ultimoCiclo) - 1
            $scope.i = parseInt(response.data[8].ultimoCiclo) - 1
            $scope.j = parseInt(response.data[9].ultimoCiclo) - 1
            $scope.k = parseInt(response.data[10].ultimoCiclo) - 1
            $scope.l = parseInt(response.data[11].ultimoCiclo) - 1
            $scope.m = parseInt(response.data[12].ultimoCiclo) - 1

            $scope.ea = response.data[0].estado
            $scope.eb = response.data[1].estado
            $scope.ec = response.data[2].estado
            $scope.ed = response.data[3].estado
            $scope.ee = response.data[4].estado
            $scope.ef = response.data[5].estado
            $scope.eg = response.data[6].estado
            $scope.eh = response.data[7].estado
            $scope.ei = response.data[8].estado
            $scope.ej = response.data[9].estado
            $scope.ek = response.data[10].estado
            $scope.el = response.data[11].estado
            $scope.em = response.data[12].estado


            $scope.na = response.data[0].nombre
            $scope.nb = response.data[1].nombre
            $scope.nc = response.data[2].nombre
            $scope.nd = response.data[3].nombre
            $scope.ne = response.data[4].nombre
            $scope.nf = response.data[5].nombre
            $scope.ng = response.data[6].nombre
            $scope.nh = response.data[7].nombre
            $scope.ni = response.data[8].nombre
            $scope.nj = response.data[9].nombre
            $scope.nk = response.data[10].nombre
            $scope.nl = response.data[11].nombre
            $scope.nm = response.data[12].nombre




        });
    $scope.LoadData = function () {
       

       
        $http.get('/Home/ListHome')
            .then(function (response) {
                $scope.loading = false;
                $scope.myWelcome = response.data;
                $scope.a = parseInt(response.data[0].ultimoCiclo) - 1
                $scope.b = parseInt(response.data[1].ultimoCiclo) - 1
                $scope.c = parseInt(response.data[2].ultimoCiclo) - 1
                $scope.d = parseInt(response.data[3].ultimoCiclo) - 1
                $scope.e = parseInt(response.data[4].ultimoCiclo) - 1
                $scope.f = parseInt(response.data[5].ultimoCiclo) - 1
                $scope.g = parseInt(response.data[6].ultimoCiclo) - 1
                $scope.h = parseInt(response.data[7].ultimoCiclo) - 1
                $scope.i = parseInt(response.data[8].ultimoCiclo) - 1
                $scope.j = parseInt(response.data[9].ultimoCiclo) - 1
                $scope.k = parseInt(response.data[10].ultimoCiclo) - 1
                $scope.l = parseInt(response.data[11].ultimoCiclo) - 1
                $scope.m = parseInt(response.data[12].ultimoCiclo) - 1

                $scope.ea = response.data[0].estado
                $scope.eb = response.data[1].estado
                $scope.ec = response.data[2].estado
                $scope.ed = response.data[3].estado
                $scope.ee = response.data[4].estado
                $scope.ef = response.data[5].estado
                $scope.eg = response.data[6].estado
                $scope.eh = response.data[7].estado
                $scope.ei = response.data[8].estado
                $scope.ej = response.data[9].estado
                $scope.ek = response.data[10].estado
                $scope.el = response.data[11].estado
                $scope.em = response.data[12].estado


                $scope.na = response.data[0].nombre
                $scope.nb = response.data[1].nombre
                $scope.nc = response.data[2].nombre
                $scope.nd = response.data[3].nombre
                $scope.ne = response.data[4].nombre
                $scope.nf = response.data[5].nombre
                $scope.ng = response.data[6].nombre
                $scope.nh = response.data[7].nombre
                $scope.ni = response.data[8].nombre
                $scope.nj = response.data[9].nombre
                $scope.nk = response.data[10].nombre
                $scope.nl = response.data[11].nombre
                $scope.nm = response.data[12].nombre
                
            });
    }; 
    $interval($scope.LoadData, 2000);
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