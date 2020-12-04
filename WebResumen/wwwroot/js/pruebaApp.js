var app = angular.module("Myapp", []);
app.controller("Mycontroller", function ($scope, $http,$q) {
    $scope.loading = true;
    var defer = $q.defer();
    $http.get('/CiclosAutoClaveAgua/ListAgua').then(function (result) {
        $scope.loading = false;
       defer.resolve(result.data);
       
        $scope.searchid=result.data
        data.searchCity = $('#sel_city').val();
        data.searchGender = $('#sel_gender').val();
        
       
    });
});