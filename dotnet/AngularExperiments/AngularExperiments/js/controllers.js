'use strict';

var myControllers = angular.module('myControllers', []);

myControllers.controller('homeController', function ($scope) {

    $scope.name = "Your Name Here!";

});



myControllers.controller('gridController', function ($scope) {


    $scope.gridOptions = {
        data: 'myData',
        enablePinning: true,
        columnDefs: [{ field: "name", width: 120, pinned: true },
                    { field: "age", width: 120 },
                    { field: "birthday", width: 120 },
                    { field: "salary", width: 120}]
    };

    $scope.myData = [{ name: "Moroni", age: 50, birthday: "Oct 28, 1970", salary: "60,000" },
                    { name: "Tiancum", age: 43, birthday: "Feb 12, 1985", salary: "70,000" },
                    { name: "Jacob", age: 27, birthday: "Aug 23, 1983", salary: "50,000" },
                    { name: "Nephi", age: 29, birthday: "May 31, 2010", salary: "40,000" },
                    { name: "Enos", age: 34, birthday: "Aug 3, 2008", salary: "30,000" },
                    { name: "Moroni", age: 50, birthday: "Oct 28, 1970", salary: "60,000" },
                    { name: "Tiancum", age: 43, birthday: "Feb 12, 1985", salary: "70,000" },
                    { name: "Jacob", age: 27, birthday: "Aug 23, 1983", salary: "40,000" },
                    { name: "Nephi", age: 29, birthday: "May 31, 2010", salary: "50,000" },
                    { name: "Enos", age: 34, birthday: "Aug 3, 2008", salary: "30,000" },
                    { name: "Moroni", age: 50, birthday: "Oct 28, 1970", salary: "60,000" },
                    { name: "Tiancum", age: 43, birthday: "Feb 12, 1985", salary: "70,000" },
                    { name: "Jacob", age: 27, birthday: "Aug 23, 1983", salary: "40,000" },
                    { name: "Nephi", age: 29, birthday: "May 31, 2010", salary: "50,000" },
                    { name: "Enos", age: 34, birthday: "Aug 3, 2008", salary: "30,000"}];


    // Function to be called from old school "legacy" code
    $scope.newSchool = function (newData) {

        $scope.myData = newData;
        
        // When called from legacy code, we need to do this
        $scope.$apply();
    }

});

/*
	Sample of fetching remote , plus re-retrieval via a button click
*/
myControllers.controller('faqController', function ($scope, $http) {
	
	$http.get('faq.json.txt').success(function(data){
		
		$scope.faqList = data;
	});

	$scope.faqRefresh = function($event){
		
		$http.get('faq-updated.json.txt').success(function(data){
		
			$scope.faqList = data;
		});
		
	};
	
});
