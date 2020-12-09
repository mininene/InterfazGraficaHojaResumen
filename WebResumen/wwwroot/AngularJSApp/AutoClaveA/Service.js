//Service to get data from employee mvc controller
myapp.service('autoclaveService', function ($http) {

    //listar
    this.getAllAutoclaves = function () {

        return $http.get("/AutoClaveA/ListAutoclaveA");
    }

    //Vista Previa
    this.getPreview = function (empId) {

       return $http({
            method: 'Get',
            url: '/AutoClaveA/Vista',
            params: {
                id: empId
            },
        });
    }

     //Imprimir
        this.getPrint = function (empId) {

        return $http({
            method: 'Get',
            url: '/AutoClaveA/Print',
            params: {
                id: empId
            },
        });
    }

   


   
});