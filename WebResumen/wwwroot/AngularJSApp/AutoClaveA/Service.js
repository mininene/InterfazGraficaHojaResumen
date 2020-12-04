//Service to get data from employee mvc controller
myapp.service('autoclaveService', function ($http) {

    this.getAllAutoclaves = function () {

        return $http.get("/AutoClaveA/ListAutoclaveA");
    }

    //update Employee records
    this.update = function (Employee) {
        var updaterequest = $http({
            method: 'post',
            url: '/Employee/Update',
            data: Employee
        });
        return updaterequest;
    }
});