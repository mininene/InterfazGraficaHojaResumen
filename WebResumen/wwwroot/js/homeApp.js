var app = angular.module('datatablesHomeApp', []);
app.controller('homeCtrl', function ($scope, $http, $interval,$timeout,$q) {
    $scope.loading = true;
    $q.all([
        $http.get('/AutoClaveA/ListaAutoclaveA'),
        $http.get('/AutoClaveB/ListaAutoclaveB'),
        $http.get('/AutoClaveC/ListaAutoclaveC'),
        $http.get('/AutoClaveD/ListaAutoclaveD'),
        $http.get('/AutoClaveE/ListaAutoclaveE'),
        $http.get('/AutoClaveF/ListaAutoclaveF'),
        $http.get('/AutoClaveG/ListaAutoclaveG'),
        $http.get('/AutoClaveH/ListaAutoclaveH'),
        $http.get('/AutoClaveI/ListaAutoclaveI'),
        $http.get('/AutoClaveM/ListaAutoclaveM'),
        $http.get('/AutoClaveJ/ListaAutoclaveJ'),
        $http.get('/AutoClaveK/ListaAutoclaveK'),
        $http.get('/AutoClaveL/ListaAutoclaveL')

    ]).then(function (results) {
        /* enter your logic here */
        //console.log(results[0].data[0]);
        $scope.loading = false;
        $scope.horaA = results[0].data[0].horaFin;
        $scope.horaB = results[1].data[0].horaFin;
        $scope.horaC = results[2].data[0].horaFin;
        $scope.horaD = results[3].data[0].horaFin;
        $scope.horaE = results[4].data[0].horaFin;
        $scope.horaF = results[5].data[0].horaFin;
        $scope.horaG = results[6].data[0].horaFin;
        $scope.horaH = results[7].data[0].horaFin;
        $scope.horaI = results[8].data[0].horaFin;
        $scope.horaM = results[9].data[0].horaFin;
        $scope.horaJ = results[10].data[0].horaFin;
        $scope.horaK = results[11].data[0].horaFin;
        $scope.horaL = results[12].data[0].horaFin;


    });

    $http.get('/Inicio/ListHome')
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

            //$scope.newa = parseInt(response.data[0].ultimoCiclo) - 1
            //$scope.newb = parseInt(response.data[1].ultimoCiclo) - 1
            //$scope.newc = parseInt(response.data[2].ultimoCiclo) - 1
            //$scope.newd = parseInt(response.data[3].ultimoCiclo) - 1
            //$scope.newe = parseInt(response.data[4].ultimoCiclo) - 1
            //$scope.newf = parseInt(response.data[5].ultimoCiclo) - 1
            //$scope.newg = parseInt(response.data[6].ultimoCiclo) - 1
            //$scope.newh = parseInt(response.data[7].ultimoCiclo) - 1
            //$scope.newi = parseInt(response.data[8].ultimoCiclo) - 1
            //$scope.newj = parseInt(response.data[9].ultimoCiclo) - 1
            //$scope.newk = parseInt(response.data[10].ultimoCiclo) - 1
            //$scope.newl = parseInt(response.data[11].ultimoCiclo) - 1
            //$scope.newm = parseInt(response.data[12].ultimoCiclo) - 1

            //let date = new Date()
            //date.toLocaleString();
            //let day = date.getDate();
            //let month = date.getMonth() + 1;
            //let year = date.getFullYear();
            //console.log(`${day}/${month}/${year}`)
            //$scope.horax = date.toLocaleString();

          //  $scope.hola = parseInt(response.data[0].ultimoCiclo) - 1;


        });

        


    //$scope.loadinga = true;
    //$scope.loadingb = true;
    //$scope.loadingc = true;
    //$scope.loadingd = true;
    //$scope.loadinge = true;
    //$scope.loadingf = true;
    //$scope.loadingg = true;
    //$scope.loadingh = true;
    //$scope.loadingi = true;
    //$scope.loadingm = true;
   

    $scope.LoadData = function () {
       
       
        //$http.get('/AutoClaveA/ListaAutoclaveA')
        //    .then(function (response) {
                
        //        $scope.resA = response.data;
        //        console.log($scope.resA)
        //        $scope.horaA = response.data[0].horaFin;
        //        $scope.loadinga = false;
               
        //    });

        //$http.get('/AutoClaveB/ListaAutoclaveB')
        //    .then(function (response) {
        //        $scope.resB = response.data;
        //        $scope.horaB = response.data[0].horaFin;
        //        $scope.loadingb = false;
        //    });

        //$http.get('/AutoClaveC/ListAutoclaveC')
        //    .then(function (response) {
        //        $scope.resC = response.data;
        //        $scope.horaC = response.data[0].horaFin;
        //        $scope.loadingc = false;
        //    });

        //$http.get('/AutoClaveD/ListAutoclaveD')
        //    .then(function (response) {
        //        $scope.resD = response.data;
        //        $scope.horaD = response.data[0].horaFin;
        //        $scope.loadingd = false;
        //    });

        //$http.get('/AutoClaveE/ListAutoclaveE')
        //    .then(function (response) {
        //        $scope.resE = response.data;
        //        $scope.horaE = response.data[0].horaFin;
        //        $scope.loadinge = false;
        //    });

        //$http.get('/AutoClaveF/ListAutoclaveF')
        //    .then(function (response) {
        //        $scope.resF = response.data;
        //        $scope.horaF = response.data[0].horaFin;
        //        $scope.loadingf = false;
        //    });

        //$http.get('/AutoClaveG/ListAutoclaveG')
        //    .then(function (response) {
        //        $scope.resG = response.data;
        //        $scope.horaG = response.data[0].horaFin;
        //        $scope.loadingg = false;
        //    });

        //$http.get('/AutoClaveH/ListAutoclaveH')
        //    .then(function (response) {
        //        $scope.resH = response.data;
        //        $scope.horaH = response.data[0].horaFin;
        //        $scope.loadingh = false;
        //    });
        //$http.get('/AutoClaveI/ListAutoclaveI')
        //    .then(function (response) {
        //        $scope.resI = response.data;
        //        $scope.horaI = response.data[0].horaFin;
        //        $scope.loadingi = false;
        //    });
        //$http.get('/AutoClaveM/ListAutoclaveM')
        //    .then(function (response) {
        //        $scope.resM = response.data;
        //        $scope.horaM = response.data[0].horaFin;
        //        $scope.loadingm = false;
        //    });

        $q.all([
            $http.get('/AutoClaveA/ListaAutoclaveA'),
            $http.get('/AutoClaveB/ListaAutoclaveB'),
            $http.get('/AutoClaveC/ListaAutoclaveC'),
            $http.get('/AutoClaveD/ListaAutoclaveD'),
            $http.get('/AutoClaveE/ListaAutoclaveE'),
            $http.get('/AutoClaveF/ListaAutoclaveF'),
            $http.get('/AutoClaveG/ListaAutoclaveG'),
            $http.get('/AutoClaveH/ListaAutoclaveH'),
            $http.get('/AutoClaveI/ListaAutoclaveI'),
            $http.get('/AutoClaveM/ListaAutoclaveM'),
            $http.get('/AutoClaveJ/ListaAutoclaveJ'),
            $http.get('/AutoClaveK/ListaAutoclaveK'),
            $http.get('/AutoClaveL/ListaAutoclaveL')
           
        ]).then(function (results) {
            /* enter your logic here */
            //console.log(results[0].data[0]);
            $scope.horaA = results[0].data[0].horaFin;
            $scope.horaB = results[1].data[0].horaFin;
            $scope.horaC = results[2].data[0].horaFin;
            $scope.horaD = results[3].data[0].horaFin;
            $scope.horaE = results[4].data[0].horaFin;
            $scope.horaF = results[5].data[0].horaFin;
            $scope.horaG = results[6].data[0].horaFin;
            $scope.horaH = results[7].data[0].horaFin;
            $scope.horaI = results[8].data[0].horaFin;
            $scope.horaM = results[9].data[0].horaFin;
            $scope.horaJ = results[10].data[0].horaFin;
            $scope.horaK = results[11].data[0].horaFin;
            $scope.horaL = results[12].data[0].horaFin;


        });

        //$http.get('/AutoClaveJ/ListAutoclaveJ')
        //    .then(function (response) {
        //        $scope.resJ = response.data;
        //        $scope.horaJ = response.data[0].horaFin;
        //    });

        //$http.get('/AutoClaveK/ListAutoclaveK')
        //    .then(function (response) {
        //        $scope.resK = response.data;
        //        $scope.horaK = response.data[0].horaFin;
        //    });

        //$http.get('/AutoClaveL/ListAutoclaveL')
        //    .then(function (response) {
        //        $scope.resL = response.data;
        //        $scope.horaL = response.data[0].horaFin;
        //    });







       
        $http.get('/Inicio/ListHome')
            .then(function (response) {
                //$scope.loading = false;
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
               
                //$scope.olda = parseInt(response.data[0].ultimoCiclo) - 1
                //$scope.oldb = parseInt(response.data[1].ultimoCiclo) - 1
                //$scope.oldc = parseInt(response.data[2].ultimoCiclo) - 1
                //$scope.oldd = parseInt(response.data[3].ultimoCiclo) - 1
                //$scope.olde = parseInt(response.data[4].ultimoCiclo) - 1
                //$scope.oldf = parseInt(response.data[5].ultimoCiclo) - 1
                //$scope.oldg = parseInt(response.data[6].ultimoCiclo) - 1
                //$scope.oldh = parseInt(response.data[7].ultimoCiclo) - 1
                //$scope.oldi = parseInt(response.data[8].ultimoCiclo) - 1
                //$scope.oldj = parseInt(response.data[9].ultimoCiclo) - 1
                //$scope.oldk = parseInt(response.data[10].ultimoCiclo) - 1
                //$scope.oldl = parseInt(response.data[11].ultimoCiclo) - 1
                //$scope.oldm = parseInt(response.data[12].ultimoCiclo) - 1
               // $scope.oldVar = parseInt(response.data[0].ultimoCiclo) - 1;

                //if ($scope.olda != $scope.newa) {
                //    console.log("testVariable has changed!");
                //    let date = new Date()
                //   console.log( date.toLocaleString())
                //    //let day = date.getDate();
                //    //let month = date.getMonth() + 1;
                //    //let year = date.getFullYear();
                //    //let hour = date
                //   // console.log(`${day}/${month}/${year}`)
                //    $scope.horaA = date.toLocaleString();
                //    //$scope.horaA = `${day}/${month}/${year}`
                //    $scope.newa = parseInt(response.data[0].ultimoCiclo) - 1;
                //}

                //else {
                //    console.log("Valor no ha cambiado")
                //    //$scope.horaA= $scope.horax
                //    //let date = new Date()
                //    //let day = date.getDate();
                //    //let month = date.getMonth() + 1;
                //    //let year = date.getFullYear();
                //    //console.log(`${day}/${month}/${year}`)
                //}
               
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