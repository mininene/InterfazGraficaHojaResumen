var app = angular.module('datatablesHomeApp', []);
app.controller('homeCtrl', function ($scope, $http, $interval,$timeout,$q) {
    $scope.loading = true;
    $q.all([
        //$http.get('/Inicio/ListHomex'),
        $http.get('/Inicio/ListHome'),
        $http.get('/AutoClaveA/ListaAutoclaveA'),
        //$http.get('/AutoClaveB/ListaAutoclaveB'),
        //$http.get('/AutoClaveC/ListaAutoclaveC'),
        //$http.get('/AutoClaveD/ListaAutoclaveD'),
        //$http.get('/AutoClaveE/ListaAutoclaveE'),
        //$http.get('/AutoClaveF/ListaAutoclaveF'),
        //$http.get('/AutoClaveG/ListaAutoclaveG'),
        //$http.get('/AutoClaveH/ListaAutoclaveH'),
        //$http.get('/AutoClaveI/ListaAutoclaveI'),
        //$http.get('/AutoClaveM/ListaAutoclaveM'),
        $http.get('/AutoClaveJ/ListaAutoclaveJ')
        //$http.get('/AutoClaveK/ListaAutoclaveK'),
        //$http.get('/AutoClaveL/ListaAutoclaveL')

    ]).then(function (response) {
        /* enter your logic here */
        //console.log(response[0].data);
        //console.log(response[2].data);
        $scope.loading = false;
        //$scope.horaA = response[1].data[0].horaFin;
        //$scope.horaB = response[2].data[0].horaFin;
        //$scope.horaC = response[3].data[0].horaFin;
        //$scope.horaD = response[4].data[0].horaFin;
        //$scope.horaE = response[5].data[0].horaFin;
        //$scope.horaF = response[6].data[0].horaFin;
        //$scope.horaG = response[7].data[0].horaFin;
        //$scope.horaH = response[8].data[0].horaFin;
        //$scope.horaI = response[9].data[0].horaFin;
        //$scope.horaM = response[10].data[0].horaFin;
        //$scope.horaJ = response[11].data[0].horaFin;
        //$scope.horaK = response[12].data[0].horaFin;
        //$scope.horaL = response[13].data[0].horaFin;

        $scope.horaA = response[1].data[0].horaFin;
        $scope.horaB = response[1].data[1].horaFin;
        $scope.horaC = response[1].data[2].horaFin;
        $scope.horaD = response[1].data[3].horaFin;
        $scope.horaE = response[1].data[4].horaFin;
        $scope.horaF = response[1].data[5].horaFin;
        $scope.horaG = response[1].data[6].horaFin;
        $scope.horaH = response[1].data[7].horaFin;
        $scope.horaI = response[1].data[8].horaFin;
        $scope.horaM = response[1].data[9].horaFin;
        $scope.horaJ = response[2].data[0].horaFin;
        $scope.horaK = response[2].data[1].horaFin;
        $scope.horaL = response[2].data[2].horaFin;
       
        $scope.na = response[0].data[0].nombre;
        $scope.nb = response[0].data[1].nombre;
        $scope.nc = response[0].data[2].nombre;
        $scope.nd = response[0].data[3].nombre;
        $scope.ne = response[0].data[4].nombre;
        $scope.nf = response[0].data[5].nombre;
        $scope.ng = response[0].data[6].nombre;
        $scope.nh = response[0].data[7].nombre;
        $scope.ni = response[0].data[8].nombre;
        $scope.nm = response[0].data[12].nombre;
        $scope.nj = response[0].data[9].nombre;
        $scope.nk = response[0].data[10].nombre;
        $scope.nl = response[0].data[11].nombre;

        $scope.ea = response[0].data[0].estado;
        $scope.eb = response[0].data[1].estado;
        $scope.ec = response[0].data[2].estado;
        $scope.ed = response[0].data[3].estado;
        $scope.ee = response[0].data[4].estado;
        $scope.ef = response[0].data[5].estado;
        $scope.eg = response[0].data[6].estado;
        $scope.eh = response[0].data[7].estado;
        $scope.ei = response[0].data[8].estado;
        $scope.em = response[0].data[12].estado;
        $scope.ej = response[0].data[9].estado;
        $scope.ek = response[0].data[10].estado;
        $scope.el = response[0].data[11].estado;

        $scope.a = parseInt(response[0].data[0].ultimoCiclo) - 1
        $scope.b = parseInt(response[0].data[1].ultimoCiclo) - 1
        $scope.c = parseInt(response[0].data[2].ultimoCiclo) - 1
        $scope.d = parseInt(response[0].data[3].ultimoCiclo) - 1
        $scope.e = parseInt(response[0].data[4].ultimoCiclo) - 1
        $scope.f = parseInt(response[0].data[5].ultimoCiclo) - 1
        $scope.g = parseInt(response[0].data[6].ultimoCiclo) - 1
        $scope.h = parseInt(response[0].data[7].ultimoCiclo) - 1
        $scope.i = parseInt(response[0].data[8].ultimoCiclo) - 1
        $scope.m = parseInt(response[0].data[12].ultimoCiclo) - 1
        $scope.j = parseInt(response[0].data[9].ultimoCiclo) - 1
        $scope.k = parseInt(response[0].data[10].ultimoCiclo) - 1
        $scope.l = parseInt(response[0].data[11].ultimoCiclo) - 1




          //if (response[0].data[0].sabiUnoList != null) {
          //      $scope.horaA = response[0].data[0].sabiUnoList.horaFin;
          //  } else { $scope.horaA = "Desconocido"; }

          //  if (response[0].data[1].sabiUnoList != null) {
          //      $scope.horaB = response[0].data[1].sabiUnoList.horaFin;
          //  } else { $scope.horaB = "Desconocido"; }

          //  if (response[0].data[2].sabiUnoList != null) {
          //      $scope.horaC = response[0].data[2].sabiUnoList.horaFin;
          //  } else { $scope.horaC = "Desconocido"; }

          //  if (response[0].data[3].sabiUnoList != null) {
          //      $scope.horaD = response[0].data[3].sabiUnoList.horaFin;
          //  } else { $scope.horaD = "Desconocido"; }

          //  if (response[0].data[4].sabiUnoList != null) {
          //      $scope.horaE = response[0].data[4].sabiUnoList.horaFin;
          //  } else { $scope.horaE = "Desconocido"; }

          //  if (response[0].data[5].sabiUnoList != null) {
          //      $scope.horaF = response[0].data[5].sabiUnoList.horaFin;
          //  } else { $scope.horaF = "Desconocido"; }

          //  if (response[0].data[6].sabiUnoList != null) {
          //      $scope.horaG = response[0].data[6].sabiUnoList.horaFin;
          //  } else { $scope.horaG = "Desconocido"; }

          //  if (response[0].data[8].sabiUnoList != null) {                //  response.data[8] BEFORE
          //      $scope.horaH = response[0].data[8].sabiUnoList.horaFin;
          //  } else { $scope.horaH = "Desconocido"; }

          //  if (response[0].data[9].sabiUnoList != null) {
          //      $scope.horaI = response[0].data[9].sabiUnoList.horaFin;        //  response.data[9] BEFORE
          //  } else { $scope.horaI = "Desconocido"; }

          //  if (response[0].data[13].sabiUnoList != null) {
          //      $scope.horaM = response[0].data[13].sabiUnoList.horaFin;        //  response.data[13] BEFORE
          //  } else { $scope.horaM = "Desconocido"; }


          //  if (response[0].data[10].sabiDosList != null) {
          //      $scope.horaJ = response[0].data[10].sabiDosList.horaFin;     //  response.data[10] BEFORE
          //  } else { $scope.horaJ = "Desconocido"; }


          //  if (response[0].data[11].sabiDosList != null) {
          //      $scope.horaK = response[0].data[11].sabiDosList.horaFin;
          //  } else { $scope.horaK = "Desconocido"; }


          //  if (response[0].data[12].sabiDosList != null) {
          //      $scope.horaL = response[0].data[12].sabiDosList.horaFin;
          //  } else { $scope.horaL = "Desconocido"; }

       

    });

    //$http.get('/Inicio/ListHome')
    //    .then(function (response) {
    //        $scope.loading = false;
    //        $scope.myWelcome = response.data;
    //        console.log($scope.myWelcome);

          
           
            // if (response.data[0].horaFin != null) {
            //    $scope.horaA = response.data[0].horaFin;
            //} else { $scope.horaA = "Desconocido"; }

            //if (response.data[1].horaFin != null) {
            //    $scope.horaB = response.data[1].horaFin;
            //} else { $scope.horaB = "Desconocido"; }

            //if (response.data[2].horaFin != null) {
            //    $scope.horaC = response.data[2].horaFin;
            //} else { $scope.horaC = "Desconocido"; }

            //if (response.data[3].horaFin != null) {
            //    $scope.horaD = response.data[3].horaFin;
            //} else { $scope.horaD = "Desconocido"; }

            //if (response.data[4].horaFin != null) {
            //    $scope.horaE = response.data[4].horaFin;
            //} else { $scope.horaE = "Desconocido"; }

            //if (response.data[5].horaFin != null) {
            //    $scope.horaF = response.data[5].horaFin;
            //} else { $scope.horaF = "Desconocido"; }

            //if (response.data[6].horaFin != null) {
            //    $scope.horaG = response.data[6].horaFin;
            //} else { $scope.horaG = "Desconocido"; }

            //if (response.data[7].horaFin != null) {          
            //    $scope.horaH = response.data[7].horaFin;
            //} else { $scope.horaH = "Desconocido"; }

            //if (response.data[8].horaFin != null) {
            //    $scope.horaI = response.data[8].horaFin;     
            //} else { $scope.horaI = "Desconocido"; }

            //if (response.data[9].horaFin != null) {
            //    $scope.horaM = response.data[9].horaFin;    
            //} else { $scope.horaM = "Desconocido"; }


            //if (response.data[10].horaFin != null) {
            //    $scope.horaJ = response.data[10].horaFin;     
            //} else { $scope.horaJ = "Desconocido"; }


            //if (response.data[11].horaFin != null) {
            //    $scope.horaK = response.data[11].horaFin;
            //} else { $scope.horaK = "Desconocido"; }


            //if (response.data[12].horaFin != null) {
            //    $scope.horaL = response.data[12].horaFin;
            //} else { $scope.horaL = "Desconocido"; }
           
           // //ultimoCiclo
           // $scope.a = parseInt(response.data[0].maestroList.ultimoCiclo) - 1;
           // $scope.b = parseInt(response.data[1].maestroList.ultimoCiclo) - 1;
           // $scope.c = parseInt(response.data[2].maestroList.ultimoCiclo) - 1;
           // $scope.d = parseInt(response.data[3].maestroList.ultimoCiclo) - 1;
           // $scope.e = parseInt(response.data[4].maestroList.ultimoCiclo) - 1;
           // $scope.f = parseInt(response.data[5].maestroList.ultimoCiclo) - 1;
           // $scope.g = parseInt(response.data[6].maestroList.ultimoCiclo) - 1;
           // $scope.h = parseInt(response.data[7].maestroList.ultimoCiclo) - 1;  //antes   $scope.h = parseInt(response.data[8].maestroList.ultimoCiclo) - 1;
           // $scope.i = parseInt(response.data[8].maestroList.ultimoCiclo) - 1;  //antes   $scope.i = parseInt(response.data[9].maestroList.ultimoCiclo) - 1;
           // $scope.m = parseInt(response.data[12].maestroList.ultimoCiclo) - 1; //antes   $scope.m = parseInt(response.data[13].maestroList.ultimoCiclo) - 1;

           // $scope.j = parseInt(response.data[9].maestroList.ultimoCiclo) - 1;
           // $scope.k = parseInt(response.data[10].maestroList.ultimoCiclo) - 1;
           // $scope.l = parseInt(response.data[11].maestroList.ultimoCiclo) - 1;
           
           //// estado
           // $scope.ea = response.data[0].maestroList.estado;
           // $scope.eb = response.data[1].maestroList.estado;
           // $scope.ec = response.data[2].maestroList.estado;
           // $scope.ed = response.data[3].maestroList.estado;
           // $scope.ee = response.data[4].maestroList.estado;
           // $scope.ef = response.data[5].maestroList.estado;
           // $scope.eg = response.data[6].maestroList.estado;
           // $scope.eh = response.data[7].maestroList.estado;  // $scope.eh = response.data[8].maestroList.estado;
           // $scope.ei = response.data[8].maestroList.estado;  // $scope.ei = response.data[9].maestroList.estado;
           // $scope.em = response.data[12].maestroList.estado;  // $scope.em = response.data[13].maestroList.estado;

           // $scope.ej = response.data[9].maestroList.estado;  // response.data[10].maestroList.estado; 11-12
           // $scope.ek = response.data[10].maestroList.estado;
           // $scope.el = response.data[11].maestroList.estado;
           

           // //Nombre
           // $scope.na = response.data[0].maestroList.nombre;
           // $scope.nb = response.data[1].maestroList.nombre;
           // $scope.nc = response.data[2].maestroList.nombre;
           // $scope.nd = response.data[3].maestroList.nombre;
           // $scope.ne = response.data[4].maestroList.nombre;
           // $scope.nf = response.data[5].maestroList.nombre;
           // $scope.ng = response.data[6].maestroList.nombre;
         
           // $scope.nh = response.data[7].maestroList.nombre;   // $scope.nh = response.data[8].maestroList.nombre; antes
           // $scope.ni = response.data[8].maestroList.nombre;  //  $scope.ni = response.data[9].maestroList.nombre; antes
           // $scope.nm = response.data[12].maestroList.nombre;  // $scope.nm = response.data[13].maestroList.nombre;

          
           // $scope.nj = response.data[9].maestroList.nombre;   //  $scope.nj = response.data[10].maestroList.nombre; 
           // $scope.nk = response.data[10].maestroList.nombre;   // $scope.nk = response.data[11].maestroList.nombre;
           // $scope.nl = response.data[11].maestroList.nombre;  // $scope.nl = response.data[12].maestroList.nombre;


              
            
           // if (response.data[0].sabiUnoList != null) {
           //     $scope.horaA = response.data[0].sabiUnoList.horaFin;
           // } else { $scope.horaA = "Desconocido"; }

           // if (response.data[1].sabiUnoList != null) {
           //     $scope.horaB = response.data[1].sabiUnoList.horaFin;
           // } else { $scope.horaB = "Desconocido"; }

           // if (response.data[2].sabiUnoList != null) {
           //     $scope.horaC = response.data[2].sabiUnoList.horaFin;
           // } else { $scope.horaC = "Desconocido"; }

           // if (response.data[3].sabiUnoList != null) {
           //     $scope.horaD = response.data[3].sabiUnoList.horaFin;
           // } else { $scope.horaD = "Desconocido"; }

           // if (response.data[4].sabiUnoList != null) {
           //     $scope.horaE = response.data[4].sabiUnoList.horaFin;
           // } else { $scope.horaE = "Desconocido"; }

           // if (response.data[5].sabiUnoList != null) {
           //     $scope.horaF = response.data[5].sabiUnoList.horaFin;
           // } else { $scope.horaF = "Desconocido"; }

           // if (response.data[6].sabiUnoList != null) {
           //     $scope.horaG = response.data[6].sabiUnoList.horaFin;
           // } else { $scope.horaG = "Desconocido"; }

           // if (response.data[7].sabiUnoList != null) {                //  response.data[8] BEFORE
           //     $scope.horaH = response.data[7].sabiUnoList.horaFin;
           // } else { $scope.horaH = "Desconocido"; }

           // if (response.data[8].sabiUnoList != null) {
           //     $scope.horaI = response.data[8].sabiUnoList.horaFin;        //  response.data[9] BEFORE
           // } else { $scope.horaI = "Desconocido"; }

           // if (response.data[12].sabiUnoList != null) {
           //     $scope.horaM = response.data[12].sabiUnoList.horaFin;        //  response.data[13] BEFORE
           // } else { $scope.horaM = "Desconocido"; }
               

           // if (response.data[9].sabiDosList != null) {
           //     $scope.horaJ = response.data[9].sabiDosList.horaFin;     //  response.data[10] BEFORE
           // } else { $scope.horaJ = "Desconocido"; }


           // if (response.data[10].sabiDosList != null) {
           //     $scope.horaK = response.data[10].sabiDosList.horaFin;
           // } else { $scope.horaK = "Desconocido"; }


           // if (response.data[11].sabiDosList != null) {
           //     $scope.horaL = response.data[11].sabiDosList.horaFin;
           // } else { $scope.horaL = "Desconocido"; }


           

                         
          
               
            //$scope.a = parseInt(response.data[0].ultimoCiclo) - 1
            //$scope.b = parseInt(response.data[1].ultimoCiclo) - 1
            //$scope.c = parseInt(response.data[2].ultimoCiclo) - 1
            //$scope.d = parseInt(response.data[3].ultimoCiclo) - 1
            //$scope.e = parseInt(response.data[4].ultimoCiclo) - 1
            //$scope.f = parseInt(response.data[5].ultimoCiclo) - 1
            //$scope.g = parseInt(response.data[6].ultimoCiclo) - 1
            //$scope.h = parseInt(response.data[7].ultimoCiclo) - 1
            //$scope.i = parseInt(response.data[8].ultimoCiclo) - 1
            //$scope.j = parseInt(response.data[9].ultimoCiclo) - 1
            //$scope.k = parseInt(response.data[10].ultimoCiclo) - 1
            //$scope.l = parseInt(response.data[11].ultimoCiclo) - 1
            //$scope.m = parseInt(response.data[12].ultimoCiclo) - 1
          
           
            //$scope.ea = response.data[0].estado
            //$scope.eb = response.data[1].estado
            //$scope.ec = response.data[2].estado
            //$scope.ed = response.data[3].estado
            //$scope.ee = response.data[4].estado
            //$scope.ef = response.data[5].estado
            //$scope.eg = response.data[6].estado
            //$scope.eh = response.data[7].estado
            //$scope.ei = response.data[8].estado
            //$scope.ej = response.data[9].estado
            //$scope.ek = response.data[10].estado
            //$scope.el = response.data[11].estado
            //$scope.em = response.data[12].estado


            //$scope.na = response.data[0].nombre
            //$scope.nb = response.data[1].nombre
            //$scope.nc = response.data[2].nombre
            //$scope.nd = response.data[3].nombre
            //$scope.ne = response.data[4].nombre
            //$scope.nf = response.data[5].nombre
            //$scope.ng = response.data[6].nombre
            //$scope.nh = response.data[7].nombre
            //$scope.ni = response.data[8].nombre
            //$scope.nj = response.data[9].nombre
            //$scope.nk = response.data[10].nombre
            //$scope.nl = response.data[11].nombre
            //$scope.nm = response.data[12].nombre

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


        //});

        


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
       
       
       
       
        $q.all([
              //$http.get('/Inicio/ListHomex'),
              $http.get('/Inicio/ListHome'),
            $http.get('/AutoClaveA/ListaAutoclaveA'),
            //$http.get('/AutoClaveB/ListaAutoclaveB'),
            //$http.get('/AutoClaveC/ListaAutoclaveC'),
            //$http.get('/AutoClaveD/ListaAutoclaveD'),
            //$http.get('/AutoClaveE/ListaAutoclaveE'),
            //$http.get('/AutoClaveF/ListaAutoclaveF'),
            //$http.get('/AutoClaveG/ListaAutoclaveG'),
            //$http.get('/AutoClaveH/ListaAutoclaveH'),
            //$http.get('/AutoClaveI/ListaAutoclaveI'),
            //$http.get('/AutoClaveM/ListaAutoclaveM'),
            $http.get('/AutoClaveJ/ListaAutoclaveJ')
            //$http.get('/AutoClaveK/ListaAutoclaveK'),
            //$http.get('/AutoClaveL/ListaAutoclaveL')
           
        ]).then(function (response) {
            //$scope.horaA = response[1].data[0].horaFin;
            //$scope.horaB = response[2].data[0].horaFin;
            //$scope.horaC = response[3].data[0].horaFin;
            //$scope.horaD = response[4].data[0].horaFin;
            //$scope.horaE = response[5].data[0].horaFin;
            //$scope.horaF = response[6].data[0].horaFin;
            //$scope.horaG = response[7].data[0].horaFin;
            //$scope.horaH = response[8].data[0].horaFin;
            //$scope.horaI = response[9].data[0].horaFin;
            //$scope.horaM = response[10].data[0].horaFin;
            //$scope.horaJ = response[11].data[0].horaFin;
            //$scope.horaK = response[12].data[0].horaFin;
            //$scope.horaL = response[13].data[0].horaFin;

            $scope.horaA = response[1].data[0].horaFin;
            $scope.horaB = response[1].data[1].horaFin;
            $scope.horaC = response[1].data[2].horaFin;
            $scope.horaD = response[1].data[3].horaFin;
            $scope.horaE = response[1].data[4].horaFin;
            $scope.horaF = response[1].data[5].horaFin;
            $scope.horaG = response[1].data[6].horaFin;
            $scope.horaH = response[1].data[7].horaFin;
            $scope.horaI = response[1].data[8].horaFin;
            $scope.horaM = response[1].data[9].horaFin;
            $scope.horaJ = response[2].data[0].horaFin;
            $scope.horaK = response[2].data[1].horaFin;
            $scope.horaL = response[2].data[2].horaFin;


            $scope.na = response[0].data[0].nombre;
            $scope.nb = response[0].data[1].nombre;
            $scope.nc = response[0].data[2].nombre;
            $scope.nd = response[0].data[3].nombre;
            $scope.ne = response[0].data[4].nombre;
            $scope.nf = response[0].data[5].nombre;
            $scope.ng = response[0].data[6].nombre;
            $scope.nh = response[0].data[7].nombre;
            $scope.ni = response[0].data[8].nombre;
            $scope.nm = response[0].data[12].nombre;
            $scope.nj = response[0].data[9].nombre;
            $scope.nk = response[0].data[10].nombre;
            $scope.nl = response[0].data[11].nombre;

            $scope.ea = response[0].data[0].estado;
            $scope.eb = response[0].data[1].estado;
            $scope.ec = response[0].data[2].estado;
            $scope.ed = response[0].data[3].estado;
            $scope.ee = response[0].data[4].estado;
            $scope.ef = response[0].data[5].estado;
            $scope.eg = response[0].data[6].estado;
            $scope.eh = response[0].data[7].estado;
            $scope.ei = response[0].data[8].estado;
            $scope.em = response[0].data[12].estado;
            $scope.ej = response[0].data[9].estado;
            $scope.ek = response[0].data[10].estado;
            $scope.el = response[0].data[11].estado;

            $scope.a = parseInt(response[0].data[0].ultimoCiclo) - 1
            $scope.b = parseInt(response[0].data[1].ultimoCiclo) - 1
            $scope.c = parseInt(response[0].data[2].ultimoCiclo) - 1
            $scope.d = parseInt(response[0].data[3].ultimoCiclo) - 1
            $scope.e = parseInt(response[0].data[4].ultimoCiclo) - 1
            $scope.f = parseInt(response[0].data[5].ultimoCiclo) - 1
            $scope.g = parseInt(response[0].data[6].ultimoCiclo) - 1
            $scope.h = parseInt(response[0].data[7].ultimoCiclo) - 1
            $scope.i = parseInt(response[0].data[8].ultimoCiclo) - 1
            $scope.m = parseInt(response[0].data[12].ultimoCiclo) - 1
            $scope.j = parseInt(response[0].data[9].ultimoCiclo) - 1
            $scope.k = parseInt(response[0].data[10].ultimoCiclo) - 1
            $scope.l = parseInt(response[0].data[11].ultimoCiclo) - 1



            //if (response[0].data[0].sabiUnoList != null) {
            //    $scope.horaA = response[0].data[0].sabiUnoList.horaFin;
            //} else { $scope.horaA = "Desconocido"; }

            //if (response[0].data[1].sabiUnoList != null) {
            //    $scope.horaB = response[0].data[1].sabiUnoList.horaFin;
            //} else { $scope.horaB = "Desconocido"; }

            //if (response[0].data[2].sabiUnoList != null) {
            //    $scope.horaC = response[0].data[2].sabiUnoList.horaFin;
            //} else { $scope.horaC = "Desconocido"; }

            //if (response[0].data[3].sabiUnoList != null) {
            //    $scope.horaD = response[0].data[3].sabiUnoList.horaFin;
            //} else { $scope.horaD = "Desconocido"; }

            //if (response[0].data[4].sabiUnoList != null) {
            //    $scope.horaE = response[0].data[4].sabiUnoList.horaFin;
            //} else { $scope.horaE = "Desconocido"; }

            //if (response[0].data[5].sabiUnoList != null) {
            //    $scope.horaF = response[0].data[5].sabiUnoList.horaFin;
            //} else { $scope.horaF = "Desconocido"; }

            //if (response[0].data[6].sabiUnoList != null) {
            //    $scope.horaG = response[0].data[6].sabiUnoList.horaFin;
            //} else { $scope.horaG = "Desconocido"; }

            //if (response[0].data[8].sabiUnoList != null) {                //  response.data[8] BEFORE
            //    $scope.horaH = response[0].data[8].sabiUnoList.horaFin;
            //} else { $scope.horaH = "Desconocido"; }

            //if (response[0].data[9].sabiUnoList != null) {
            //    $scope.horaI = response[0].data[9].sabiUnoList.horaFin;        //  response.data[9] BEFORE
            //} else { $scope.horaI = "Desconocido"; }

            //if (response[0].data[13].sabiUnoList != null) {
            //    $scope.horaM = response[0].data[13].sabiUnoList.horaFin;        //  response.data[13] BEFORE
            //} else { $scope.horaM = "Desconocido"; }


            //if (response[0].data[10].sabiDosList != null) {
            //    $scope.horaJ = response[0].data[10].sabiDosList.horaFin;     //  response.data[10] BEFORE
            //} else { $scope.horaJ = "Desconocido"; }


            //if (response[0].data[11].sabiDosList != null) {
            //    $scope.horaK = response[0].data[11].sabiDosList.horaFin;
            //} else { $scope.horaK = "Desconocido"; }


            //if (response[0].data[12].sabiDosList != null) {
            //    $scope.horaL = response[0].data[12].sabiDosList.horaFin;
            //} else { $scope.horaL = "Desconocido"; }

        //    /* enter your logic here */
        //    //console.log(results[0].data[0]);
        //    $scope.horaA = results[0].data[0].horaFin;
        //    $scope.horaB = results[1].data[0].horaFin;
        //    $scope.horaC = results[2].data[0].horaFin;
        //    $scope.horaD = results[3].data[0].horaFin;
        //    $scope.horaE = results[4].data[0].horaFin;
        //    $scope.horaF = results[5].data[0].horaFin;
        //    $scope.horaG = results[6].data[0].horaFin;
        //    $scope.horaH = results[7].data[0].horaFin;
        //    $scope.horaI = results[8].data[0].horaFin;
        //    $scope.horaM = results[9].data[0].horaFin;
        //    $scope.horaJ = results[10].data[0].horaFin;
        //    $scope.horaK = results[11].data[0].horaFin;
        //    $scope.horaL = results[12].data[0].horaFin;


        });

       






       
        //$http.get('/Inicio/ListHomex')
        //    .then(function (response) {

        //        $scope.loading = false;
        //        $scope.myWelcome = response.data;
                

                //ultimoCiclo
                //$scope.a = parseInt(response.data[0].maestroList.ultimoCiclo) - 1;
                //$scope.b = parseInt(response.data[1].maestroList.ultimoCiclo) - 1;
                //$scope.c = parseInt(response.data[2].maestroList.ultimoCiclo) - 1;
                //$scope.d = parseInt(response.data[3].maestroList.ultimoCiclo) - 1;
                //$scope.e = parseInt(response.data[4].maestroList.ultimoCiclo) - 1;
                //$scope.f = parseInt(response.data[5].maestroList.ultimoCiclo) - 1;
                //$scope.g = parseInt(response.data[6].maestroList.ultimoCiclo) - 1;
                //$scope.h = parseInt(response.data[7].maestroList.ultimoCiclo) - 1;  //antes   $scope.h = parseInt(response.data[8].maestroList.ultimoCiclo) - 1;
                //$scope.i = parseInt(response.data[8].maestroList.ultimoCiclo) - 1;  //antes   $scope.i = parseInt(response.data[9].maestroList.ultimoCiclo) - 1;
                //$scope.m = parseInt(response.data[12].maestroList.ultimoCiclo) - 1; //antes   $scope.m = parseInt(response.data[13].maestroList.ultimoCiclo) - 1;

                //$scope.j = parseInt(response.data[9].maestroList.ultimoCiclo) - 1;
                //$scope.k = parseInt(response.data[10].maestroList.ultimoCiclo) - 1;
                //$scope.l = parseInt(response.data[11].maestroList.ultimoCiclo) - 1;

                //// estado
                //$scope.ea = response.data[0].maestroList.estado;
                //$scope.eb = response.data[1].maestroList.estado;
                //$scope.ec = response.data[2].maestroList.estado;
                //$scope.ed = response.data[3].maestroList.estado;
                //$scope.ee = response.data[4].maestroList.estado;
                //$scope.ef = response.data[5].maestroList.estado;
                //$scope.eg = response.data[6].maestroList.estado;
                //$scope.eh = response.data[7].maestroList.estado;  // $scope.eh = response.data[8].maestroList.estado;
                //$scope.ei = response.data[8].maestroList.estado;  // $scope.ei = response.data[9].maestroList.estado;
                //$scope.em = response.data[12].maestroList.estado;  // $scope.em = response.data[13].maestroList.estado;

                //$scope.ej = response.data[9].maestroList.estado;  // response.data[10].maestroList.estado; 11-12
                //$scope.ek = response.data[10].maestroList.estado;
                //$scope.el = response.data[11].maestroList.estado;


                ////Nombre
                //$scope.na = response.data[0].maestroList.nombre;
                //$scope.nb = response.data[1].maestroList.nombre;
                //$scope.nc = response.data[2].maestroList.nombre;
                //$scope.nd = response.data[3].maestroList.nombre;
                //$scope.ne = response.data[4].maestroList.nombre;
                //$scope.nf = response.data[5].maestroList.nombre;
                //$scope.ng = response.data[6].maestroList.nombre;

                //$scope.nh = response.data[7].maestroList.nombre;   // $scope.nh = response.data[8].maestroList.nombre; antes
                //$scope.ni = response.data[8].maestroList.nombre;  //  $scope.ni = response.data[9].maestroList.nombre; antes
                //$scope.nm = response.data[12].maestroList.nombre;  // $scope.nm = response.data[13].maestroList.nombre;

                //$scope.nj = response.data[9].maestroList.nombre;   //  $scope.nj = response.data[10].maestroList.nombre; 
                //$scope.nk = response.data[10].maestroList.nombre;   // $scope.nk = response.data[11].maestroList.nombre;
                //$scope.nl = response.data[11].maestroList.nombre;  // $scope.nl = response.data[12].maestroList.nombre;



                //if (response.data[0].sabiUnoList != null) {
                //    $scope.horaA = response.data[0].sabiUnoList.horaFin;
                //} else { $scope.horaA = "Desconocido"; }

                //if (response.data[1].sabiUnoList != null) {
                //    $scope.horaB = response.data[1].sabiUnoList.horaFin;
                //} else { $scope.horaB = "Desconocido"; }

                //if (response.data[2].sabiUnoList != null) {
                //    $scope.horaC = response.data[2].sabiUnoList.horaFin;
                //} else { $scope.horaC = "Desconocido"; }

                //if (response.data[3].sabiUnoList != null) {
                //    $scope.horaD = response.data[3].sabiUnoList.horaFin;
                //} else { $scope.horaD = "Desconocido"; }

                //if (response.data[4].sabiUnoList != null) {
                //    $scope.horaE = response.data[4].sabiUnoList.horaFin;
                //} else { $scope.horaE = "Desconocido"; }

                //if (response.data[5].sabiUnoList != null) {
                //    $scope.horaF = response.data[5].sabiUnoList.horaFin;
                //} else { $scope.horaF = "Desconocido"; }

                //if (response.data[6].sabiUnoList != null) {
                //    $scope.horaG = response.data[6].sabiUnoList.horaFin;
                //} else { $scope.horaG = "Desconocido"; }

                //if (response.data[7].sabiUnoList != null) {                //  response.data[8] BEFORE
                //    $scope.horaH = response.data[7].sabiUnoList.horaFin;
                //} else { $scope.horaH = "Desconocido"; }

                //if (response.data[8].sabiUnoList != null) {
                //    $scope.horaI = response.data[8].sabiUnoList.horaFin;        //  response.data[9] BEFORE
                //} else { $scope.horaI = "Desconocido"; }

                //if (response.data[12].sabiUnoList != null) {
                //    $scope.horaM = response.data[12].sabiUnoList.horaFin;        //  response.data[13] BEFORE
                //} else { $scope.horaM = "Desconocido"; }


                //if (response.data[9].sabiDosList != null) {
                //    $scope.horaJ = response.data[9].sabiDosList.horaFin;     //  response.data[10] BEFORE
                //} else { $scope.horaJ = "Desconocido"; }


                //if (response.data[10].sabiDosList != null) {
                //    $scope.horaK = response.data[10].sabiDosList.horaFin;
                //} else { $scope.horaK = "Desconocido"; }


                //if (response.data[11].sabiDosList != null) {
                //    $scope.horaL = response.data[11].sabiDosList.horaFin;
                //} else { $scope.horaL = "Desconocido"; }


                //$scope.loading = false;
                //$scope.myWelcome = response.data;
                //$scope.a = parseInt(response.data[0].ultimoCiclo) - 1
                //$scope.b = parseInt(response.data[1].ultimoCiclo) - 1
                //$scope.c = parseInt(response.data[2].ultimoCiclo) - 1
                //$scope.d = parseInt(response.data[3].ultimoCiclo) - 1
                //$scope.e = parseInt(response.data[4].ultimoCiclo) - 1
                //$scope.f = parseInt(response.data[5].ultimoCiclo) - 1
                //$scope.g = parseInt(response.data[6].ultimoCiclo) - 1
                //$scope.h = parseInt(response.data[7].ultimoCiclo) - 1
                //$scope.i = parseInt(response.data[8].ultimoCiclo) - 1
                //$scope.j = parseInt(response.data[9].ultimoCiclo) - 1
                //$scope.k = parseInt(response.data[10].ultimoCiclo) - 1
                //$scope.l = parseInt(response.data[11].ultimoCiclo) - 1
                //$scope.m = parseInt(response.data[12].ultimoCiclo) - 1

              

                //$scope.ea = response.data[0].estado
                //$scope.eb = response.data[1].estado
                //$scope.ec = response.data[2].estado
                //$scope.ed = response.data[3].estado
                //$scope.ee = response.data[4].estado
                //$scope.ef = response.data[5].estado
                //$scope.eg = response.data[6].estado
                //$scope.eh = response.data[7].estado
                //$scope.ei = response.data[8].estado
                //$scope.ej = response.data[9].estado
                //$scope.ek = response.data[10].estado
                //$scope.el = response.data[11].estado
                //$scope.em = response.data[12].estado


                //$scope.na = response.data[0].nombre
                //$scope.nb = response.data[1].nombre
                //$scope.nc = response.data[2].nombre
                //$scope.nd = response.data[3].nombre
                //$scope.ne = response.data[4].nombre
                //$scope.nf = response.data[5].nombre
                //$scope.ng = response.data[6].nombre
                //$scope.nh = response.data[7].nombre
                //$scope.ni = response.data[8].nombre
                //$scope.nj = response.data[9].nombre
                //$scope.nk = response.data[10].nombre
                //$scope.nl = response.data[11].nombre
                //$scope.nm = response.data[12].nombre
               
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
                //$scope.oldVar = parseInt(response.data[0].ultimoCiclo) - 1;

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
               
            //});



    }; 

   



   


    



    $interval($scope.LoadData, 120000);
    
    
});













