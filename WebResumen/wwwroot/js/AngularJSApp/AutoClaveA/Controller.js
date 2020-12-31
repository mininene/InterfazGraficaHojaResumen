//employee controller



myapp.controller('autoclave-controller', function ($scope, autoclaveService, $timeout, $window, $http) {



    $scope.loading = true;
    //Loads all Employee records when page loads
    loadSearch();
    loadPreview();
    loadPrint();
    loadLogin();
    loadAutoclaves();



    //listar
    function loadAutoclaves() {
        var AutoclavesRecords = autoclaveService.getAllAutoclaves();
        AutoclavesRecords.then(function (d) {
            //success

            $scope.Autoclaves = d.data;
            // console.log("realizado")
            $scope.loading = false;
        },
            function () {
                alert("Error occured while fetching employee list...");
            });
    }

    //Vista Previa

    function loadPreview() {

        //$scope.loading = true;
        //$scope.count = 0;
        $scope.vistaPrevia = function (empId) {


            //console.log(empId)
            $scope.count++;
            var AutoclavesPreview = autoclaveService.getPreview(empId);
            AutoclavesPreview.then(function (response) {
                id = empId
                //console.log(id)
                $scope.loading = false;
                $scope.original = response.data;
            });


        }
    }



    //Impresion

    function loadPrint() {

        $scope.imprimir = function (empId) {
            window.alert("El archivo ha sido enviado a la impresora")
            //$scope.received = false;
            //$scope.send = true;
            window.alert("Espere!, la página lo redireccioanará automáticamente")

            var AutoclavesPrint = autoclaveService.getPrint(empId);
            AutoclavesPrint.then(function (response) {
                id = empId
                $scope.loading = false;
                //$scope.send = false;

                $scope.original = response.data;
                window.alert("El archivo ha sido impreso")
                //$scope.received = true;
               // $timeout(function () { $scope.received = false; }, 3000);
                $window.location.href = '/AutoClaveA'


            });


        }
    }

    //Login

    function loadLogin() {

        //$scope.loading = true;
        //$scope.count = 0;
        $scope.login = function (empId) {


            //console.log(empId)
            $scope.count++;
            var AutoclavesLogin = autoclaveService.getLogin(empId);
            AutoclavesLogin.then(function (response) {
                id = empId
                $scope.loading = false;
                $scope.original = response.data;


                $window.location.href = '/AutoClaveA/Login/' + id.toString();
                //console.log(id)

            });


        }
    }





    function loadSearch() {


       
            $scope.programa = null;
            $scope.numeroCiclo = null;

            $scope.postdata = function (programa, numeroCiclo) {

                var data = {
                    nCiclo: numeroCiclo,
                    nPrograma: programa
                };
                console.log(data);


                //Call the services

                //$http.post('/AutoClaveA/ListAutoclaveA', JSON.stringify(data)).then(function (response) {
               $http.post('/AutoClaveA/ListAutoclaveA', JSON.stringify(data)).then(function (response) {
                    if (response.data)
                        console.log(response.data);
                        $scope.msg = "Post Data Submitted Successfully!";
                    console.log($scope.msg);

                }, function (response) {

                    $scope.msg = "Service not Exists";

                    $scope.statusval = response.status;

                    $scope.statustext = response.statusText;

                    $scope.headers = response.headers();
                   

                });

            };


        

    }


        
    


    
 
    
});


