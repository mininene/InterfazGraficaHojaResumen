//employee controller
myapp.controller('autoclave-controller', function ($scope, autoclaveService) {
    console.log("hola")
    $scope.loading = true;
    //Loads all Employee records when page loads
    loadAutoclaves();
   
    function loadAutoclaves() {
        var AutoclavesRecords = autoclaveService.getAllAutoclaves();
        AutoclavesRecords.then(function (d) {
            //success
           
            $scope.Autoclaves = d.data;
            console.log("realizado")
            $scope.loading = false;
        },
            function () {
                alert("Error occured while fetching employee list...");
            });
    }
});
