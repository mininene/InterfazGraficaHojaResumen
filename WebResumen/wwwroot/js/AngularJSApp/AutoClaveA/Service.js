//Service to get data from employee mvc controller
myapp.service('autoclaveService', function ($http) {

    //listar
    this.getAllAutoclaves = function () {

        return $http({
            method: 'Get',
            url: '/AutoClaveA/ListAutoclaveA',
            headers: {
                'Accept': 'application/json'
            }
           
        });
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

    //Re-Login
    this.getLogin = function (empId) {

        return $http({
            method: 'Get',
            url: '/AutoClaveA/Login',
            params: {
                id: empId
            },
        });
    }

    //Buscar
    this.getSearch = function (programa, numeroCiclo) {

        return $http({
            method: 'Post',
            url: '/AutoClaveA/ListAutoclaveA',

            params: {
                nCiclo: programa,
                nPrograma: numeroCiclo
            },
        });
    }

   


   
});