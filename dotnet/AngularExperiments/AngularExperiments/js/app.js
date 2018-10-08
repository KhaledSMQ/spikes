"use strict";

var myApp = angular.module('myApp', [
  'ngRoute',
  'ngGrid',
  'myControllers'
]);

var basePath = ''; // TODO: override by determining path for WebAPI 

myApp.config(['$routeProvider',

      function ($routeProvider) {

          $routeProvider.
           when('/home', {
               templateUrl: basePath + 'partials/home.html',
               controller: 'homeController'
           })
          .when('/faq', {
              templateUrl: basePath + 'partials/faq.html',
              controller: 'faqController'
          })
		  .when('/grid', {
				templateUrl : basePath + 'partials/grid.html',
				controller: 'gridController'
		  })
          .otherwise({
              redirectTo: basePath + 'home'
          });
      }

]);

$(function(){

	if ((window.location.href.indexOf('file://') == 0)) {
	
		alert("Warning, this must be served from a web server");
		
		$("html").prepend('<h1 style="color:red;">This sample must be served from a web server due to angular routing</h1>');
	}

});

/**
    
    Calling angular methods from Legacy code

*/
function oldSchool() {

    // build some new data
    var data = [{ name: "Test1", age: 50, birthday: "Oct 28, 1970", salary: "60,000" },
        { name: "Test2", age: 43, birthday: "Feb 12, 1985", salary: "70,000" },
        { name: "Test3", age: 27, birthday: "Aug 23, 1983", salary: "50,000" },
        { name: "Test4", age: 29, birthday: "May 31, 2010", salary: "40,000" },
        { name: "Test5", age: 34, birthday: "Aug 3, 2008", salary: "30,000" },
        { name: "Test6", age: 50, birthday: "Oct 28, 1970", salary: "60,000"}];

    // get access to the angular controller (ng-view, in this case), then get the scope, then make the call
    angular.element(document.getElementById('myControllerDiv')).scope().newSchool(data);
}