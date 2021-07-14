var app = angular.module('datatablesHomeApp', []);
app.controller('homeCtrl', function ($scope, $http, $interval,$timeout,$q) {
    $scope.loading = true;
    var getUrl = window.location;
    var baseUrl ="";
    //var baseUrl =  getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1];
    

   
    $q.all([
       
        $http.get(baseUrl+"/Inicio/ListHome"),
        $http.get(baseUrl+"/AutoClaveA/ListaAutoclaveA"),
       
        $http.get(baseUrl+"/AutoClaveJ/ListaAutoclaveJ")
       

    ]).then(function (response) {
       
        $scope.loading = false;
       

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

        

       

    });

    
   

    $scope.LoadData = function () {
       
       
       
       
        $q.all([
             
            $http.get(baseUrl + "/Inicio/ListHome"),
            $http.get(baseUrl + "/AutoClaveA/ListaAutoclaveA"),
           
            $http.get(baseUrl+"/AutoClaveJ/ListaAutoclaveJ")
           
           
        ]).then(function (response) {
           

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



           


        });

       




       
       


    }; 

    


    



    $interval($scope.LoadData, 60000);
    
    
});













