//employee controller

myapp.controller('autoclave-controller', function ($scope, autoclaveService,$timeout) {
   

    
    $scope.loading = true;
    //Loads all Employee records when page loads
   
    loadPreview();
    loadPrint();
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
           // window.alert("El archivo ha sido enviado a la impresora")
            $scope.received = false;
            $scope.send = true;
          
            
            var AutoclavesPrint = autoclaveService.getPrint(empId);
            AutoclavesPrint.then(function (response) {
                id = empId
               
                $scope.send = false;
              
                $scope.original = response.data;
                //window.alert("El archivo ha sido impreso")
                $scope.received = true;
                $timeout(function () { $scope.received = false; }, 4000);

               
            });


        }
    }


    
 
    
});


